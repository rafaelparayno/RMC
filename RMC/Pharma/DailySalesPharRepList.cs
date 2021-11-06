using RMC.Database.Controllers;
using RMC.Pharma.Dialogs;
using RMC.Reception.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Pharma
{
    public partial class DailySalesPharRepList : Form
    {
        DailySalesReportController reportController = new DailySalesReportController();
        public DailySalesPharRepList()
        {
            InitializeComponent();
            initColLv();
        }

        private void initColLv()
        {
            lvSales.View = View.Details;
            lvSales.Columns.Add("id", 200, HorizontalAlignment.Center);
            lvSales.Columns.Add("Date", 450, HorizontalAlignment.Center);

        }

        private async Task refreshData()
        {
            Dictionary<string, string> data = await reportController.getData(1);
            lvSales.Items.Clear();
            foreach (KeyValuePair<string, string> kp in data)
            {
                ListViewItem lv = new ListViewItem();
                lv.Text = kp.Key;
                lv.SubItems.Add(kp.Value.Split(' ')[0]);

                lvSales.Items.Add(lv);
            }
        }

        private async Task searchData()
        {
            Dictionary<string, string> data = await
                reportController.getData(dateTimePicker1.Value.ToString("yyyy-MM-dd"), 1);
            lvSales.Items.Clear();
            foreach (KeyValuePair<string, string> kp in data)
            {
                ListViewItem lv = new ListViewItem();
                lv.Text = kp.Key;
                lv.SubItems.Add(kp.Value.Split(' ')[0]);

                lvSales.Items.Add(lv);
            }
        }

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditDailyPharSalesRep form = new AddEditDailyPharSalesRep();
            form.ShowDialog();
            await refreshData();
        }

        private async void DailySalesPharRepList_Load(object sender, EventArgs e)
        {
            await refreshData();
        }

        private async void btnEditItem_Click(object sender, EventArgs e)
        {
            if (lvSales.SelectedItems.Count == 0)
                return;

            int id = int.Parse(lvSales.SelectedItems[0].SubItems[0].Text);

            AddEditDailyPharSalesRep form = new AddEditDailyPharSalesRep(id);
            form.ShowDialog();
            await refreshData();
        }

        private async void iconButton3_Click(object sender, EventArgs e)
        {
            if (lvSales.SelectedItems.Count == 0)
                return;

            int id = int.Parse(lvSales.SelectedItems[0].SubItems[0].Text);

            DailySalesReportPhFormDiag form = new DailySalesReportPhFormDiag(id);
            form.ShowDialog();
            await refreshData();
        }
    }
}
