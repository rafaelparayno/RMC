using RMC.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelLabForms.Dialogs
{
    public partial class nameAutomated : Form
    {
        public string name = "";

        List<CoordinatesList> cor = new List<CoordinatesList>();
       
        public nameAutomated(List<CoordinatesList> cors)
        {
            InitializeComponent();
            name = "";
            cor = cors;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
                return;

            if (!(isValid(txtName.Text.Trim())))
            {
                MessageBox.Show("Already existing Name in The Parameters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            name = txtName.Text.Trim();

            this.Close();
        }

        private bool isValid(string name)
        {
          

            foreach(CoordinatesList corlop in cor)
            {
                if(name == corlop.nameVar)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
