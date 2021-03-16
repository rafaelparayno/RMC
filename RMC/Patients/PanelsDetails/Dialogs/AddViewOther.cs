using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Patients.PanelsDetails.Dialogs
{
    public partial class AddViewOther : Form
    {
        public AddViewOther()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();


            if(fd.ShowDialog() == DialogResult.OK)
            {
                axAcroPDF1.src = fd.FileName;
            }
            else
            {

            }
        }
    }
}
