using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.Dialogs
{
    public partial class seniorDiag : Form
    {
        public string seniorId = "";
        public seniorDiag()
        {
            InitializeComponent();
            seniorId = "";
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (txtRole.Text.Trim() == "")
                return;

            seniorId = txtRole.Text.Trim();
            this.Close();
        }
    }
}
