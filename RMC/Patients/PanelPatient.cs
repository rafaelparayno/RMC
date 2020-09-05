using FontAwesome.Sharp;
using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Patients
{
    public partial class PanelPatient : Form
    {

        PatientDetailsController patientDetailsController = new PatientDetailsController();
        List<patientDetails> listDetails = new List<patientDetails>();
        AccessController accessController = new AccessController();
        List<int> listAcc = new List<int>();
        int currentPage = 1;
        int rowsPerPage = 10;
       
        public PanelPatient()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            refreshListPatient();
            getAccess();
        }

        private void getAccess()
        {
            listAcc = accessController.accesses(UserLog.getRole());
            if (listAcc.Contains(4))
            {
                iconButton2.Visible = true;
            }
        }
         
        private async Task loadPatientDetails()
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
                patientControl.btnView1.Click += new EventHandler(ClickBtnView);
                panelPatientList.Controls.Add(patientControl);
            }
        }

        private void ClickBtnView(object sender, EventArgs e)
        {
            int id = int.Parse(((IconButton)sender).Tag.ToString());
            addEditPatient form = new addEditPatient(id);
            form.ShowDialog();
            refreshListPatient();
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

        private async void refreshListPatient()
        {
            await loadPatientDetails();
            populateitems();
            showPaginate(listDetails.Count);
        }

        private async Task loadPatientSearchDetails()
        {
            listDetails = await patientDetailsController.getSearchPatient(txtName.Text.Trim(),
                                                                        comboBox1.SelectedIndex);
        }

        private async Task searchListPatient()
        {
            await loadPatientSearchDetails();
            populateitems();
            showPaginate(listDetails.Count);

        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            int _;
            if(comboBox1.SelectedIndex == -1)
            {
                refreshListPatient();
            }
            else
            {
                if (comboBox1.SelectedIndex == 0 && !(int.TryParse(txtName.Text.Trim(), out _)))
                    return;

               await searchListPatient();
               
            }
        }
    }
}
