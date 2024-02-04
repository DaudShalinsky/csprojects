using CakesLibrary.Models;

namespace CakesForms
{
    public partial class frmMain : Form
    {
        private readonly Storage _storage;
        private readonly Kitchen _kitchen;

        public frmMain()
        {
            InitializeComponent();
            _storage = new Storage();
            _kitchen = new Kitchen(_storage);
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            var frmManager = new frmManager(_storage);
            frmManager.ShowDialog();
        }
    }
}
