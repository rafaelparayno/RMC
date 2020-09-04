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
            refreshLvs();
        }


        private void refreshLvs()
        {
            foreach(patientXrayModel pmodel in listXrayModel)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = pmodel.id.ToString();
                lvitem.SubItems.Add(pmodel.name);
                lvitem.SubItems.Add(getType(pmodel.type));
                lvitem.SubItems.Add(pmodel.date.ToString("dddd, dd MMMM yyyy"));

                lvLabDetails.Items.Add(lvitem);
            }
        }

        private string getType(int type)
        {
            string typeStr = "";

            foreach(KeyValuePair<string,int> k in StaticData.XrayTypes)
            {
                if(type == k.Value)
                {

                    return k.Key;
                }
            }

            return typeStr;
        }

        private void initListCols()
        {
            lvLabDetails.View = View.Details;

            lvLabDetails.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Name", 200, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Type", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Date", 300, HorizontalAlignment.Left);
        }

        private async void lvLabDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLabDetails.Items.Count == 0)
                return;

            if (lvLabDetails.SelectedItems.Count == 0)
                return;

            int id = int.Parse(lvLabDetails.SelectedItems[0].Text);

           await showImg(id);
        }

        private async Task showImg(int id)
        {
            string path = await patientXrayController.getFullPath(id);

            pbEdited.Image = Image.FromFile(path);
        }
    }
}
