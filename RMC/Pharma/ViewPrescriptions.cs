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

namespace RMC.Pharma
{
    public partial class ViewPrescriptions : Form
    {
        PatientPrescriptionController ppController = new PatientPrescriptionController();
        int dyn = 0;
        public string sku = "";
        string idRightClick = "";


        public ViewPrescriptions()
        {
            InitializeComponent();
        }

        public ViewPrescriptions(int dyn)
        {
            InitializeComponent();
            this.dyn = dyn;
            sku = "";
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            loadData();

        }

        private async void loadData()
        {
            DataSet ds = await ppController.getDataset();
            refreshGrid(ds);
        }

        private async void searchData(string date)
        {
            int selectedCb = comboBox1.SelectedIndex;
            DataSet ds = new DataSet();
            switch (selectedCb)
            {
                case 0:
                    bool isInt = int.TryParse(txtName.Text.Trim(), out _);

                    if (!isInt)
                        return;

                    int patientid = int.Parse(txtName.Text.Trim());
                    ds = await ppController.getDataset(patientid);

                    break;
                case 1:
                    ds = await ppController.getDatasetName(txtName.Text.Trim());
                    break;
                case 2:
                    ds = await ppController.getDataset(date);
                    break;


            }

            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();
        }

        private void ViewPrescriptions_Load(object sender, EventArgs e)
        {
            string dateToday = DateTime.Now.ToString("yyyy-MM-dd");

            //  searchData(dateToday);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            searchData(dateTimePicker1.Value.ToString("yyyy-MM-dd"));

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Text = "";
            if (comboBox1.SelectedIndex == 2)
            {
                dateTimePicker1.Visible = true;

                txtName.Visible = false;
            }
            else
            {

                dateTimePicker1.Visible = false;
                txtName.Visible = true;
            }

        }

        private void dgItemList_MouseClick(object sender, MouseEventArgs e)
        {
            if (dyn == 0)
                return;

            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgItemList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idRightClick = dgItemList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    /* Sku = dgItemList.Rows[currentMouseOverRow].Cells[5].Value.ToString();*/
                    contextMenuStrip1.Show(dgItemList, new Point(e.X, e.Y));

                }

            }
        }

        private async void addToCartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idRightClick);
            sku = await ppController.getPrescriptionSKU(id);
            this.Close();
        }
    }
}
