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
        public int qty = 0;
        public AddQtyRo(int maxQty)
        {
            InitializeComponent();
            qty = 0;
            numericUpDown1.Maximum = maxQty;
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
                return;

            qty = int.Parse(numericUpDown1.Value.ToString());

            this.Close();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
