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
    public partial class ReceiveArDiag : Form
    {
        string invoiceno = "";
        ReceivableTransferModel receivableTransferModel = new ReceivableTransferModel();

        ReceivableTransferController receivableTransferController = new ReceivableTransferController();

        public ReceiveArDiag(string invoiceno)
        {
            InitializeComponent();
            this.invoiceno = invoiceno;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task loadDetails()
        {

            receivableTransferModel = await receivableTransferController.getModelSingle(invoiceno);
            textBox1.Text = receivableTransferModel.checkNo == "" || receivableTransferModel.checkNo == null ?
                                "" : receivableTransferModel.checkNo;
            dateTimePicker1.Value = receivableTransferModel.checkDate == "" || receivableTransferModel.checkDate == null ?
                 DateTime.Now : DateTime.Parse(receivableTransferModel.checkDate.Split(' ')[1]);
            textBox3.Text = receivableTransferModel.amountPaid.ToString();

            checkBox1.Checked = receivableTransferModel.amount == receivableTransferModel.amountPaid ? true : false;
            textBox3.Enabled = receivableTransferModel.amount == receivableTransferModel.amountPaid ? false : true;

            bool isCheck = false;
            if(receivableTransferModel.checkNo != "" )
            {
                isCheck = true;
            }

            radioButton2.Checked = isCheck;

            radioButton1.Checked = !isCheck;

            enabledCheck(isCheck);
        }

        private async void ReceiveArDiag_Load(object sender, EventArgs e)
        {
            await loadDetails();

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.Text = $"{receivableTransferModel.amount}";
                textBox3.Enabled = false;
            }
            else
            {
                textBox3.Text = "";
                textBox3.Enabled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            enabledCheck(true);
        }

        private void enabledCheck(bool enable)
        {
            textBox1.Enabled = enable;
            dateTimePicker1.Enabled = enable;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            enabledCheck(false);
        }

        private async void btnQty_Click(object sender, EventArgs e)
        {
            float _;
            if (textBox3.Text.Trim() == "")
                return;

            if (!(float.TryParse(textBox3.Text.Trim(), out _)))
                return;

            if (radioButton2.Checked)
            {
                if (textBox1.Text.Trim() == "")
                    return;
            }
            string checkNo = radioButton2.Checked ? textBox1.Text.Trim() : "";

            string checkDate = radioButton2.Checked ? dateTimePicker1.Value.ToString("yyyy-MM-dd") : "";

            float totalAmountPaid = float.Parse(textBox3.Text.Trim());


            await receivableTransferController.updateData(receivableTransferModel.amount, totalAmountPaid,
                checkNo, checkDate, invoiceno);

            MessageBox.Show("Sucessfully Update Data");
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            float _;
            if (textBox3.Text.Trim() == "")
                return;

            if (!(float.TryParse(textBox3.Text.Trim(), out _)))
                return;

            float totalAmountPaid = float.Parse(textBox3.Text.Trim());

            if(totalAmountPaid>receivableTransferModel.amount)
            {
                textBox3.Text = "0";
                checkBox1.Checked = false;
                MessageBox.Show("Total Amount paid is over the total balance");
            }
        }
    }
}
