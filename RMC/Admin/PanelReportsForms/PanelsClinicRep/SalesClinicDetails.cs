using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Reception;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelReportsForms.PanelsClinicRep
{
    public partial class SalesClinicDetails : Form
    {

        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        CustomerRequestsController customerRequestsController = new CustomerRequestsController();
        DoctorQueueController docQController = new DoctorQueueController();
        LabQueueController labQueueController = new LabQueueController();
        RadioQueueController radioQueueController = new RadioQueueController();
        OthersQueueController othersQueueController = new OthersQueueController();
        UserracountsController uc = new UserracountsController();
        PatientMedcertController patientMedcertController = new PatientMedcertController();
        InvoiceController invoiceController = new InvoiceController();
        SalesClinicController salesClinicController = new SalesClinicController();

        DataTable dt = new DataTable();
        ImageList ImageList1 = new ImageList();
        List<customerDetailsMod> customerDetailsModsList;
        private int consultS = 1;
        private int medCert = 2;
        private int labS = 3;
        private int xRayS = 4;
        private int packagesS = 5;
        private int otherS = 7;
        private string selectedDate = "";
        private string formattedDate = "";
        private string idLeftClick = "";

        public SalesClinicDetails(string selectedDate)
        {
            InitializeComponent();
            initLvCol();
            this.selectedDate = selectedDate;
            //yyyy-MM-dd
            this.formattedDate = $"{selectedDate.Split('/')[2]}-{selectedDate.Split('/')[1]}-{selectedDate.Split('/')[0]}";
        }

        private void initLvCol()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("QueueNo", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Consult", typeof(Image));
            dt.Columns.Add("Radio", typeof(Image));
            dt.Columns.Add("Lab", typeof(Image));
            dt.Columns.Add("Services", typeof(Image));
            dt.Columns.Add("Paid", typeof(Image));


            ImageList1.ImageSize = new Size(30, 30);


            ImageList1.Images.Add(Properties.Resources.check);
            ImageList1.Images.Add(Properties.Resources.x);
            ImageList1.Images.Add(Properties.Resources.wait);
            ImageList1.Images.Add(Properties.Resources.emp);

        }


        private async Task showRequestDetails(int cid)
        {
            panel1.Controls.Clear();

            float price = await invoiceController.getInvoiceSale(cid);

            int qno = int.Parse(dgCustomerList.SelectedRows[0].Cells[1].Value.ToString());

            int isPay = await customerDetailsController.getIsPaid(qno, formattedDate);

            Task<List<labModel>> task1 = labQueueController.getReqLabByPatientID(cid);
            Task<List<xraymodel>> task2 = radioQueueController.getReqLabByPatientID(cid);
            Task<List<ServiceModel>> task3 = othersQueueController.getReqServiceByPatientID(cid);

            Task[] processes = new Task[] { task1, task2, task3 };

            await Task.WhenAll(processes);

            List<labModel> labModels = task1.Result;
            List<xraymodel> xraymodels = task2.Result;
            List<ServiceModel> serviceModels = task3.Result;

            RequestDetailsControl requestDetailsControl = new RequestDetailsControl();

            requestDetailsControl.Dock = DockStyle.Fill;

            requestDetailsControl.labelPrice.Text = isPay == 0 ? "Total Sales : Not Paid" : $"Total Sales : ₱{price}";

            requestDetailsControl.listViewLab.View = View.Details;
            requestDetailsControl.listViewLab.Columns.Add("Laboratory Name", 250, HorizontalAlignment.Center);
            requestDetailsControl.listViewLab.Columns.Add("Is Done", 120, HorizontalAlignment.Center);
            foreach (labModel l in labModels)
            {
                ListViewItem lvItems = new ListViewItem();
                lvItems.Text = l.name;
                lvItems.SubItems.Add(l.is_done == 0 ? "Not Done" : "Done");
                requestDetailsControl.listViewLab.Items.Add(lvItems);
            }


            requestDetailsControl.listViewRad.View = View.Details;
            requestDetailsControl.listViewRad.Columns.Add("Radio Name", 250, HorizontalAlignment.Center);
            requestDetailsControl.listViewRad.Columns.Add("Is Done", 120, HorizontalAlignment.Center);
            foreach (xraymodel l in xraymodels)
            {
                ListViewItem lvItems = new ListViewItem();
                lvItems.Text = l.name;
                lvItems.SubItems.Add(l.is_done == 0 ? "Not Done" : "Done");
                requestDetailsControl.listViewRad.Items.Add(lvItems);
            }

            requestDetailsControl.listViewService.View = View.Details;
            requestDetailsControl.listViewService.Columns.Add("Service Name", 250, HorizontalAlignment.Center);
            requestDetailsControl.listViewService.Columns.Add("Is Done", 120, HorizontalAlignment.Center);
            foreach (ServiceModel l in serviceModels)
            {
                ListViewItem lvItems = new ListViewItem();
                lvItems.Text = l.serviceName;
                lvItems.SubItems.Add(l.isDone == 0 ? "Not Done" : "Done");
                requestDetailsControl.listViewService.Items.Add(lvItems);
            }

            panel1.Controls.Add(requestDetailsControl);


        }


        private async Task getData(string date)
        {
            customerDetailsModsList = new List<customerDetailsMod>();

            customerDetailsModsList = await customerDetailsController.getDetailsList(date);
            RefreshGrid(customerDetailsModsList);

        }

        private async void RefreshGrid(List<customerDetailsMod> customers)
        {
            dt.Rows.Clear();
            foreach (customerDetailsMod c in customers)
            {
                List<int> requests = new List<int>();
                requests = await customerRequestsController.getListTypeReq(c.id);
                Image imgConsult;
                Image imgX;
                Image imgLab;
                Image imgServices;
                Image imgPaid = c.isPaid == 0 ? ImageList1.Images[1] : ImageList1.Images[0];



                if (requests.Contains(consultS))
                {
                    if (await docQController.isDone(c.quueu_no))
                    {
                        imgConsult = ImageList1.Images[0];
                    }
                    else
                    {
                        imgConsult = ImageList1.Images[2];
                    }

                }
                else
                {
                    imgConsult = ImageList1.Images[3];
                }

                if (requests.Contains(xRayS))
                {
                    imgX = ImageList1.Images[2];
                }
                else
                {
                    imgX = ImageList1.Images[3];
                }

                if (requests.Contains(labS))
                {
                    imgLab = ImageList1.Images[2];
                }
                else
                {
                    imgLab = ImageList1.Images[3];
                }

                if (requests.Contains(otherS) || requests.Contains(medCert) || requests.Contains(packagesS))
                {
                    imgServices = ImageList1.Images[2];
                }
                else
                {
                    imgServices = ImageList1.Images[3];
                }


                dt.Rows.Add(c.id, c.quueu_no, c.name, c.age, imgConsult, imgX, imgLab, imgServices, imgPaid);

            }

            dgCustomerList.DataSource = "";
            dgCustomerList.DataSource = dt;

        }

        private async void SalesClinicDetails_Load(object sender, EventArgs e)
        {
            await getData(formattedDate);
        }

        private void SalesClinicDetails_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            if (control.Size.Width < 500)
            {
                panel1.Visible = false;
            }
            else
            {

                panel1.Visible = true;
            }
        }

        private async void dgCustomerList_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                int currentMouseOverRow = dgCustomerList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idLeftClick = dgCustomerList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    int cid = int.Parse(idLeftClick);
                    await showRequestDetails(cid);
                }
            }
        }
    }
}
