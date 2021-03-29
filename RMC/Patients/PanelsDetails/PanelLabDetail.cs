using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Lab.DialogReports;
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
        LaboratoryController laboratoryController = new LaboratoryController();
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

      /*  private async Task showDocLab(int id)
        {
            string fullpath = await patientLabController.getFullPath(id);


            try
            {
                pbEdited.Image = Image.FromFile(fullpath);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("There was an error opening the image." +
                           "Please check the path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }*/


       /* private async void lvLabDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLabDetails.Items.Count == 0)
                return;

            if (lvLabDetails.SelectedItems.Count == 0)
                return;

            int id = int.Parse(lvLabDetails.SelectedItems[0].Text);

            await showDocLab(id);
        }*/

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            await searchData();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void lvLabDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                var focusedItem = lvLabDetails.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                { 
                    contextMenuStrip1.Show(Cursor.Position);
                }

            }
        }

        private async void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvLabDetails.SelectedItems.Count == 0)
                return;

            if (lvLabDetails.Items.Count == 0)
                return;

            int selectedIds = int.Parse(lvLabDetails.SelectedItems[0].SubItems[0].Text);
            labModel labModel = await laboratoryController.getLabModelinPatientLab(selectedIds);

         

            if (labModel.crystal_id_lab > 0)
            {

                /* DynamicLabReportsValue dynform = new DynamicLabReportsValue(lb.crystal_id_lab, patientid, selectedIds);
                 dynform.ShowDialog();*/
                switch (labModel.crystal_id_lab)
                {
                    case 1:
                        BloodChemDiagForms bloodChemDiagForms = new BloodChemDiagForms(id, labModel.id,selectedIds);
                        bloodChemDiagForms.ShowDialog();
                        break;
                    case 2:

                        break;
                    case 3:
                        HematologyDiagForms hematologyDiagForms = new HematologyDiagForms(id, labModel.id);
                        hematologyDiagForms.ShowDialog();
                        break;
                    case 4:

                        break;
                    case 5:
                        UrinalysisDiagForms urinalysisDiagForms = new UrinalysisDiagForms(id, labModel.id);
                        urinalysisDiagForms.ShowDialog();
                        break;
                }

            }
            else
            {

            }

           
        }
    }
}
