using RMC.InventoryPharma.PayRec.Panels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PayRec
{
    public partial class PanelPayRec : Form
    {
        private Form activeForm = null;

        public PanelPayRec()
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
            openChildForm(new PanelPayables());
        }

        private void btnAutomated_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelReceivables());
        }
    }
}
