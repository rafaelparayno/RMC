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

namespace RMC.OthersPanels.panels
{
    public partial class othersQueue : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();

        private string idRightClick;
        public othersQueue()
        {
            InitializeComponent();
        }

        private async Task loadGrid()
        {
            DataSet ds = await customerDetailsController.getServiceQueue();
            RefreshGrid(ds);
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

        private async void othersQueue_Load(object sender, EventArgs e)
        {
            await loadGrid();
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

        private void showLabRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            if (!isNumber)
                return;

            int id = int.Parse(idRightClick);



        }
    }
}
