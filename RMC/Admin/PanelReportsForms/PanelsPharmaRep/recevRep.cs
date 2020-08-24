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
    public partial class recevRep : Form
    {
        ReceiveControllers recC = new ReceiveControllers();
        public recevRep()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            DataSet ds = await recC.getData();
            refreshGrid(ds);

        }

        private async void searchDate(string date)
        {
            DataSet ds = await recC.getData(date);
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
            searchDate(dateTimePicker1.Value.ToString("yyyy/MM/dd"));
        }
    }
}
