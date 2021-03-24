using MySql.Data;
using Org.BouncyCastle.Ocsp;
using RMC.Database.Controllers;
using RMC.Database.Models;
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
        UserracountsController uc = new UserracountsController();
        DataTable dt = new DataTable();
        ImageList ImageList1 = new ImageList();

        string idRightClick = "";
        string queNoClick = "";
        private int consultS = 1;
        private int medCert = 2;
        private int labS = 3;
        private int xRayS = 4;
        private int packagesS = 5;
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


            ImageList1.ImageSize = new Size(30, 30);

          
            ImageList1.Images.Add(Properties.Resources.check);
            ImageList1.Images.Add(Properties.Resources.x);
            ImageList1.Images.Add(Properties.Resources.wait);
            ImageList1.Images.Add(Properties.Resources.emp);

        }
        

        private async void getData()
        {
            customerDetailsModsList = new List<customerDetailsMod>();

            customerDetailsModsList = await customerDetailsController.getDetailsList();
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
                    if(await docQController.isDone(c.quueu_no))
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
                    imgConsult = ImageList1.Images[1];
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgCustomerList.Rows.Count == 0)
                return;

            if (dgCustomerList.SelectedRows.Count == 0)
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
           
            List<string> doctors = await uc.listDoctorOnlines();

            foreach(string d in doctors)
            {
                goToDoctorToolStripMenuItem.DropDownItems.Add(d, null, new EventHandler(SubmenuItem_Click));
            }
        }

        private async void SubmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem click = ((ToolStripMenuItem)sender);

            int uid = int.Parse(click.Text.Split('-')[1]);
            int id = int.Parse(idRightClick);

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

    }
}
