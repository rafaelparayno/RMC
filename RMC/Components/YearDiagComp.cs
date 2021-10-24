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
    public partial class YearDiagComp : Form
    {

        public int year = 0;
        public YearDiagComp()
        {
            InitializeComponent();
            year = 0;
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            year = dateTimePicker1.Value.Year;
            this.Close();
        }
    }
}
