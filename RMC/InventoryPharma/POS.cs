using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma
{
    public partial class POS : Form
    {
        ItemController itemz = new ItemController();
        ItemList items;
        public POS()
        {
            InitializeComponent();
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                txtName.Text = "";
                txtStock.Text = "";
                txtrue.Text = "";
               
                return;
            }
            searchPharmItem(txtCode.Text.Trim());
            txtName.Text = items == null ? "" : items.name;
            txtStock.Text = items == null ? "" : items.stocks + "";
            txtrue.Text = String.Format("PHP {0:0.##}", items == null ? 0 : items.sellingPrice);
            numericUpDown1.Maximum = items == null ? 0:  items.stocks;
        }

        private void searchPharmItem(string searchKey)
        {
            
            items = itemz.Details(searchKey).Count > 0 ? itemz.Details(searchKey)[0] : null ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (items == null)
                return;
            if(items.sku == txtCode.Text.Trim())
            {
                MessageBox.Show("wew");
            }
        }
    }
}

