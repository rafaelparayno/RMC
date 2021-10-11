using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PanelTransfer.Dialog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelTransfer
{
    public partial class PanelTransfer : Form
    {

        private int id = 0;
        private string name = "";
        private bool isPharma;
        private int quantityStocks = 0;
        private int editId = 0;
        private int cbTransfId = 0;
        private int startQty = 0;
        PlacesTransferController placesTransferController = new PlacesTransferController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        TransferLogsController transferLogs = new TransferLogsController();
        TransferLogsModel transferLogsModel = new TransferLogsModel();

        public PanelTransfer()
        {
            InitializeComponent();
        }

        private async Task loadPoCbs()
        {
            List<ComboBoxItem> cbs = await placesTransferController.getComboDatas();

            cbPo.Items.AddRange(cbs.ToArray());
        }


        private void initLvCols()
        {
            lvItemLab.View = View.Details;

            lvItemLab.Columns.Add("Item Name", 250, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Desc", 150, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("last Unit", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Qty", 70, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Amount", 100, HorizontalAlignment.Right);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ViewItemsTransfer frm = new ViewItemsTransfer();
            frm.ShowDialog();
        }

        private async void PanelTransfer_Load(object sender, EventArgs e)
        {
            await loadPoCbs();
        }
    }
}
