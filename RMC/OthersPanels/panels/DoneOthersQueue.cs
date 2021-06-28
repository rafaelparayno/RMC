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
    public partial class DoneOthersQueue : Form
    {

        CustomerDetailsController customerDetailsController = new CustomerDetailsController();

        private string idRightClick;
        private string cidRightClick;
        public DoneOthersQueue()
        {
            InitializeComponent();
        }

        private async Task loadGrid()
        {
            DataSet ds = await customerDetailsController.getServiceQueueDone();
            RefreshGrid(ds);
        }

        private async Task searchGrid()
        {
            DataSet ds = await customerDetailsController.getServiceQueueDone(txtName.Text.Trim());
            RefreshGrid(ds);
        }


        private void RefreshGrid(DataSet ds)
        {
            dbServiceList.DataSource = "";
            dbServiceList.DataSource = ds.Tables[0];
            dbServiceList.AutoResizeColumns();

        }

        private async void DoneOthersQueue_Load(object sender, EventArgs e)
        {
            await loadGrid();
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

        private void dbServiceList_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dbServiceList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idRightClick = dbServiceList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    contextMenuStrip1.Show(dbServiceList, new Point(e.X, e.Y));

                }

            }
        }

        private async void showLabRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            bool isNumber2 = int.TryParse(cidRightClick, out _);
            if (!isNumber || isNumber2)
                return;

            int id = int.Parse(idRightClick);
            int cid = int.Parse(cidRightClick);

            ViewPatientServiceReq viewPatientServiceReq = new ViewPatientServiceReq(id,cid);
            viewPatientServiceReq.ShowDialog();
            await loadGrid();
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
    }
}
