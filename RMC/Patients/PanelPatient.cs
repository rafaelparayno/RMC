using FontAwesome.Sharp;
using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace RMC.Patients
{
    public partial class PanelPatient : Form
    {

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        List<patientDetails> listDetails = new List<patientDetails>();
        int currentPage = 1;
        int rowsPerPage = 10;
       
        public PanelPatient()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            loadPatientDetails();
            populateitems();
            showPaginate(listDetails.Count);

        }

        private async void loadPatientDetails()
        {
            listDetails = await patientDetailsController.getPatientDetails();
        }

        private void populateitems()
        {
            /*List<patientDetails> listDetails = new List<patientDetails>();

            for (int i = 0; i < 100; i++)
            {
                patientDetails pdetails = new patientDetails();
                pdetails.id = i;
                pdetails.Firstname = "RAFAEL";
                pdetails.lastname = "Parayno";
                pdetails.age = i;
                pdetails.contact = "6391231";
                pdetails.address = "blk 22 lot 8 adelfa street gardenai valley molino bacoor";
                pdetails.gender = "Male";
                listDetails.Add(pdetails);
            }*/
            panelPatientList.Controls.Clear();
            int indexofLastRow = currentPage * rowsPerPage;
            int indexofFirstRow = indexofLastRow - rowsPerPage;
            indexofFirstRow = indexofLastRow > listDetails.Count ? listDetails.Count - rowsPerPage :
                indexofFirstRow;
            int rowsss = rowsPerPage;
            listDetails = listDetails.Count > rowsPerPage ? listDetails.GetRange(indexofFirstRow, rowsss).ToList()
                : listDetails;

            foreach (patientDetails p in listDetails)
            {
                PatientControl patientControl = new PatientControl();
                patientControl.Age = "Age : " + p.age.ToString();
                patientControl.PatientId = p.id;
                patientControl.PatientName = "Name: " + p.FullName;
                patientControl.Address = "Address: " +  p.address;
                patientControl.Gender = "Gender : " + p.gender;
                patientControl.Cnumber = "Contact Number : " + p.contact;
                patientControl.Dock = DockStyle.Top;
                panelPatientList.Controls.Add(patientControl);
            }
        }


        private void showPaginate(int total)
        {
            List<int> pagenumbers = new List<int>();
            decimal xyz = decimal.Parse((Decimal.Divide(total, rowsPerPage) + ""));
            decimal totalPagess = Math.Ceiling(xyz);
            for (int i = 1; i <= totalPagess; i++)
            {
                pagenumbers.Add(i);
            }

            flowPage.Controls.Clear();

            foreach(int i in pagenumbers)
            {
                Label lb = new Label();
                lb.Tag = i;
                lb.Text = i.ToString();
                lb.Margin = new Padding(1);
                lb.Padding = Padding.Empty;
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.AutoSize = false;
                lb.ForeColor = Color.White;  
                lb.Click += new EventHandler(setNumber);
                flowPage.Controls.Add(lb);
            }
        }

        private void setNumber(object sender,EventArgs e)
        {  
            currentPage = int.Parse(((Label)sender).Tag.ToString());
            panelPatientList.Controls.Clear();
            populateitems();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            addEditPatient form = new addEditPatient();
            form.ShowDialog();
            refreshListPatient();
        }

        private void refreshListPatient()
        {
            loadPatientDetails();
            populateitems();
            showPaginate(listDetails.Count);
        }
    }
}
