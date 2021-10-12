using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Doctor.PanelDoctor.Diag;
using RMC.Patients;
using System;

using System.Data;
using System.Drawing;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Doctor.PanelDoctor
{
    public partial class DoctorQueue : Form
    {


        DoctorQueueController doctorQueueController = new DoctorQueueController();
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        PatientMedcertController patientMedcertController = new PatientMedcertController();
        doctorResultsController doctorResultsController = new doctorResultsController();

        private ImageList imageList;
        string idRightClick = "";
        private DataTable dt = new DataTable();
   
        public DoctorQueue()
        {
            InitializeComponent();
            initGrid();
            
            timer1.Start();
        }

        private async Task loadGrid()
        {
            pictureBox1.Show();
            pictureBox1.Update();
            DataSet ds = await doctorQueueController.getDataSetDocQ(UserLog.getUserId());
            await RefreshGrid(ds);
            pictureBox1.Hide();
        }


        private async Task RefreshGrid(DataSet ds)
        {
            DataTable lopDt = ds.Tables[0];
          
            dt.Rows.Clear();

            foreach(DataRow row in lopDt.Rows)
            {
                int medType = int.Parse(row["med_cert_type"].ToString());
                int qno = int.Parse(row["queue_no"].ToString());
                int patid = int.Parse(row["patient_id"].ToString());
                int age = int.Parse(row["age"].ToString());
                int custid = await customerDetailsController.getCustomerIdinQueue(qno);

                Image imgConsult;
                Image imgmedCert;

                imgmedCert = await patientMedcertController.isDoneMedCert(custid) ? imageList.Images[0] :
                            medType == 0 ? imageList.Images[3] : imageList.Images[2];

                imgConsult = await doctorResultsController.hasDoctorResultsIdNowWithCC(row["cc_doctor"].ToString(),patid) 
                    ? imageList.Images[0] :
                    (string.IsNullOrEmpty(row["cc_doctor"].ToString())) ? 
                    imageList.Images[3] : imageList.Images[2];


                dt.Rows.Add(qno, patid,
                    row["patientname"].ToString(), age,
                    row["gender"].ToString(), imgConsult, imgmedCert);
            }

            dbServiceList.DataSource = "";
            dbServiceList.DataSource = dt;
            dbServiceList.AutoResizeColumns();

        }

        private void initGrid()
        {
          
            dt.Columns.Add("Queue No", typeof(int));
            dt.Columns.Add("Patient Id", typeof(int));
            dt.Columns.Add("Patient Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Consult", typeof(Image));
            dt.Columns.Add("MedCert", typeof(Image));

            imageList = StaticData.listImages();
        }

        private async void dbServiceList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dbServiceList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    timer1.Stop();
                    idRightClick = dbServiceList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    
                    await contextMenuFillItems();

                    contextMenuStrip1.Show(dbServiceList, new Point(e.X, e.Y));
                }

            }
        }

        private async Task contextMenuFillItems()
        {

            contextMenuStrip1.Items.Clear();

            contextMenuStrip1.Items.Add("View Data").Click 
                += new EventHandler(viewDataToolStripMenuItem_Click);
         

            int cusid = int.Parse(idRightClick);
            string cc = await doctorQueueController.getCC(cusid);
            int medType = await doctorQueueController.getMedCertType(cusid);


            if (!string.IsNullOrEmpty(cc))
            {
                contextMenuStrip1.Items.Add("Doctor Form").Click 
                    += new EventHandler(doctorFormToolStripMenuItem_Click);
             
            }
           

            if (medType == 1)
            {
                contextMenuStrip1.Items.Add("add Med Cert").Click += new EventHandler(medCert_Click);
            }
                
            else if (medType == 2)
            {
                contextMenuStrip1.Items.Add("add Pre Employment").Click += new EventHandler(preEmployment_Click);
            }
             

            contextMenuStrip1.Items.Add("Done").Click 
                += new EventHandler(doneToolStripMenuItem_Click);

           

        }

        private async void doctorFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int queueno = int.Parse(idRightClick);
            int patientid = await customerDetailsController.getPatientIDinQueue(queueno);
            string cc = await doctorQueueController.getCC(queueno);

            DoctorForm form = new DoctorForm(patientid, cc);
            form.ShowDialog();
            await  loadGrid();
            timer1.Start();
        }

        private async void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int queueno = int.Parse(idRightClick);
            int patientid = await customerDetailsController.getPatientIDinQueue(queueno);

            addEditPatient form = new addEditPatient(patientid);
            form.ShowDialog();
            await loadGrid();
            timer1.Start();

        }

        private async void medCert_Click(object sender, EventArgs e)
        {
            int queueno = int.Parse(idRightClick);
            int custid = await customerDetailsController.getCustomerIdinQueue(queueno);

            if (await patientMedcertController.isDoneMedCert(custid))
            {

                MessageBox.Show("Already Done with Medcert", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                

            AddMedCertDiags addMedCertDiags = new AddMedCertDiags(queueno);
            addMedCertDiags.Show();
            await loadGrid();
            timer1.Start();
        }

        private async void preEmployment_Click(object sender, EventArgs e)
        {
            int queueno = int.Parse(idRightClick);
            int custid = await customerDetailsController.getCustomerIdinQueue(queueno);

            if (await patientMedcertController.isDoneMedCert(custid))
            {

                MessageBox.Show("Already Done with Pre Employment", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            addEditPeForm addEditPeForm = new addEditPeForm(queueno);
            addEditPeForm.Show();
            await loadGrid();
            timer1.Start();
        }

        private async void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {


            int queueno = int.Parse(idRightClick);

            DialogResult dialogResult = MessageBox.Show($"Set The Queue Number {queueno} to Done?", "Validation",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.OK)
            {
               await doctorQueueController.setDone(queueno);
            }

            await loadGrid();
            timer1.Start();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
          await  loadGrid();
        }

        private async void DoctorQueue_Load(object sender, EventArgs e)
        {
            await loadGrid();
        }
    }
}
