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
            openChildForm(new PanelRecPo());
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
    }
}
