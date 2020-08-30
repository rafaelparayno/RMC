using RMC.Database.Controllers;
using RMC.Database.Models;
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

namespace RMC.Xray.Panels
{
    public partial class PanelXrayForm : Form
    {
        patientDetails patientmod = new patientDetails();
        PatientDetailsController patD = new PatientDetailsController();
        PatientLabController patientLabC = new PatientLabController();
        List<Image> listImg = new List<Image>();
  /*      ConsumablesController consumablesController = new ConsumablesController();*/
        Dictionary<int, int> consumables = new Dictionary<int, int>();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();
        public PanelXrayForm()
        {
            InitializeComponent();
            initLvCols();
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

        private void initLvCols()
        {
            lvItemLab.View = View.Details;
            lvItemLab.Columns.Add("Xray Type", 100, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Xray Name", 400, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Xray Id", 100, HorizontalAlignment.Center);
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

            if (patientmod.id != 0)
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
    }
}
