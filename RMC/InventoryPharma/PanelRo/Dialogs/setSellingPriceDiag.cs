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
    public partial class setSellingPriceDiag : Form
    {
        public float sellingPrice = 0;
        public setSellingPriceDiag(float selling)
        {
         
            InitializeComponent();
            this.sellingPrice = selling;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            float _;

            if (!(float.TryParse(textBox1.Text.Trim(), out _)))
                return;


            sellingPrice = float.Parse(textBox1.Text.Trim());

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
    }
}
