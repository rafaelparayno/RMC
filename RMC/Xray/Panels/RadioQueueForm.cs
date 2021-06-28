using RMC.Database.Controllers;
using RMC.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Xray.Panels
{
    public partial class RadioQueueForm : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();

        private string idRightClick;
        private string cidRightClick;

        public RadioQueueForm()
        {
            InitializeComponent();
            timer1.Start();
           
        }

        private async Task loadGrid()
        {
            DataSet ds = await customerDetailsController.getRadioQueue();
            RefreshGrid(ds);
        }


        private async Task loadGridPending()
        {
            DataSet ds = await customerDetailsController.getRadioPending();
            RefreshGridPending(ds);
        }

        private async Task searchGrid()
        {
            DataSet ds = await customerDetailsController.getRadioQueue(txtName.Text.Trim());
            RefreshGrid(ds);
        }


        private void RefreshGrid(DataSet ds)
        {
            dbServiceList.DataSource = "";
            dbServiceList.DataSource = ds.Tables[0];
            dbServiceList.AutoResizeColumns();

        }

        private void RefreshGridPending(DataSet ds)
        {
            dataGridView1.DataSource = "";
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoResizeColumns();

        }



        private async void timer1_Tick(object sender, EventArgs e)
        {
           await loadGrid();
        }

        private async void showRadioRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            bool isNumber2 = int.TryParse(cidRightClick, out _);
            if (!isNumber || !isNumber2)
                return;

            int id = int.Parse(idRightClick);
            int cid = int.Parse(cidRightClick);
            ViewPatientRadioReq view = new ViewPatientRadioReq(id,cid);
            view.ShowDialog();
            await loadGrid();
            await loadGridPending();
        }

        private async void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
          
            if (!isNumber)
                return;

            int id = int.Parse(idRightClick);
         

            addEditPatient form = new addEditPatient(id);
            form.ShowDialog();
            await loadGrid();
            await loadGridPending();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
               await loadGrid();
            }
            else
            {
               await searchGrid();
            }


        }

        private void dbServiceList_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dbServiceList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idRightClick = dbServiceList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    cidRightClick = dbServiceList.Rows[currentMouseOverRow].Cells[1].Value.ToString();
                    contextMenuStrip1.Show(dbServiceList, new Point(e.X, e.Y));

                }

            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idRightClick = dataGridView1.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    cidRightClick = dataGridView1.Rows[currentMouseOverRow].Cells[1].Value.ToString();
                    contextMenuStrip1.Show(dataGridView1, new Point(e.X, e.Y));
                }

            }
        }

        private async void RadioQueueForm_Load(object sender, EventArgs e)
        {
           await loadGrid();
           await loadGridPending();
        }
    }
}
