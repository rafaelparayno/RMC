using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Doctor;
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

namespace RMC.Patients.PanelsDetails
{
    public partial class PanelDoctorFindings : Form
    {
        AccessController accessController = new AccessController();
        doctorResultsController dsController = new doctorResultsController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        PatientVController patientVController = new PatientVController();
        PatientSymptomsController patientSymptomsController = new PatientSymptomsController();
        PatientPrescriptionController patientPrescriptionController = new PatientPrescriptionController();

        List<int> listAccess = new List<int>();
        private int id = 0;
        private int timerRefresh = 0;

        List<DoctorResult> listDoctorResult;
        DoctorResult doctorResultData;
        patientDetails patientDetailsData;
        patientVModel patientVModelData;
        List<PatientSymptomsModel> listPatientSymp;
        List<PatientPrescriptionModel> listPatientPrescription;


        public PanelDoctorFindings(int id)
        {
            InitializeComponent();
            this.id = id;
            getAccess();
            initColLv();
            loadData();
        }

        private void initColLv()
        {

            lvLabDetails.View = View.Details;
            lvLabDetails.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Date", 300, HorizontalAlignment.Left);
        }


        private async void loadData()
        {
            listDoctorResult = await dsController.getDoctorResults(id);
            refreshListView();
        }


        private async Task searchData()
        {
            listDoctorResult = await dsController.getDoctorResults(id,
                                        dateTimePicker1.Value.ToString("yyy-MM-dd"));

            refreshListView();
        }

        private void refreshListView()
        {
            lvLabDetails.Items.Clear();
            foreach (DoctorResult l in listDoctorResult)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = l.id.ToString();
                lvitem.SubItems.Add(l.date_results.ToString("dddd, dd MMMM yyyy"));
                lvLabDetails.Items.Add(lvitem);
            }
        }

        private void getAccess()
        {
            listAccess =  accessController.accesses(UserLog.getRole());

            if (listAccess.Contains(5))
            {
                iconButton3.Visible = true;
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            DoctorForm form = new DoctorForm(id);
            form.ShowDialog();
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            await searchData();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void lvLabDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLabDetails.Items.Count == 0)
                return;

            if (lvLabDetails.SelectedItems.Count == 0)
                return;


            startShowing();

            int id = int.Parse(lvLabDetails.SelectedItems[0].Text);

            showCrystalReportData(id);
        }


        private async void showCrystalReportData(int resultdsid)
        {
            doctorResultData = await dsController.getDoctorResultsSearchId(resultdsid);
            patientDetailsData = await patientDetailsController.getPatientId(id);
            patientVModelData = await patientVController.getDetailsidDate(id,
                doctorResultData.date_results.ToString("yyyy-MM-dd"));

            

            listPatientSymp = await patientSymptomsController.getPatientSymptomsMod(resultdsid);
            listPatientPrescription = await patientPrescriptionController.getPrescriptionModel(resultdsid);

            string objectiveSymp = "" ;
            foreach (PatientSymptomsModel listPs in listPatientSymp)
            {
                objectiveSymp += listPs.symptoms + "\n\t";
            }

            string prescriptions = "";

            foreach(PatientPrescriptionModel listPP in listPatientPrescription)
            {
                prescriptions += listPP.medName + " " + listPP.instruction + "\n\t";
            }
                
            

            DoctorsSample cos = new DoctorsSample();
            cos.SetParameterValue("ccParam",doctorResultData.cc);
            cos.SetParameterValue("aParam", doctorResultData.assestment);
            cos.SetParameterValue("aParam", doctorResultData.assestment);

            cos.SetParameterValue("patientNameParam", patientDetailsData.FullName);
            cos.SetParameterValue("cnParam", patientDetailsData.contact);
            cos.SetParameterValue("ageParam", patientDetailsData.age);
            cos.SetParameterValue("genderParam", patientDetailsData.gender);
            cos.SetParameterValue("bdayparam", patientDetailsData.birthdate.Split(' ')[0]);
            cos.SetParameterValue("cvilParam", patientDetailsData.civil_status);
            cos.SetParameterValue("addressParam", patientDetailsData.address);
            cos.SetParameterValue("dateDynamic", doctorResultData.date_results.ToShortDateString());

            cos.SetParameterValue("bpParam", patientVModelData.bp  == "" || patientVModelData.bp == null ? "" :
                                        patientVModelData.bp);
            cos.SetParameterValue("tempParam", patientVModelData.temp == "" || patientVModelData.temp == null ? 
                                "" : patientVModelData.temp);

            cos.SetParameterValue("oParam", objectiveSymp);

            cos.SetParameterValue("pParam", doctorResultData.procedureA);

            cos.SetParameterValue("medsReqParam", prescriptions);

            crystalReportViewer1.ReportSource = cos;

            
        }


        private void startShowing()
        {
            timer1.Start();
            timerRefresh = 0;
            lvLabDetails.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerRefresh++;

            if(timerRefresh == 5)
            {
                timer1.Stop();
                lvLabDetails.Enabled = true;
            }
        }
    }
}
