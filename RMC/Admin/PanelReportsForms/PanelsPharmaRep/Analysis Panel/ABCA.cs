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

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep.Analysis_Panel
{
    public partial class ABCA : Form
    {
        ItemController itemz = new ItemController();
        SalesPharmaController salesPharmaController = new SalesPharmaController();
        public ABCA()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
            initLvs();
        }

        private void initLvs()
        {
            lvsAnalysis.View = View.Details;
            lvsAnalysis.Columns.Add("Item Name", 300, HorizontalAlignment.Left) ;
            lvsAnalysis.Columns.Add("Annual Number Of Unit Sold", 300, HorizontalAlignment.Left);
            lvsAnalysis.Columns.Add("Cost Per Unit", 150, HorizontalAlignment.Left);
            lvsAnalysis.Columns.Add("Annual Consumation Value", 150, HorizontalAlignment.Left);
            lvsAnalysis.Columns.Add("% of annual units sold", 150, HorizontalAlignment.Left);
            lvsAnalysis.Columns.Add("% of consumption value", 150, HorizontalAlignment.Left);
          
        }

        private async Task getDs()
        {
            List<AnalysisModel> ListAnalysis = new List<AnalysisModel>();
            DataSet ds = await itemz.getdataSetActive();
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                AnalysisModel analysisModel = new AnalysisModel();

                float unitprice = float.Parse(dr[2].ToString());
                int annual = await salesPharmaController.getAnualUnitsSold(int.Parse(dr[0].ToString()), dateTimePicker1.Value.Year);
                float annualConsumationVaalue = unitprice * annual;


                analysisModel.id = int.Parse(dr[0].ToString());
                analysisModel.name = dr[1].ToString();
                analysisModel.price = float.Parse(dr[2].ToString());
                analysisModel.unitsSold = annual;
                analysisModel.anualConsumation = annualConsumationVaalue;
                ListAnalysis.Add(analysisModel);
            }
            ListAnalysis =  ListAnalysis.OrderByDescending(x => x.anualConsumation).ToList();
            int totalAnnualUnitsSold = ListAnalysis.Sum(x => x.unitsSold);
            float totalAnnualConsumationValue = ListAnalysis.Sum(x => x.anualConsumation);
            foreach(AnalysisModel a in ListAnalysis)
            {
                ListViewItem lvItem = new ListViewItem();
              
                 decimal percentageUnitsSold = a.unitsSold == 0 ? 0 : decimal.Round((decimal.Divide(a.unitsSold, totalAnnualUnitsSold) * 100),1);

                float percentageAnnualCon = a.anualConsumation == 0 ? 0 : (float)Math.Round(a.anualConsumation / totalAnnualConsumationValue * 100,1);
                lvItem.Text = a.name;
                lvItem.SubItems.Add(a.unitsSold.ToString());
                lvItem.SubItems.Add(a.price.ToString());
                lvItem.SubItems.Add(a.anualConsumation.ToString());

                lvItem.SubItems.Add(percentageUnitsSold.ToString());
                lvItem.SubItems.Add(percentageAnnualCon.ToString());

                lvsAnalysis.Items.Add(lvItem);
            }
            label1.Text = "";
            label1.Text = $"Total Annual Units Sold: : {totalAnnualUnitsSold} \nTotal Annual Consumation Value  : ₱{totalAnnualConsumationValue}";
            label1.Visible = true;
        }

        private void displayItemsA()
        {
            float Cumulative = 0;
          
            foreach(ListViewItem lvI in lvsAnalysis.Items)
            {
                int index = lvI.Index;
                float f = float.Parse(lvI.SubItems[5].Text);
                if (f == 0)
                    return;

                Cumulative += f;
                if (Cumulative <= 80)
                {
                    lvsAnalysis.Items[index].BackColor = Color.DarkSeaGreen;
                }
            }

        }



        private async void iconButton1_Click(object sender, EventArgs e)
        {
            lvsAnalysis.Items.Clear();
            await getDs();
            displayItemsA();
         

        }
    }
}
