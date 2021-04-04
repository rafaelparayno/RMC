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

namespace RMC.Reception
{
    public partial class PendingRequestForms : Form
    {
        doctorResultsController doctorResultsController = new doctorResultsController();


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
    }
}
