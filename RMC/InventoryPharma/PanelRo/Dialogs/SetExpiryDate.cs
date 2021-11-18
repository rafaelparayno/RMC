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
    public partial class SetExpiryDate : Form
    {
        public string DateString = "";
        public SetExpiryDate(string date)
        {
            InitializeComponent();
            DateString = date;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            DateString = "";
            this.Close();
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            DateString = date;
            this.Close();
        }
    }
}
