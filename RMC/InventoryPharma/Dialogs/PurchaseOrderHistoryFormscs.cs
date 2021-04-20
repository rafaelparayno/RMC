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
    public partial class PurchaseOrderHistoryFormscs : Form
    {
        private DataSet ds = new DataSet();
        public PurchaseOrderHistoryFormscs(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }

        private void PurchaseOrderHistoryFormscs_Load(object sender, EventArgs e)
        {
            PurchaseOrdersHistory purchaseOrdersHistory = new PurchaseOrdersHistory();

            DateTime today = DateTime.Today;
            purchaseOrdersHistory.SetDataSource(ds);
            purchaseOrdersHistory.SetParameterValue("DateParam", today.ToString("MMMM dd, yyyy"));
            purchaseOrdersHistory.SetParameterValue("prepareBy", UserLog.getFullName());
            crystalReportViewer1.ReportSource = purchaseOrdersHistory;
        }
    }
}
