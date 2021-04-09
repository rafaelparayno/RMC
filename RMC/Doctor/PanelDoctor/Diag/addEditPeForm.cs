using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Patients.PanelsDetails.Dialogs;
using RMC.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RMC.Doctor.PanelDoctor.Diag
{
    public partial class addEditPeForm : Form
    {



        #region variables



        PatientMedcertController patientMedcertController = new PatientMedcertController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        DoctorQueueController doctorQueueController = new DoctorQueueController();
        PatientVController patientVController = new PatientVController();
        patientDetails patdetails;
        patientVModel patientVModel;
        private int qno = 0;
        int patid = 0;
        int customerid = 0;
        private bool isEdit = false;
        private int idEdit = 0;
        private string companyName = "";
        private string pt = "N/a";
        private string hepa = "Non-Reactive";
        private string drugTest = "Negative";
        private string typeTest = "Type A.Physical Fit for all types of work. No Physical Defects Noted";
        #endregion


        public addEditPeForm(int qno)
        {
            InitializeComponent();
            this.qno = qno;
        }



        public addEditPeForm(int idEdit, bool isEdit)
        {
            InitializeComponent();
            this.idEdit = idEdit;
            this.isEdit = isEdit;
        }

        private void cbCheckAllLeft_Click(object sender, EventArgs e)
        {
            if (cbCheckAllLeft.Checked)
            {
                checkAllLeftInPE();

            }
            else
            {
                checkAllNotLeft();
            }
        }


        private void cbCheckAllRight_Click(object sender, EventArgs e)
        {
            if (cbCheckAllRight.Checked)
            {
                checkAllRight();
            }
            else
            {
                checkNotALlRight();
            }
        }


        #region CheckRightAllEvent

        private void checkNotALlRight()
        {

            txtCarido.Text = "";
            txtPulmop.Text = "";
            txtBreast.Text = "";
            txtSkin.Text = "";
            txtGastro.Text = "";
            txtGen.Text = "";
            txtGyna.Text = "";
            txtEndo.Text = "";

            cbCardio.Checked = false;
            cbPulmo.Checked = false;
            cbBreast.Checked = false;
            cbSkin.Checked = false;
            cbGastro.Checked = false;
            cbGen.Checked = false;
            cbGyna.Checked = false;
            cbEndo.Checked = false;

            txtCarido.Enabled = true;
            txtPulmop.Enabled = true;
            txtBreast.Enabled = true;
            txtSkin.Enabled = true;
            txtGastro.Enabled = true;
            txtGen.Enabled = true;
            txtGyna.Enabled = true;
            txtEndo.Enabled = true;
        }

        private void checkAllRight()
        {
            txtCarido.Text = "Normal";
            txtPulmop.Text = "Normal";
            txtBreast.Text = "Normal";
            txtSkin.Text = "Normal";
            txtGastro.Text = "Normal";
            txtGen.Text = "Normal";
            txtGyna.Text = "Normal";
            txtEndo.Text = "Normal";

            cbCardio.Checked = true;
            cbPulmo.Checked = true;
            cbBreast.Checked = true;
            cbSkin.Checked = true;
            cbGastro.Checked = true;
            cbGen.Checked = true;
            cbGyna.Checked = true;
            cbEndo.Checked = true;

            txtCarido.Enabled = false;
            txtPulmop.Enabled = false;
            txtBreast.Enabled = false;
            txtSkin.Enabled = false;
            txtGastro.Enabled = false;
            txtGen.Enabled = false;
            txtGyna.Enabled = false;
            txtEndo.Enabled = false;
        }

        #endregion

        #region CheckAllLeftEvent
        private void checkAllNotLeft()
        {


            txtGeneral.Text = "";
            txtEars.Text = "";
            txtEyes.Text = "";
            txtNose.Text = "";
            txtMouth.Text = "";
            txtThroat.Text = "";
            txtHema.Text = "";
            txtNeuro.Text = "";


            //cb
            cbGeneral.Checked = false;
            cbEars.Checked = false;
            cbEyes.Checked = false;
            cbNose.Checked = false;
            cbMouth.Checked = false;
            cbThroat.Checked = false;
            cbHema.Checked = false;
            cbNeuro.Checked = false;
            //cb
            txtGeneral.Enabled = true;
            txtEars.Enabled = true;
            txtEyes.Enabled = true;
            txtNose.Enabled = true;
            txtThroat.Enabled = true;
            panelMouth.Enabled = true;
            txtHema.Enabled = true;
            txtNeuro.Enabled = true;


            //cbMO
        }


        private void checkAllLeftInPE()
        {


            //Txt
            txtGeneral.Text = "Normal";
            txtEars.Text = "Normal";
            txtEyes.Text = "Normal";
            txtNose.Text = "Normal";
            txtMouth.Text = "Normal";
            txtThroat.Text = "Normal";
            txtHema.Text = "Normal";
            txtNeuro.Text = "Normal";
            //txt


            //cb
            cbGeneral.Checked = true;
            cbEars.Checked = true;
            cbEyes.Checked = true;
            cbNose.Checked = true;
            cbMouth.Checked = true;
            cbThroat.Checked = true;
            cbHema.Checked = true;
            cbNeuro.Checked = true;
            //cb
            txtGeneral.Enabled = false;
            txtEars.Enabled = false;
            txtEyes.Enabled = false;
            txtNose.Enabled = false;
            txtThroat.Enabled = false;
            txtMouth.Enabled = false;
            txtHema.Enabled = false;
            txtNeuro.Enabled = false;


            //cbMO
        }
        #endregion

        #region CbsLefts
        private void cbcheckAllLeftTrigger()
        {
            cbCheckAllLeft.Checked = cbGeneral.Checked && cbEyes.Checked && cbEars.Checked &&
                                   cbNose.Checked && cbMouth.Checked && cbThroat.Checked &&
                                   cbHema.Checked && cbNeuro.Checked;
        }

        private void cbGeneral_Click(object sender, EventArgs e)
        {
            if (cbGeneral.Checked)
            {
                txtGeneral.Enabled = false;
                txtGeneral.Text = "Normal";
            
            }
            else
            {
                txtGeneral.Enabled = true;
                txtGeneral.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbEyes_Click(object sender, EventArgs e)
        {
            if (cbEyes.Checked)
            {
                txtEyes.Enabled = false;
                txtEyes.Text = "Normal";
               
            }
            else
            {
                txtEyes.Text = "";

                txtEyes.Enabled = true;
            }
            cbcheckAllLeftTrigger();
        }

        private void cbEars_Click(object sender, EventArgs e)
        {
            if (cbEars.Checked)
            {
                txtEars.Enabled = false;
                txtEars.Text = "Normal";
               
            }
            else
            {
                txtEars.Enabled = true;
                txtEars.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbNose_Click(object sender, EventArgs e)
        {
            if (cbNose.Checked)
            {
                txtNose.Text = "Normal";
                txtNose.Enabled = false;
             
            }
            else
            {
                txtNose.Enabled = true;
                txtNose.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbMouth_Click(object sender, EventArgs e)
        {
            if (cbMouth.Checked)
            {
                txtMouth.Text = "Normal";
                txtMouth.Enabled = false;
               
            }
            else
            {
                txtMouth.Enabled = true;
                txtMouth.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbThroat_Click(object sender, EventArgs e)
        {
            if (cbThroat.Checked)
            {
                txtThroat.Text = "Normal";
              
                txtThroat.Enabled = false;
            }
            else
            {
                txtThroat.Text = "";
                txtThroat.Enabled = true;
            }
            cbcheckAllLeftTrigger();
        }

        private void cbHema_Click(object sender, EventArgs e)
        {
            if (cbHema.Checked)
            {
                txtHema.Text = "Normal";
                txtHema.Enabled = false;

            
            }
            else
            {
                txtHema.Enabled = true;

                txtHema.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbNeuro_Click(object sender, EventArgs e)
        {
            if (cbNeuro.Checked)
            {
                txtNeuro.Text = "Normal";
                txtNeuro.Enabled = false;
         
            }
            else
            {
                txtNeuro.Enabled = true;
                txtNeuro.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        #endregion

        #region CbsRight
        private void cbcheckAllRightTrigger()
        {
            cbCheckAllRight.Checked = cbCardio.Checked && cbPulmo.Checked && cbBreast.Checked &&
                cbSkin.Checked && cbGastro.Checked && cbGen.Checked && cbGyna.Checked && cbEndo.Checked;
        }

        private void cbCardio_Click(object sender, EventArgs e)
        {
            if (cbCardio.Checked)
            {
                txtCarido.Text = "Normal";
                txtCarido.Enabled = false;
                
            }
            else
            {
                txtCarido.Enabled = true;
                txtCarido.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbPulmo_Click(object sender, EventArgs e)
        {
            if (cbPulmo.Checked)
            {
                txtPulmop.Text = "Normal";
                txtPulmop.Enabled = false;
         
            }
            else
            {
                txtPulmop.Enabled = true;
                txtPulmop.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbBreast_Click(object sender, EventArgs e)
        {
            if (cbBreast.Checked)
            {
                txtBreast.Text = "Normal";
                txtBreast.Enabled = false;
           
            }
            else
            {
                txtBreast.Enabled = true;
                txtBreast.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbSkin_Click(object sender, EventArgs e)
        {
            if (cbSkin.Checked)
            {
                txtSkin.Text = "Normal";
                txtSkin.Enabled = false;
          
            }
            else
            {
                txtSkin.Enabled = true;
                txtSkin.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbEndo_Click(object sender, EventArgs e)
        {
            if (cbEndo.Checked)
            {
                txtEndo.Text = "Normal";
                txtEndo.Enabled = false;
          
            }
            else
            {
                txtEndo.Enabled = true;
                txtEndo.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbGyna_Click(object sender, EventArgs e)
        {
            if (cbGyna.Checked)
            {
                txtGyna.Text = "Normal";
                txtGyna.Enabled = false;
              
            }
            else
            {
                txtGyna.Enabled = true;
                txtGyna.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbGen_Click(object sender, EventArgs e)
        {
            if (cbGen.Checked)
            {
                txtGen.Text = "Normal";
                txtGen.Enabled = false;
          
            }
            else
            {
                txtGen.Enabled = true;
                txtGen.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbGastro_Click(object sender, EventArgs e)
        {
            if (cbGastro.Checked)
            {
                txtGastro.Text = "Normal";
                txtGastro.Enabled = false;
              
            }
            else
            {
                txtGastro.Enabled = true;
                txtGastro.Text = "";
            }
            cbcheckAllRightTrigger();
        }


        #endregion


        #region Diagnosis Events

        private void cbXray_Click(object sender, EventArgs e)
        {
            if (cbXray.Checked)
            {
                txtXray.Enabled = false;
                txtXray.Text = "Normal";
            }
            else
            {
                txtXray.Enabled = true;
                txtXray.Text = "";
            }
          
        }

        private void cbCbc_Click(object sender, EventArgs e)
        {
            if (cbCbc.Checked)
            {
                txtCbc.Enabled = false;
                txtCbc.Text = "Normal";
            }
            else
            {
                txtCbc.Enabled = true;
                txtCbc.Text = "";
            }
          
        }

        private void cbUrinalysis_Click(object sender, EventArgs e)
        {
            if (cbUrinalysis.Checked)
            {
                txtUri.Enabled = false;
                txtUri.Text = "Normal";
            }
            else
            {
                txtUri.Enabled = true;
                txtUri.Text = "";
            }
            
        }

        private void cbStool_Click(object sender, EventArgs e)
        {
            if (cbStool.Checked)
            {
                txtStool.Enabled = false;
                txtStool.Text = "Normal";
            }
            else
            {
                txtStool.Enabled = true;
                txtStool.Text = "";
            }
            
        }


        private void cbEcg_Click(object sender, EventArgs e)
        {
            if (cbEcg.Checked)
            {
                txtEcg.Enabled = false;
                txtEcg.Text = "Normal";
            }
            else
            {
                txtEcg.Enabled = true;
                txtEcg.Text = "";
            }
            
        }


        private void radioButton3_Click(object sender, EventArgs e)
        {
            pt = "N/a";
          
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            pt = "Negative";
         
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            pt = "Positive";
        
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            hepa = "Non-Reactive";
        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            hepa = "Reactive";
        }

        private void radioButton7_Click(object sender, EventArgs e)
        {
            drugTest = "Negative";
        }

        private void radioButton8_Click(object sender, EventArgs e)
        {
            drugTest = "Positive";
        }


        #endregion




        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!isEdit)
            {
                string filePath = filePathSaving.saveMedCert(patdetails.lastname + "-" + patid);
                string datenow = DateTime.Now.ToString("yyyy--MM--dd");
                string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
                string combine = datenow + "--" + timenow;
                string filename = "preemployment-" + combine;
                saveXml(filePath, filename);
                await patientMedcertController.save(customerid.ToString(), filePath + filename + ".xml", "2");
            }
            else
            {
                MedCertModel medCertModel = await patientMedcertController.getMedcert(idEdit);


                saveXml(medCertModel.path, "");
            }

            MessageBox.Show("Succesfully Save Data");
            this.Close();
        }


        private void saveXml(string path, string filename)
        {
            string pathSave = isEdit ? path : path + filename + ".xml";

            XmlWriter xwriter = XmlWriter.Create(pathSave);

            xwriter.WriteStartElement("PreEmployment");

            xwriter.WriteElementString("eDate", DateTime.Now.ToString("MMMM dd, yyyy"));


            //History
            xwriter.WriteElementString("pastDiseases", txtSignificant.Text.Trim());
            xwriter.WriteElementString("operations", txtOperations.Text.Trim());
            xwriter.WriteElementString("presentSymp", txtPresent.Text.Trim());

            //History

            //Physical Exams

            //Left
            xwriter.WriteElementString("general", txtGeneral.Text.Trim());
            xwriter.WriteElementString("eyes", txtEyes.Text.Trim());
            xwriter.WriteElementString("ears", txtEars.Text.Trim());
            xwriter.WriteElementString("nose", txtNose.Text.Trim());
            xwriter.WriteElementString("mouth", txtMouth.Text.Trim());
            xwriter.WriteElementString("throat", txtThroat.Text.Trim());
            xwriter.WriteElementString("hema", txtHema.Text.Trim());
            xwriter.WriteElementString("neuro", txtNeuro.Text.Trim());
            //Left

            xwriter.WriteElementString("cardio", txtCarido.Text.Trim());
            xwriter.WriteElementString("pulmonary", txtPulmop.Text.Trim());
            xwriter.WriteElementString("breast", txtBreast.Text.Trim());
            xwriter.WriteElementString("skin", txtSkin.Text.Trim());
            xwriter.WriteElementString("gastro", txtGastro.Text.Trim());
            xwriter.WriteElementString("geni", txtGen.Text.Trim());
            xwriter.WriteElementString("gyna", txtGyna.Text.Trim());
            xwriter.WriteElementString("endo", txtEndo.Text.Trim());


            //Physical Exams

            //Diagnostic

            xwriter.WriteElementString("xray", txtXray.Text.Trim());
            xwriter.WriteElementString("cbc", txtCbc.Text.Trim());
            xwriter.WriteElementString("urinaly", txtUri.Text.Trim());
            xwriter.WriteElementString("pt", pt);
            xwriter.WriteElementString("others", txtOthersDiag.Text.Trim());
            xwriter.WriteElementString("stool", txtStool.Text.Trim());
            xwriter.WriteElementString("heba", hepa);
            xwriter.WriteElementString("drug", drugTest);
            xwriter.WriteElementString("ecg", txtEcg.Text.Trim());
            //Diagnostic


            //RecomAssClassifcation
            xwriter.WriteElementString("recommendation", txtRecommend.Text.Trim());
            xwriter.WriteElementString("assestment", txtAssestment.Text.Trim());
            xwriter.WriteElementString("classification", typeTest);

            //RecomAssClassifcation


            xwriter.WriteEndElement();
            xwriter.Flush();
            xwriter.Close();

        }

        private async void addEditPeForm_Load(object sender, EventArgs e)
        {
            if (!isEdit)
            {
                patid = await customerDetailsController.getPatientIDinQueue(qno);
                customerid = await customerDetailsController.getCustomerIdinQueue(qno);
                
                patdetails = await patientDetailsController.getPatientId(patid);
                companyName = await doctorQueueController.getCompanyName(qno);

                DateTime today = DateTime.Now;
                patientVModel = await patientVController.getDetailsidDate(patid,today.ToString("yyyy-MM-dd"));
              
            
            }
        }

        private void rbTypeA_Click(object sender, EventArgs e)
        {
            typeTest = "Type A.Physical Fit for all types of work. No Physical Defects Noted";
        }

        private void rbTypeB_Click(object sender, EventArgs e)
        {
            typeTest = "Type B. Physical Fit for all types of work. Has minor Ailment/ Defect. Easily Curable or offers no Handicap to job applied for";
        }

        private void rbTypeC_Click(object sender, EventArgs e)
        {
            typeTest = "Type C. Generally Not Acceptable for Employment/ Pending for Futher Evaluation";
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            PreEmploymentViewer preEmploymentViewer = new PreEmploymentViewer(0,setDictionary());
            preEmploymentViewer.Show();
            
        }


        private Dictionary<string,string> setDictionary()
        {

            Dictionary<string, string> values = new Dictionary<string, string>();

            //Personal Info
            values.Add("patientName", patdetails.FullName);
            values.Add("companyName", companyName);
            values.Add("idNo", "3");
            values.Add("civilStatus", patdetails.civil_status);
            values.Add("Gender", patdetails.gender);
            values.Add("bdate", patdetails.birthdate.ToString().Split(' ')[0]);
            values.Add("age", patdetails.age.ToString());
            //Personal Info

            //vital
            values.Add("bp", patientVModel.bp);
            values.Add("weight", patientVModel.wt == 0 ? "" : patientVModel.wt + " KG");
            values.Add("height", patientVModel.height == 0 ? "" : patientVModel.height + " cm");
            values.Add("bmi", patientVModel.bmi + " / \t" + patientVModel.bmiLabel);
            values.Add("lmp", patientVModel.lmp);
            values.Add("hrate", patientVModel.heartrate);


            //vital


            //History
            values.Add("pastDiseases", txtSignificant.Text.Trim());
            values.Add("operations", txtOperations.Text.Trim());
            values.Add("presentSymp", txtPresent.Text.Trim());

            //History

            //Physical Exams

            //Left
            values.Add("general", txtGeneral.Text.Trim());
            values.Add("eyes", txtEyes.Text.Trim());
            values.Add("ears", txtEars.Text.Trim());
            values.Add("nose", txtNose.Text.Trim());
            values.Add("mouth", txtMouth.Text.Trim());
            values.Add("throat", txtThroat.Text.Trim());
            values.Add("hema", txtHema.Text.Trim());
            values.Add("neuro", txtNeuro.Text.Trim());
            //Left

            values.Add("cardio", txtCarido.Text.Trim());
            values.Add("pulmonary", txtPulmop.Text.Trim());
            values.Add("breast", txtBreast.Text.Trim());
            values.Add("skin", txtSkin.Text.Trim());
            values.Add("gastro", txtGastro.Text.Trim());
            values.Add("geni", txtGen.Text.Trim());
            values.Add("gyna", txtGyna.Text.Trim());
            values.Add("endo", txtEndo.Text.Trim());


            //Physical Exams

            //Diagnostic

            values.Add("xray", txtXray.Text.Trim());
            values.Add("cbc", txtCbc.Text.Trim());
            values.Add("urinaly", txtUri.Text.Trim());
            values.Add("pt", pt);
            values.Add("others", txtOthersDiag.Text.Trim());
            values.Add("stool", txtStool.Text.Trim());
            values.Add("heba", hepa);
            values.Add("drug", drugTest);
            values.Add("ecg", txtEcg.Text.Trim());
            //Diagnostic


            //RecomAssClassifcation
            values.Add("recommendation", txtRecommend.Text.Trim());
            values.Add("assestment", txtAssestment.Text.Trim());
            values.Add("classification", typeTest);

            //RecomAssClassifcation

            return values;
        }
    }

}

