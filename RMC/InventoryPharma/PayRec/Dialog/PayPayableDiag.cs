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

namespace RMC.InventoryPharma.PayRec.Dialog
{
    public partial class PayPayableDiag : Form
    {

   
        PayablesController payablesController = new PayablesController();
        private string invoice = "";
        PayableModel p = new PayableModel();

        public PayPayableDiag(string invoice)
        {
            InitializeComponent();
            this.invoice = invoice;
        }

        private async Task loadDetails()
        {
            p = await payablesController.getModel(invoice);

            radioButton1.Checked = p.checkNo == "" ? true : false;
            radioButton2.Checked = p.checkNo == "" ? false : true;
            textBox1.Text = p.checkNo == "" || p.checkNo == null ? "" : p.checkNo;

            dateTimePicker1.Value = p.checkDate == "" || p.checkDate == null ?
               DateTime.Now : DateTime.Parse(p.checkDate.Split(' ')[1]);

        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void PayPayableDiag_Load(object sender, EventArgs e)
        {
            await loadDetails();
        }


        private void enabledCheck(bool enable)
        {
            textBox1.Enabled = enable;
            dateTimePicker1.Enabled = enable;
        }

        private async void btnQty_Click(object sender, EventArgs e)
        {

            if(radioButton2.Checked)
            {
                if (textBox1.Text.Trim() == "")
                    return;
            }


            string checkNo =
                radioButton1.Checked ?
                        "" : textBox1.Text.Trim();
            string checkDate = radioButton1.Checked ? "" : dateTimePicker1.Value.ToString("yyyy-MM-dd");



            await payablesController.UpdatePaid(invoice, 1, checkNo, checkDate);

            MessageBox.Show("Sucessfully Update Data");
            this.Close();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            enabledCheck(true);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            enabledCheck(false);
        }

       
    }
}
