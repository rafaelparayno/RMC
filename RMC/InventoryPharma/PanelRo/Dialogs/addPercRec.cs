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
    public partial class addPercRec : Form
    {
        public float percentage = 0;
        public addPercRec(float percentage)
        {
            this.percentage = percentage ;
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            int _;

            if (!(int.TryParse(textBox1.Text.Trim(), out _)))
                return;

            percentage = float.Parse(textBox1.Text.Trim());

            this.Close();
        }
    }
}
