using RMC.Patients.PanelsDetails;
using RMC.Patients.PanelsDetails.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Patients
{
    public partial class addEditPatient : Form
    {

        private Form activeForm = null;
        private int patient_id = 0;
        public addEditPatient()
        {
            InitializeComponent();
            newInitState();
            openChildForm(new PanelPatientDetails(patient_id));
        }

        public addEditPatient(int id)
        {
            InitializeComponent();
            patient_id = id;
            openChildForm(new PanelPatientDetails(patient_id));
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

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newInitState()
        {
            btnVital.Visible = false;
            btnLabFiles.Visible = false;
            btnXray.Visible = false;
            btnDoctorRecord.Visible = false;
            btnPrescription.Visible = false;
            iconButton1.Visible = false;
            iconButton2.Visible = false;
        }

        private void btnVital_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelVPatient(patient_id));
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelPatientDetails(patient_id));
        }

        private void btnLabFiles_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelLabDetail(patient_id));
        }

        private void btnXray_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelXrayDetail(patient_id));
        }

        private void btnDoctorRecord_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelDoctorFindings(patient_id));
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnPrescription_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelPrescriptionData(patient_id));

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelOtherFiles(patient_id));
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelPatientMedcert(patient_id));
        }
    }
}
