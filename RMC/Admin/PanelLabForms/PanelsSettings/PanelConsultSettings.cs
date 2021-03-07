using RMC.Database.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelLabForms.PanelsSettings
{
    public partial class PanelConsultSettings : Form
    {
        float priceMedCert = 0;
        float priceConsult = 0;
        float priceConsultS = 0;

        PricesServiceController serviceController = new PricesServiceController();
        public PanelConsultSettings()
        {
            InitializeComponent();
            setInitPrice();
        }

        private void txtSellingPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void setInitPrice()
        {
            priceMedCert = await serviceController.getPrice("MedCert");
            priceConsult = await serviceController.getPrice("Consulation");
            priceConsultS = await serviceController.getPrice("SConsultation");
            txtSellingPrice.Text = priceConsult.ToString();
            textBox1.Text = priceMedCert.ToString();
            textBox2.Text = priceConsultS.ToString();
        }

        private bool isValid()
        {
            bool isValid = true;

            errorProvider1.Clear();

            float _;

            isValid = (txtSellingPrice.Text.Trim() != "") && isValid;
            setError(ref txtSellingPrice, "Please Input Data");

            isValid = (textBox1.Text.Trim() != "") && isValid;
            setError(ref textBox1, "Please Input Data");

            isValid = (textBox2.Text.Trim() != "") && isValid;
            setError(ref textBox1, "Please Input Data");


            isValid = (float.TryParse(txtSellingPrice.Text.Trim(), out _)) && isValid;
            setNumberFormat(isValid,ref txtSellingPrice, "Please Insert A Correct Format");
            isValid = (float.TryParse(textBox1.Text.Trim(), out _)) && isValid;
            setNumberFormat(isValid, ref textBox1, "Please Insert A Correct Format");

            isValid = (float.TryParse(textBox2.Text.Trim(), out _)) && isValid;
            setNumberFormat(isValid, ref textBox2, "Please Insert A Correct Format");

            return isValid;
        }

        private void setError(ref TextBox tb, string message)
        {
            if(tb.Text.Trim() == "")
            {
                errorProvider1.SetError(tb, message);
            }
        }

        private void setNumberFormat(bool isError,ref TextBox tb, string message)
        {
            if (!isError)
            {
                errorProvider1.SetError(tb, message);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Complete the data to be save");
                return;
            }

            float priceConsulation = float.Parse(txtSellingPrice.Text.Trim());
            float priceMedCert = float.Parse(textBox1.Text.Trim());
            float priceSConsulation = float.Parse(textBox2.Text.Trim());
            serviceController.save(priceConsulation, "Consulation");
            serviceController.save(priceMedCert, "MedCert");
            serviceController.save(priceSConsulation, "SConsultation");

            MessageBox.Show("Succesfully Save Data");
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
