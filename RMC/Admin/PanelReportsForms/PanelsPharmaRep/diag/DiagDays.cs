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
    public partial class DiagDays : Form
    {
        public string dateTo = "";
        public string dateFrom = "";
        public DiagDays()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now.AddDays(1);
            dateTimePicker2.Enabled = false;
            dateTo = "";
            dateFrom = "";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker2.Enabled = true;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            dateFrom = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            dateTo = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            this.Close();
        }
    }
}
