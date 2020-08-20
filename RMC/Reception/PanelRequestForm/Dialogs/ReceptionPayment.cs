using RMC.Components;
using RMC.Database.Controllers;
using RMC.InventoryPharma.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class ReceptionPayment : Form
    {

        CustomerRequestsController customerRequestsController = new CustomerRequestsController();
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        PackagesController packagesController = new PackagesController();
        LaboratoryController laboratoryController = new LaboratoryController();
        ServiceController serviceController = new ServiceController();
        XrayControllers xrayControllers = new XrayControllers();
        PricesServiceController pricesService = new PricesServiceController();

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
        float totalPrice = 0;
        DataTable dt = new DataTable();

        public ReceptionPayment()
        {
            InitializeComponent();
            setInitPrice();
            setCustomerId();
            InitRequests();
            initGroupBoxState();
            loadFromDbtoCb();
            setTotalPrice();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
        }

        private async void InitRequests()
        {
            // foreach()
            requests = await customerRequestsController.getListTypeReq(customerid);
        }

        private async Task<int> getCurrentQueue()
        {
            return await customerDetailsController.getCurrentCustomer();
        }


        private async void setCustomerId()
        {
            customerid = await getCurrentQueue();
            txtCode.Text = customerid.ToString();
        }

        private void initGroupBoxState()
        {

            if (requests.Contains(consultS))
            {
                gpConsulation.Visible = true;
            
            }
          

            if (requests.Contains(medCert))
            {
                gbMedCert.Visible = true;
         
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
          
        }

        private void setTotalPrice()
        {
            totalPrice = 0;
            double removeVat = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                float price = float.Parse(row.Cells["Price"].Value.ToString());

                totalPrice += price;
            }

            if (requests.Contains(consultS))
            {
                totalPrice += priceConsult;
                if (cbFree.Checked)
                {
                    totalPrice -= priceConsult;
                }
            }

            if (requests.Contains(medCert))
            {
                totalPrice += priceMedCert;

            }

           

            if (seniorId != "")
            {
                removeVat = Math.Round(totalPrice / 1.12, 2);
                totalPrice = float.Parse(removeVat + "");
                float discount = totalPrice * .20f;
                totalPrice -= discount;
            }

            textBox3.Text = String.Format("PHP {0:0.##}", totalPrice);
        }

        private async void setInitPrice()
        {
            priceMedCert = await pricesService.getPrice("MedCert");
            priceConsult = await pricesService.getPrice("Consulation");

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

        private void finishTransaction(float payment)
        {
            dt.Rows.Clear();
            dataGridView1.DataSource = dt;
            
            float change = payment - totalPrice;
            textBox4.Text = "PHP " + String.Format("{0:0.##}", change);
            button2.Enabled = false;
            btnUpdate.Enabled = false;
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


            if (!isFoundGrid("XEU", cbValueXray))
            {
                float price = await xrayControllers.getPrice(cbValueXray);
                dt.Rows.Add(cbValueXray,cbXray.Text, "XEU", price);

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

        private void btnUpdate_Click(object sender, EventArgs e)
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

            finishTransaction(payment);

            //customerDetailsController.nextQueue();


            //this.Hide();
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

            if (totalPrice > 0)
            {
                setTotalPrice();
            }

        }

        private void cbFree_Click(object sender, EventArgs e)
        {
            setTotalPrice();
        }
    }
}
