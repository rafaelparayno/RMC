using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Lab.Panels.Diags;
using RMC.Patients;
using RMC.Utilities;
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
        PatientLabController patientLabC = new PatientLabController();
        List<Image> listImg = new List<Image>();
        public PanelLabForm()
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
            lvItemLab.Columns.Add("Lab Type", 100, HorizontalAlignment.Center);      
            lvItemLab.Columns.Add("Laboratory Name", 400, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Laboratory Id", 100, HorizontalAlignment.Center);
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

            if (form.Lab == "")
                return;


            ListViewItem lvitems = new ListViewItem();

            lvitems.Text = form.labType;
            lvitems.SubItems.Add(form.Lab);
            lvitems.SubItems.Add(form.labid + "");
            lvItemLab.Items.Add(lvitems);
            listImg.Add(form.imgToAdd);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (lvItemLab.Items.Count == 0)
                return;

            if (lvItemLab.SelectedItems.Count == 0)
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lvItemLab.Items.Count == 0)
                return;

            if (patientmod.id == 0)
                return;


            string newFilePath = CreateDirectory.CreateDir(patientmod.lastname +"-"+patientmod.id);
            string datenow = DateTime.Now.ToString("dd-mm-yyyy");

            saveData(datenow, newFilePath);
        }

        private void saveData(string datenow,string path)
        {
           foreach(ListViewItem lv in lvItemLab.Items)
            {
                Image im = listImg[lv.Index];
                saveImginPath(im, path, patientmod.id + "-" + lv.SubItems[2].Text + "-" + datenow);
                patientLabC.save(patientmod.id, int.Parse(lv.SubItems[2].Text),
                                patientmod.id + "-" + lv.SubItems[2].Text + "-" + datenow, path);

            }
        }

        private void saveImginPath(Image imgSave,string path, string fileName)
        {


            Image newImg = imgSave;
            newImg.Save(path + fileName + ".jpg");
        }
    }
}
