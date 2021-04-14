using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Lab.DialogReports;
using RMC.Lab.Panels.Diags;
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
            pictureBox1.Show();
            pictureBox1.Update();

            listpatientModel = await patientLabController.getPatientLabModel(id,
                                        dateTimePicker1.Value.ToString("yyy-MM-dd"));
        
            refreshListView();
            pictureBox1.Hide();
        }

        private void initLvsCols()
        {
            lvLabDetails.View = View.Details;
            lvLabDetails.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Laboratory Name", 200, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Lab Type", 200, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Date Taken", 300, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("File name", 300, HorizontalAlignment.Left);
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
                lvitem.SubItems.Add(l.filename);
                lvLabDetails.Items.Add(lvitem);
            }
        }

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

            pictureBox1.Show();

            pictureBox1.Update();
            int selectedIds = int.Parse(lvLabDetails.SelectedItems[0].SubItems[0].Text);
            labModel labModel = await laboratoryController.getLabModelinPatientLab(selectedIds);
            pictureBox1.Hide();

            string filenameExt = lvLabDetails.SelectedItems[0].SubItems[4].Text.Split('.')[1];
            
            if (labModel.crystal_id_lab > 0 && filenameExt == "xml")
            {

                /* DynamicLabReportsValue dynform = new DynamicLabReportsValue(lb.crystal_id_lab, patientid, selectedIds);
                 dynform.ShowDialog();*/
                switch (labModel.crystal_id_lab)
                {
                    case 1:
                        BloodChemDiagForms bloodChemDiagForms = new BloodChemDiagForms(id, labModel.id,selectedIds);
                        bloodChemDiagForms.Show();
                        break;
                    case 2:
                        FecalysisDiagForms fecalysisDiagForms = new FecalysisDiagForms(id,labModel.id,selectedIds);
                        fecalysisDiagForms.Show();
                        break;
                    case 3:
                        HematologyDiagForms hematologyDiagForms = new HematologyDiagForms(id, labModel.id, selectedIds);
                        hematologyDiagForms.Show();
                        break;
                    case 4:
                        SerologyDiagForms serologyDiagForms = new SerologyDiagForms(id, labModel.id, selectedIds);
                        serologyDiagForms.Show();

                        break;
                    case 5:
                        UrinalysisDiagForms urinalysisDiagForms = new UrinalysisDiagForms(id, labModel.id, selectedIds);
                        urinalysisDiagForms.Show();
                        break;
                    case 6:
                        ViewDiagnosticReport viewDiagnosticReport = new ViewDiagnosticReport(id,labModel.id,selectedIds);
                        viewDiagnosticReport.Show();
                        break;
                    case 7:
                        ClinicalChemistryDiagForms clinicalChemistryDiagForms = new ClinicalChemistryDiagForms(id, labModel.id, selectedIds);
                        clinicalChemistryDiagForms.Show();
                        break;
                }

            }
            else
            {
                ViewImageFile viewImageFile = new ViewImageFile(id, labModel.id, selectedIds,lvLabDetails.SelectedItems[0].SubItems[1].Text);
                viewImageFile.Show();
            }

           
        }

        private async void editDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show()
            if (lvLabDetails.SelectedItems.Count == 0)
                return;

            if (lvLabDetails.Items.Count == 0)
                return;

            pictureBox1.Show();

            pictureBox1.Update();
            int selectedIds = int.Parse(lvLabDetails.SelectedItems[0].SubItems[0].Text);
            labModel labModel = await laboratoryController.getLabModelinPatientLab(selectedIds);
            pictureBox1.Hide();

            if (labModel.autodocsid > 0)
            {
                DiagWithAutomated diagWithAutomated = new DiagWithAutomated(labModel.id, id,selectedIds);
                diagWithAutomated.ShowDialog();
            }

            if (labModel.crystal_id_lab == 6)
            {
                AddEditDiagnosticForm addEditDiagnosticForm = new AddEditDiagnosticForm(id, labModel.id, selectedIds);
                addEditDiagnosticForm.ShowDialog();
            }
            else
            {
                DynamicLabReportsValue dynform = new DynamicLabReportsValue(labModel.crystal_id_lab, id, labModel.id, selectedIds);
                dynform.ShowDialog();
            }

        
            if (labModel.autodocsid == 0 && labModel.crystal_id_lab == 0)
            {
                DiagFileUpload fileUpload = new DiagFileUpload(labModel.id, id,selectedIds);
                fileUpload.ShowDialog();
            }
           // setData(patientid);
        }
    }
}
