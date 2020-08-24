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

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep
{
    public partial class RopRep : Form
    {

        bool isShowEoq = false;
        int days = 0;
        float PercentStocks = 0;
        ReceiveControllers receiveControllers = new ReceiveControllers();
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        ItemController itemz = new ItemController();
        public RopRep()
        {
            InitializeComponent();
            initSettings();
            loadGrid();
        }

        private async void loadGrid()
        {
            DataSet ds = await itemz.getdatasetActiveWithStock();
            RefreshGrid(ds);
        }


        private void initColumns()
        {
            lvItemsROP.Columns.Clear();
            lvItemsROP.View = View.Details;
            lvItemsROP.Columns.Add("ID", 80, HorizontalAlignment.Left);
            lvItemsROP.Columns.Add("Item Name", 150, HorizontalAlignment.Left);
            lvItemsROP.Columns.Add("SKU", 80, HorizontalAlignment.Left);
            lvItemsROP.Columns.Add("Current Stocks", 150, HorizontalAlignment.Left);
            lvItemsROP.Columns.Add("Unit Cost", 80, HorizontalAlignment.Left);
            lvItemsROP.Columns.Add("Avg Sales", 80, HorizontalAlignment.Left);
            lvItemsROP.Columns.Add("Lead Time", 120, HorizontalAlignment.Left);
            lvItemsROP.Columns.Add("Safety Stocks", 100, HorizontalAlignment.Left);
            lvItemsROP.Columns.Add("ROP", 80, HorizontalAlignment.Left);
            lvItemsROP.Columns.Add("Optimal Order", 80, HorizontalAlignment.Left);
            if (rbEoqShow.Checked) lvItemsROP.Columns.Add("EOQ", 80, HorizontalAlignment.Left);
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

        private async void RefreshGrid(DataSet ds)
        {
            initColumns();

            lvItemsROP.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int itemId = int.Parse(dr[0].ToString());

                int sum = days == 0 ? 0 : await getSum(days, itemId);
                double avgLeadInt = days == 0 ? 0 : Math.Round(await getLeadSum(itemId, days), MidpointRounding.AwayFromZero);
                decimal avg = Math.Round(Decimal.Divide(sum, days), MidpointRounding.AwayFromZero);
                decimal safetyStock = Math.Round(days * avg, MidpointRounding.AwayFromZero);
                decimal ROP = computeRop(avg, avgLeadInt, safetyStock);
                decimal percentsOptimal = safetyStock * decimal.Parse((PercentStocks + 1) + "");
                string currentStocks = dr[2].ToString() == "" ? "0" : dr[2].ToString();
                ListViewItem items = new ListViewItem();



                items.Text = dr[0].ToString();
                items.SubItems.Add(dr[1].ToString());
                items.SubItems.Add(dr[4].ToString());
            
                items.SubItems.Add(currentStocks);
                items.SubItems.Add(dr[3].ToString());
                items.SubItems.Add(avg == 0 ? "no data" : avg + "");
                items.SubItems.Add(avgLeadInt == 0 ? "no data" : avgLeadInt + "");
                items.SubItems.Add(safetyStock == 0 ? "no data" : safetyStock + "");
                items.SubItems.Add(ROP == 0 ? "no data" : ROP + "");
                items.SubItems.Add(percentsOptimal == 0 ? "no data" : percentsOptimal + "");
                if (rbEoqShow.Checked) items.SubItems.Add("NONE");
                int s = int.Parse(currentStocks);
                int r = int.Parse(ROP + "");
                if(r >= s) 
                    lvItemsROP.Items.Add(items);

            }

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
            loadGrid();
        }
    }
}
