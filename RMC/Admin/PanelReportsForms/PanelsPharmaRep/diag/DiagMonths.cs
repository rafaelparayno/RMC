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
    public partial class DiagMonths : Form
    {
        public int m = 0;
        public int m2 = 0;
        public int year = DateTime.Now.Year;
        public DiagMonths()
        {
            InitializeComponent();
            m = 0;
            m2 = 0;
            year = DateTime.Now.Year;
            dateTimePicker1.MaxDate = DateTime.Now;
            foreach(string months in StaticData.months)
            {
                comboBox1.Items.Add(months);
            }
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            int i = comboBox1.SelectedIndex;
            comboBox2.Items.Clear();
            for(int j = i; j < StaticData.months.Length; j++)
            {
                comboBox2.Items.Add(StaticData.months[j]);
            }

        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            year = dateTimePicker1.Value.Year;
            if (comboBox1.SelectedIndex == -1)
                return;
            if (comboBox2.SelectedIndex == -1)
                return;

            m = comboBox1.SelectedIndex + 1;
            m2 = StaticData.months.ToList().IndexOf(comboBox2.SelectedItem.ToString()) + 1;
            this.Close();
        }
    }
}
