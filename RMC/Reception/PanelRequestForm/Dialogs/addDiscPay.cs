using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class addDiscPay : Form
    {

        public float Percentage = 0;
        public float sellingPrice = 0;

        public addDiscPay(float sellingPrice)
        {
            InitializeComponent();
            this.sellingPrice = sellingPrice;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Percentage = 0;
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

        private void btnQty_Click(object sender, EventArgs e)
        {
            float _;

            if (!(float.TryParse(textBox1.Text.Trim(), out _)))
                return;

            if (!(sellingPrice >= float.Parse(textBox1.Text.Trim())))
            {

                MessageBox.Show("Discount is Greather than the selling price");
                return;
            }
                

            Percentage = float.Parse(textBox1.Text.Trim());

            this.Close();

        }
    }
}
