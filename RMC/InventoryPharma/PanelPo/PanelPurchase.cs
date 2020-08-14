using RMC.Components;
using RMC.Database.Controllers;
using RMC.SystemSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelPo
{
    public partial class PanelPurchase : Form
    {
        SupplierController supplierController = new SupplierController();
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        ItemController itemz = new ItemController();
        bool isShowEoq = false;
        int days = 0;
        float PercentStocks = 0;
        public PanelPurchase()
        {
            InitializeComponent();
            loadFromDbtoCb();
            initSettings();
        }

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = supplierController.getComboDatas();

            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1 };
            await Task.WhenAll(Cbs);

           
            cbSuppliers.Items.AddRange(task1.Result.ToArray());
        }

        private void cbSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cbSupValue = int.Parse((cbSuppliers.SelectedItem as ComboBoxItem).Value.ToString());
            loadGrid(cbSupValue);
        }

        private async void loadGrid(int id)
        {
            DataSet ds = await itemz.getDataWithSupplierIdTotalStocks(id);
            RefreshGrid(ds);
        }

        private async void RefreshGrid(DataSet ds)
        {
           

            lvItemsSuppliers.Columns.Clear();
            lvItemsSuppliers.View = View.Details;
            lvItemsSuppliers.Columns.Add("ID",80 ,HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Item Name", 150, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Current Stocks", 150, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Avg Sales", 80, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("ROP",80, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Safety Stocks", 100, HorizontalAlignment.Left);
            lvItemsSuppliers.Columns.Add("Optimal Order", 80, HorizontalAlignment.Left);       
            if (rbEoqShow.Checked) lvItemsSuppliers.Columns.Add("EOQ", 80, HorizontalAlignment.Left);
         

            lvItemsSuppliers.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ListViewItem items = new ListViewItem();
                items.Text = dr[0].ToString();
                items.SubItems.Add(dr[1].ToString());
                items.SubItems.Add(dr[2].ToString());
                int sum = days == 0 ? 0 :  await getSum(days, int.Parse(dr[0].ToString()));
                Decimal avg = Decimal.Divide(sum, days);
             
                items.SubItems.Add(Math.Round(avg,0) + "");
                items.SubItems.Add("NONE");
                items.SubItems.Add("NONE");
                items.SubItems.Add("NONE");  
                if (rbEoqShow.Checked) items.SubItems.Add("NONE");
                lvItemsSuppliers.Items.Add(items);
            }
           
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
                return;
            if (numericUpDown2.Value == 0)
                return;

            string lines = string.Format("{0}#{1}#{2}", numericUpDown1.Value.ToString(), numericUpDown2.Value.ToString(),isShowEoq.ToString());
            System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"\poSettings.txt");
            file.WriteLine(lines);
            file.Close();
        }

        private void rbEoqShow_CheckedChanged(object sender, EventArgs e)
        {
            if(rbEoqShow.Checked)
              isShowEoq = true;

        }

        private void rbHideEoq_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHideEoq.Checked)
                isShowEoq = false;
        }

        private void initSettings()
        {

            if (!isFoundFile())
                return;

            numericUpDown1.Value = int.Parse(PoSettings.FetchPoSettings()[0]);
            numericUpDown2.Value = int.Parse(PoSettings.FetchPoSettings()[1]);
            if(PoSettings.FetchPoSettings()[2] == "True")
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
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show("Please Save a settings");
                return false;
            }
        }
    }
}
