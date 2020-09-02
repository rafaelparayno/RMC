﻿using RMC.Database.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep
{
    public partial class stockHistory : Form
    {
        StocksHistoryController stocksHistoryController = new StocksHistoryController();
        public stockHistory()
        {
            InitializeComponent();
            loadGrid();
        }


        private async void loadGrid()
        {
            DataSet ds = await stocksHistoryController.getStockHis();
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        private async void searchGrid(string date)
        {
           DataSet ds = await stocksHistoryController.getStockHis(date);
            refreshGrid(ds);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            searchGrid(dateTimePicker1.Value.ToString("yyyy/MM/dd"));
        }
    }
}