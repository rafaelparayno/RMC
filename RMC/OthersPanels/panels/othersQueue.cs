using RMC.Database.Controllers;
using RMC.OthersPanels.Dialogs;
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

namespace RMC.OthersPanels.panels
{
    public partial class othersQueue : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        OthersQueueController OthersQueueController = new OthersQueueController();
        private string idRightClick;
        private string cidRightClick;
        public othersQueue()
        {
            InitializeComponent();
          
        }

        private async Task loadGrid()
        {
            DataSet ds = await customerDetailsController.getServiceQueue();
            RefreshGrid(ds);
        }


        private async Task loadGridPending()
        {
            DataSet ds = await customerDetailsController.getServicePending();
            RefreshGridPending(ds);
        }

        private async Task searchGrid()
        {
            DataSet ds = await customerDetailsController.getServiceQueue(txtName.Text.Trim());
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

        private async void othersQueue_Load(object sender, EventArgs e)
        {
            await loadGrid();
            await loadGridPending();
        }

       

        private async void showLabRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            bool isNumber2 = int.TryParse(cidRightClick, out _);
            if (!isNumber || !isNumber2)
                return;

            int id = int.Parse(idRightClick);
            int cid = int.Parse(cidRightClick);
            ViewPatientServiceReq viewPatientServiceReq = new ViewPatientServiceReq(id,cid);
            viewPatientServiceReq.ShowDialog();
            await loadGrid();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Trim() == "")
            {
                await loadGrid();
            }
            else
            {
                await searchGrid();
            }
        }

        private async void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            if (!isNumber)
                return;

            int id = int.Parse(idRightClick);

            addEditPatient form = new addEditPatient(id);
            form.ShowDialog();
            await loadGrid();
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
                    contextMenuStrip2.Show(dataGridView1, new Point(e.X, e.Y));

                }

            }
        }

        private async void showServiceRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            bool isNumber2 = int.TryParse(cidRightClick, out _);
            if (!isNumber || !isNumber2)
                return;

            int id = int.Parse(idRightClick);
            int cid = int.Parse(cidRightClick);
            ViewPatientServiceReq viewPatientServiceReq = new ViewPatientServiceReq(id, cid);
            viewPatientServiceReq.ShowDialog();
            await loadGrid();
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
        }

        private async void removePendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            DialogResult dialogResult = MessageBox.Show("Are you sure to remove this Selected Pending Request?", "Validation",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    await OthersQueueController.updateStatus(int.Parse(row.Cells[1].Value.ToString()),1);
                }
                MessageBox.Show("Succesfully Remove Pending Customer");
                await loadGridPending();
            }
        }

       
    }
}
