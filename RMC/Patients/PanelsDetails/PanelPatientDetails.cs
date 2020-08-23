using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Patients.PanelsDetails
{
    public partial class PanelPatientDetails : Form
    {
        public PanelPatientDetails()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int yrNow = DateTime.Now.Year;
            int bdate = dateTimePicker1.Value.Year;
            int age = yrNow - bdate;
            textBox3.Text = age.ToString();
        }
    }
}
