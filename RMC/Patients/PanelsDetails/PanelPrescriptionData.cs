using RMC.Database.Controllers;
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
    public partial class PanelPrescriptionData : Form
    {

        int patientid = 0;
        string idRightClick = "";
    

        PatientPrescriptionController ppController = new PatientPrescriptionController();

        public PanelPrescriptionData(int patientid)
        {
            InitializeComponent();
            this.patientid = patientid;
            loadData();
        }

        private async void loadData()
        {
            DataSet ds = await ppController.GetDataSetInfo(patientid);
            refreshGrid(ds);
        }

        private async void loadDataItem(int resid)
        {
            DataSet ds = await ppController.getPrescriptionByResID(resid);
            refreshGridItem(ds);
        }

        private async void searchData(string date)
        {
           
            DataSet ds = new DataSet();
         
            ds = await ppController.GetDataSetInfoDate(date);
             

            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();
        }

        private void refreshGridItem(DataSet ds)
        {
            dataGridView1.DataSource = "";
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoResizeColumns();
        }





        private void iconButton1_Click(object sender, EventArgs e)
        {
            searchData(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = "";
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            loadData();

        }

        private void dgItemList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                int currentMouseOverRow = dgItemList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idRightClick = dgItemList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    /* Sku = dgItemList.Rows[currentMouseOverRow].Cells[5].Value.ToString();*/
                    int res = int.Parse(idRightClick);
                    loadDataItem(res);

                }

            }
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;

            if (dgItemList.SelectedRows.Count == 0)
                return;

            pictureBox1.Show();
            pictureBox1.Update();

            int docresid = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());
            DataSet ds = await ppController.getPrescriptionByResID(docresid);
            pictureBox1.Hide();
            prescriptionViewerDiag prescriptionViewer = new prescriptionViewerDiag(docresid,ds);
            prescriptionViewer.ShowDialog();
        }
    }
}
