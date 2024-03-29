﻿using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.Dialogs;
using RMC.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
        PackageLabController packageLabController = new PackageLabController();
        PackageOthers packageOthers = new PackageOthers();
        PackageXray packageXrayController = new PackageXray();
        OthersQueueController othersQueueController = new OthersQueueController();
        #endregion

        #region VariableState
        List<int> requests = new List<int>();
        private int indexInDg = 0;
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
        float priceS2Consult = 0;
        float priceOnline = 0;
        float totalPrice = 0;
        int invoice_no = 0;
        DataTable dt = new DataTable();
   /*     PrintDocument printDocument = new PrintDocument();
        PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();*/
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
            dt.Columns.Add("Discount", typeof(decimal));
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
                dt.Rows.Add(1, "Consultation", "Service", 0,0);
            }
            else
            {
                if (radioButton1.Checked)
                    dt.Rows.Add(1, "Consultation", "Service", priceConsult,0);
                else if(radioButton2.Checked)
                    dt.Rows.Add(1, "Consultation", "Service", priceSConsult,0);
                else if(radioButton3.Checked)
                    dt.Rows.Add(1, "Consultation", "Service", priceFConsult,0);
                else if (radioButton4.Checked)
                    dt.Rows.Add(1, "Consultation", "Service", priceS2Consult,0);
                else if (radioButton5.Checked)
                    dt.Rows.Add(1, "Consultation", "Service", priceOnline,0);
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
                dt.Rows.Add(1, "Consultation", "Service", priceConsult,0);
            }
          

            if (requests.Contains(medCert))
            {
                gbMedCert.Visible = true;
                dt.Rows.Add(2, "MedCert", "Service", priceMedCert,0);
            }


            gbLab.Visible = requests.Contains(labS);
           
            gbXray.Visible = requests.Contains(xRayS);

            gbPackages.Visible = requests.Contains(packagesS);

            gbServices.Visible = requests.Contains(otherS);

            dataGridView1.DataSource = dt;
          
        }


        private void setTotalPrice()
        {
            totalPrice = 0;
            //    double removeVat = 0;
            float totalDisc = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                float price = float.Parse(row.Cells["Price"].Value.ToString());
                float dis = float.Parse(row.Cells["Discount"].Value.ToString());
                totalPrice += price;
                totalDisc += dis;
            }

            totalPrice -= totalDisc;
       
            txtDis.Text = String.Format("PHP {0:0.##}", totalDisc);

            textBox3.Text = String.Format("PHP {0:0.##}", totalPrice);
        }

        private async void setInitPrice()
        {
            Task<float> price1 =  pricesService.getPrice("MedCert");
            Task<float> price2 = pricesService.getPrice("Consulation");
            Task<float> price3 = pricesService.getPrice("SConsultation");
            Task<float> price4 = pricesService.getPrice("priceConsultF");
            Task<float> task5 = pricesService.getPrice("onlineConsult");
            Task<float> task6 = pricesService.getPrice("S2Consultation");

            Task<float>[] prices = new Task<float>[] {price1,price2,price3,price4, task5, task6 };

            await Task.WhenAll(prices);

            priceMedCert = price1.Result;
            priceConsult = price2.Result;
            priceSConsult = price3.Result;
            priceFConsult = price4.Result;
            priceOnline = task5.Result;
            priceS2Consult = task6.Result;

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

            printReceipt(payment);
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            dataGridView1.DataSource = dt;
            gbLab.Enabled = false;
            gbMedCert.Enabled = false;
            gbPackages.Enabled = false;
            gbServices.Enabled = false;
            gbXray.Enabled = false;
            button2.Enabled = false;
            btnUpdate.Enabled = false;
        }


        public void printReceipt(float payment)
        {
            float change = payment - totalPrice;
            textBox4.Text = "PHP " + String.Format("{0:0.##}", change);

            float subtotal = 0;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("itemname", typeof(string));
            dt.Columns.Add("qty", typeof(int));
            dt.Columns.Add("price", typeof(float));



            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string name = dr.Cells["Name"].Value.ToString();
             
                float Tprice = float.Parse(dr.Cells["Price"].Value.ToString());
           
                subtotal += Tprice;
                dt.Rows.Add(name, 1, Tprice);

            }

            ds.Tables.Add(dt);

            receipts rec = new receipts();
            rec.SetDataSource(ds);
            rec.SetParameterValue("subTotal", subtotal);
            rec.SetParameterValue("discount", float.Parse(txtDis.Text.Trim().Split(' ')[1]));
            rec.SetParameterValue("total", float.Parse(textBox3.Text.Trim().Split(' ')[1]));
            rec.SetParameterValue("payment", float.Parse(textBox2.Text.Trim()));
            rec.SetParameterValue("change", float.Parse(textBox4.Text.Trim().Split(' ')[1]));
            rec.SetParameterValue("in_no", invoice_no);

            var dialog = new PrintDialog();
            dialog.ShowDialog();
            rec.PrintOptions.PrinterName = dialog.PrinterSettings.PrinterName;
            rec.PrintToPrinter(1, false, 0, 0);

        }


        private async Task processTransaction()
        {
            await invoiceController.Save(totalPrice, float.Parse(txtDis.Text.Trim().Split(' ')[1]));
            await savesRadioLabQ();
            await customerDetailsController.setPaid(customerid, 1);
            await saveclinicSales();
            
            int task4 = await invoiceController.getLatestNo();
          
            invoice_no = task4;
        }


        private async Task saveclinicSales()
        {
       
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                int id = int.Parse(dr.Cells[0].Value.ToString());
                float dis = float.Parse(dr.Cells[4].Value.ToString());
                string type = dr.Cells[2].Value.ToString();
                float amt = float.Parse(dr.Cells[3].Value.ToString());
                await salesClinicController.Save(type, id, customerid,amt,dis);
            
            }
          
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
                if (type == "Packages")
                    saves.Add(savePackageQueue(id));
                if (type == "OtherServices")
                    saves.Add(othersQueueController.save(id, customerid));
            }

            await Task.WhenAll(saves);
        }


        private async Task savePackageQueue(int id)
        {
            List<Task> saves = new List<Task>();
            List<PackagesNames> listLabs = await packageLabController.getPackagesLab(id);
            List<PackagesNames> listRadios = await packageXrayController.getPackagesNames(id);
            List<PackagesNames> listOthers = await packageOthers.getPackagesNames(id);


            foreach (PackagesNames r in listRadios)
            {
              
                saves.Add(radioQueueController.save(customerid, r.id));
            }

            foreach (PackagesNames p in listLabs)
            {
                saves.Add(labQueueController.save(p.id, customerid));
            }

            foreach(PackagesNames o in listOthers)
            {
                saves.Add(othersQueueController.save(o.id, customerid));
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
                dt.Rows.Add(cbValueLab,cbLab.Text, "Laboratory", price,0);

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
                dt.Rows.Add(cbValueXray,cbXray.Text, "Radio", price,0);

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
                dt.Rows.Add(cbValueService, cbOther.Text, "OtherServices", price,0);

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
                dt.Rows.Add(cbValuePackages, cbPackages.Text, "Packages", price,0);

                dataGridView1.DataSource = dt;
                setTotalPrice();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
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


            pictureBox1.BringToFront();
            pictureBox1.Show();
            pictureBox1.Update();

            await processTransaction();


            pictureBox1.Hide();
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

         /*   dt = new DataTable();*/
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


        private void checkBox1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string type = dr.Cells[2].Value.ToString();
                string name = dr.Cells[1].Value.ToString();
                if (type == "Service" && name == "MedCert")
                {
                    int index = dr.Index;
                    dt.Rows.RemoveAt(index);
                }
            }

            if (checkBox1.Checked)
            {
                dt.Rows.Add(2, "MedCert", "Service", 0,0);
            }
            else
            {
                dt.Rows.Add(2, "MedCert", "Service", priceMedCert,0);
            }
            setTotalPrice();
        }

        private void ReceptionPayment_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnRemove.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnRemoveALl.PerformClick();
            }
            if (e.KeyCode == Keys.F3)
            {
                button2.PerformClick();
            }
            if (e.KeyCode == Keys.F4)
            {
                btnUpdate.PerformClick();
            }

        }



        #endregion

        private void radioButton5_Click(object sender, EventArgs e)
        {
            trigerCb();
            txtPriceConsult.Text = priceOnline.ToString();
            setTotalPrice();
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            trigerCb();
            txtPriceConsult.Text = priceS2Consult.ToString();
            setTotalPrice();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {                   
                    contextMenuStrip1.Show(dataGridView1, new Point(e.X, e.Y));
                    indexInDg = currentMouseOverRow;
                }

            }
        }

        private void addDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {

            float sellingPrice = float.Parse(dataGridView1.Rows[indexInDg].Cells[3].Value.ToString());

            addDiscPay frmDisc = new addDiscPay(sellingPrice);

            frmDisc.ShowDialog();

            if (frmDisc.Percentage == 0)
                return;

            float setPerc = frmDisc.Percentage;

            dataGridView1.Rows[indexInDg].Cells[4].Value = setPerc;
            setTotalPrice();
        }
    }
}
