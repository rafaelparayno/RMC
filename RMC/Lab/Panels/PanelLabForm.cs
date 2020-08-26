using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Lab.Panels.Diags;
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

namespace RMC.Lab.Panels
{
    public partial class PanelLabForm : Form
    {
        patientDetails patientmod = new patientDetails();
        PatientDetailsController patD = new PatientDetailsController();
        public PanelLabForm()
        {
            InitializeComponent();
        }

        private async void getDataId(int id)
        {
            patientmod = await patD.getPatientId(id);
        }

        private async void getDataFName(string name)
        {
            patientmod = await patD.getPatientFName(name);
        }

        private async void getDataLName(string name)
        {
            patientmod = await patD.getPatientLName(name);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            int selectedCb = comboBox1.SelectedIndex;

            switch (selectedCb)
            {
                case 0:
                    int _;
                    if (!int.TryParse(txtName.Text.Trim(), out _))
                        return;
                    int id = int.Parse(txtName.Text.Trim());
                    getDataId(id);
                    break;
                case 1:
                    getDataFName(txtName.Text.Trim());
                    break;
                case 2:
                    getDataLName(txtName.Text.Trim());
                    break;
                default:
                    return;
            }
            panelPatient.Controls.Clear();

            if(patientmod.id != 0)
            {

                PatientControl patView = new PatientControl();
                patView.PatientId = patientmod.id;
                patView.PatientName = "Name: " + patientmod.FullName;
                patView.btnView1.Visible = false;
                patView.Age = "Age : " + patientmod.age.ToString();
                patView.Gender = "Gender : " + patientmod.gender;
                patView.Address = "Address: " + patientmod.address;
                patView.Cnumber = "Contact Number : " + patientmod.contact;
               
                patView.Dock = DockStyle.Fill;
                panelPatient.BackColor = Color.FloralWhite;
                panelPatient.Controls.Add(patView);
            }
            else
            {
                panelPatient.BackColor = Color.Salmon;
                panelPatient.Controls.Add(label2);
                
            }
          
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            DiagLab form = new DiagLab();
            form.ShowDialog();
        }
    }
}
