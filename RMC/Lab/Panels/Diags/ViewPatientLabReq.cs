using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Lab.Panels.Diags
{
    public partial class ViewPatientLabReq : Form
    {
        private int patientid = 0;
        patientDetails patientmod = new patientDetails();
        PatientDetailsController patD = new PatientDetailsController();
        List<labModel> listLabModels = new List<labModel>();
        LabQueueController labQueueController = new LabQueueController();

        public ViewPatientLabReq(int patientid)
        {
            InitializeComponent();
            this.patientid = patientid;
            initLvCols();
            setData(patientid);
            setPatientData();
            setLabData();
        }


        private async void setData(int id)
        {

            patientmod = await patD.getPatientId(id);
            listLabModels = await labQueueController.getReqLabByPatientID(id);


        }

        private void initLvCols()
        {
            lvItemLab.View = View.Details;
            lvItemLab.Columns.Add("Lab Type", 150, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Laboratory Name", 400, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Laboratory Id", 100, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Is Done", 100, HorizontalAlignment.Center);
        }

        private void setPatientData()
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
            if (File.Exists(patientmod.imgPath))
            {
                Image img = Image.FromFile(patientmod.imgPath);

                patView.Icon = img;
            }
            panelPatient.Controls.Add(patView);

        }
        
        private void setLabData()
        {
            foreach(labModel l in listLabModels)
            {
              
                ListViewItem lvs = new ListViewItem();
                lvs.Text = l.labtypename;
                lvs.SubItems.Add(l.name);
                lvs.SubItems.Add(l.labID.ToString());
                string isDone = l.is_done == 0 ?  "No Data" : "Done";
             

                lvs.SubItems.Add(isDone);
                lvItemLab.Items.Add(lvs);
            }
        }

    }
}
