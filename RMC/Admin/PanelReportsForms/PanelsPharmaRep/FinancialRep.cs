using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryRep;
using RMC.Reports;
using RMC.Utilities;
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
using System.Xml;

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep
{
    public partial class FinancialRep : Form
    {
        FinancialReportPh reportPh = new FinancialReportPh();
        ItemController itemController = new ItemController();
        ReceiveControllers receiveControllers = new ReceiveControllers();
        DailySalesReportController dailySalesReportController = new DailySalesReportController();
        TransferLogsController transferLogs = new TransferLogsController();
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        PharmaStocksController stocksController = new PharmaStocksController();
        PoItemController poic = new PoItemController();
  

        Dictionary<int, int> itemsStocks = new Dictionary<int, int>();
        Dictionary<int, int> currentStocks = new Dictionary<int, int>();
        float totalOut = 0;
        float ar = 0;
        float totalExpenses = 0;
        float excess = 0;
        float totalSales = 0;

        public FinancialRep()
        {
            InitializeComponent();
            loadMonths();
        }

        public void loadMonths()
        {
            foreach(string m in StaticData.months)
            {
                comboBox1.Items.Add(m);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker1.Value.Year;
            if (comboBox1.SelectedIndex == -1)
                return;
            int m = comboBox1.SelectedIndex + 1;
            pictureBox1.Show();
            pictureBox1.Update();
            List<string> paths = await dailySalesReportController.getFilesPath(year, m, 1);


            totalOut = await transferLogs.getTotalOut(year,m);
            totalSales = await salesPharmaController.getSumInMonth(m, year);
            
            await loadData(paths);
            currentStocks = await stocksController.getAllStocks();
            itemsStocks = await loadStocks(m, year);
            float totalBeginning = await getBeginningInv();
            List<PoModel> plist = await poic.getPoNoWithOrigStocks(year,m);
            float totalCostSales = await salesPharmaController.getTotalCostMonths(m, year);
            float actualInv = await getActualInv();
            float totalExpired = await itemController.getSumExpiredItems(m, year);
            float sumPurchaseOrder = plist.Sum(p => p.totalCost);
            float tiq = sumPurchaseOrder + totalBeginning;
            
            pictureBox1.Hide();
            reportPh.SetParameterValue("biParam", totalBeginning);
            reportPh.SetParameterValue("tpParam", sumPurchaseOrder);
            reportPh.SetParameterValue("tiqParam", tiq);
            reportPh.SetParameterValue("totalSales", totalSales);
            reportPh.SetParameterValue("csParam", totalCostSales);
            reportPh.SetParameterValue("arParam", ar);
            reportPh.SetParameterValue("oParam", totalOut);
            reportPh.SetParameterValue("aiParam", actualInv);
            reportPh.SetParameterValue("exParam", excess);
            reportPh.SetParameterValue("teParam", totalExpenses);
            reportPh.SetParameterValue("expParam", totalExpired);

            crystalReportViewer1.ReportSource = reportPh;
        }

        private async Task<Dictionary<int,int>> loadStocks(int m,int year)
        {
            Dictionary<int, int> updatedStocks = new Dictionary<int, int>();
            foreach (KeyValuePair<int,int> kp in currentStocks)
            {
                int totalSalesQty = await salesPharmaController.getTotalQtySalesID(year, m, kp.Key);
                int totalRec = await receiveControllers.getReceive(kp.Key, year, m);
                int currentStocks = kp.Value + totalSalesQty;
                currentStocks -= totalRec;
                updatedStocks.Add(kp.Key, currentStocks);
                //itemsStocks[kp.Key] = currentStocks;
            }
            return updatedStocks;

        }

        private async Task<float> getActualInv()
        {
            float actualInv = 0;
            foreach (KeyValuePair<int, int> kp in currentStocks)
            {
                float unitPrice = await itemController.getUnitCosts(kp.Key);
                actualInv += unitPrice * kp.Value;
            }

            return actualInv;
        }

        private async Task<float> getBeginningInv()
        {
            float totalBeginning = 0;
            foreach (KeyValuePair<int, int> kp in itemsStocks)
            {
                float unitPrice = await itemController.getUnitCosts(kp.Key);
                totalBeginning += unitPrice * kp.Value;
            }

            return totalBeginning;
        }

        private async Task loadData(List<string> paths)
        {
           
            excess = 0;
            ar = 0;
            float totalCashonHand = 0;
            float cashflow = 0;
         

            foreach (string path in paths)
            {
                XmlDocument doc = new XmlDocument();

                if (!File.Exists(path))
                    return;


                doc.Load(path);

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {

                    if (node.Name == "arParam")
                        ar += float.Parse(node.InnerText);

                    if (node.Name == "expenseCost1")
                        totalExpenses += float.Parse(node.InnerText);

                    if (node.Name == "expenseCost2")
                        totalExpenses += float.Parse(node.InnerText);

                    if (node.Name == "expenseCost3")
                        totalExpenses += float.Parse(node.InnerText);

                    if (node.Name == "expenseCost4")
                        totalExpenses += float.Parse(node.InnerText);

                    if (node.Name == "ThouQtyParam")
                    {
                      
                        totalCashonHand += 1000 * int.Parse(node.InnerText); ;
                    }
                      
                    if (node.Name == "FiveHQtyParam")
                    {
                        
                        totalCashonHand += 500 * int.Parse(node.InnerText); ;
                    }
                      
                    if (node.Name == "TwoHQtyParam")
                    {
                        totalCashonHand += 200 * int.Parse(node.InnerText);
                    }
                   
                    if (node.Name == "OneHQtyParam")
                    {
                        totalCashonHand += 100 * int.Parse(node.InnerText);   
                    }
                       
                    if (node.Name == "fiftyQtyParam")
                    {
                        totalCashonHand += 50 * int.Parse(node.InnerText);
                    }
                      
                    if (node.Name == "twentyQtyParam")
                    {
                        totalCashonHand += 20 * int.Parse(node.InnerText);
                    }
                        
                    if (node.Name == "coinsTotal")
                    {
                        totalCashonHand +=  int.Parse(node.InnerText);
                    }

                    cashflow = totalCashonHand + totalExpenses + ar + totalOut;
                    excess = cashflow - totalSales;  
                }
                await Task.Delay(300);
            }
           
        }
       
    }
}
