namespace Doviz.WinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnJsonKurgu_Click(object sender, EventArgs e)
        {
            Doviz.Core.BusinessLogicLayer BLL = new Core.BusinessLogicLayer();
            BLL.KurBilgileriniGuncelle();
        }
    }
}