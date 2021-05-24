using RMC.Admin.PanelReportsForms.PanelsClinicRep;
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
    public partial class LabReports : Form
    {

        private Form activeForm = null;
        public LabReports()
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

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            openChildForm(new salesClinic());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new LabCostRepForm());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new FinancialReportLab());
        }
    }
}
