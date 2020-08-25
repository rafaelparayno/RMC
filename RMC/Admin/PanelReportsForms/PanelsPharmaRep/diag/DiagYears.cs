using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep.diag
{
    public partial class DiagYears : Form
    {
        public int yrFrom = 0;
        public int yrTo = 0;
        public DiagYears()
        {
            InitializeComponent();
            yrFrom = 0;
            yrTo = 0;
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Enabled = true;
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            yrFrom = dateTimePicker1.Value.Year;
            yrTo = dateTimePicker2.Value.Year;
            this.Close();
        }
    }
}
