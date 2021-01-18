using RMC.Patients;
using RMC.Reception;
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
        int countTimer = 0;
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

        private void btnDaily_Click(object sender, EventArgs e)
        {
            countTimer = 0;
            openChildForm(new DailySalesRep());
            disableBtns();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            countTimer++;

            if(countTimer > 5)
            {

                timer1.Stop();
                enableBtns();
            }
        }

        private void disableBtns()
        {
            btnPo.Enabled = false;
            btnDaily.Enabled = false;
            iconButton1.Enabled = false;
            iconButton2.Enabled = false;
        }

        private void enableBtns()
        {
            btnPo.Enabled = true;
            btnDaily.Enabled = true;
            iconButton1.Enabled = true;
            iconButton2.Enabled = true;
        }
    }
}
