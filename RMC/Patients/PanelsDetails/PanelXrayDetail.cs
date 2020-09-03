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
    public partial class PanelXrayDetail : Form
    {
        private int id = 0;
        PatientXrayController patientXrayController = new PatientXrayController();
        List<patientXrayModel> listXrayModel = new List<patientXrayModel>();
        public PanelXrayDetail(int id)
        {
            InitializeComponent();
            this.id = id;
            initListCols();
            getData();
        }

        private async void getData()
        {
            listXrayModel = await patientXrayController.getPatientXray(id);
        }


        private void refreshLvs()
        {
            foreach(patientXrayModel pmodel in listXrayModel)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = pmodel.id.ToString();
                lvitem.SubItems.Add(pmodel.name);
            }
        }

        private string getType(int type)
        {
            string type = "";

            /*if (StaticData.XrayTypes.key)
            {

            }*/

            return type;
        }

        private void initListCols()
        {
            lvLabDetails.View = View.Details;

            lvLabDetails.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Name", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Type", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Date", 100, HorizontalAlignment.Left);
        }

    }
}
