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
    public partial class ReceiveHistoryForms : Form
    {
        private DataSet ds = new DataSet();
        public ReceiveHistoryForms(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }

        private void ReceiveHistoryForms_Load(object sender, EventArgs e)
        {
            ReceiveHistory receiveHistory = new ReceiveHistory();

            receiveHistory.SetDataSource(ds);

            DateTime today = DateTime.Today;
            receiveHistory.SetDataSource(ds);
            receiveHistory.SetParameterValue("DateParam", today.ToString("MMMM dd, yyyy"));
            receiveHistory.SetParameterValue("prepareBy", UserLog.getFullName());
            crystalReportViewer1.ReportSource = receiveHistory;
        }
    }
}
