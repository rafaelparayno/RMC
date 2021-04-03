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

namespace RMC.Doctor.PanelDoctor.Diag
{
    public partial class dStocks : Form
    {

        ItemController itemController = new ItemController();
        public string itemName = "";
        public string isBranded = "";
        public dStocks()
        {
            InitializeComponent();
            itemName = "";
            loadData();
        }

        private async void loadData()
        {
            DataSet ds = await itemController.getMedicinesActives();
            refreshGrid(ds);
        }

        private async void searchData(string search)
        {
            DataSet ds = await itemController.getMedicinesActives(search);
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                loadData();
            }
            else
            {
                searchData(txtName.Text.Trim());
            }
        }

        private void dgItemList_MouseClick(object sender, MouseEventArgs e)
        {
           

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
            itemName = dgItemList.SelectedRows[0].Cells[1].Value.ToString();
            isBranded = dgItemList.SelectedRows[0].Cells[6].Value.ToString();
            this.Close();
        }
    }
}
