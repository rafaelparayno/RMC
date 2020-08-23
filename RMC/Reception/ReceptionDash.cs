using RMC.Patients;
using RMC.Reception.PanelRequestForm;
using RMC.Reception.PanelRequestForm.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC
{
    public partial class ReceptionDash : Form
    {
        private Form activeForm = null;
        public ReceptionDash()
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
            openChildForm(new PanelRequestForm());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ShowQueue formq = new ShowQueue();
            formq.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelPatient());
        }
    }
}
