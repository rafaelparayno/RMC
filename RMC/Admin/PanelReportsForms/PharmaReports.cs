using RMC.Admin.PanelReportsForms.PanelsPharmaRep;
using RMC.Database.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelReportsForms
{
    public partial class PharmaReports : Form
    {
        private Form activeForm = null;
        public PharmaReports()
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
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();


            childForm.Show();
        }

        private void btnReorder_Click(object sender, EventArgs e)
        {
            openChildForm(new RopRep());
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            openChildForm(new ExpRep());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new returnRep());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new recevRep());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            openChildForm(new PoRep());
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            openChildForm(new salePharma());
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            openChildForm(new stockHistory());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new InventoryAnalysis());
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            openChildForm(new RTSREPORT());

        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            openChildForm(new FinancialRep());
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            openChildForm(new PayablesReportAdmin());
        }
    }
}
