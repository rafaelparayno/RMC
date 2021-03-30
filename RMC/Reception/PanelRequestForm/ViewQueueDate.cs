using RMC.Database.Controllers;
using RMC.Database.Models;
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
    public partial class ViewQueueDate : Form
    {
        CustomerDetailsController customerDetails = new CustomerDetailsController();
        CustomerRequestsController customerRequestsController = new CustomerRequestsController();
        DoctorQueueController doctorQueueController = new DoctorQueueController();
        DataTable dt = new DataTable();
        ImageList ImageList1 = new ImageList();
        List<customerDetailsMod> customerDetailsModsList;
        private int consultS = 1;
        private int medCert = 2;
        private int labS = 3;
        private int xRayS = 4;
        private int packagesS = 5;
        private int otherS = 7;

        public ViewQueueDate()
        {
            InitializeComponent();
            initLvCol();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");

           await getData(date);
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


        private async Task getData(string date)
        {
            customerDetailsModsList = new List<customerDetailsMod>();

            customerDetailsModsList = await customerDetails.getDetailsList(date);
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
                    if (await doctorQueueController.isDone(c.quueu_no))
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
    }
}
