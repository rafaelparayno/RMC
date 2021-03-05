using RMC.Database.Controllers;
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
    public partial class PanelPrescriptionData : Form
    {

        int patientid = 0;
        PatientPrescriptionController ppController = new PatientPrescriptionController();

        public PanelPrescriptionData(int patientid)
        {
            InitializeComponent();
            this.patientid = patientid;
            loadData();
        }

        private async void searchData()
        {
            DataSet ds = await ppController.getDataset(patientid, dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            refreshGrid(ds);
        }

        private async void loadData()
        {
            DataSet ds = await ppController.getDataset(patientid);
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();
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
