using CakesLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CakesForms
{
    public partial class frmManager : Form
    {
        private readonly Storage _storage;

        public frmManager(Storage storage)
        {
            InitializeComponent();
            _storage = storage;
        }

        private void frmManager_Load(object sender, EventArgs e)
        {
            var ingredients = _storage.GetAllIngredients();
            foreach (var ingredient in ingredients)
            {
                lsIngredients.Items.Add(ingredient.Name);
            }
            lsIngredients.Refresh();
        }
    }
}
