using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Doctor.PanelDoctor.Diag;
using RMC.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Doctor.PanelDoctor
{
    public partial class DoctorQueue : Form
    {


        DoctorQueueController doctorQueueController = new DoctorQueueController();
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();


        string idRightClick = "";
      
   
        public DoctorQueue()
        {
            InitializeComponent();
            loadGrid();
            timer1.Start();
        }

        private async void loadGrid()
        {
            DataSet ds = await doctorQueueController.getDataSetDocQ(UserLog.getUserId());
            RefreshGrid(ds);
        }


        private void RefreshGrid(DataSet ds)
        {
            dbServiceList.DataSource = "";
            dbServiceList.DataSource = ds.Tables[0];
            dbServiceList.AutoResizeColumns();

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
                contextMenuStrip1.Items.Add("add Pre Employment");
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
            loadGrid();
            timer1.Start();
        }

        private async void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int queueno = int.Parse(idRightClick);
            int patientid = await customerDetailsController.getPatientIDinQueue(queueno);

            addEditPatient form = new addEditPatient(patientid);
            form.ShowDialog();
            loadGrid();
            timer1.Start();

        }

        private async void medCert_Click(object sender, EventArgs e)
        {
            int queueno = int.Parse(idRightClick);
      
            AddMedCertDiags addMedCertDiags = new AddMedCertDiags(queueno);
            addMedCertDiags.Show();
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {


            int queueno = int.Parse(idRightClick);

            DialogResult dialogResult = MessageBox.Show($"Set The Queue Number {queueno} to Done?", "Validation",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.OK)
            {
                doctorQueueController.setDone(queueno);
            }

            loadGrid();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}
