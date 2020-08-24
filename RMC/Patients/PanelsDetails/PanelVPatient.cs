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
    public partial class PanelVPatient : Form
    {
        int id = 0;
        PatientVController patV = new PatientVController();
        List<patientVModel> listpatv = new List<patientVModel>();
        public PanelVPatient(int id)
        {
            InitializeComponent();
            this.id = id;
            initColLv();
            getDataFromDb();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditVital form = new AddEditVital(id);
            form.ShowDialog();
            getDataFromDb();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (lvVitals.Items.Count == 0)
                return;

            if (lvVitals.SelectedItems.Count == 0)
                return;

            int idd = int.Parse(lvVitals.SelectedItems[0].SubItems[0].Text);
            AddEditVital form = new AddEditVital(id,idd);
            form.ShowDialog();
            getDataFromDb();

        }

        private void initColLv()
        {
            lvVitals.View = View.Details;
            lvVitals.Columns.Add("id", 80, HorizontalAlignment.Center);
            lvVitals.Columns.Add("Date", 100, HorizontalAlignment.Center);
            lvVitals.Columns.Add("BP", 80, HorizontalAlignment.Center);
            lvVitals.Columns.Add("TEMP", 80, HorizontalAlignment.Center);
            lvVitals.Columns.Add("WT", 80, HorizontalAlignment.Center);
            lvVitals.Columns.Add("LMP", 80, HorizontalAlignment.Center);
            lvVitals.Columns.Add("UA", 80, HorizontalAlignment.Center);
            lvVitals.Columns.Add("PUS", 80, HorizontalAlignment.Center);
            lvVitals.Columns.Add("RBC", 80, HorizontalAlignment.Center);
        }

        private async void getDataFromDb()
        {
            listpatv = await patV.getPatientV(id);
            refreshGrid(listpatv);
        }

        private void refreshGrid(List<patientVModel> listvv)
        {
            lvVitals.Items.Clear();
            foreach (patientVModel p in listvv)
            {
                ListViewItem lvitems = new ListViewItem();
                lvitems.Text = p.id.ToString();
                lvitems.SubItems.Add(p.date_vital);
                lvitems.SubItems.Add(p.bp);
                lvitems.SubItems.Add(p.temp);
                lvitems.SubItems.Add(p.wt);
                lvitems.SubItems.Add(p.lmp);
                lvitems.SubItems.Add(p.ua);
                lvitems.SubItems.Add(p.pus);
                lvitems.SubItems.Add(p.rbc);
                lvVitals.Items.Add(lvitems);
            }
        }


    }
}
