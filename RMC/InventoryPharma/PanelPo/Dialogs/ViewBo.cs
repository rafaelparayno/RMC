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
        ItemController itemz = new ItemController();
      
        PoItemController poItemController = new PoItemController();
        BackOrderController backOrderController = new BackOrderController();
        List<string> Po = new List<string>();
        string idRightClick = "";
        int po_no = 0;
        private int supid = 0;
        public int qtyAdd = 0;
        public int itemIdClickAdd;
        public ViewBo(int supid)
        {
            InitializeComponent();
            this.supid = supid;
            itemIdClickAdd = 0;
            qtyAdd = 0;
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
            itemIdClickAdd = 0;
            qtyAdd = 0;
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

        private void dgInPo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgInPo.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    
                    idRightClick = dgInPo.Rows[currentMouseOverRow].Cells[0].Value.ToString();

                  

                    contextMenuStrip1.Show(dgInPo, new Point(e.X, e.Y));
                }

            }
        }

        private async void addToPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int id = int.Parse(idRightClick);
            DataSet ds = await itemz.getDataWithSupplierIdTotalStocks(supid);


            if (hasItemId(ds, id))
            {
                itemIdClickAdd = id;
                this.Close();
            }
            else
                MessageBox.Show("The Supplier doesn't Have This Item", "validation", MessageBoxButtons.OK, MessageBoxIcon.Error);




        }

        private bool hasItemId(DataSet ds,int id)
        {
            bool hasItemID = false;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int idInRow = int.Parse(dr[0].ToString());

                if (id == idInRow)
                {
                    hasItemID = true;
                    break;
                }
            }

            return hasItemID;
        }
    }
}
