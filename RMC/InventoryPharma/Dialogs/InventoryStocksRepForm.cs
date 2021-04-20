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
    public partial class InventoryStocksRepForm : Form
    {
        private DataSet ds = new DataSet();

        public InventoryStocksRepForm(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }

        private void InventoryStocksRepForm_Load(object sender, EventArgs e)
        {
            StockHistoryReport stockHistoryReport = new StockHistoryReport();
            DateTime today = DateTime.Today;
            stockHistoryReport.SetDataSource(ds);
            stockHistoryReport.SetParameterValue("DateParam", today.ToString("MMMM dd, yyyy"));
            stockHistoryReport.SetParameterValue("prepareBy", UserLog.getFullName());
            crystalReportViewer1.ReportSource = stockHistoryReport;

        }
    }
}
