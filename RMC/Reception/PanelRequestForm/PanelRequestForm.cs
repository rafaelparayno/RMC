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
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        List<customerDetailsMod> customerDetailsModsList;
        CustomerRequestsController customerRequestsController = new CustomerRequestsController();
        DoctorQueueController docQController = new DoctorQueueController();
        LabQueueController labQueueController = new LabQueueController();
        RadioQueueController radioQueueController = new RadioQueueController();
        OthersQueueController othersQueueController = new OthersQueueController();
        UserracountsController uc = new UserracountsController();
        PatientMedcertController patientMedcertController = new PatientMedcertController();

        DataTable dt = new DataTable();
        ImageList ImageList1 = new ImageList();

        string idRightClick = "";
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
            getData();

            loadOnlineDoctors();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditRequestForm form = new AddEditRequestForm();
            form.ShowDialog();
            getData();
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
        

        private async void getData()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            customerDetailsModsList = new List<customerDetailsMod>();
            customerDetailsModsList = await customerDetailsController.getDetailsList();
            RefreshGrid(customerDetailsModsList);
            pictureBox1.Hide();

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
         
        }

        private void btnNextReq_Click(object sender, EventArgs e)
        {
            //delete
            getData();
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
            getData();
        }

        private void dgCustomerList_MouseClick(object sender, MouseEventArgs e)
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
                return;

            int req = int.Parse(dgCustomerList.SelectedRows[0].Cells[0].Value.ToString());
            ReceptionPayment form = new ReceptionPayment(req);
            form.ShowDialog();
            getData();
        }

        private void btnResetQ_Click(object sender, EventArgs e)
        {

        }

        private async void addVitalToolStripMenuItem_Click(object sender, EventArgs e)
        {


            int qno = int.Parse(queNoClick);
            int patientid =    await customerDetailsController.getPatientIDinQueue(qno);
            AddEditVital ad = new AddEditVital(patientid);
            ad.ShowDialog();
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            VoidForm voidForm = new VoidForm();
            voidForm.ShowDialog();


            Console.WriteLine(voidForm.isFound);
        }
    }
}
