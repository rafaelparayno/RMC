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
        public RadioQueueForm()
        {
            InitializeComponent();
            loadGrid();
            timer1.Start();
        }

        private async void loadGrid()
        {
            DataSet ds = await customerDetailsController.getRadioQueue();
            RefreshGrid(ds);
        }


        private void RefreshGrid(DataSet ds)
        {
            dbServiceList.DataSource = "";
            dbServiceList.DataSource = ds.Tables[0];
            dbServiceList.AutoResizeColumns();

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void showRadioRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            if (!isNumber)
                return;

            int id = int.Parse(idRightClick);
            ViewPatientRadioReq view = new ViewPatientRadioReq(id);
            view.ShowDialog();
         /*   ViewPatientLabReq v = new ViewPatientLabReq(id);
            v.ShowDialog();*/
        }

        private void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(idRightClick, out _);
            if (!isNumber)
                return;

            int id = int.Parse(idRightClick);

            addEditPatient form = new addEditPatient(id);
            form.ShowDialog();
            loadGrid();
        }
    }
}
