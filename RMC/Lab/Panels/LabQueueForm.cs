using RMC.Database.Controllers;
using RMC.Lab.Panels.Diags;
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

namespace RMC.Lab.Panels
{
    public partial class LabQueueForm : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        LabQueueController labQueueController = new LabQueueController();
        private string idRightClick;
        private string cidRightClick;

        public LabQueueForm()
        {
            InitializeComponent();
          
            timer1.Start();
        }


        private async Task loadGrid()
        {
            DataSet ds = await customerDetailsController.getLabQueue();
            RefreshGrid(ds);
        }

        private async Task loadGridPend()
        {
            DataSet ds = await customerDetailsController.getLabPending();
            RefreshGridPending(ds);
        }

        private async void searchGrid()
        {
            DataSet ds = await customerDetailsController.getLabQueue(txtName.Text.Trim());
            RefreshGrid(ds);
        }

        private async void searchPendGrid()
        {
            DataSet ds = await customerDetailsController.getLabPending(textBox1.Text.Trim());
            RefreshGridPending(ds);
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

        private void dbServiceList_MouseClick(object sender, MouseEventArgs e)
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

        private async void timer1_Tick(object sender, EventArgs e)
        {
            await loadGrid();
            //await loadGridPend();
        }

        private async void showLabRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);

            bool isNumber2 = int.TryParse(cidRightClick, out _);

            if (!isNumber || !isNumber2)
                return;

            int id = int.Parse(idRightClick);
            int cid = int.Parse(cidRightClick);
            ViewPatientLabReq v = new ViewPatientLabReq(id,cid);
            v.ShowDialog();
            await loadGrid();
            await loadGridPend();
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
            await loadGridPend();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                timer1.Start();
              await loadGrid();
            }
            else
            {
                timer1.Stop();
                 searchGrid();

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

        private async void LabQueueForm_Load(object sender, EventArgs e)
        {
           await loadGrid();
           await loadGridPend();
        }

        private async void iconButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                timer1.Start();
                await loadGridPend();
            }
            else
            {
                timer1.Stop();
                searchPendGrid();

            }
        }

        private async void showLabRequestsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);

            bool isNumber2 = int.TryParse(cidRightClick, out _);

            if (!isNumber || !isNumber2)
                return;

            int id = int.Parse(idRightClick);
            int cid = int.Parse(cidRightClick);
            ViewPatientLabReq v = new ViewPatientLabReq(id, cid);
            v.ShowDialog();
            await loadGrid();
            await loadGridPend();
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
            await loadGridPend();
        }

        private async void removeFromPendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
         

            if (dataGridView1.SelectedRows.Count == 0)
                return;

            DialogResult dialogResult = MessageBox.Show("Are you sure to remove this Selected Pending Request?", "Validation",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if(dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    await labQueueController.updateStatus(int.Parse(row.Cells[1].Value.ToString()));
                }
                MessageBox.Show("Succesfully Remove Pending Customer");
                await loadGridPend();
            }
        }
    }
}
