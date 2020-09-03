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
        }

        private async void loadData()
        {
            listpatientModel = await patientLabController.getPatientLabModel(id);
            refreshListView();
        }

        private void initLvsCols()
        {
            lvLabDetails.View = View.Details;
            lvLabDetails.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Laboratory Name", 200, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Lab Type", 200, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Date Taken", 200, HorizontalAlignment.Left);
        }

        private void refreshListView()
        {
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
    }
}
