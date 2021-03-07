using MySql.Data;
using Org.BouncyCastle.Ocsp;
using RMC.Database.Controllers;
using RMC.Database.Models;
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
        DataTable dt = new DataTable();
        ImageList ImageList1 = new ImageList();

        string idRightClick = "";
     
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
            dt.Columns.Add("Xray", typeof(Image));
            dt.Columns.Add("Lab", typeof(Image));
            dt.Columns.Add("Services", typeof(Image));
            dt.Columns.Add("Paid", typeof(Image));


            ImageList1.ImageSize = new Size(30, 30);

          
            ImageList1.Images.Add(Properties.Resources.check);
            ImageList1.Images.Add(Properties.Resources.x);
            ImageList1.Images.Add(Properties.Resources.wait);

          
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
                Image imgPaid;

                if (requests.Contains(consultS))
                {
                    imgConsult = ImageList1.Images[2];
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
                    imgX = ImageList1.Images[1];
                }

                if (requests.Contains(labS))
                {
                    imgLab = ImageList1.Images[2];
                }
                else
                {
                    imgLab = ImageList1.Images[1];
                }

                if (requests.Contains(otherS) || requests.Contains(medCert) || requests.Contains(packagesS))
                {
                    imgServices = ImageList1.Images[2];
                }
                else
                {
                    imgServices = ImageList1.Images[1];
                }


                dt.Rows.Add(c.id,c.quueu_no ,c.name, c.age, imgConsult, imgX, imgLab, imgServices, ImageList1.Images[1]);

            }

            dgCustomerList.DataSource = "";
            dgCustomerList.DataSource = dt;
         
        }

        private void btnNextReq_Click(object sender, EventArgs e)
        {
           /* ReceptionPayment form = new ReceptionPayment();
            form.ShowDialog();*/

            /*customerDetailsController.nextQueue();*/
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
                    contextMenuStrip1.Show(dgCustomerList, new Point(e.X, e.Y));
                }

            }
        }

        private void gToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
