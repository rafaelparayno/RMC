using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PayRec.Panels
{
    public partial class PanelPayables : Form
    {
        SupplierController supplierController = new SupplierController();
        PayablesController payablesController = new PayablesController();
    

        int cbTransfId = 0;
        public PanelPayables()
        {
            InitializeComponent();
            //initLvCols();
        }

       /* private void initLvCols()
        {
            lvItemLab.View = View.Details;
            lvItemLab.CheckBoxes = true;
            lvItemLab.Columns.Add("Invoice #", 250, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Date Due", 200, HorizontalAlignment.Center);
            lvItemLab.Columns.Add("Amount", 100, HorizontalAlignment.Right);
            lvItemLab.Columns.Add("Paid", 100, HorizontalAlignment.Right);
        }*/


        private async Task loadPoCbs()
        {
            List<ComboBoxItem> cbs = await supplierController.getComboDatas();

            cbPo.Items.AddRange(cbs.ToArray());
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            await loadGrid();
        }

        private async Task loadGrid()
        {

            List<PayableModel> payableModels = await payablesController.listModel(cbTransfId);
            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(payableModels).Tables[0];
        

        }

        private DataSet FormatDg(List<PayableModel> payableModels)
        {
           

            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();

            
            dt.Columns.Add("Invoice #",typeof(string));
            dt.Columns.Add("Date Due",typeof(string));
            dt.Columns.Add("Amount",typeof(string));
            dt.Columns.Add("Paid",typeof(bool));  

            foreach (PayableModel p in payableModels)
            {
                dt.Rows.Add(p.invoice_no,p.payableDue.Split(' ')[0], String.Format("₱{0:n}", p.amount),p.isPaid);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private async  void PanelPayables_Load(object sender, EventArgs e)
        {
            await loadPoCbs();
        }

        private void cbPo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTransfId = int.Parse((cbPo.SelectedItem as ComboBoxItem).Value.ToString());
        }
    }
}
