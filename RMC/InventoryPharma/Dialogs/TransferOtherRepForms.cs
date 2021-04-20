using RMC.Database.Models;
using RMC.InventoryRep;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.Dialogs
{
    public partial class TransferOtherRepForms : Form
    {
        private DataSet ds = new DataSet();
        public TransferOtherRepForms(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }

        private void TransferOtherRepForms_Load(object sender, EventArgs e)
        {
            OtherTransferReport otherTransferReport = new OtherTransferReport();

         


            DateTime today = DateTime.Today;
            otherTransferReport.SetDataSource(ds);
            otherTransferReport.SetParameterValue("DateParam", today.ToString("MMMM dd, yyyy"));
            otherTransferReport.SetParameterValue("prepareBy", UserLog.getFullName());

            crystalReportViewer1.ReportSource = otherTransferReport;

        }
    }
}
