using RMC.Admin.PanelPharForms.Dialogs;
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

namespace RMC.Admin.PanelPharForms
{
    public partial class ItemList : Form
    {
     
        ItemController itemz = new ItemController();
        public ItemList()
        {
            InitializeComponent();
            
        }

        private void ItemList_Load(object sender, EventArgs e)
        {
            //loadGrid();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            
        }


        private void btnAddItem_Click_1(object sender, EventArgs e)
        {
            addEditItems frm = new addEditItems();
            frm.ShowDialog();
            loadGrid();
        }


        private async void loadGrid()
        {
            DataSet ds = await itemz.getDsActive();
            RefreshGrid(ds);
        }

        private void RefreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();


        }
    }
}
