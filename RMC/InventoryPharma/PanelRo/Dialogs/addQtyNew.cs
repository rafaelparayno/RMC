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
    public partial class addQtyNew : Form
    {
        public int qty = 0;
        public addQtyNew(int qty)
        {
            InitializeComponent();
            this.qty = qty;
            numericUpDown1.Value = qty;
            numericUpDown1.Maximum = qty;
            numericUpDown1.Focus();

        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            qty = int.Parse(numericUpDown1.Value.ToString());
            this.Close();
        }
    }
}
