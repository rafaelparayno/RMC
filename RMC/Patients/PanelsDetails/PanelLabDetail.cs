using RMC.Database.Controllers;
using RMC.Database.Models;
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
    public partial class PanelLabDetail : Form
    {
        PatientLabController patientLabController = new PatientLabController();

        List<patientLabModel> listpatientModel;
        private int id = 0;
        public PanelLabDetail(int id)
        {
            InitializeComponent();
            this.id = id;
            initLvsCols();
            loadData();
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private async void loadData()
        {
            listpatientModel = await patientLabController.getPatientLabModel(id);
            refreshListView();
        }

        private async Task searchData()
        {
            listpatientModel = await patientLabController.getPatientLabModel(id,
                                        dateTimePicker1.Value.ToString("yyy-MM-dd"));

            refreshListView();
        }

        private void initLvsCols()
        {
            lvLabDetails.View = View.Details;
            lvLabDetails.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Laboratory Name", 200, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Lab Type", 200, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Date Taken", 300, HorizontalAlignment.Left);
        }

        private void refreshListView()
        {
            lvLabDetails.Items.Clear();
           foreach(patientLabModel l in listpatientModel)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = l.id.ToString();
                lvitem.SubItems.Add(l.name);
                lvitem.SubItems.Add(l.type);
                lvitem.SubItems.Add(l.date.ToString("dddd, dd MMMM yyyy"));

                lvLabDetails.Items.Add(lvitem);
            }
        }

        private async Task showDocLab(int id)
        {
            string fullpath = await patientLabController.getFullPath(id);
           
           pbEdited.Image = Image.FromFile(fullpath);

        }

        private async void lvLabDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLabDetails.Items.Count == 0)
                return;

            if (lvLabDetails.SelectedItems.Count == 0)
                return;

            int id = int.Parse(lvLabDetails.SelectedItems[0].Text);

            await showDocLab(id);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            searchData();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
