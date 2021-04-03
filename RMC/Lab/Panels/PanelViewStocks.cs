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
        private int doc = 0;
        public string itemname = "";
        public PanelViewStocks()
        {
            InitializeComponent();
            loadData();
        }

        public PanelViewStocks(int doc)
        {
            InitializeComponent();
            this.doc = doc;
            itemname = "";
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

        private async void SearchGrid(string searchkey, int cbSelect)
        {

            DataSet ds = await itemz.getDsSearchActiveClinic(cbSelect, searchkey);
            refreshGrid(ds);
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

        private void iconButton2_Click(object sender, EventArgs e)
        {
            int selectedCombobx = comboBox1.SelectedIndex;
            if (selectedCombobx == -1)
            {

                loadData();

            }
            else
            {
                SearchGrid(txtName.Text.Trim(), selectedCombobx);
            }
        }

        private void dgItemList_MouseClick(object sender, MouseEventArgs e)
        {

            if (doc == 0)
                return;

            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgItemList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                  //  idRightClick = dgItemList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    contextMenuStrip1.Show(dgItemList, new Point(e.X, e.Y));

                }

            }
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            itemname = dgItemList.SelectedRows[0].Cells[1].Value.ToString();
            this.Close();
        }
    }
}
