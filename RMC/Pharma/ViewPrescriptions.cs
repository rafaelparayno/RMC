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
        string idRightClick2 = "";


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
            DataSet ds = await ppController.GetDataSetInfo();
            refreshGrid(ds);
        }

        private async void loadDataItem(int resid)
        {
            DataSet ds = await ppController.getPrescriptionByResID(resid);
            refreshGridItem(ds);
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
                    ds = await ppController.GetDataSetInfo(patientid);

                    break;
                case 1:
                    ds = await ppController.GetDataSetInfo(txtName.Text.Trim());
                    break;
                case 2:
                    ds = await ppController.GetDataSetInfoDate(date);
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

        private void refreshGridItem(DataSet ds)
        {
            dataGridView1.DataSource = "";
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoResizeColumns();
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
            /*   if (dyn == 0)
                   return;*/

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

        private async void addToCartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idRightClick2);
            sku = await ppController.getPrescriptionSKU(id);
            this.Close();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dyn == 0)
                return;

            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idRightClick2 = dataGridView1.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    contextMenuStrip1.Show(dataGridView1, new Point(e.X, e.Y));

                }

            }
        }
    }
}
