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

namespace RMC.InventoryPharma.HistoryStocks.Dialogs
{
    public partial class ViewItemsInPo : Form
    {
        PoItemController poic = new PoItemController();
        private int pono = 0;
        public ViewItemsInPo(int pono)
        {
            InitializeComponent();
            this.pono = pono;
        }

        private async Task loadItemGrid()
        {
            List<PoModel> plist = await poic.getPoNoWithOrigStocks(pono);

            dataGridView1.DataSource = "";
            dataGridView1.DataSource = plist;
            dataGridView1.AutoResizeColumns();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void ViewItemsInPo_Load(object sender, EventArgs e)
        {
            await loadItemGrid();
        }
    }
}
