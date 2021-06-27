using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Components
{
    public partial class InputInvoice : Form
    {
        public string input = "";


        public InputInvoice(string title,string labelName)
        {
            InitializeComponent();
            input = "";
            label1.Text = title;
            label6.Text = labelName;
        }


        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            input = "";
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            input = txtPlaceName.Text.Trim();
            this.Close();
        }
    }
}
