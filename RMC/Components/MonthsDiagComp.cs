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
    public partial class MonthsDiagComp : Form
    {
        public int m = 0;
      
        public int year = DateTime.Now.Year;
        public MonthsDiagComp()
        {
            InitializeComponent();
            m = 0;
            year = DateTime.Now.Year;
            dateTimePicker1.MaxDate = DateTime.Now;
            foreach (string months in StaticData.months)
            {
                comboBox1.Items.Add(months);
            }
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            year = dateTimePicker1.Value.Year;
            if (comboBox1.SelectedIndex == -1)
                return;
            m = comboBox1.SelectedIndex + 1;
            this.Close();
        }
    }
}
