using RMC.Database.Controllers;
using RMC.Reception.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception
{
    public partial class PendingRequestForms : Form
    {
        doctorResultsController doctorResultsController = new doctorResultsController();
        
        private string idRightClick = "";
        private string idRightClick2 = "";


        public PendingRequestForms()
        {
            InitializeComponent();
        }


        private async Task loadGrid()
        {

            DataSet ds = await doctorResultsController.getPatientDetailsWithReq();


            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();

        }

        private async void PendingRequestForms_Load(object sender, EventArgs e)
        {
           await loadGrid();
        }

        private void dgItemList_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgItemList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idRightClick = dgItemList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    idRightClick2 = dgItemList.Rows[currentMouseOverRow].Cells[1].Value.ToString();
                    contextMenuStrip1.Show(dgItemList, new Point(e.X, e.Y));
                }
            }
        }

        private void viewRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idRightClick);
            int patid = int.Parse(idRightClick2);


            DiagPaymentRequest diagPaymentRequest = new DiagPaymentRequest(id,patid);
            diagPaymentRequest.ShowDialog();

        }
    }
}
