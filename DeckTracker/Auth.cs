namespace DeckTracker
{
    public partial class Auth : Form
    {
        private MainForm mainForm;
        public Auth(MainForm form)
        {
            mainForm = form;
            InitializeComponent();
            TopMost = true;
        }
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (NickNameBox.Text != "" && TeamCodeBox.Text != "")
            {
                mainForm.Login = NickNameBox.Text;
                mainForm.Team = TeamCodeBox.Text;
                mainForm.StartScan();
                mainForm.Enabled = true;
                Close();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            mainForm.Close();
        }

    }
}
