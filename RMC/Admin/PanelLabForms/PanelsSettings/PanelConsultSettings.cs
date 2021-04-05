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
      /*  float priceMedCert = 0;
        float priceConsult = 0;
        float priceConsultS = 0;
        float priceConsultF = 0;
        float pricePreEmployment = 0;*/

        PricesServiceController serviceController = new PricesServiceController();
        public PanelConsultSettings()
        {
            InitializeComponent();
         /*   setInitPrice();*/
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
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

        private async Task setInitPrice()
        {
           // List<Task> listTask = new List<Task>();
            Task<float> task1 = serviceController.getPrice("MedCert");
            Task<float> task2 = serviceController.getPrice("Consulation");
            Task<float> task3 = serviceController.getPrice("SConsultation");
            Task<float> task4 = serviceController.getPrice("priceConsultF");
            Task<float> task5 = serviceController.getPrice("PreEmployment");
            List<Task> listTask = new List<Task>() { task1,task2,task3,task4,task5};

            await Task.WhenAll(listTask);
            //listTask.Add()
/*            priceMedCert = ;
            priceConsult = await serviceController.getPrice("Consulation");
            priceConsultS = await serviceController.getPrice("SConsultation");
            priceConsultF = await serviceController.getPrice("priceConsultF");
            pricePreEmployment = await serviceController.getPrice("PreEmployment");*/


            txtSellingPrice.Text = task2.Result.ToString();
            textBox1.Text = task1.Result.ToString();
            textBox2.Text = task3.Result.ToString();
            textBox3.Text = task4.Result.ToString();
            textBox4.Text = task5.Result.ToString();
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
            setError(ref textBox2, "Please Input Data");

            isValid = (textBox3.Text.Trim() != "") && isValid;
            setError(ref textBox3, "Please Input Data");

            isValid = (textBox4.Text.Trim() != "") && isValid;
            setError(ref textBox4, "Please Input Data");


            isValid = (float.TryParse(txtSellingPrice.Text.Trim(), out _)) && isValid;
            setNumberFormat(isValid,ref txtSellingPrice, "Please Insert A Correct Format");
            isValid = (float.TryParse(textBox1.Text.Trim(), out _)) && isValid;
            setNumberFormat(isValid, ref textBox1, "Please Insert A Correct Format");
            isValid = (float.TryParse(textBox2.Text.Trim(), out _)) && isValid;
            setNumberFormat(isValid, ref textBox2, "Please Insert A Correct Format");
            isValid = (float.TryParse(textBox3.Text.Trim(), out _)) && isValid;
            setNumberFormat(isValid, ref textBox3, "Please Insert A Correct Format");
            isValid = (float.TryParse(textBox4.Text.Trim(), out _)) && isValid;
            setNumberFormat(isValid, ref textBox4, "Please Insert A Correct Format");

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

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Complete the data to be save");
                return;
            }
            List<Task> listTask = new List<Task>();
            float priceConsulation = float.Parse(txtSellingPrice.Text.Trim());
            float priceMedCert = float.Parse(textBox1.Text.Trim());
            float priceSConsulation = float.Parse(textBox2.Text.Trim());
            float priceFConsulation = float.Parse(textBox3.Text.Trim());
            float pricePreEmployment = float.Parse(textBox4.Text.Trim());
            listTask.Add(serviceController.save(priceConsulation, "Consulation"));
            listTask.Add(serviceController.save(priceMedCert, "MedCert"));
            listTask.Add(serviceController.save(priceSConsulation, "SConsultation"));
            listTask.Add(serviceController.save(priceFConsulation, "priceConsultF"));
            listTask.Add(serviceController.save(pricePreEmployment, "PreEmployment"));

            await Task.WhenAll(listTask);
            MessageBox.Show("Succesfully Save Data");
        }

        private async void PanelConsultSettings_Load(object sender, EventArgs e)
        {
            await setInitPrice();
        }
    }
}
