using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Text;
using System.Management;

namespace DeckTracker
{
    public partial class MainForm : Form
    {

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        static PixelFormat format8bit = PixelFormat.Format8bppIndexed;
        static PixelFormat format1bit = PixelFormat.Format1bppIndexed;
        static Process pokerLobby;
        static Process pokerGame;
        static List<Bitmap> tablecardbase = new List<Bitmap>();
        static List<string> tablecardnamebase = new List<string>();
        static List<Bitmap> tablemastbase = new List<Bitmap>();
        static List<string> tablemastnamebase = new List<string>();
        string[] table = { "", "", "", "", "" };

        static List<Bitmap> handcardbase = new List<Bitmap>();
        static List<string> handcardnamebase = new List<string>();
        static List<Bitmap> handmastbase = new List<Bitmap>();
        static List<string> handmastnamebase = new List<string>();
        string[] hand = { "", "", "", "", "" };


        static UserOutput user;
        static List<UserOutput> team = new List<UserOutput>();
        static List<TextBox> CardCombos = new List<TextBox>();
        static List<PictureBox> CardList = new List<PictureBox>();
        static string uid = GetHash(GetUUID());
        private bool newPool = false;
        private int X { get; set; }
        private int Y { get; set; }
        public string Login {get;set;}
        public string Team { get; set; }
        string gamePath = "";
        Rectangle size;
        private int round = 0;
        private bool isStarted = false;
        CardsBox cardsBox;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public static int GWL_STYLE = -16;
        public static int WS_CHILD = 0x40000000;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            X = 591;
            Y = 1063;
            FileInfo conf = new FileInfo("config.cfg"); //путь к конфигу
            using (var streamReader = new StreamReader(conf.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                foreach (string line in streamReader.ReadToEnd().Split("\n"))
                    if (line.Contains("game = "))
                    {
                        gamePath = line.Replace("game = ", "");
                    }
            }
            if (!File.Exists(gamePath)) //если путь отсутствует вызывает менеджер пути
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    gamePath = openFileDialog1.FileName;
                    File.AppendAllText("config.cfg", $"game = {gamePath.Replace("\\","/")}");
                }
            }
        }
        public void StartScan()
        {
            pokerLobby = new Process(); //запуск игры
            pokerLobby.StartInfo.FileName = gamePath;
            pokerLobby.Start();
            CreateBoxes();
            isStarted = true;
        }
        private void CreateBoxes() // генерация полей
        {
            // users
            user = new UserOutput(1, true, this);
            user.Location = new Point(0, 10);
            user.Name.Text = Login;
            EquityPanel.Controls.Add(user);
            for(int i=0;i<5;i++)
            {
                var newuser = new UserOutput(2+i,false, this);
                newuser.Location = new Point(0, 31+i*21);
                EquityPanel.Controls.Add(newuser);
                team.Add(newuser);
            }
            // combinations
            string[] cardComb = { "Royal", "SFlash", "Quad", "Full", "Flash", "Straight", "Set", "2pare", "Pare", "High"};
            for(int i =0; i<cardComb.Length;i++)
            {
                var nameBox = new TextBox();
                nameBox.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
                nameBox.BorderStyle = BorderStyle.None;
                nameBox.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
                nameBox.ForeColor = SystemColors.Menu;
                nameBox.Location = new Point(17, 20+20*i);
                nameBox.Size = new Size(101, 15);
                nameBox.Text = cardComb[i];
                nameBox.TextAlign = HorizontalAlignment.Center;
                OddsPanel.Controls.Add(nameBox);

                var valueBox = new TextBox();
                valueBox.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
                valueBox.BorderStyle = BorderStyle.None;
                valueBox.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
                valueBox.ForeColor = SystemColors.Menu;
                valueBox.Location = new Point(120, 20+20*i);
                valueBox.Size = new Size(200, 15);
                valueBox.Text = "-";
                valueBox.Tag = cardComb[i];
                valueBox.TextAlign = HorizontalAlignment.Center;
                CardCombos.Add(valueBox);
                OddsPanel.Controls.Add(valueBox);
            }
            //cardlist
            string[] cardValues = new string[] {"2","3","4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A"};
            string[] cardSuits = new string[] { "s", "c", "d", "h"};
            for(int t = 0; t<cardSuits.Length;t++)
                for (int i = 0; i < cardValues.Length; i++)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Location = new Point(8 + 32 * i, 10 + 54 * t);
                    pictureBox.Size = new Size(30, 44);
                    pictureBox.BackgroundImage = System.Drawing.Image.FromFile($"CardBase\\MiniCards\\{cardValues[i]}{cardSuits[t]}.png");
                    pictureBox.BackgroundImageLayout = ImageLayout.Zoom;
                    pictureBox.TabStop = false;
                    pictureBox.Tag = $"{cardValues[i]}{cardSuits[t]}";
                    CardList.Add(pictureBox);
                    CardsPanel.Controls.Add(pictureBox);
                }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Invoke(new void_delegate(ScanCards));
        }
        private async void ScanCards()
        {
            // проверка на количество процессов
            if (Process.GetProcessesByName("PPPoker").Length > 0 && Process.GetProcessesByName("PPPoker").Length < 3) 
            {
                pokerGame = null;
                foreach (var proc in Process.GetProcessesByName("PPPoker"))
                    if (proc.Id != pokerLobby.Id)
                        pokerGame = proc;
            }
            else
            if (Process.GetProcessesByName("PPPoker").Length == 0 && isStarted)
                Close();

            if (pokerGame != null)
            {
                var hwnd = pokerGame.MainWindowHandle;
                GetWindowRect(hwnd, out var rect); // захват окна игры
                try
                {
                    int[] tableCards = { 126, 196, 269, 340, 411 }; //координаты положения карт на столе

                    var image = new Bitmap(rect.Right - rect.Left, rect.Bottom - rect.Top);
                    var graphics = Graphics.FromImage(image);
                    var hdcBitmap = graphics.GetHdc();
                    PrintWindow(hwnd, hdcBitmap, 0);
                    graphics.ReleaseHdc(hdcBitmap);
                    image.Save($"deb\\Table.png");

                    //запись базы карт из папки
                    if (round == 0)
                    {
                        Show();
                        SizedDB("CardBase\\TableNumbers", "CardBase\\TableSuits", tablecardbase, tablecardnamebase, tablemastbase, tablemastnamebase);
                        SizedDB("CardBase\\HandNumbers", "CardBase\\HandSuits", handcardbase, handcardnamebase, handmastbase, handmastnamebase);
                        round++;
                    }
                    //обрезка всех карт на столе и скан
                    for (int i = 0; i < 5; i++)
                    {
                        var bsize = new Rectangle(image.Width / X * (120 + (int)(71.7 * i)), image.Height / Y * 411, image.Width / X * 64, image.Height / Y * 96);
                        size = new Rectangle(image.Width / X * tableCards[i], image.Height / Y * 418, image.Width / X * 17, image.Height / Y * 26);
                        var msize = new Rectangle(image.Width / X * tableCards[i] + 2, image.Height / Y * 446, image.Width / X * 15, image.Height / Y * 17);
                        var crop = new Crop(size).Apply(image);
                        var mast = new Crop(msize).Apply(image);
                        var img = new Crop(bsize).Apply(image);
                        //скан карты
                        CheckCard(crop, mast, i, tablecardbase, tablecardnamebase, tablemastbase, tablemastnamebase, false);
                    }
                    int[] handCards = { 332, 357, 381, 406, 431 }; //координаты карт в руке
                    //обрезка всех карт в руке и скан
                    for (int i = 0; i < 5; i++)
                    {
                        var bsize = new Rectangle(image.Width / X * (332 + (int)(25 * i)), image.Height / Y * 849, image.Width / X * 25, image.Height / Y * 76);
                        size = new Rectangle(image.Width / X * handCards[i], image.Height / Y * 849, image.Width / X * 11, image.Height / Y * 20);
                        var msize = new Rectangle(image.Width / X * handCards[i], image.Height / Y * 872, image.Width / X * 12, image.Height / Y * 13);
                        var crop = new Crop(size).Apply(image);
                        var mast = new Crop(msize).Apply(image);
                        var img = new Crop(bsize).Apply(image);
                        //скан карты
                        CheckCard(crop, mast, i, handcardbase, handcardnamebase, handmastbase, handmastnamebase, true);
                    }
                string ht = "";
                    TableBox.Text = "";
                    foreach (var card in hand)
                        ht += card;
                    foreach (var card in table)
                        TableBox.Text += card + " ";
                    if (cardsBox != null)
                        cardsBox.SetCards(TableBox.Text.Replace(" ", ""));
                    // n - не обнаружена цифра, f - не обнаружена масть
                    if (!ht.Contains("n") && !ht.Contains("f") && !Calculate.CheckReapeted(ht + TableBox.Text.Replace(" ", "")))
                    {
                        newPool = false;
                        user.Hand.Text = ht;
                        if (user.cardsBox != null)
                            user.cardsBox.SetCards(ht);
                        using var client = new HttpClient();
                        //отправка запроса на сервер
                        var content = client.GetStringAsync($"http://almadinero.ru/pp_equity/baikal/put?id={uid}&team={Team}&nick={user.Name.Text}&hand={user.Hand.Text}&board={TableBox.Text.Replace("nf", "").Replace(" ", "")}");
                        //обработка ответа
                        ScanJson(await content);
                    }
                    else if (ht.Contains("nfnfnfnfnf") && TableBox.Text.Contains("nfnfnfnfnf")) //проверка на обновление стола
                    {
                        if (!newPool)
                        {
                            newPool = true;
                            user.Hand.Text = ht;
                            if (user.cardsBox != null)
                                user.cardsBox.SetCards(ht);
                        }
                    }
                }
                catch
                {}

            }
        }
        public static string GetUUID() //получить уникальный код по железу пользователся
        {
            Dictionary<string, string> ids = new Dictionary<string, string>();

            ManagementObjectSearcher searcher;
            string result = "";
            searcher = new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT UUID FROM Win32_ComputerSystemProduct");
            foreach (ManagementObject queryObj in searcher.Get())
                ids.Add("UUID", queryObj["UUID"].ToString());

            foreach (var x in ids)
                result += x.Key + ": " + x.Value + "\r\n";
            return result;
        }
        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        } //получить хеш, для uuid
        private void SizedDB(string numbers, string suits,  List<Bitmap> cardbase, List<string> cardnamebase, List<Bitmap> mastbase, List<string> mastnamebase)
        { //загрузка базы карт в списки
            foreach (var file in Directory.GetFiles(numbers))
            {
                var card = new Bitmap(System.Drawing.Image.FromFile(file));
                var newcard = card.Clone(new Rectangle(0, 0, card.Width, card.Height), format8bit);
                cardbase.Add(newcard);
                cardnamebase.Add(Path.GetFileName(file).Replace("f",""));
            }
            foreach (var file in Directory.GetFiles(suits))
            {
                var card = new Bitmap(System.Drawing.Image.FromFile(file));
                var newcard = card.Clone(new Rectangle(0, 0, card.Width, card.Height), format8bit);
                mastbase.Add(newcard);
                mastnamebase.Add(Path.GetFileName(file).Replace("f", ""));
            }
            
        }

        //сканирование карты по базе
        private void CheckCard(Bitmap picbox, Bitmap mastbox, int n, List<Bitmap> cardbase, List<string> cardnamebase, List<Bitmap> mastbase, List<string> mastnamebase, bool ishand)
        {
            
            string cardName = "n";
            string mastName = "f";
            float percent = 0.7f;
            float similarity = 0.0f;
            float similaritym = 0.0f;
            //форматирование в упрощенный вид для сокращения базы и меньшей погрешности
            var screencard = picbox.Clone(new Rectangle(0, 0, picbox.Width, picbox.Height), format1bit); //формат цифры в чб
            screencard = screencard.Clone(new Rectangle(0, 0, picbox.Width, picbox.Height), format8bit); //формат масти в 8бит
            var mast = mastbox.Clone(new Rectangle(0, 0, mastbox.Width, mastbox.Height), format8bit);
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(percent);
            //сохранение значения карты в папку deb
            screencard.Save($"deb\\{n}{ishand}.png");
            //сохранение масти карты в папку deb
            mast.Save($"deb\\m{n}{ishand}.png");
            //скан значения
            for (int i = 0; i < cardbase.Count; i++)
            {
                TemplateMatch[] matches = tm.ProcessImage(screencard, cardbase[i]);
                if (matches.Length > 0)
                {
                    foreach (var match in matches)
                    {
                        if (match.Similarity > similarity)
                        {
                            cardName = Path.GetFileName(cardnamebase[i]);
                            similarity = match.Similarity;
                        }
                    }
                }
            }
            //скан масти
            for (int i = 0; i < mastbase.Count; i++)
            {
                TemplateMatch[] matches = tm.ProcessImage(mast, mastbase[i]);
                if (matches.Length > 0)
                {
                    foreach (var match in matches)
                    {
                        if (match.Similarity > similaritym)
                        {
                            mastName = Path.GetFileName(mastnamebase[i]);
                            similaritym = match.Similarity;
                        }
                    }
                }
            }

            if(ishand)
                hand[n] = cardName.Replace(".png", "") + mastName.Replace(".png", "");
            else
                table[n] = cardName.Replace(".png", "") + mastName.Replace(".png", "");
        }

        
        public delegate void void_delegate();
        private void ChangePos() //прилипание худа к окну игры
        {
            if (pokerGame != null)
            {
                var hwnd = pokerGame.MainWindowHandle;
                GetWindowRect(hwnd, out var rect);
                Location = new Point(rect.Right - 7, rect.Top);
                Size = new Size(430, rect.Bottom - rect.Top - 7);
                Show();
            }
            else
                Hide();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Auth authform = new Auth(this);
            this.Enabled = false;
            this.Hide();
            authform.Show();
        }

        private void timerPos_Tick(object sender, EventArgs e)
        {
            Invoke(new void_delegate(ChangePos));
        }
        
        private void ScanJson(string json) //скан ответа от сервера и вывод в визуал
        {
            var obj = JsonConvert.DeserializeObject<Rootobject>(json);
            
            user.Chance.Text = obj.Equity;
            user.Action.Text = obj.Do;
            user.Result.Text = "";
            if (user.Action.Text == "ALL IN")
            {
                user.Chance.ForeColor = Color.Gold;
                user.Action.ForeColor = Color.Gold;
            }
            else
            {
                user.Chance.ForeColor = SystemColors.Menu;
                user.Action.ForeColor = SystemColors.Menu;
            }
            try
            {
                for (int i = 0; i < obj.players.Length; i++)
                {
                    team[i].Name.Text = obj.players[i].name;
                    team[i].Hand.Text = obj.players[i].hand;
                    if (team[i].cardsBox != null)
                        team[i].cardsBox.SetCards(obj.players[i].hand.Replace(" ", ""));
                    team[i].Chance.Text = obj.players[i].Equity;
                    team[i].Action.Text = obj.players[i].Do;
                    team[i].Result.Text = "";
                    if (team[i].Action.Text == "ALL IN")
                    {
                        team[i].Chance.ForeColor = Color.Gold;
                        team[i].Action.ForeColor = Color.Gold;
                    }
                    else
                    {
                        team[i].Chance.ForeColor = SystemColors.Menu;
                        team[i].Action.ForeColor = SystemColors.Menu;
                    }
                }
            }
            catch { }

            for(int i=0;i<obj.Odds.Length;i++)
                CardCombos[i].Text = obj.Odds[i].label;

            BurnCards();

        }

        private void TableBox_Click(object sender, EventArgs e) //вывод окна с визуализацие карт на столе
        {
            if (cardsBox != null)
                cardsBox.Close();
            cardsBox = new CardsBox(new Point(Location.X + TableBox.Location.X, Location.Y + TableBox.Location.Y - 80));
            cardsBox.SetCards(TableBox.Text.Replace(" ", ""));
            cardsBox.Show();
        }

        private void BurnCards() //затемнение обнаруженных карт
        {
            var actualCards = Calculate.GetPack(Directory.GetFiles("CardBase\\MiniCards\\"), user, team, TableBox.Text.Replace(" ", ""));
            foreach(var card in CardList)
            {
                if (actualCards.Contains(card.Tag.ToString()))
                    card.Image = null;
                else
                    card.Image = System.Drawing.Image.FromFile("CardBase\\filter\\Gray.png");
            }
        }
    }
}