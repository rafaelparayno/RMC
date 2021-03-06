using RMC.InventoryPharma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Pharma
{
    public partial class Pdash : Form
    {
        private Form activeForm = null;

        public Pdash()
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

        private void btnPos_Click(object sender, EventArgs e)
        {
            openChildForm(new POS());
        }

        private void btnViewPrescription_Click(object sender, EventArgs e)
        {
            //Viewing prescription
            openChildForm(new ViewPrescriptions());
        }
    }
}
