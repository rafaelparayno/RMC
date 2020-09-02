using RMC.Admin.PanelReportsForms.PanelsPharmaRep.Analysis_Panel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep
{
    public partial class InventoryAnalysis : Form
    {

        private Form activeForm = null;
        public InventoryAnalysis()
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
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new ABCA());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new FNSA());
        }
    }
}
