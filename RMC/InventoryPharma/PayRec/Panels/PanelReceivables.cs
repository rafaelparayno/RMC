using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PayRec.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PayRec.Panels
{
    public partial class PanelReceivables : Form
    {
        
        ReceivableTransferController receivableTransferController = new ReceivableTransferController();
        PlacesTransferController placesTransferController = new PlacesTransferController();
        string id = "";
        private string ino = "";
        int cbTransfId = 0;

        public PanelReceivables()
        {
            InitializeComponent();
            foreach (string months in StaticData.months)
            {
                comboBox1.Items.Add(months);
            }
        }


        private async Task loadPoCbs()
        {
            List<ComboBoxItem> cbs = await placesTransferController.getComboDatas();

            cbPo.Items.AddRange(cbs.ToArray());
        }

        private async Task loadGrid()
        {
            List<ReceivableTransferModel> receivableTransferModels = 
                await receivableTransferController.getModel();

            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(receivableTransferModels).Tables[0];
            DataGridViewColumn column = dgItemList.Columns[1];
            column.Width = 300;
        }


        private DataSet FormatDg(List<ReceivableTransferModel> receivableTransferModels)
        {


            DataSet newDataset = new DataSet();
            DataTable dt = new DataTable();


            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Customer name", typeof(string));
            dt.Columns.Add("Invoice No", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Amount Paid", typeof(string));
            dt.Columns.Add("Paid", typeof(bool));

            foreach (ReceivableTransferModel p in receivableTransferModels)
            {
                
                dt.Rows.Add(p.id, p.namep, p.invoice,p.dueDate.Split(' ')[0] ,String.Format("₱{0:n}", p.amount),
                    String.Format("₱{0:n}", p.amountPaid), p.isPaid == 1 ? true: false);
            }

            newDataset.Tables.Add(dt);
            return newDataset;

        }

        private async  void PanelReceivables_Load(object sender, EventArgs e)
        {
            await loadPoCbs();
        }

        private void dgItemList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dgItemList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {

                    id = dgItemList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    ino = dgItemList.Rows[currentMouseOverRow].Cells[2].Value.ToString();



                    contextMenuStrip1.Show(dgItemList, new Point(e.X, e.Y));
                }

            }
        }

        private async  void receiveARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiveArDiag frm = new ReceiveArDiag(ino);
            frm.ShowDialog();
            if (checkBox1.Checked)
            {
                await loadGrid();
            }
            else
            {
                await searchGrid();
            }

        }

        private async void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                await loadGrid();
            }
            else
            {
                dgItemList.DataSource = "";
            }
        }

        public async Task searchGrid()
        {
           
            List<ReceivableTransferModel> receivableTransferModels = radioButton2.Checked ?
                await receivableTransferController.getModel(cbTransfId) :
                await receivableTransferController.getModel(cbTransfId,
                comboBox1.SelectedIndex + 1,
                dateTimePicker1.Value.Year);

            dgItemList.DataSource = "";
            dgItemList.DataSource = FormatDg(receivableTransferModels).Tables[0];
            DataGridViewColumn column = dgItemList.Columns[1];
            column.Width = 300;
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (cbTransfId == -1)
                return;

            await searchGrid();
            checkBox1.Checked = false;
        }

        private void cbPo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTransfId = int.Parse((cbPo.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rtid = int.Parse(id);
            DetailsReceivable frm = new DetailsReceivable(rtid);
            frm.ShowDialog();
        }
    }
}
