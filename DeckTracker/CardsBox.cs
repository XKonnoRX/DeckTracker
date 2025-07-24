using DeckTracker.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeckTracker
{
    public partial class CardsBox : Form
    {
        private List<PictureBox> Cards = new List<PictureBox>();
        public CardsBox(Point locate)
        {
            InitializeComponent();
            Location = locate;
            TopMost = true;
        }

        public void SetCards(string cardList) //из строки карт составляет изображения карт
        {
            panel1.Controls.Clear();
            Cards.Clear();
            for (int i = 0; i < 5; i++)
            {
                PictureBox picbox = new PictureBox();
                picbox.Location = new Point(3 + 36*i, 6);
                picbox.Size = new Size(30, 44);
                try
                {
                    picbox.BackgroundImage = Image.FromFile($"CardBase/MiniCards/{cardList[2 * i]}{cardList[2 * i + 1]}.png");
                }
                catch
                {
                    picbox.BackgroundImage = Image.FromFile($"CardBase/MiniCards/nf.png");
                }
                picbox.BackgroundImageLayout = ImageLayout.Zoom;
                panel1.Controls.Add(picbox);
                Cards.Add(picbox);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CardsBox_Leave(object sender, EventArgs e)
        {
            Close();
        }

        private void CardsBox_MouseDown(object sender, MouseEventArgs e) //перетягивание окна мышкой
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) //перетягивание окна мышкой
        {
            panel1.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
    }
}
