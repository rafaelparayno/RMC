﻿using RMC.Database.Controllers;
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

namespace RMC.Reception
{
    public partial class DailySalesRepList : Form
    {
        DailySalesReportController reportController = new DailySalesReportController();
        public DailySalesRepList()
        {
            InitializeComponent();
            initColLv();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            addEditDailySalesRep form = new addEditDailySalesRep();
            form.ShowDialog();
        }

        private void initColLv()
        {
            lvSales.View = View.Details;
            lvSales.Columns.Add("id", 200, HorizontalAlignment.Center);
            lvSales.Columns.Add("Date", 450, HorizontalAlignment.Center);
            
        }


        private async Task refreshData()
        {
            Dictionary<string,string> data = await reportController.getData();

            foreach(KeyValuePair<string,string> kp in data){
                ListViewItem lv = new ListViewItem();
                lv.Text = kp.Key;
                lv.SubItems.Add(kp.Value.Split(' ')[0]);

                lvSales.Items.Add(lv);
            }
        }

        private async void DailySalesRepList_Load(object sender, EventArgs e)
        {
           await refreshData();
        }
    }
}
