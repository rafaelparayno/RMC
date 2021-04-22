using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RMC.Database.Controllers;
using RMC.InventoryPharma.PanelViewStocks.Dialog;
using SwiftExcel;

namespace RMC.InventoryPharma.PanelViewStocks
{
    public partial class ViewStocks : Form
    {
        bool isPharmaList = true;

        ItemController itemz = new ItemController();

        public ViewStocks()
        {
            InitializeComponent();
            loadGridPharma();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            isPharmaList = !isPharmaList;
            if (!isPharmaList)
            {
                iconButton1.Text = "View Pharma Stocks";
                loadGridClinic();
                groupBox2.Text = "Stocks In Clinic";
            }
            else
            {
                iconButton1.Text = "View Clinic Stocks";
                loadGridPharma();
                groupBox2.Text = "Stocks In Pharma";
            }
          
        }


        private async void loadGridClinic()
        {
            DataSet ds = await itemz.getDataSetWithStockClinic();
            RefreshGrid(ds);
        }

        private async void loadGridPharma()
        {
            DataSet ds = await itemz.getDataSetWithStockPharma();
            RefreshGrid(ds);
        }

        private async void SearchGrid(string searchkey, int cbSelect)
        {
            if (isPharmaList)
            {
                DataSet ds = await itemz.getDsSearchActivePharma(cbSelect, searchkey);
                RefreshGrid(ds);
            }
            else
            {
                DataSet ds = await itemz.getDsSearchActiveClinic(cbSelect, searchkey);
                RefreshGrid(ds);
            }         
        }

        private void RefreshGrid(DataSet ds)
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
            dt.Columns.Add("Unit Cost");
            dt.Columns.Add("Sub Total");
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

                int stocksInt = int.Parse(stocks);
                float UnitCost = float.Parse(dr[3].ToString());
                float subTotal = stocksInt * UnitCost;

                dt.Rows.Add(dr[0], dr[1], stocks, dr[3],subTotal ,dr[4],dr[5] ,isBrand,category, dr[8], dr[9]);
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

        private void btnAdjust_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;

            int id = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());
            string name = dgItemList.SelectedRows[0].Cells[1].Value.ToString();

            AdjustStocks frm = new AdjustStocks(id,name,isPharmaList);
            frm.ShowDialog();


            if (!isPharmaList)
            {                
                loadGridClinic();              
             }
            else
            {             
                loadGridPharma();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;


            int id = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());
            string name = dgItemList.SelectedRows[0].Cells[1].Value.ToString();
            Transfer frm = new Transfer(id,name,isPharmaList);
            frm.ShowDialog();


            if (!isPharmaList)
            {
                loadGridClinic();
            }
            else
            {
                loadGridPharma();
            }

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            int selectedCombobx = comboBox1.SelectedIndex;
            if (selectedCombobx == -1)
            {
                if (!isPharmaList)
                {
                    loadGridClinic();
                }
                else
                {
                    loadGridPharma();
                }

            }
            else
            {
                SearchGrid(txtName.Text.Trim(), selectedCombobx);
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;


            int id = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());
            string name = dgItemList.SelectedRows[0].Cells[1].Value.ToString();
            TransferOtherForm frm = new TransferOtherForm(id, name, isPharmaList);
            frm.ShowDialog();


            if (!isPharmaList)
            {
                loadGridClinic();
            }
            else
            {
                loadGridPharma();
            }
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if(dgItemList.Rows.Count == 0)
                return;

            string path = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                path = folderBrowserDialog.SelectedPath;
            }
            DateTime date = DateTime.Today;
            string label = isPharmaList ? "Pharmacy" : "Clinic";
            path += $"/{label}_{date.ToString("yyyy-MM-dd")}.xlsx";

            writeExcel(path);
        }


        private void writeExcel(string path)
        {
            var sheet = new Sheet
            {
                Name = "Item Stocks",
                ColumnsWidth = new List<double> { 15, 35, 15, 15, 20, 25, 50, 25, 25, 25,25 }
            };

            ExcelWriter _excelWriter;

            using (_excelWriter = new ExcelWriter(path, sheet))
            {
                int lastRow = 0;
          
                foreach (DataGridViewRow dr in dgItemList.Rows)
                {
                    if (dr.Index == 0)
                    {
                        for (int i = 0; i < dgItemList.Columns.Count; i++)
                        {
                            int colnumber = i + 1;
                            string header = dgItemList.Columns[i].HeaderText;
                            _excelWriter.Write(
                                header,
                                colnumber, 1,
                                DataType.Text
                                );
                        }
                    }

                    for (int i = 0; i < dgItemList.Columns.Count; i++)
                    {
                        int colnumber = i + 1;
                        int rownumber = dr.Index + 2;

                    
                        string cellText = dr.Cells[i].Value.ToString();


                        _excelWriter.Write(cellText,
                            colnumber, rownumber,
                            float.TryParse(cellText, out _) ?
                            DataType.Number : DataType.Text
                            );
                        lastRow = rownumber;
                    }
                 

                }
                int lastRowForFormula = lastRow + 4;
                _excelWriter.Write("Total Quantity", 1, lastRowForFormula);
                _excelWriter.WriteFormula(FormulaType.Sum, 2, lastRowForFormula, 3, 2, lastRow);
                _excelWriter.Write("Total Inventory", 1, ++lastRowForFormula);
                _excelWriter.WriteFormula(FormulaType.Sum, 2, lastRowForFormula,5,2,lastRow);
            }
        }
    }
}
