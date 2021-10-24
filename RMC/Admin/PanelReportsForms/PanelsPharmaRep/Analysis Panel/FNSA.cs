using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep.Analysis_Panel
{
    public partial class FNSA : Form
    {
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        ReceiveControllers receiveControllers = new ReceiveControllers();
        ItemController itemz = new ItemController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        Dictionary<int, List<fnsaItemMod>> datasDic;
        List<averageStayFns> ListAvgStayFns = new List<averageStayFns>();
        List<ConsumationRateFns> listConsumtionRate = new List<ConsumationRateFns>();
        public FNSA()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now.AddDays(1);
            dateTimePicker2.Enabled = false;
            initLvsView();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker2.Enabled = true;
        }

        private void initLvsView()
        {
            lvsAnalysis.View = View.Details;
          
            lvsAnalysis.Columns.Add("Id Code", 200, HorizontalAlignment.Left);
            lvsAnalysis.Columns.Add("Average Stay", 200, HorizontalAlignment.Left);
            lvsAnalysis.Columns.Add("Cumulative Avg Stay", 200, HorizontalAlignment.Left);
            lvsAnalysis.Columns.Add("% average Stay", 200, HorizontalAlignment.Left);
           // lvsAnalysis.Columns.Add("FSN Classification", 200, HorizontalAlignment.Left);


            lvAnalysis2.View = View.Details;
            lvAnalysis2.Columns.Add("Id Code", 200, HorizontalAlignment.Left);
            lvAnalysis2.Columns.Add("Consumpation Rate", 200, HorizontalAlignment.Left);
            lvAnalysis2.Columns.Add("Cumulative Con Rate", 200, HorizontalAlignment.Left);
            lvAnalysis2.Columns.Add("% Con Rate", 200, HorizontalAlignment.Left);
            // lvsAnalysis.Columns.Add("FSN Classification", 200, HorizontalAlignment.Left);

        }

        private async Task<List<fnsaItemMod>> getItemData(int id)
        {
            int CurrentStocks = await pharmaStocksController.getStocks(id);
            int Balance = await salesPharmaController.getOpeningBalance(id,
                                                dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            int openingBalance = CurrentStocks + Balance;
            int closingBalance = openingBalance;
            int inventoryHoldingDays = 0;
            List<fnsaItemMod> listfnsa = new List<fnsaItemMod>();
          

            List<Task<fnsaItemMod>> task = new List<Task<fnsaItemMod>>();
            //Do Loop HEre
            DateTime startDate = DateTime.Parse(dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            DateTime endDate = DateTime.Parse(dateTimePicker2.Value.ToString("dd/MM/yyyy"));
            for(DateTime date = startDate;date <= endDate.Date; date = date.AddDays(1))
            {
          
                int rec = await receiveControllers.getReceive(id, date.ToString("yyyy-MM-dd"));
                int salesQty = await salesPharmaController.getQtyInDate(id, date.ToString("yyyy-MM-dd"));
                closingBalance += rec;
                closingBalance -= salesQty;
                inventoryHoldingDays += closingBalance;
                task.Add(Task.Run(()=> getDatas(id, date.ToString("yyyy-MM-dd"), 
                                                rec, salesQty, closingBalance, 
                                                    inventoryHoldingDays)));
            }

            var results = await Task.WhenAll(task);
           
            listfnsa.AddRange(results);

            return listfnsa;

        }

        private fnsaItemMod getDatas(int id,string date, 
                                    int rec,int salesqty,
                                    int closingBal,int invHolding)
        {
           
            fnsaItemMod f = new fnsaItemMod();
            f.date = DateTime.Parse(date);
            f.id = id;
            f.recQty = rec;
            f.salesQty = salesqty;
            f.closingBal = closingBal;
            f.holdingDays = invHolding;
            return f;
        }

        private async Task setDisc()
        {
            DataSet ds = await itemz.getdataSetActive();
            await Task.Run(async () =>
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int id = int.Parse(dr[0].ToString());
                    datasDic.Add(id, await getItemData(id));
                }

                foreach (KeyValuePair<int, List<fnsaItemMod>> d in datasDic)
                {
                    averageStayFns avgSStayFns = new averageStayFns();
                    ConsumationRateFns conSsFns = new ConsumationRateFns();
                    int CurrentStocks = await pharmaStocksController.getStocks(d.Key);
                    int Balance = await salesPharmaController.getOpeningBalance(d.Key,
                                                        dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    int openingBalance = CurrentStocks + Balance;
                    float avgStay = (float)d.Value.LastOrDefault().holdingDays /
                                                            (openingBalance +
                                                                 d.Value.Sum(x => x.recQty));
                    float ConsumationRate = (float)d.Value.Sum(x => x.salesQty) / d.Value.Count;
                    avgSStayFns.itemcode = d.Key;
                    avgSStayFns.avgStay = avgStay;

                    conSsFns.itemcode = d.Key;
                    conSsFns.consumtionRate = ConsumationRate;

                    ListAvgStayFns.Add(avgSStayFns);
                    listConsumtionRate.Add(conSsFns);
                }
            });
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            pictureBox1.Show();
            pictureBox1.Update();
            datasDic = new Dictionary<int, List<fnsaItemMod>>();
            ListAvgStayFns = new List<averageStayFns>();
            listConsumtionRate = new List<ConsumationRateFns>();
            lvsAnalysis.Items.Clear();
            lvAnalysis2.Items.Clear();
            await setDisc();


            pictureBox1.Hide();
            ListAvgStayFns = ListAvgStayFns.OrderByDescending(x => x.avgStay).ToList();
            listConsumtionRate = listConsumtionRate.OrderByDescending(x => x.consumtionRate).ToList();
            settingLvAvgStay(ListAvgStayFns);
            settingLvConsumtionRate(listConsumtionRate);
            

        }

        private void settingLvAvgStay(List<averageStayFns> ListAvgStayFns)
        {
            float cumAvgStay = 0;

            foreach (averageStayFns a in ListAvgStayFns)
            {
                cumAvgStay += a.avgStay;
                a.CumAvgStay = cumAvgStay;
            }

            float lastAvgStay = ListAvgStayFns.LastOrDefault().CumAvgStay;
            foreach (averageStayFns a in ListAvgStayFns)
            {
                a.percentAvgStay = (float)Math.Round((a.CumAvgStay / lastAvgStay) * 100, 2);
            }

            foreach (averageStayFns a in ListAvgStayFns)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = a.itemcode.ToString();
                lvitem.SubItems.Add(a.avgStay.ToString());
                lvitem.SubItems.Add(a.CumAvgStay.ToString());
                lvitem.SubItems.Add(a.percentAvgStay.ToString());

                lvsAnalysis.Items.Add(lvitem);
            }
        }

        private void settingLvConsumtionRate(List<ConsumationRateFns> listConsumtionRate)
        {
            float cumConsumtionRate = 0;

            foreach(ConsumationRateFns c in listConsumtionRate)
            {
                cumConsumtionRate += c.consumtionRate;
                c.cumConsumtionRate = cumConsumtionRate;
            }

            float lastCumConsumtionRate = listConsumtionRate.LastOrDefault().cumConsumtionRate;

            foreach (ConsumationRateFns c in listConsumtionRate)
            {
                c.percentageConsumtionRate = (float)Math.Round((c.cumConsumtionRate
                                                                / lastCumConsumtionRate) * 100, 2);
            }

            foreach (ConsumationRateFns c in listConsumtionRate)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = c.itemcode.ToString();
                lvitem.SubItems.Add(c.consumtionRate.ToString());
                lvitem.SubItems.Add(c.cumConsumtionRate.ToString());
                lvitem.SubItems.Add(c.percentageConsumtionRate.ToString());

                lvAnalysis2.Items.Add(lvitem);
            }

        }

       
    }
}
