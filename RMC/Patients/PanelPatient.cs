using FontAwesome.Sharp;
using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading;
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
         
      

        private void populateitems()
        {
         
            panelPatientList.Controls.Clear();
            int indexofLastRow = currentPage * rowsPerPage;
            int indexofFirstRow = indexofLastRow - rowsPerPage;

            indexofFirstRow = indexofLastRow > listDetails.Count ? listDetails.Count - rowsPerPage :
                indexofFirstRow;

            int rowsss =  rowsPerPage ;
            List<patientDetails> listDetails2 = new List<patientDetails>();
            listDetails2 = listDetails.Count > rowsPerPage ? listDetails.Skip(indexofFirstRow).Take( rowsss).ToList()
                : listDetails;

           

            foreach (patientDetails p in listDetails2)
            {
                PatientControl patientControl = new PatientControl();
                patientControl.Age = "Age : " + p.age.ToString();
                patientControl.PatientId = p.id;
                patientControl.PatientName = "Name: " + p.FullName;
                patientControl.Address = "Address: " + p.address;
                patientControl.Gender = "Gender : " + p.gender;
                patientControl.Cnumber = "Contact Number : " + p.contact;
                patientControl.Dock = DockStyle.Top;
                patientControl.btnView1.Click += new EventHandler(ClickBtnView);
                patientControl.btnDelete1.Click += new EventHandler(ClickDelete);

                if(File.Exists(p.imgPath))
                {
                    Image img = Image.FromFile(p.imgPath);

                    patientControl.Icon = img;
                }
            

                panelPatientList.Controls.Add(patientControl);
            }
        }

        private async void ClickBtnView(object sender, EventArgs e)
        {
            int id = int.Parse(((IconButton)sender).Tag.ToString());
            addEditPatient form = new addEditPatient(id);
            form.ShowDialog();
            await loadPatientDetails();
        }

        private async void ClickDelete(object sender, EventArgs e)
        {
            int id = int.Parse(((IconButton)sender).Tag.ToString());
            VoidForm voidForm = new VoidForm();
            voidForm.ShowDialog();


            if (voidForm.isFound)
            {
                DialogResult dialogResult = MessageBox.Show(@"Removing this will show in the Patient Archives." +
                                                    Environment.NewLine + @"Are you sure removing this Patient?",
                                                    "Removing", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if(DialogResult.OK == dialogResult)
                {
                    await patientDetailsController.updateAcvite(0, id);
                    MessageBox.Show("Succesfully Remove");
                }
            }

            await loadPatientDetails();
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
                lb.AutoSize = false;
                lb.Height = 20;
                lb.Width = 50;
                lb.Margin = Padding.Empty;
                lb.Padding = Padding.Empty;
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.ForeColor = Color.White;  
                lb.Click += new EventHandler(setNumber);
                flowPage.Controls.Add(lb);
            }
        }

        private void setNumber(object sender,EventArgs e)
        {  
            currentPage = int.Parse(((Label)sender).Tag.ToString());
            Label l = (Label)sender;
            foreach(Control c in flowPage.Controls)
            {
                c.Font = new Font("Tahoma", 10, FontStyle.Regular);
            }
            l.Font = new Font("Tahoma", 12, FontStyle.Bold|FontStyle.Underline);

            panelPatientList.Controls.Clear();
            populateitems();
        }

        private async void iconButton2_Click(object sender, EventArgs e)
        {
            addEditPatient form = new addEditPatient();
            form.ShowDialog();
            await loadPatientDetails();
        }

        private async Task loadPatientDetails()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            listDetails =  await patientDetailsController.getPatientDetails();
            
            populateitems();
            showPaginate(listDetails.Count);

            pictureBox1.Hide();
        }

        private async Task loadPatientSearchDetails()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            listDetails = await patientDetailsController.getSearchPatient(txtName.Text.Trim(),
                                                                        comboBox1.SelectedIndex);
           
            populateitems();
            showPaginate(listDetails.Count);


            pictureBox1.Hide();
        }


        private async void iconButton1_Click(object sender, EventArgs e)
        {
            int _;
            if(comboBox1.SelectedIndex == -1)
            {
              await  loadPatientDetails();
            }
            else
            {
                if (comboBox1.SelectedIndex == 0 && !(int.TryParse(txtName.Text.Trim(), out _)))
                    return;

              await loadPatientSearchDetails();
               
            }
        }

        private async void PanelPatient_Load(object sender, EventArgs e)
        {
           await loadPatientDetails();
        }
    }
}
