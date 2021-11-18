using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using RMC.Components;
using RMC.Database.Controllers;
using RMC.InventoryPharma.PanelViewStocks.Dialog;
using SwiftExcel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RMC.InventoryPharma.PanelViewStocks
{
    public partial class ViewStocks : Form
    {
        bool isPharmaList = true;
        PharmaStocksController pharmaStocksController = new PharmaStocksController();

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
            System.Data.DataTable dt = new System.Data.DataTable();
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

            if (path == "")
                return;

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

        private async Task loopExcelObject(string fileName)
        {
            await Task.Run(async() =>
            {
                string fname = fileName;
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fname);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count - 5;
                int colCount = xlRange.Columns.Count;


                for (int i = 1; i <= rowCount; i++)
                {
                    if (i == 1)
                        continue;

                    if(xlRange.Cells[i, 1].Value2.ToString() != "" || xlRange.Cells[i, 1].Value2.ToString() != null)
                    {
                        await saveDataInExcel(xlRange.Cells[i, 1].Value2.ToString(), 
                                    xlRange.Cells[i, 3].Value2.ToString());
                    }

                 
                   
                } 
                GC.Collect();
                GC.WaitForPendingFinalizers();

               
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);
                
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

             
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            });
        }

        private async Task saveDataInExcel(params string[] data)
        {
            int _;

            if (!(int.TryParse(data[0], out _)))
                return;

            if (!(int.TryParse(data[1], out _)))
                return;


            int id = int.Parse(data[0]);
            int stocks = int.Parse(data[1]);
            await pharmaStocksController.Save(id, stocks);
        }

        private async void iconButton5_Click(object sender, EventArgs e)
        {
            VoidForm voidForm = new VoidForm();
            voidForm.ShowDialog();


            if (voidForm.isFound)
            {
                DialogResult dialogResult = MessageBox.Show(@"Are you sure you want to import Data?",
                                                    "Import", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (DialogResult.OK == dialogResult)
                {
                    
                    OpenFileDialog fdlg = new OpenFileDialog();
                    fdlg.Title = "Excel File Dialog";
                    fdlg.InitialDirectory = @"c:\";
                    fdlg.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                    fdlg.FilterIndex = 2;
                    fdlg.RestoreDirectory = true;
                    if (fdlg.ShowDialog() == DialogResult.OK)
                    {
                        panelMenus.Enabled = false;
                        pictureBox1.Show();
                        pictureBox1.Update();
                        await loopExcelObject(fdlg.FileName);
                        pictureBox1.Hide();
                        panelMenus.Enabled = true;
                    }

                }
            }
        }
    }
}
