using RMC.Database.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Lab.Panels
{
    public partial class PanelViewStocks : Form
    {
        ItemController itemz = new ItemController();
        public PanelViewStocks()
        {
            InitializeComponent();
            loadData();
        }

        private async void loadData()
        {
            DataSet ds = await itemz.getDataSetWithStockClinic();
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(ds).Tables[0];
            dgItemList.AutoResizeColumns();
        }

        private DataSet FormatDg(DataSet ds)
        {

            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Item Name");
            dt.Columns.Add("Stocks");
            dt.Columns.Add("SKU");
            dt.Columns.Add("Description");
            dt.Columns.Add("Generic Or Branded");
            dt.Columns.Add("Category");
            dt.Columns.Add("Sub Category");
            dt.Columns.Add("Unit Name");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //your code here
                string isBrand = formatBranded(int.Parse(dr["isBranded"].ToString()));
                string category = formatCategory(int.Parse(dr["item_type"].ToString()));
                string stocks = dr[2].ToString() == "" ? "0" : dr[2].ToString();

                dt.Rows.Add(dr[0], dr[1], stocks, dr[3], dr[4], isBrand, category, dr[7], dr[8]);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private string formatBranded(int ds)
        {
            string brand;

            if (ds == 1)
            {
                brand = "Branded";
            }
            else if (ds == 2)
            {
                brand = "Generic";
            }
            else
            {
                brand = "N/A";
            }

            return brand;
        }


        private string formatCategory(int ds)
        {
            string cat;

            if (ds == 1)
            {
                cat = "Medicine";
            }
            else if (ds == 2)
            {
                cat = "Products";
            }
            else
            {
                cat = "Equipment";
            }

            return cat;
        }
    }
}
