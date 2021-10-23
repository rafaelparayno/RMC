using MySql.Data;
using Org.BouncyCastle.Ocsp;
using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.OthersPanels.panels;
using RMC.Patients.PanelsDetails.Dialogs;
using RMC.Reception.PanelRequestForm.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception.PanelRequestForm
{
    public partial class PanelRequestForm : Form
    {
        //PatientDetailsController patientDetailsController = new PatientDetailsController();
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
        string idRightClick = "";
        string idLeftClick = "";
        string queNoClick = "";
        private int consultS = 1;
        private int medCert = 2;
        private int labS = 3;
        private int xRayS = 4;
        private int otherS = 7;

        public PanelRequestForm()
        {
            InitializeComponent();
            initLvCol();
            

            loadOnlineDoctors();
        }

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditRequestForm form = new AddEditRequestForm();
            form.ShowDialog();
            await getData();
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

            ImageList1 = StaticData.listImages();

        }
        

        private async Task getData()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            customerDetailsModsList = new List<customerDetailsMod>();
            customerDetailsModsList = await customerDetailsController.getDetailsList();
            await RefreshGrid(customerDetailsModsList);
            pictureBox1.Hide();

        }

        private async Task RefreshGrid(List<customerDetailsMod> customers)
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
                bool done = false;

                imgConsult = requests.Contains(consultS) ? 
                    await docQController.isDone(c.quueu_no) ? 
                    ImageList1.Images[0] : ImageList1.Images[2] 
                    :   ImageList1.Images[3];


                imgX = requests.Contains(xRayS) ? await radioQueueController.isDone(c.id) ? 
                    ImageList1.Images[0] : ImageList1.Images[2]
                    : ImageList1.Images[3];

                imgLab = requests.Contains(labS) ? await labQueueController.isDone(c.id) ?
                      ImageList1.Images[0] : ImageList1.Images[2]
                    : ImageList1.Images[3];


           

                if (requests.Contains(otherS) || requests.Contains(medCert))
                {

                    int medType = await docQController.getMedCertByCustomeId(c.id);
                  
                    if (medType == 0)
                    {
                        done = true;

                    }
                    else
                    {
                        done = await patientMedcertController.isDoneMedCert(c.id);
                    }
                        

                    if(await othersQueueController.isDone(c.id) && done )
                    {
                        imgServices = ImageList1.Images[0];
                    }
                    else
                    {
                        imgServices = ImageList1.Images[2];
                    }

                  
                }
                else
                {
                    imgServices = ImageList1.Images[3];
                }


                dt.Rows.Add(c.id,c.quueu_no ,c.name, c.age, imgConsult, imgX, imgLab, imgServices, imgPaid);

            }

            dgCustomerList.DataSource = "";
            dgCustomerList.DataSource = dt;
            dgCustomerList.Columns[2].Width = 300;
         
        }


        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgCustomerList.Rows.Count == 0)
                return;

            if (dgCustomerList.SelectedRows.Count == 0)
                return;

            int qno = int.Parse(dgCustomerList.SelectedRows[0].Cells[1].Value.ToString());

            int isPay = await customerDetailsController.getIsPaid(qno);

            if (isPay == 1)
                return;

            AddEditRequestForm form = new AddEditRequestForm(dgCustomerList.SelectedRows[0].Cells[0].Value.ToString(), 
                dgCustomerList.SelectedRows[0].Cells[1].Value.ToString());
            form.ShowDialog();
            await getData();
        }

        private async void dgCustomerList_MouseClick(object sender, MouseEventArgs e)
        {
   
            if (e.Button == MouseButtons.Right)
            {
           
                int currentMouseOverRow = dgCustomerList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idRightClick = dgCustomerList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    queNoClick = dgCustomerList.Rows[currentMouseOverRow].Cells[1].Value.ToString();
                    contextMenuStrip1.Show(dgCustomerList, new Point(e.X, e.Y));
                }

            }


            if(e.Button == MouseButtons.Left)
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

        private async Task showRequestDetails(int cid)
        {
            panel1.Controls.Clear();

            float price = await invoiceController.getInvoiceSale(cid);

            int qno = int.Parse(dgCustomerList.SelectedRows[0].Cells[1].Value.ToString());

            int isPay = await customerDetailsController.getIsPaid(qno);

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

        private async void loadOnlineDoctors()
        {
            List<DoctorQueueModel> listdocs = await uc.listDoctorOnlinesModel();

            foreach(DoctorQueueModel d in listdocs)
            {
                goToDoctorToolStripMenuItem.DropDownItems.Add($"ID-{d.id}-{d.doctorname}", null, new EventHandler(SubmenuItem_Click));
            }

        }

        private async void SubmenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem click = ((ToolStripMenuItem)sender);

            int uid = int.Parse(click.Text.Split('-')[1]);
            int id = int.Parse(idRightClick);

            int cusid = int.Parse(dgCustomerList.SelectedRows[0].Cells[0].Value.ToString());
            int qno = int.Parse(dgCustomerList.SelectedRows[0].Cells[1].Value.ToString());
            List<int> req = await customerRequestsController.getListTypeReq(cusid);
            int medType = await docQController.getMedCertType(qno);
            if (!req.Contains(consultS) && medType == 0)
                return;
       

            await docQController.updateDoctorQueue(uid, id);

            MessageBox.Show("Updated data");
        }


        private async void btnpay_Click(object sender, EventArgs e)
        {
            if (dgCustomerList.Rows.Count == 0)
                return;

            if (dgCustomerList.SelectedRows.Count == 0)
                return;

            int qno = int.Parse(dgCustomerList.SelectedRows[0].Cells[1].Value.ToString());

            int isPay = await customerDetailsController.getIsPaid(qno);

            if (isPay == 1)
            {

                return;
            }

            int req = int.Parse(dgCustomerList.SelectedRows[0].Cells[0].Value.ToString());
            ReceptionPayment form = new ReceptionPayment(req);
            form.ShowDialog();
            await getData();
        }

     

        private async void addVitalToolStripMenuItem_Click(object sender, EventArgs e)
        {


            int qno = int.Parse(queNoClick);
            int patientid =    await customerDetailsController.getPatientIDinQueue(qno);
            AddEditVital ad = new AddEditVital(patientid);
            ad.ShowDialog();
        }

        private async void btnVoid_Click(object sender, EventArgs e)
        {
            if (dgCustomerList.Rows.Count == 0)
                return;

            if (dgCustomerList.SelectedRows.Count == 0)
                return;

            int qno = int.Parse(dgCustomerList.SelectedRows[0].Cells[1].Value.ToString());

            int isPay = await customerDetailsController.getIsPaid(qno);

            if (isPay == 0)
                return;

            int cusid = int.Parse(dgCustomerList.SelectedRows[0].Cells[0].Value.ToString());

            VoidForm voidForm = new VoidForm();
            voidForm.ShowDialog();

            if (!voidForm.isFound)
                return;


            List<Task> tasks = new List<Task>();

            tasks.Add(invoiceController.Delete(cusid));
            tasks.Add(labQueueController.Delete(cusid));
            tasks.Add(radioQueueController.Delete(cusid));
            tasks.Add(othersQueueController.Delete(cusid));
            tasks.Add(customerDetailsController.setPaid(cusid, 0));
            tasks.Add(salesClinicController.delete(cusid));

            await Task.WhenAll(tasks);

            MessageBox.Show("Succesfully Void");

            await getData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgCustomerList.Rows.Count == 0)
                return;

            if (dgCustomerList.SelectedRows.Count == 0)
                return;

            int qno = int.Parse(dgCustomerList.SelectedRows[0].Cells[1].Value.ToString());

            int isPay = await customerDetailsController.getIsPaid(qno);

            if (isPay == 1)
            {
                MessageBox.Show("To Delete Request. You Need to void the process.");
                return;
            }


            VoidForm voidForm = new VoidForm();
            voidForm.ShowDialog();

            if (!voidForm.isFound)
                return;

            int cusid = int.Parse(dgCustomerList.SelectedRows[0].Cells[0].Value.ToString());
            bool isDone = await patientMedcertController.isDoneMedCert(cusid);
            bool isDone2 = await docQController.isDone(qno);

            if (isDone || isDone2)
            {
                MessageBox.Show("This Request has already done by the doctor");
                return;
            }

            List<Task> tasks = new List<Task>();
            tasks.Add(customerDetailsController.Delete(cusid));
            tasks.Add(docQController.Remove(cusid));
            tasks.Add(customerRequestsController.remove(cusid));


            await Task.WhenAll(tasks);

            MessageBox.Show("Successfully Delete Request");

            await getData();

        }

        

        private void PanelRequestForm_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;



            if (control.Size.Width < 700)
            {
                panel1.Visible = false;
            }
            else
            {

                panel1.Visible = true;
            }
        }

        private async void PanelRequestForm_Load(object sender, EventArgs e)
        {
           await getData();
        }
    }
}
