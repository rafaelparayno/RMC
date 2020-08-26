using RMC.Lab.Panels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Lab
{
    public partial class LabDashboard : Form
    {
        private Form activeForm = null;
        public LabDashboard()
        {
            InitializeComponent();
        }

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();


            childForm.Show();
        }

        private void btnPo_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelLabForm());
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelViewStocks());
        }
    }
}
