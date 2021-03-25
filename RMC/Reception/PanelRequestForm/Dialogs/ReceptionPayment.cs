using RMC.Components;
using RMC.Database.Controllers;
using RMC.InventoryPharma.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class ReceptionPayment : Form
    {
        #region DBVARS
        CustomerRequestsController customerRequestsController = new CustomerRequestsController();
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        PackagesController packagesController = new PackagesController();
        LaboratoryController laboratoryController = new LaboratoryController();
        ServiceController serviceController = new ServiceController();
        XrayControllers xrayControllers = new XrayControllers();
        PricesServiceController pricesService = new PricesServiceController();
        InvoiceController invoiceController = new InvoiceController();
        SalesClinicController salesClinicController = new SalesClinicController();
        LabQueueController labQueueController = new LabQueueController();
        RadioQueueController radioQueueController = new RadioQueueController();
        #endregion

        #region VariableState
        List<int> requests = new List<int>();
        private int consultS = 1;
        private int medCert = 2;
        private int labS = 3;
        private int xRayS = 4;
        private int packagesS = 5;
        private int otherS = 7;
        string seniorId = "";
        int customerid = 0;
        int cbValueLab = 0;
        int cbValueService = 0;
        int cbValueXray = 0;
        int cbValuePackages = 0;
        float priceMedCert = 0;
        float priceConsult = 0;
        float priceSConsult = 0;
        float priceFConsult = 0;
        float totalPrice = 0;
        DataTable dt = new DataTable();
        #endregion
         

        public ReceptionPayment(int reqid)
        {
            InitializeComponent();
            initColDg();
            setInitPrice();
            setCustomerId(reqid);
            InitRequests();
            initGroupBoxState();
            loadFromDbtoCb();
            setTotalPrice();
           
        }


        #region OwnFunctions
        private void initColDg()
        {
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
        }

        private async void InitRequests()
        {
            
            requests = await customerRequestsController.getListTypeReq(customerid);
        }


        private void trigerCb()
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string type = dr.Cells[2].Value.ToString();
                string name = dr.Cells[1].Value.ToString();
                if (type == "Service" && name == "Consultation")
                {
                    int index = dr.Index;
                    dt.Rows.RemoveAt(index);
                }
            }

            if (cbFree.Checked)
            {
                dt.Rows.Add(1, "Consultation", "Service", 0);
            }
            else
            {
                if (radioButton1.Checked)
                    dt.Rows.Add(1, "Consultation", "Service", priceConsult);
                else if(radioButton2.Checked)
                    dt.Rows.Add(1, "Consultation", "Service", priceSConsult);
                else
                    dt.Rows.Add(1, "Consultation", "Service", priceFConsult);
            }

        }

        private void setCustomerId(int reqid)
        {
            customerid = reqid;
            txtCode.Text = customerid.ToString();
        }

        private void initGroupBoxState()
        {

            if (requests.Contains(consultS))
            {
                gpConsulation.Visible = true;
                dt.Rows.Add(1, "Consultation", "Service", priceConsult);
            }
          

            if (requests.Contains(medCert))
            {
                gbMedCert.Visible = true;
                dt.Rows.Add(2, "MedCert", "Service", priceMedCert);
            }
           

            if (requests.Contains(labS))
            {
                gbLab.Visible = true;
            }
         

            if (requests.Contains(xRayS))
            {
                gbXray.Visible = true;
            }

            if (requests.Contains(packagesS))
            {
                gbPackages.Visible = true;
            }


            if (requests.Contains(otherS))
            {
                gbServices.Visible = true;
            }

            dataGridView1.DataSource = dt;
          
        }

        private void setTotalPrice()
        {
            totalPrice = 0;
            //    double removeVat = 0;
            float dis = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                float price = float.Parse(row.Cells["Price"].Value.ToString());
          
                totalPrice += price;
            }

         

            if(seniorId != "")
            {
                bool isValidDis = float.TryParse(txtDis.Text.Trim(), out _);

                dis = isValidDis ? float.Parse(txtDis.Text.Trim()) : 0;

                if (totalPrice >= dis)
                {
                    totalPrice -= dis;
                }
            }

            //Senior discount automatically

            /* if (seniorId != "")
             {
                 removeVat = Math.Round(totalPrice / 1.12, 2);
                 totalPrice = float.Parse(removeVat + "");
                 float discount = totalPrice * .20f;
                 totalPrice -= discount;
             }*/

            textBox3.Text = String.Format("PHP {0:0.##}", totalPrice);
        }

        private async void setInitPrice()
        {
            Task<float> price1 =  pricesService.getPrice("MedCert");
            Task<float> price2 = pricesService.getPrice("Consulation");
            Task<float> price3 = pricesService.getPrice("SConsultation");
            Task<float> price4 = pricesService.getPrice("priceConsultF");
         
            Task<float>[] prices = new Task<float>[] {price1,price2,price3,price4 };

            await Task.WhenAll(prices);

            priceMedCert = price1.Result;
            priceConsult = price2.Result;
            priceSConsult = price3.Result;
            priceFConsult = price4.Result;

            txtPriceConsult.Text = priceConsult.ToString();
            textBox1.Text = priceMedCert.ToString();
        }

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = laboratoryController.getComboDatas();
            Task<List<ComboBoxItem>> task2 = xrayControllers.getComboDatas();
            Task<List<ComboBoxItem>> task3 = serviceController.getComboDatas();
            Task<List<ComboBoxItem>> task4 = packagesController.getComboDatas();
            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1, task2, task3,task4 };

            await Task.WhenAll(Cbs);

            cbLab.Items.AddRange(task1.Result.ToArray());
            cbXray.Items.AddRange(task2.Result.ToArray());
            cbOther.Items.AddRange(task3.Result.ToArray());
            cbPackages.Items.AddRange(task4.Result.ToArray());
        }

        private void finishTransaction(float payment)
        {
            dt.Rows.Clear();
            dataGridView1.DataSource = dt;

            float change = payment - totalPrice;
            textBox4.Text = "PHP " + String.Format("{0:0.##}", change);
            button2.Enabled = false;
            btnUpdate.Enabled = false;
        }



        private async void processTransaction()
        {
          
          
            Task task1 = savesRadioLabQ();
            Task task2 = customerDetailsController.setPaid(customerid);
            Task task3 = saveclinicSales();
            Task task4 = invoiceController.Save(totalPrice);
            Task[] processes = new Task[] { task1, task2,task3,task4 };

            await Task.WhenAll(processes);
        }


        private async Task saveclinicSales()
        {
            List<Task> saves = new List<Task>();
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string type = dr.Cells[2].Value.ToString();
                int id = int.Parse(dr.Cells[0].Value.ToString());
                saves.Add(salesClinicController.Save(type, id));
            
            }
            await Task.WhenAll(saves);
        }

        

        private async Task savesRadioLabQ()
        {
            List<Task> saves = new List<Task>();
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string type = dr.Cells[2].Value.ToString();
                int id = int.Parse(dr.Cells[0].Value.ToString());

                if (type == "Laboratory")
                    saves.Add(labQueueController.save(id, customerid));
                if (type == "Radio")
                    saves.Add(radioQueueController.save(customerid, id));


            }

            await Task.WhenAll(saves);
        }



        private bool isFoundGrid(string type, int idInSelect)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int id = int.Parse(row.Cells["id"].Value.ToString());
                string typename = row.Cells["Type"].Value.ToString();
                if (id == idInSelect && typename == type)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion


        #region Handler
        private void cbLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValueLab = int.Parse((cbLab.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void cbXray_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValueXray = int.Parse((cbXray.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void cbOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValueService = int.Parse((cbOther.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void cbPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValuePackages = int.Parse((cbPackages.SelectedItem as ComboBoxItem).Value.ToString());
        } 

        private async void btnLabAdd_Click(object sender, EventArgs e)
        {
            if (cbValueLab == 0)
                return;


            if (!isFoundGrid("Laboratory", cbValueLab))
            {
                float price = await laboratoryController.getPrice(cbValueLab);
                dt.Rows.Add(cbValueLab,cbLab.Text, "Laboratory", price);

                dataGridView1.DataSource = dt;
                setTotalPrice();
            }
        
        }

        private async void btnXrayAdd_Click(object sender, EventArgs e)
        {
            if (cbValueXray == 0)
                return;


            if (!isFoundGrid("Radio", cbValueXray))
            {
                float price = await xrayControllers.getPrice(cbValueXray);
                dt.Rows.Add(cbValueXray,cbXray.Text, "Radio", price);

                dataGridView1.DataSource = dt;
                setTotalPrice();
            }
        }

        private async void btnAddServices_Click(object sender, EventArgs e)
        {
            if (cbValueService == 0)
                return;


            if (!isFoundGrid("OtherServices", cbValueService))
            {
                float price = await serviceController.getPrice(cbValueService);
                dt.Rows.Add(cbValueService, cbOther.Text, "OtherServices", price);

                dataGridView1.DataSource = dt;
                setTotalPrice();
            }
        }

        private async void btnAddPackages_Click(object sender, EventArgs e)
        {
            if (cbValuePackages == 0)
                return;


            if (!isFoundGrid("Packages", cbValuePackages))
            {
                float price = await packagesController.getPrice(cbValuePackages);
                dt.Rows.Add(cbValuePackages, cbPackages.Text, "Packages", price);

                dataGridView1.DataSource = dt;
                setTotalPrice();
            }
        }

        private  void btnUpdate_Click(object sender, EventArgs e)
        {
            int _;
            float payment = 0;
            if (textBox2.Text == "")
                return;

            if (!(int.TryParse(textBox2.Text.Trim(), out _)))
                return;

            payment = float.Parse(textBox2.Text.Trim());

            if (totalPrice > payment)
            {
                MessageBox.Show("Payment is Not enough", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            processTransaction();
            finishTransaction(payment);
           
            //show OR
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            if (dataGridView1.SelectedRows.Count == 0)
                return;


            int index = dataGridView1.SelectedRows[0].Index;

            dt.Rows.RemoveAt(index);
            setTotalPrice();
        }

        private void btnRemoveALl_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            if (dataGridView1.SelectedRows.Count == 0)
                return;


            dt.Rows.Clear();
            dataGridView1.DataSource = dt;
            setTotalPrice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            seniorDiag form = new seniorDiag();
            form.ShowDialog();
            seniorId = form.seniorId;

            if (seniorId != "" || seniorId != null)
            {
                txtDis.Visible = true;
                label11.Visible = true;
            }
       
            //automated discount
          /*  if (totalPrice > 0)
            {
                setTotalPrice();
            }*/

        }

        private void cbFree_Click(object sender, EventArgs e)
        {
            trigerCb();
            setTotalPrice();
        }

        

        private void radioButton2_Click(object sender, EventArgs e)
        {
            trigerCb();
            txtPriceConsult.Text = priceSConsult.ToString();
            setTotalPrice();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            trigerCb();
            txtPriceConsult.Text = priceConsult.ToString();
            setTotalPrice();
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            trigerCb();
            txtPriceConsult.Text = priceFConsult.ToString();
            setTotalPrice();
        }


        private void txtDis_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDis_TextChanged(object sender, EventArgs e)
        {
            setTotalPrice();
        }

      
        #endregion


    }
}
