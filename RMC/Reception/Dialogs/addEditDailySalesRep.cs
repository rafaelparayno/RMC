﻿using RMC.Database.Controllers;
using RMC.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RMC.Reception.Dialogs
{
    public partial class addEditDailySalesRep : Form
    {
        private bool isAddAnother = false;
        private bool isAddAnother2 = false;

        DailySalesReportController reportController = new DailySalesReportController();

        public addEditDailySalesRep()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Today;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private string GenerateRandomChars(int length)
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }

        private async Task saveData()
        {
            string pathDir = CreateDirectory.CreateDir("DailySales");
            string fullPath = pathDir + "Sales-" + GenerateRandomChars(5) +"-"
                        + dateTimePicker1.Value.ToString("yyyy-MM-dd")  + ".xml";


            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            XmlWriter xwriter = XmlWriter.Create(fullPath,xmlWriterSettings);

            xwriter.WriteStartElement("Sales");
            xwriter.WriteElementString("dateParam", DateTime.Now.ToString("MMMM dd, yyyy"));
            xwriter.WriteElementString("xrayExpense", txtXrayReading.Text.Trim());
            xwriter.WriteElementString("laboutsExpense", txtLabSendout.Text.Trim());
            xwriter.WriteElementString("outByParam", txtOutBy.Text.Trim());
            xwriter.WriteElementString("arParam", txtAr.Text.Trim());
            xwriter.WriteElementString("dpfParam", txtDpf.Text.Trim());

            xwriter.WriteElementString("expenseOtherName1", txtName1.Text.Trim());
            xwriter.WriteElementString("fareExpense", txtExpenseCost1.Text.Trim());

            if (isAddAnother)
            {
                xwriter.WriteElementString("expenseOtherName2", txtName2.Text.Trim());
                xwriter.WriteElementString("bigasExpnse", txtExpenseCost2.Text.Trim());
            }

            if (isAddAnother2)
            {
                xwriter.WriteElementString("expenseOtherName3", txtName3.Text.Trim());
                xwriter.WriteElementString("snackExpense", txtExpenseCost3.Text.Trim());
            }

            xwriter.WriteElementString("ThouQtyParam", num1k.Value.ToString());
            xwriter.WriteElementString("FiveHQtyParam", num5h.Value.ToString());
            xwriter.WriteElementString("TwoHQtyParam", num2h.Value.ToString());
            xwriter.WriteElementString("OneHQtyParam", num1h.Value.ToString());
            xwriter.WriteElementString("fiftyQtyParam", numfifty.Value.ToString());
            xwriter.WriteElementString("twentyQtyParam", numtwen.Value.ToString());
            xwriter.WriteElementString("coinsTotal", numcoins.Value.ToString());

            xwriter.WriteEndElement();
            xwriter.Flush();
            xwriter.Close();
            MessageBox.Show("Succesfully Save Data");
            
            await reportController.save(0, fullPath,dateTimePicker1.Value.ToString("yyyy-MM-dd"));

            this.Close();
        }



        public bool isValid()
        {
            bool isValid = true;

            isValid = !(txtXrayReading.Text.Trim() == "") && isValid;
            isValid = float.TryParse(txtXrayReading.Text.Trim(), out _ ) && isValid;

            isValid = !(txtLabSendout.Text.Trim() == "") && isValid;
            isValid = float.TryParse(txtLabSendout.Text.Trim(), out _) && isValid;
            isValid = !(txtDpf.Text.Trim() == "") && isValid;
            isValid = float.TryParse(txtDpf.Text.Trim(), out _) && isValid;
            isValid = !(txtOutBy.Text.Trim() == "") && isValid;
            isValid = float.TryParse(txtOutBy.Text.Trim(), out _) && isValid;
            isValid = !(txtAr.Text.Trim() == "") && isValid;
            isValid = float.TryParse(txtAr.Text.Trim(), out _) && isValid;
            isValid = !(txtAr.Text.Trim() == "") && isValid;
            isValid = float.TryParse(txtXrayReading.Text.Trim(), out _) && isValid;

            isValid = !(string.IsNullOrEmpty(txtName1.Text.Trim())) && isValid;
            isValid = float.TryParse(txtExpenseCost1.Text.Trim(), out _) && isValid;

            if (isAddAnother)
            {
                isValid = !(string.IsNullOrEmpty(txtName2.Text.Trim())) && isValid;
                isValid = float.TryParse(txtExpenseCost2.Text.Trim(), out _) && isValid;
            }

            if (isAddAnother2)
            {
                isValid = !(string.IsNullOrEmpty(txtName3.Text.Trim())) && isValid;
                isValid = float.TryParse(txtExpenseCost3.Text.Trim(), out _) && isValid;
            }

            return isValid;
        }

        private void txtXrayReading_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtLabSendout_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtOutBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAr_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtExpenseCost1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtExpenseCost2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtExpenseCost3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            isAddAnother = !isAddAnother;

            iconButton1.IconChar = isAddAnother ? 
            FontAwesome.Sharp.IconChar.MinusCircle : FontAwesome.Sharp.IconChar.PlusCircle;

            
            panel6.Visible = isAddAnother;
            isAddAnother2 = false;
            panel7.Visible = false;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            txtName2.Text = "";
            txtExpenseCost2.Text = "";
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            isAddAnother2 = !isAddAnother2;

            iconButton2.IconChar = isAddAnother2 ?
            FontAwesome.Sharp.IconChar.MinusCircle : FontAwesome.Sharp.IconChar.PlusCircle;


            panel7.Visible = isAddAnother2;
            txtName3.Text = "";
            txtExpenseCost3.Text = "";
        }

        private async void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!isValid())
            {
                tabControl1.SelectedTab = tabPage1;
                MessageBox.Show("Please Fill The Data Below", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (await reportController.isFoundDate(dateTimePicker1.Value.ToString("yyyy-MM-dd")))
            {
                MessageBox.Show("Already Has a Date in Database", "Validation",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await saveData();

        }
    }
}
