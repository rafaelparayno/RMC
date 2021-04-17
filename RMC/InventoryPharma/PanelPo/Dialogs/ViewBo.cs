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

namespace RMC.InventoryPharma.PanelPo.Dialogs
{
    public partial class ViewBo : Form
    {
        PoController poController = new PoController();
        PoItemController poItemController = new PoItemController();
        BackOrderController backOrderController = new BackOrderController();
        List<string> Po = new List<string>();
        int po_no = 0;
        public ViewBo()
        {
            InitializeComponent();
        }

        private async void loadPoItems(int pono)
        {
            List<PoModel> pomodels = new List<PoModel>();
            pomodels = await poItemController.getPoNo(pono);

            dgInPo.DataSource = pomodels;
            dgInPo.AutoResizeColumns();
        }
        private async void loadBo()
        {
            Po = await backOrderController.getBoActive();
            listBox1.Items.AddRange(Po.ToArray());
        }


        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewBo_Load(object sender, EventArgs e)
        {
            loadBo();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _;


            if (listBox1.Items.Count == 0)
                return;

            if (listBox1.SelectedItem == null)
                return;

            if (!(int.TryParse(listBox1.SelectedItem.ToString().Split(' ')[1], out _)))
                return;

            po_no = int.Parse(listBox1.SelectedItem.ToString().Split(' ')[1]);
            loadPoItems(po_no);
           
        }
    }
}
