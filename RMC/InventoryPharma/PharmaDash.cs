using RMC.InventoryPharma.Barcodes;
using RMC.InventoryPharma.HistoryStocks;
using RMC.InventoryPharma.PanelPo;
using RMC.InventoryPharma.PanelReturn;
using RMC.InventoryPharma.PanelRo;
using RMC.InventoryPharma.PanelViewStocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma
{
    public partial class PharmaDash : Form
    {
        private Form activeForm = null;
        int countTimer = 0;
        public PharmaDash()
        {
            InitializeComponent();
        }


        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();


            childForm.Show();
        }

        private void btnViewStocks_Click(object sender, EventArgs e)
        {
            openChildForm(new ViewStocks());
        }

        private void btnRec_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelNewRec());
        }

        private void btnPos_Click_1(object sender, EventArgs e)
        {

            /*openChildForm(new POS());*/
        }

        private void btnPo_Click_1(object sender, EventArgs e)
        {
            openChildForm(new PanelPurchase());
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelR());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            countTimer = 0;
            timer1.Start();
            disableButtons();
            openChildForm(new PrintBarcodes());
           
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            openChildForm(new PanelReturnShop());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            countTimer++;
            if (countTimer == 5)
            {
               
                timer1.Stop();
                enabledButtons();
            }
        }

        private void disableButtons()
        {
            btnPo.Enabled = false;

            btnRec.Enabled = false;

            btnReturn.Enabled = false;
            btnViewStocks.Enabled = false;
            iconButton1.Enabled = false;
            iconButton2.Enabled = false;
            iconButton3.Enabled = false;
            iconButton4.Enabled = false;
            iconButton5.Enabled = false;
        }

        private void enabledButtons()
        {
            btnPo.Enabled = true;

            btnRec.Enabled = true;

            btnReturn.Enabled = true;
            btnViewStocks.Enabled = true;
            iconButton1.Enabled = true;
            iconButton2.Enabled = true;
            iconButton3.Enabled = true;
            iconButton4.Enabled = true;
            iconButton5.Enabled = true;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            openChildForm(new HistoryStocksForms());
        }
    }
}
