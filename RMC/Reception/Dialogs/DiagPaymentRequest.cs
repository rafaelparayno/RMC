using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.InventoryPharma.Dialogs;
using RMC.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception.Dialogs
{
    public partial class DiagPaymentRequest : Form
    {
        DoctorRequestLabController doctorRequestLabController = new DoctorRequestLabController();
        DoctorRequestXrayController doctorRequestXrayController = new DoctorRequestXrayController();
        InvoiceController invoiceController = new InvoiceController();
        SalesClinicController salesClinicController = new SalesClinicController();
        LabQueueController labQueueController = new LabQueueController();
        RadioQueueController radioQueueController = new RadioQueueController();
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        CustomerRequestsController customerRequestsController = new CustomerRequestsController();

        private int docres = 0;
        private float totalPrice = 0;
        private string seniorId = "";
        private int customerid = 0;
        private int patientid = 0;
        private int labS = 3;
        private int xRayS = 4;
        private int invoice_no = 0;
        DataTable dt = new DataTable();

        public DiagPaymentRequest(int docres,int patientid)
        {
            InitializeComponent();
            this.docres = docres;
            this.patientid = patientid;
            initColDg();
        }

        private void initColDg()
        {
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
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



            if (seniorId != "")
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



        public async Task loadDatas()
        {
            List<labModel> labModels = await doctorRequestLabController.getLabModelDoctorResult(docres);
            List<xraymodel> xraymodels = await doctorRequestXrayController.getXrayData(docres);


            foreach (labModel l in labModels)
            {
                dt.Rows.Add(l.labID, l.name, "Laboratory", l.price);

            }

            foreach (xraymodel x in xraymodels)
            {
                dt.Rows.Add(x.id, x.name, "Radio", x.price);
            }

            dataGridView1.DataSource = dt;

            setTotalPrice();
        }

        private async void DiagPaymentRequest_Load(object sender, EventArgs e)
        {
            await loadDatas();
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

        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            int _;
            float payment = 0;
            if (textBox2.Text == "")
                return;

            if (!(int.TryParse(textBox2.Text.Trim(), out _)))
                return;


            if (dataGridView1.Rows.Count == 0)
                return;



            payment = float.Parse(textBox2.Text.Trim());

            if (totalPrice > payment)
            {
                MessageBox.Show("Payment is Not enough", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await  processTransaction();
            printReceipt(payment);
        }

        private async Task processTransaction()
        {
           
            int lastQ = await customerDetailsController.getLastQueue() + 1;
            invoice_no = await invoiceController.getLatestNo();
            await customerDetailsController.save(lastQ.ToString(), patientid.ToString());
            await invoiceController.Save(totalPrice);
            customerid = await customerDetailsController.getCustomerIdinQueue(lastQ);

            if (hasLab())
               await customerRequestsController.newReq(labS);

            if (hasRadio())
                await customerRequestsController.newReq(xRayS);


            Task task1 = savesRadioLabQ();
            Task task2 = customerDetailsController.setPaid(customerid);
            Task task3 = saveclinicSales();

            Task[] processes = new Task[] { task1,task2 ,task3 };

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


        private bool hasRadio()
        {
            bool hasR = false;

            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string type = dr.Cells[2].Value.ToString();

                if (type == "Radio")
                {
                    hasR = true;
                    break;
                }
            }

            return hasR;

        }

        private bool hasLab()
        {
            bool hasLab = false;

            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                string type = dr.Cells[2].Value.ToString();

                if (type == "Laboratory")
                {
                    hasLab = true;
                    break;
                }
            }

            return hasLab;

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

        public void printReceipt(float payment)
        {

            button2.Enabled = false;
            btnUpdate.Enabled = false;
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
            rec.SetParameterValue("discount", float.Parse(txtDis.Text.Trim()));
            rec.SetParameterValue("total", float.Parse(textBox3.Text.Trim().Split(' ')[1]));
            rec.SetParameterValue("payment", float.Parse(textBox2.Text.Trim()));
            rec.SetParameterValue("change", float.Parse(textBox4.Text.Trim().Split(' ')[1]));
            rec.SetParameterValue("in_no", invoice_no);
            var dialog = new PrintDialog();
            dialog.ShowDialog();
            rec.PrintOptions.PrinterName = dialog.PrinterSettings.PrinterName;
            rec.PrintToPrinter(1, false, 0, 0);

        }
    }
}
