using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.PanelPo.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelViewStocks.Dialog
{
    public partial class TransferOtherForm : Form
    {
        private int id = 0;
        private string name = "";
        private bool isPharma;
        private int quantityStocks = 0;
        private int editId = 0;
        private int cbTransfId = 0;
        private int startQty = 0;
        PlacesTransferController placesTransferController = new PlacesTransferController();
        PharmaStocksController pharmaStocksController = new PharmaStocksController();
        ClinicStocksController clinicStocksController = new ClinicStocksController();
        TransferLogsController transferLogs = new TransferLogsController();
        TransferLogsModel transferLogsModel = new TransferLogsModel();


        public TransferOtherForm(int editId)
        {
            InitializeComponent();
            this.editId = editId;
        
        }

        public TransferOtherForm(int id, string name, bool isPharma)
        {
            InitializeComponent();
            this.id = id;
            this.name = name;
            this.isPharma = isPharma;
            txtName.Text = name;
           
          
        }

        private async Task getStocks()
        {
            if (isPharma)
            {
                quantityStocks = await pharmaStocksController.getStocks(id);

            }
            else
            {
                quantityStocks = await clinicStocksController.getStocks(id);
            }
            textBox1.Text = quantityStocks + "";
            
            numericUpDown2.Maximum = editId == 0 ?  
                quantityStocks : startQty + quantityStocks;
          
        }

        private void setLabels(string placeName)
        {
           
            string txt2;

            if (isPharma)
            {
              
                txt2 = $"Transfer to {placeName} quantity";

            }
            else
            {
              
                txt2 = $"Pharma to {placeName} quantity";

            }
            label3.Text = txt2;
            label3.Visible = true;
           
        }

        private async Task loadCbs()
        {
            List<ComboBoxItem> cbs = await placesTransferController.getComboDatas();

            comboBox1.Items.AddRange(cbs.ToArray());


        }


        private async void TransferOtherForm_Load(object sender, EventArgs e)
        {
            await loadCbs();

            if (editId > 0)
            {
                transferLogsModel = await transferLogs.getModel(editId);
                isPharma = transferLogsModel.fromTo == 0 ? true : false;
                id = transferLogsModel.itemid;
                txtName.Text = transferLogsModel.itemName;
                startQty = transferLogsModel.qtyTransfer;
                numericUpDown2.Value = transferLogsModel.qtyTransfer;
                cbTransfId = transferLogsModel.transferid;
                comboBox1.Text = transferLogsModel.transferName;
            }

            string txt;
            if (isPharma)
            {
                 txt = "Pharma Stocks";
             
            }
            else
            {
                  txt = "Clinic Stocks";  
            }

            label1.Text = txt;
            label1.Visible = true;
            await getStocks();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int qty = int.Parse(numericUpDown2.Value.ToString());
          


            if (quantityStocks > 0)
            {
                if(editId == 0)
                {
                    if (qty > quantityStocks)
                        return;
                    int newQty = quantityStocks - qty;

                    textBox1.Text = newQty + "";
                }
                else
                {
                   
                    int addQty = startQty - qty;
                   
                    int newQty =  quantityStocks + addQty;
               
                    
                    textBox1.Text = newQty + "";
                }
          
            }
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTransfId = int.Parse((comboBox1.SelectedItem as ComboBoxItem).Value.ToString());
            string name = (comboBox1.SelectedItem as ComboBoxItem).Text.ToString();
            setLabels(name);
            
            numericUpDown2.Value = startQty;

            await getStocks();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            int fromto;
            int addStocks = int.Parse(numericUpDown2.Value.ToString());

            if (comboBox1.SelectedIndex == -1)
                return;

            if (isPharma)
            {
                await pharmaStocksController.Save(id, int.Parse(textBox1.Text));
                fromto = 0;
                
            }
            else
            {
                await clinicStocksController.Save(id, int.Parse(textBox1.Text));
                fromto = 1;
            }

            if (editId == 0)
                await transferLogs.save(id, addStocks, cbTransfId, UserLog.getUserId(), fromto);
            else
                await transferLogs.update(addStocks, cbTransfId, UserLog.getUserId(), editId);
            MessageBox.Show("Succesfully Transfer");
            this.Close();
        }
    }
}
