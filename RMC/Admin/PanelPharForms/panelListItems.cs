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
    public partial class panelListItems : Form
    {
        ItemController items = new ItemController();
        public panelListItems()
        {
            InitializeComponent();
        }

        private void panelListItems_Load(object sender, EventArgs e)
        {
          // loadGrid();
        }

      /*  private async void loadGrid()
        {
            DataSet ds = await items.getDsActive();
            RefreshGrid(ds);
        }

        private void RefreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();


        }*/
    }
}
