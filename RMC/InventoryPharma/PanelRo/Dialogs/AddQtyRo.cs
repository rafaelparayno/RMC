using RMC.Database.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelRo.Dialogs
{
    public partial class AddQtyRo : Form
    {
        ItemController itemController = new ItemController();
        public int qty = 0;
        public float price = 0;
        private float currentPrice = 0;
        private int itemid;
        public AddQtyRo(int maxQty,int itemid)
        {
            InitializeComponent();
            qty = 0;
            price = 0;
            numericUpDown1.Maximum = maxQty;
            this.itemid = itemid;
        }

        private async void btnQty_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
                return;

            if (!float.TryParse(textBox1.Text.Trim(), out _))
                return;

            qty = int.Parse(numericUpDown1.Value.ToString());
            price = float.Parse(textBox1.Text.Trim());

            if (price > currentPrice)
               await itemController.updateUnitCost(itemid, price);
            this.Close();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            qty = 0;
            price = 0;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void AddQtyRo_Load(object sender, EventArgs e)
        {
            currentPrice = await itemController.getUnitCosts(itemid);
            textBox1.Text = currentPrice.ToString();
        }
    }
}
