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
        public int selectedBo = 0;
        public ViewBo(int supid)
        {
            InitializeComponent();
            this.supid = supid;
            itemIdClickAdd = 0;
            qtyAdd = 0;
            selectedBo = 0;
        }

        private async void loadPoItems(int pono)
        {
         
            List<PoModel> pomodels = await poItemController.getPoNo(pono);

            dgInPo.DataSource = pomodels;
            dgInPo.AutoResizeColumns();
        }
        private async void loadBo()
        {
            listBox1.Items.Clear();
            Po = await backOrderController.getBoActive();
            listBox1.Items.AddRange(Po.ToArray());
            dgInPo.DataSource = "";
        }


        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            itemIdClickAdd = 0;
            qtyAdd = 0;
            selectedBo = 0;
            this.Close();
        }

        private void ViewBo_Load(object sender, EventArgs e)
        {
            loadBo();
        }


        private void addToPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int id = int.Parse(idRightClick);
          
            itemIdClickAdd = id;
            this.Close();
       
        }

       
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int _;


            if (listBox1.Items.Count == 0)
                return;

            if (listBox1.SelectedItem == null)
                return;

            if (!(int.TryParse(listBox1.SelectedItem.ToString().Split(' ')[1], out _)))
                return;

            po_no = int.Parse(listBox1.SelectedItem.ToString().Split(' ')[1]);
            selectedBo = po_no;
            loadPoItems(po_no);
        }

        private void dgInPo_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgInPo.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {

                    idRightClick = dgInPo.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    qtyAdd = int.Parse(dgInPo.Rows[currentMouseOverRow].Cells[2].Value.ToString());


                    contextMenuStrip1.Show(dgInPo, new Point(e.X, e.Y));
                }

            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            loadBo();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Po = await backOrderController.getBoActive(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            listBox1.Items.AddRange(Po.ToArray());
            dgInPo.DataSource = "";
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {

                listBox1.SelectedIndex = listBox1.IndexFromPoint(e.Location);
                if (listBox1.SelectedIndex != -1)
                {
                    contextMenuStrip2.Show(listBox1, new Point(e.X, e.Y));
                }
            }
        }

        private async void deleteBackOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            po_no = int.Parse(listBox1.SelectedItem.ToString().Split(' ')[1]);

            DialogResult dialogResult = MessageBox.Show("Are you want to remove this Back Order", "Valid",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(DialogResult.Yes == DialogResult)
            {
                await poItemController.removeBackOrder(po_no);
                MessageBox.Show("Succesfully Remove Backorder");
            }
            
            loadBo();
        }
    }
}
