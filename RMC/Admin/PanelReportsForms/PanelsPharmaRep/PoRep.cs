using RMC.Database.Controllers;
using RMC.Database.Models;
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
    public partial class PoRep : Form
    {
        PoController poc = new PoController();
        PoItemController poic = new PoItemController();
        public PoRep()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private async void loadGrid()
        {
            DataSet ds = await poc.getDsPo();
            refreshGrid(ds);
        }
        

        private async void loadItemGrid(int pono)
        {
            List<PoModel> plist = await poic.getPoNo(pono);

            refreshInItems(plist);
        }


        private async void searchGrid(string date)
        {
            DataSet ds = await poc.getDsPo(date);
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgItemList.DataSource = "";
            dgItemList.DataSource = ds.Tables[0];
            dgItemList.AutoResizeColumns();
        }

        private void refreshInItems(List<PoModel> plist)
        {
            dataGridView1.DataSource = "";
            dataGridView1.DataSource = plist;
            dataGridView1.AutoResizeColumns();
        }


        private void iconButton3_Click(object sender, EventArgs e)
        {
            loadGrid();
        }


        private void iconButton1_Click(object sender, EventArgs e)
        {
            searchGrid(dateTimePicker1.Value.ToString("yyyy/MM/dd"));
        }

        private void dgItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgItemList.Rows.Count == 0)
                return;
            if (dgItemList.SelectedRows.Count == 0)
                return;

            int poid = int.Parse(dgItemList.SelectedRows[0].Cells[0].Value.ToString());
            loadItemGrid(poid);
        }
    }
}
