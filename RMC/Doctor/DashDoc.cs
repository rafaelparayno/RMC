using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Doctor.PanelDoctor;
using RMC.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Doctor
{
    public partial class DashDoc : Form
    {
        private Form activeForm = null;
        DoctorDataController doctorDataController = new DoctorDataController();


        public DashDoc()
        {
            InitializeComponent();
            showNotif();
        }

        private async void showNotif()
        {
            if (await doctorDataController.isFound(UserLog.getUserId()) == false)
            {
                AlertForm alertForm = new AlertForm("Alert!!!", 
                    "Please Put a Data in The Doctor Data. you need to add Your PR number and License No",32);
                alertForm.ShowDialog();
            }

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

        private void btnPatientRec_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelPatient());
        }

        private void btnDoctorSet_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelDoctorSettings());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new DoctorData());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new DoctorQueue());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new medcertsRequestsForm());
        }
    }
}
