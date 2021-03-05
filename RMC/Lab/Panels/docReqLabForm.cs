using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Lab.Panels
{
    public partial class docReqLabForm : Form
    {
        public docReqLabForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                txtName.Visible = true;
                dateTimePicker1.Visible = false;
            }
            else
            {
                txtName.Visible = false;
                dateTimePicker1.Visible = true;
            }

        }
    }
}
