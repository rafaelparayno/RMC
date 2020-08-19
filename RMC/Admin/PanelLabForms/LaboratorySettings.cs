using RMC.Admin.PanelLabForms.PanelsSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelLabForms
{
    public partial class LaboratorySettings : Form
    {
        private Form activeForm = null;
        public LaboratorySettings()
        {
            InitializeComponent();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelLabType());
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


        private void iconButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelMedCertSet());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelConsultSettings());
        }
    }
}
