using RMC.Database.Controllers;
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
    public partial class PurchaseOrderRepForms : Form
    {
        private DataSet ds = new DataSet();
        int PONO = 0;
        PoController poController = new PoController();

        public PurchaseOrderRepForms(DataSet ds,int PONO)
        {
            InitializeComponent();
            this.PONO = PONO;
            this.ds = ds;
        }

        private async void PurchaseOrderRepForms_Load(object sender, EventArgs e)
        {
            PurchasOrderModel purchasOrderModel = await poController.getModel(PONO);

            PurchaseOrder purchaseOrder = new PurchaseOrder();
           
            purchaseOrder.SetDataSource(ds);
            purchaseOrder.SetParameterValue("orderName", purchasOrderModel.orderbyName);
            purchaseOrder.SetParameterValue("DateParam", purchasOrderModel.dateOrder.ToString("MMMM dd, yyyy"));

            purchaseOrder.SetParameterValue("poNO", PONO);

            crystalReportViewer1.ReportSource = purchaseOrder;
        }
    }
}
