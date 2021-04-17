﻿using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PanelPo.Dialogs;
using RMC.InventoryRep;
using RMC.SystemSettings;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelPo
{
    public partial class PanelPurchase : Form
    {
        #region Vars

        SupplierController supplierController = new SupplierController();
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        PoController poController = new PoController();
        ItemController itemz = new ItemController();
        PoItemController poItemController = new PoItemController();
        ReceiveControllers receiveControllers = new ReceiveControllers();

        bool isShowEoq = false;
        int days = 0;
        float PercentStocks = 0;
        decimal totalCost = 0;
        int cbSupValue = 0;
        int PONO = 0;
        DataTable dt = new DataTable(); 
        #endregion

        public PanelPurchase()
        {
            InitializeComponent();
            loadFromDbtoCb();
            initSettings();
            initDg();
            initPO();
        }

        #region Own Functions
        private void initDg()
        {
            dt.Columns.Add("Itemid", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("SubTotal", typeof(decimal));
        }

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = supplierController.getComboDatas();

            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1 };
            await Task.WhenAll(Cbs);


            cbSuppliers.Items.AddRange(task1.Result.ToArray());
        }

        private async void loadGrid(int id)
        {
            DataSet ds = await itemz.getDataWithSupplierIdTotalStocks(id);
            RefreshGrid(ds);
        }


        private void initColumns()
        {
            lvItemsSuppliers.Columns.Clear();
            lvItemsSuppliers.View = View.Details;
            lvItemsSuppliers.Columns.Add("ID", 80, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Item Name", 150, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("SKU", 80, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Current Stocks", 150, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Unit Cost", 80, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Avg Sales", 80, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Lead Time", 120, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Safety Stocks", 100, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("ROP", 80, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Optimal Order", 80, HorizontalAlignment.Left);
            if (rbEoqShow.Checked) lvItemsSuppliers.Columns.Add("EOQ", 80, HorizontalAlignment.Left);
        }

        private async void RefreshGrid(DataSet ds)
        {
            initColumns();

            lvItemsSuppliers.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int itemId = int.Parse(dr[0].ToString());

                int sum = days == 0 ? 0 : await getSum(days, itemId);
                double avgLeadInt = days == 0 ? 0 : Math.Round(await getLeadSum(itemId, days), MidpointRounding.AwayFromZero);
                decimal avg = days == 0 ? 0 : Math.Round(Decimal.Divide(sum, days), MidpointRounding.AwayFromZero);
                decimal safetyStock = Math.Round(days * avg, MidpointRounding.AwayFromZero);
                decimal ROP = computeRop(avg, avgLeadInt, safetyStock);
                decimal percentsOptimal = safetyStock * decimal.Parse((PercentStocks + 1) + "");
                ListViewItem items = new ListViewItem();
                items.Text = dr[0].ToString();
                items.SubItems.Add(dr[1].ToString());
                items.SubItems.Add(dr[4].ToString());
                string currentStocks = dr[2].ToString() == "" ? "0" : dr[2].ToString();
                items.SubItems.Add(currentStocks);
                items.SubItems.Add(dr[3].ToString());
                items.SubItems.Add(avg == 0 ? "no data" : avg + "");
                items.SubItems.Add(avgLeadInt == 0 ? "no data" : avgLeadInt + "");
                items.SubItems.Add(safetyStock == 0 ? "no data" : safetyStock + "");
                items.SubItems.Add(ROP == 0 ? "no data" : ROP + "");
                items.SubItems.Add(percentsOptimal == 0 ? "no data" : percentsOptimal + "");
                if (rbEoqShow.Checked) items.SubItems.Add("NONE");

                lvItemsSuppliers.Items.Add(items);
            }

        }

        private void initSettings()
        {

            if (!isFoundFile())
                return;

            numericUpDown1.Value = int.Parse(PoSettings.FetchPoSettings()[0]);
            numericUpDown2.Value = int.Parse(PoSettings.FetchPoSettings()[1]);
            if (PoSettings.FetchPoSettings()[2] == "True")
            {
                rbEoqShow.Checked = true;
                isShowEoq = true;
            }
            else
            {
                rbHideEoq.Checked = true;
                isShowEoq = false;
            }

        }

        private decimal computeRop(decimal averageSales, double leadtime, decimal safetyStock)
        {
            return Decimal.Multiply(averageSales, decimal.Parse("" + leadtime)) + safetyStock;
        }

        private async Task<float> getLeadSum(int itemid, int days)
        {
            return await receiveControllers.getSumLead(itemid, days);
        }

        private async Task<int> getSum(int days, int id)
        {
            return await salesPharmaController.getSalesInDaysItemId(days, id);
        }

        private bool isFoundFile()
        {
            try
            {
                days = int.Parse(PoSettings.FetchPoSettings()[0]);
                PercentStocks = float.Parse(PoSettings.FetchPoSettings()[1]) / 100;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show("Please Save a settings");
                return false;
            }
        }

        private bool isFoundInDg(int id)
        {

            foreach (DataRow dr in dt.Rows)
            {
                if (id == int.Parse(dr[0].ToString()))
                {
                    return true;
                }

            }

            return false;
        }

        private int CurrentQty(int id)
        {

            foreach (DataRow dr in dt.Rows)
            {
                if (id == int.Parse(dr[0].ToString()))
                {
                    return int.Parse(dr[3].ToString());
                }

            }

            return 0;
        }

        private void ComputeTotalCost()
        {
            totalCost = 0;

            foreach (DataGridViewRow dr in dgItemList.Rows)
            {
                totalCost += decimal.Parse(dr.Cells["SubTotal"].Value.ToString());
            }

            label7.Text = "PHP " + String.Format("{0:0.##}", totalCost);
        }

        private void initPO()
        {
            PONO = poController.getLastPoNo();
            groupBox2.Text = "Purchase Order # " + PONO;
        }

        private void ResetData()
        {
            dt.Rows.Clear();
            label7.Text = "PHP 0.00";
        }

        private async void SearchGrid(string searchkey, int cbSelect)
        {
            DataSet ds = await itemz.getDataWithSupplierIdTotalStocksWithSearch(cbSupValue, cbSelect, searchkey);
            RefreshGrid(ds);
        }
        #endregion


        #region Handler Events

        private void cbSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSupValue = int.Parse((cbSuppliers.SelectedItem as ComboBoxItem).Value.ToString());

            loadGrid(cbSupValue);
            ResetData();
        }


        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
                return;
            if (numericUpDown2.Value == 0)
                return;

            string lines = string.Format("{0}#{1}#{2}", numericUpDown1.Value.ToString(), numericUpDown2.Value.ToString(), isShowEoq.ToString());
            System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"\poSettings.txt");
            file.WriteLine(lines);
            file.Close();

            initSettings();
            loadGrid(cbSupValue);
        }

        private void rbEoqShow_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEoqShow.Checked)
                isShowEoq = true;

        }

        private void rbHideEoq_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHideEoq.Checked)
                isShowEoq = false;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (lvItemsSuppliers.Items.Count == 0)
                return;
            if (lvItemsSuppliers.SelectedItems.Count == 0)
                return;

            int itemIdSelected = int.Parse(lvItemsSuppliers.SelectedItems[0].SubItems[0].Text);
            decimal unitCosts = decimal.Parse(lvItemsSuppliers.SelectedItems[0].SubItems[4].Text);
            string name = lvItemsSuppliers.SelectedItems[0].SubItems[1].Text;
            int optimal = int.TryParse(lvItemsSuppliers.SelectedItems[0].SubItems[1].Text, out _) ?
                int.Parse(lvItemsSuppliers.SelectedItems[0].SubItems[9].Text) : 0;

            QtyDiag form = new QtyDiag(optimal);
            form.ShowDialog();

            if (form.qty == 0)
                return;

            decimal subunitcosts = form.qty * unitCosts;
            if (isFoundInDg(itemIdSelected))
            {
                DataRow[] rows = dt.Select(String.Format(@"Itemid = {0}", itemIdSelected));
                int index = dt.Rows.IndexOf(rows[0]);
                int currentQty = CurrentQty(itemIdSelected);
                dt.Rows[index].SetField("Quantity", currentQty + form.qty);
                subunitcosts = (currentQty + form.qty) * unitCosts;
                dt.Rows[index].SetField("SubTotal", subunitcosts);
            }
            else
            {
                dt.Rows.Add(itemIdSelected, name,
                      unitCosts, form.qty, subunitcosts);
            }


            dgItemList.DataSource = dt;
            ComputeTotalCost();
        }

        private void btnRemoveOrder_Click(object sender, EventArgs e)
        {
            if (dgItemList.SelectedRows.Count == 0)
                return;

            int index = dgItemList.SelectedRows[0].Index;
            dt.Rows.RemoveAt(index);

            dgItemList.DataSource = dt;

            ComputeTotalCost();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;


            poController.save(cbSupValue, UserLog.getUserId());
            foreach (DataGridViewRow dr in dgItemList.Rows)
            {
                poItemController.Save(int.Parse(dr.Cells["Itemid"].Value.ToString()),
                                    int.Parse(dr.Cells["Quantity"].Value.ToString()));
                //   poController.save( cbSupValue, int.Parse(dr.Cells["Quantity"].Value.ToString()), PONO);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            DateTime dateTime = DateTime.Today;
            purchaseOrder.SetDataSource(ds);
            purchaseOrder.SetParameterValue("orderName", UserLog.getFullName());
            purchaseOrder.SetParameterValue("DateParam", dateTime.ToString("MMMM dd , yyyy"));
          
            purchaseOrder.SetParameterValue("poNO", PONO);

            purchaseOrder.PrintToPrinter(1, false, 0, 0);

            MessageBox.Show("Succesfully Added A Purchase Order");
            initPO();
            ResetData();

        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            int selectedCombobx = comboBox1.SelectedIndex;
            if (selectedCombobx == -1)
            {
                loadGrid(cbSupValue);

            }
            else
            {
                SearchGrid(txtName.Text.Trim(), selectedCombobx);
            }

        }
        #endregion

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if(cbSuppliers.SelectedIndex> -1)
            {
                ViewBo viewBo = new ViewBo(cbSupValue);
                viewBo.ShowDialog();



              /*  if (viewBo.qtyAdd == 0)
                    return;

                decimal subunitcosts = viewBo.qty * unitCosts;
                if (isFoundInDg(itemIdSelected))
                {
                    DataRow[] rows = dt.Select(String.Format(@"Itemid = {0}", itemIdSelected));
                    int index = dt.Rows.IndexOf(rows[0]);
                    int currentQty = CurrentQty(itemIdSelected);
                    dt.Rows[index].SetField("Quantity", currentQty + form.qty);
                    subunitcosts = (currentQty + form.qty) * unitCosts;
                    dt.Rows[index].SetField("SubTotal", subunitcosts);
                }
                else
                {
                    dt.Rows.Add(itemIdSelected, name,
                          unitCosts, form.qty, subunitcosts);
                }*/


                dgItemList.DataSource = dt;
                ComputeTotalCost();
            }
           
        }
    }
}
