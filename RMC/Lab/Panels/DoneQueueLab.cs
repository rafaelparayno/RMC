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
    public partial class DoneQueueLab : Form
    {

        CustomerDetailsController customerDetailsController = new CustomerDetailsController();

        string idRightClick = "";
        string cidRightClick = "";
        public DoneQueueLab()
        {
            InitializeComponent();
            loadGrid();
        }



        private async void loadGrid()
        {
            DataSet ds = await customerDetailsController.getLabQueueDone();
            RefreshGrid(ds);
        }

       

        private void RefreshGrid(DataSet ds)
        {
            dbServiceList.DataSource = "";
            dbServiceList.DataSource = ds.Tables[0];
            dbServiceList.AutoResizeColumns();

        }

        private void showLabRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            bool isNumber2 = int.TryParse(cidRightClick, out _);
            if (!isNumber || isNumber2)
                return;

            int id = int.Parse(idRightClick);
            int cid = int.Parse(cidRightClick);
            ViewPatientLabReq v = new ViewPatientLabReq(id, cid);
            v.ShowDialog();
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

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            if (!isNumber)
                return;

            int id = int.Parse(idRightClick);

            addEditPatient form = new addEditPatient(id);
            form.ShowDialog();
            loadGrid();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
            
                loadGrid();
            }
            else
            {
              
                searchGrid();

            }
        }

        private async void searchGrid()
        {
            DataSet ds = await customerDetailsController.getLabQueueDone(txtName.Text.Trim());
            RefreshGrid(ds);
        }
    }
}
