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
        DoctorRequestLabController doctorReqLabController = new DoctorRequestLabController();
        DoctorRequestXrayController docReqXrayController = new DoctorRequestXrayController();
        DoctorDataController doctorDataController = new DoctorDataController();

        List<int> listAccess = new List<int>();
        private int id = 0;
        private int timerRefresh = 0;
        List<DoctorResult> listDoctorResult;
        DoctorResult doctorResultData;
        patientDetails patientDetailsData;
        patientVModel patientVModelData;
        List<PatientSymptomsModel> listPatientSymp;
        List<PatientPrescriptionModel> listPatientPrescription;
        List<labModel> listlabModel;
        List<xraymodel> listXrayModel;

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
            listAccess = accessController.accesses(UserLog.getRole());
            if (listAccess.Contains(5))
            {
                iconButton3.Visible = true;
                iconButton4.Visible = true;
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
        private async void lvLabDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLabDetails.Items.Count == 0)
                return;
            if (lvLabDetails.SelectedItems.Count == 0)
                return;
            startShowing();
            int id = int.Parse(lvLabDetails.SelectedItems[0].Text);
            await  showCrystalReportData(id);
        }

        private async Task showCrystalReportData(int resultdsid)
        {
            
            Task<DoctorResult> task1 = dsController.getDoctorResultsSearchId(resultdsid);
            Task<patientDetails> task2 = patientDetailsController.getPatientId(id);

            Task<List<labModel>> task3 = 
                doctorReqLabController.getLabModelDoctorResult(resultdsid);
            Task<List<PatientSymptomsModel>> task4 = 
                patientSymptomsController.getPatientSymptomsMod(resultdsid);
            Task<List<PatientPrescriptionModel>> task5 = 
                patientPrescriptionController.getPrescriptionModel(resultdsid);

            Task<List<xraymodel>> task6 = docReqXrayController.getXrayData(resultdsid);
            List<Task> listTask = new List<Task>() { task1,task2,task3,task4,task5,task6};

            await Task.WhenAll(listTask);

            doctorResultData = task1.Result;
            patientDetailsData = task2.Result;

            listlabModel = task3.Result;
            listPatientSymp = task4.Result;
            listPatientPrescription = task5.Result;
            listXrayModel = task6.Result;

            patientVModelData = await patientVController.getDetailsidDate(id,
              doctorResultData.date_results.ToString("yyyy-MM-dd"));

            DoctorDataModel d = await doctorDataController.getDoctorData(doctorResultData.doctor_id);



            string objectiveSymp = "";
            string prescriptions = "";
            string docReqLab = "";
            string docReqX = "";

            foreach (PatientSymptomsModel listPs in listPatientSymp)
            {
                objectiveSymp += listPs.symptoms + "\n\t";
            }



            foreach (PatientPrescriptionModel listPP in listPatientPrescription)
            {
                prescriptions += listPP.medName + " " + listPP.instruction + "\n\t";
            }


            foreach (labModel labm in listlabModel)
            {
                docReqLab += labm.name + "\n\t";
            }

            foreach (xraymodel xrayM in listXrayModel)
            {
                docReqX += xrayM.name + "\n\t";
            }


            DoctorsSample cos = new DoctorsSample();
            cos.SetParameterValue("ccParam", doctorResultData.cc);
            cos.SetParameterValue("aParam", doctorResultData.assestment);
            cos.SetParameterValue("patientNameParam", patientDetailsData.FullName);
            cos.SetParameterValue("cnParam", patientDetailsData.contact);
            cos.SetParameterValue("ageParam", patientDetailsData.age);
            cos.SetParameterValue("genderParam", patientDetailsData.gender);
            cos.SetParameterValue("bdayparam", patientDetailsData.birthdate.Split(' ')[0]);
            cos.SetParameterValue("cvilParam", patientDetailsData.civil_status);
            cos.SetParameterValue("addressParam", patientDetailsData.address);
            cos.SetParameterValue("dateDynamic", doctorResultData.date_results.ToShortDateString());
            cos.SetParameterValue("bpParam", string.IsNullOrEmpty(patientVModelData.bp) ? "" :
                                        patientVModelData.bp);

            cos.SetParameterValue("wtParam", string.IsNullOrEmpty(patientVModelData.wt) ? "" :
                                        patientVModelData.wt);

            cos.SetParameterValue("lmpParam", string.IsNullOrEmpty(patientVModelData.lmp) ? "" :
                                      patientVModelData.lmp);

            cos.SetParameterValue("tempParam", patientVModelData.temp == "" || patientVModelData.temp == null ?
                                "" : patientVModelData.temp);
            cos.SetParameterValue("oParam", objectiveSymp);
            cos.SetParameterValue("pParam", doctorResultData.procedureA);

            cos.SetParameterValue("medsReqParam", prescriptions);

            cos.SetParameterValue("labReqParam", "Lab Request** \n\t" + docReqLab);

            cos.SetParameterValue("xrayReqParam", "xray Request** \n\t" + docReqX);
            cos.SetParameterValue("licenseNo", d.license);
            cos.SetParameterValue("prNoParam", d.pr);

            cos.SetParameterValue("imgPath", d.imgPath);

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

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (lvLabDetails.Items.Count == 0)
                return;

            if (lvLabDetails.SelectedItems.Count == 0)
                return;

            int resid = int.Parse(lvLabDetails.Items[0].SubItems[0].Text);

            DoctorForm form = new DoctorForm(id,resid);
            form.ShowDialog();
        }
    }
}
