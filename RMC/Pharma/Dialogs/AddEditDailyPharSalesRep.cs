using RMC.Database.Controllers;
using RMC.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RMC.Pharma.Dialogs
{
    public partial class AddEditDailyPharSalesRep : Form
    {

        DailySalesReportController reportController = new DailySalesReportController();


        private int repid = 0;
        private bool isAddAnother = false;
        private bool isAddAnother2 = false;
        private bool isAddAnother3 = false;
        private string nameExp2 = "";
        private string nameExp3 = "";
        private string nameExp4 = "";
        private string editExp2 = "";
        private string editExp3 = "";
        private string editExp4 = "";

        public AddEditDailyPharSalesRep()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Today;
        }

        public AddEditDailyPharSalesRep(int repid)
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Today;
            this.repid = repid;
        }


        public bool isValid()
        {
            bool isValid = true;

            isValid = !(txtOutBy.Text.Trim() == "") && isValid;
            isValid = float.TryParse(txtOutBy.Text.Trim(), out _) && isValid;
            isValid = !(txtAr.Text.Trim() == "") && isValid;
            isValid = float.TryParse(txtAr.Text.Trim(), out _) && isValid;

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


        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            isAddAnother = !isAddAnother;

            iconButton1.IconChar = isAddAnother ?
            FontAwesome.Sharp.IconChar.MinusCircle : FontAwesome.Sharp.IconChar.PlusCircle;


            panel6.Visible = isAddAnother;
            isAddAnother2 = false;
            isAddAnother3 = false;
            panel7.Visible = false;
            panel3.Visible = false;
       
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            txtName2.Text = nameExp2;
            txtExpenseCost2.Text = editExp2;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            isAddAnother2 = !isAddAnother2;

            iconButton2.IconChar = isAddAnother2 ?
            FontAwesome.Sharp.IconChar.MinusCircle : FontAwesome.Sharp.IconChar.PlusCircle;

            panel3.Visible = false;
            isAddAnother3 = false;
            iconButton3.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            panel7.Visible = isAddAnother2;
            txtName3.Text = nameExp3;
            txtExpenseCost3.Text = editExp3;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            isAddAnother3 = !isAddAnother3;

            iconButton3.IconChar = isAddAnother3 ?
            FontAwesome.Sharp.IconChar.MinusCircle : FontAwesome.Sharp.IconChar.PlusCircle;


            panel3.Visible = isAddAnother3;
            txtName4.Text = nameExp4;
            txtExpenseCost4.Text = editExp4;
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

        private void txtExpenseCost4_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                tabControl1.SelectedTab = tabPage1;
                MessageBox.Show("Please Fill The Data Below", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (repid == 0)
            {
                if (await reportController.isFoundDate(dateTimePicker1.Value.ToString("yyyy-MM-dd"), 0))
                {
                    MessageBox.Show("Already Has a Date in Database", "Validation",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await saveData();
            }
            else
            {

               await updateData();
            }

        }

        private async Task updateData()
        {

            string fullPath = await reportController.getFullPath(repid);

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            XmlWriter xwriter = XmlWriter.Create(fullPath, xmlWriterSettings);

            xwriter.WriteStartElement("Sales");
            xwriter.WriteElementString("dateParam", dateTimePicker1.Value.ToString("MMMM dd, yyyy"));
           
            xwriter.WriteElementString("outByParam", txtOutBy.Text.Trim());
            xwriter.WriteElementString("arParam", txtAr.Text.Trim());
      

            xwriter.WriteElementString("expenseOtherName1", txtName1.Text.Trim());
            xwriter.WriteElementString("expenseCost1", txtExpenseCost1.Text.Trim());

            if (isAddAnother)
            {
                xwriter.WriteElementString("expenseOtherName2", txtName2.Text.Trim());
                xwriter.WriteElementString("expenseCost2", txtExpenseCost2.Text.Trim());
            }

            if (isAddAnother2)
            {
                xwriter.WriteElementString("expenseOtherName3", txtName3.Text.Trim());
                xwriter.WriteElementString("expenseCost3", txtExpenseCost3.Text.Trim());
            }

            if (isAddAnother3)
            {
                xwriter.WriteElementString("expenseOtherName4", txtName4.Text.Trim());
                xwriter.WriteElementString("expenseCost4", txtExpenseCost4.Text.Trim());
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
            string fullPath = pathDir + "Sales-" + GenerateRandomChars(5) + "-"
                        + dateTimePicker1.Value.ToString("yyyy-MM-dd") + ".xml";


            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            XmlWriter xwriter = XmlWriter.Create(fullPath, xmlWriterSettings);

            xwriter.WriteStartElement("Sales");
            xwriter.WriteElementString("dateParam", dateTimePicker1.Value.ToString("MMMM dd, yyyy"));
            xwriter.WriteElementString("outByParam", string.IsNullOrEmpty(txtOutBy.Text.Trim()) ? 0 + "" 
                : txtOutBy.Text.Trim());
            xwriter.WriteElementString("arParam", string.IsNullOrEmpty(txtAr.Text.Trim()) ? 
                0 + "" : txtAr.Text.Trim());

            xwriter.WriteElementString("expenseOtherName1", txtName1.Text.Trim());
            xwriter.WriteElementString("expenseCost1", txtExpenseCost1.Text.Trim());

            if (isAddAnother)
            {
                xwriter.WriteElementString("expenseOtherName2", txtName2.Text.Trim());
                xwriter.WriteElementString("expenseCost2", txtExpenseCost2.Text.Trim());
            }

            if (isAddAnother2)
            {
                xwriter.WriteElementString("expenseOtherName3", txtName3.Text.Trim());
                xwriter.WriteElementString("expenseCost3", txtExpenseCost3.Text.Trim());
            }

            if (isAddAnother3)
            {
                xwriter.WriteElementString("expenseOtherName4", txtName4.Text.Trim());
                xwriter.WriteElementString("expenseCost4", txtExpenseCost4.Text.Trim());
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

            await reportController.save(1, fullPath, dateTimePicker1.Value.ToString("yyyy-MM-dd"));

            this.Close();
        }

        private async Task loadXml()
        {

            XmlDocument doc = new XmlDocument();

            string path = await reportController.getFullPath(repid);

            if (!File.Exists(path))
                return;


            doc.Load(path);


            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {

              
                if (node.Name == "outByParam")
                    txtOutBy.Text = node.InnerText;
                if (node.Name == "arParam")
                    txtAr.Text = node.InnerText;
                if (node.Name == "expenseOtherName1")
                    txtName1.Text = node.InnerText;
              

                if (node.Name == "expenseOtherName2")
                {
                    txtName2.Text = node.InnerText;
                    nameExp2 = node.InnerText;

                    isAddAnother = true;
                    panel6.Visible = true;
                    isAddAnother = true;
                    iconButton1.IconChar = FontAwesome.Sharp.IconChar.MinusCircle;
                }



                if (node.Name == "expenseOtherName3")
                {
                    txtName3.Text = node.InnerText;
                    nameExp3 = node.InnerText;
                    isAddAnother = true;
                    panel7.Visible = true;
                    isAddAnother2 = true;
                    iconButton2.IconChar = FontAwesome.Sharp.IconChar.MinusCircle;
                }


                if (node.Name == "expenseOtherName4")
                {
                    txtName4.Text = node.InnerText;
                    nameExp4 = node.InnerText;
                    isAddAnother = true;
                    isAddAnother3 = true;
                    panel3.Visible = true; 
                    panel7.Visible = true;
                    isAddAnother2 = true;
                    iconButton3.IconChar = FontAwesome.Sharp.IconChar.MinusCircle;
                }


                if(node.Name == "expenseCost1")
                    txtExpenseCost1.Text = node.InnerText;

                if (node.Name == "expenseCost2")
                    txtExpenseCost2.Text = node.InnerText;
                
                if (node.Name == "expenseCost3")
                    txtExpenseCost3.Text = node.InnerText;
                
                if (node.Name == "expenseCost4")
                    txtExpenseCost4.Text = node.InnerText;
                


                if (node.Name == "ThouQtyParam")
                    num1k.Value = int.Parse(node.InnerText);
                if (node.Name == "FiveHQtyParam")
                    num5h.Value = int.Parse(node.InnerText);
                if (node.Name == "TwoHQtyParam")
                    num2h.Value = int.Parse(node.InnerText);
                if (node.Name == "OneHQtyParam")
                    num1h.Value = int.Parse(node.InnerText);
                if (node.Name == "fiftyQtyParam")
                    numfifty.Value = int.Parse(node.InnerText);
                if (node.Name == "twentyQtyParam")
                    numtwen.Value = int.Parse(node.InnerText);
                if (node.Name == "coinsTotal")
                    numcoins.Value = int.Parse(node.InnerText);

                if (node.Name == "dateParam")
                {
                    dateTimePicker1.Value = DateTime.Parse(node.InnerText);
                    dateTimePicker1.Enabled = false;
                }

            }
        }


        private async void AddEditDailyPharSalesRep_Load(object sender, EventArgs e)
        {
            if (repid > 0)
             await loadXml();
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
    }
}
