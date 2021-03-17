using RMC.Database.Controllers;
using RMC.Database.Models;
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

namespace RMC.Patients.PanelsDetails
{
    public partial class PanelOtherFiles : Form
    {
        PatientOthersController patientOthersController = new PatientOthersController();
        int patient_id = 0;

        public PanelOtherFiles(int patient_id)
        {
            InitializeComponent();
            this.patient_id = patient_id;
            initColLv();
            RefreshLvs();
        }

        private async void RefreshLvs()
        {
            List<PatientsOtherModel> listpO = 
                await patientOthersController.getPatientsOthers(patient_id);

            lvVitals.Items.Clear();

            foreach(PatientsOtherModel p in listpO)
            {
                ListViewItem lvsItem = new ListViewItem();
                lvsItem.Text = p.id.ToString();
                lvsItem.SubItems.Add(p.name); 
               lvsItem.SubItems.Add(p.date_upload.ToString().Split(' ')[0]);

                lvVitals.Items.Add(lvsItem);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddViewOther form = new AddViewOther(patient_id);
            form.ShowDialog();

            RefreshLvs();
        }

        private void initColLv()
        {
            lvVitals.View = View.Details;
            lvVitals.Columns.Add("File Id", 80, HorizontalAlignment.Center);
            lvVitals.Columns.Add("File Name", 300, HorizontalAlignment.Center);
            lvVitals.Columns.Add("Date Uploaded", 150, HorizontalAlignment.Center);

        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {

            if (lvVitals.Items.Count == 0)
                return;

            if (lvVitals.SelectedItems.Count == 0)
                return;


            int id = int.Parse(lvVitals.SelectedItems[0].SubItems[0].Text);

            AddViewOther form = new AddViewOther(patient_id, id);
            form.ShowDialog();
            RefreshLvs();
        }
    }
}
