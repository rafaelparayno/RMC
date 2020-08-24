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

namespace RMC.Admin.PanelReportsForms.PanelsPharmaRep
{
    public partial class ExpRep : Form
    {
        ItemController itemz = new ItemController();
        public ExpRep()
        {
            InitializeComponent();
            loadGrid();
        }

        private async void loadGrid()
        {
            DataSet ds = await itemz.getdatasetActiveExpiration();
            refreshGrid(ds);
        }

        private async void searchDate(int d)
        {
            DataSet ds = await itemz.getdatasetActiveExpirationWithDate(d);
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            int days = int.Parse(numericUpDown1.Value.ToString());

            if(days == 0)
            {
                loadGrid();
            }
            else
            {
                searchDate(days);
            }
        }
    }
}
