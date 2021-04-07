using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class AddEditRequestForm : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        CustomerRequestsController customerRequestsController = new CustomerRequestsController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        DoctorQueueController docQController = new DoctorQueueController();
     
        bool isEdit = false;
        int reqid = 0;
        List<int> currentS = new List<int>();
        List<int> editedS = new List<int>();

        private int consultS = 1;
        private int medCert = 2;
        private int labS = 3;
        private int xRayS = 4;
        private int packagesS = 5;
     
        private int otherS = 7;
        private int updateQ = 0;
        int id = 0;
        private int medCertType = 0;
        string ccEdit = "";
        string companyEdit = "";


        public AddEditRequestForm()
        {
            InitializeComponent();

        }

        public AddEditRequestForm(string r_id,string queueno)
        {
            InitializeComponent();
            isEdit = true;
            this.reqid = int.Parse(r_id);
            setEditInitState(queueno);
            this.updateQ = int.Parse(queueno);
        }

        private async Task saveRequests()
        {
            foreach(int type in currentS)
            {
               await customerRequestsController.newReq(type);
            }
        }

        private void updateRequets()
        {
            foreach (int type in currentS)
            {
                customerRequestsController.updateReq(reqid,type);
            }
        }

        private async Task removeRequestCostumer()
        {

            foreach (int id in idsToBeRemove())
            {
                customerRequestsController.remove(id, reqid);
               
            }

            if (!(currentS.Contains(consultS)) && !(currentS.Contains(medCert)))
                await docQController.Remove(reqid);
        }

        private List<int> idsToBeRemove()
        {
            List<int> typesToRemove = new List<int>();

            foreach(int idS in editedS)
            {
                if (!(isFoundInList(idS)))
                {
                  
                   typesToRemove.Add(idS);

                }
            }

            return typesToRemove;

        }

        private bool isFoundInList(int id)
        {
            foreach (int idinS in currentS)
            {
                if (idinS == id)
                {
                    return true;
                }
            }

            return false;
        }

        private void setCbsEditState(List<int> typesid)
        {
           
            if (typesid.Contains(consultS))
            {
                checkConsult.Checked = true;
                groupBox6.Visible = true;
            }
            else
            {
                checkConsult.Checked = false;
            }

            if (typesid.Contains(medCert))
            {
                cbMedCert.Checked = true;
                groupBox9.Visible = true;
            }
            else
            {
                cbMedCert.Checked = false;
            }

           /* cbMedCert.Checked = typesid.Contains(medCert);*/
            checkLab.Checked = typesid.Contains(labS);
            checkXray.Checked = typesid.Contains(xRayS);
            checkPackage.Checked = typesid.Contains(packagesS);
            checkOthers.Checked = typesid.Contains(otherS);

        }

        private async void setEditInitState(string queue_no)
        {
            label8.Text = "Edit Request";
            groupBox1.Visible = false;
            int q = int.Parse(queue_no);
            this.id = await customerDetailsController.getPatientIDinQueue(q);
            currentS = await customerRequestsController.getListTypeReq(reqid);
            editedS = await customerRequestsController.getListTypeReq(reqid);
            ccEdit = await docQController.getCC(q);
            medCertType = await docQController.getMedCertType(q);
            companyEdit = await docQController.getCompanyName(q);



            patientDetails details = await patientDetailsController.getPatientQueueNo(q);
            txtfn.Text = details.Firstname;
            txtLn.Text = details.lastname;
            txtMn.Text = details.middlename;
            dateTimePicker1.Value = DateTime.Parse(details.birthdate);
            txtAge.Text = details.age.ToString();
            txtAddress.Text = details.address;
            txtCn.Text = details.contact;
            cbGender.Text = details.gender;
            cbStatus.Text = details.civil_status;
            textBox3.Text = ccEdit;
            txtCompanyName.Text = companyEdit;
            radioButton4.Checked = medCertType == 1 ? true : false;

            if(medCertType == 2)
            {
                radioButton3.Checked = true;
                label11.Visible = true;
                txtCompanyName.Visible = true; 

            }
          
            setCbsEditState(editedS);
        

        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Enter Valid Or Complete The Details Below");
                return;
            }

            SavingState();
            int customerID = await customerDetailsController.getCustomerIdLast();
            int lastQ = await customerDetailsController.getLastQueue();
            lastQ++;

            if (isEdit)
            {
                if (reqid == 0)
                    return;



               await removeRequestCostumer();
                patientDetailsController.update(txtfn.Text.Trim(), txtMn.Text.Trim(),
                                          txtLn.Text.Trim(), dateTimePicker1.Value.ToString("yyyy/MM/dd"),
                                          txtAge.Text.Trim(), cbGender.SelectedItem.ToString(), txtCn.Text.Trim(),
                                          cbStatus.SelectedItem.ToString(), txtAddress.Text.Trim(), id.ToString());

                updateRequets();

                if (currentS.Contains(consultS) || currentS.Contains(medCert))
                    docQController.Save(reqid, textBox3.Text.Trim(), medCertType,txtCompanyName.Text.Trim());
            }
            else
            {
            
                if(id> 0)
                {
                    patientDetailsController.update(txtfn.Text.Trim(), txtMn.Text.Trim(),
                                   txtLn.Text.Trim(), dateTimePicker1.Value.ToString("yyyy/MM/dd"),
                                   txtAge.Text.Trim(), cbGender.SelectedItem.ToString(), txtCn.Text.Trim(),
                                   cbStatus.SelectedItem.ToString(), txtAddress.Text.Trim(), id.ToString());

                  await  customerDetailsController.save(lastQ.ToString(), id.ToString());

                }
                else
                {

                    patientDetailsController.save(txtfn.Text.Trim(), txtMn.Text.Trim(),
                                              txtLn.Text.Trim(), dateTimePicker1.Value.ToString("yyyy/MM/dd"),
                                              txtAge.Text.Trim(), cbGender.SelectedItem.ToString(), txtCn.Text.Trim(),
                                              cbStatus.SelectedItem.ToString(), txtAddress.Text.Trim());

                    int pid = patientDetailsController.getRecentPID() - 1;

                   await  customerDetailsController.save(lastQ.ToString(), pid.ToString());

                }





                if (currentS.Contains(consultS) || currentS.Contains(medCert))
                    docQController.Save(customerID, textBox3.Text.Trim(), medCertType, txtCompanyName.Text.Trim());

                await saveRequests();
            }



            pictureBox1.Hide();
            MessageBox.Show("SuccessFuly Added Request");
            this.Close();

        }

        private void SavingState()
        {
            btnSave.Enabled = false;
            pictureBox1.Show();
            pictureBox1.Update();
        }

        private bool isValid()
        {
            bool isValid = true;
            errorProvider1.Clear();

            isValid = (txtfn.Text != "") && isValid;
            SetErrorShowNoText(ref txtfn, "Please Input a Value");

            isValid = (txtLn.Text != "") && isValid;
            SetErrorShowNoText(ref txtLn, "Please Input a Value");

            isValid = (txtCn.Text != "") && isValid;
            SetErrorShowNoText(ref txtCn, "Please Input a Value");

            isValid = (txtAddress.Text != "") && isValid;
            SetErrorShowNoText(ref txtAddress, "Please Input a Value");

            isValid = (txtAge.Text != "") && isValid;
            SetErrorShowNoText(ref txtAge, "Please Input a Value");

            isValid = (cbGender.SelectedIndex > -1) && isValid;
            setErrorCbNoValue(ref cbGender, "Please Select A value");

            isValid = (cbStatus.SelectedIndex > -1) && isValid;
            setErrorCbNoValue(ref cbStatus, "Please Select A value");

            isValid = (currentS.Count != 0) && isValid;
            errorHandlingList(ref groupBox8, "Please Select Atleast one");

            if (currentS.Contains(consultS)){
                isValid = (textBox3.Text != "") && isValid;
                SetErrorShowNoText(ref textBox3, "Please Input a Value");
            }

            if(currentS.Contains(medCert) && medCertType == 2)
            {
                isValid = (txtCompanyName.Text != "") && isValid;
                SetErrorShowNoText(ref txtCompanyName, "Please Input a Value");
            }
           
            return isValid;
        }

        private void errorHandlingList(ref GroupBox gb, string ergMsg)
        {
            if (currentS.Count == 0)
            {
                errorProvider1.SetError(gb, ergMsg);
            }
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void checkConsult_Click(object sender, EventArgs e)
        {
            if (checkConsult.Checked)
            {
               
                currentS.Add(consultS);
                groupBox6.Visible = true;
                if (isEdit)
                    textBox3.Text = ccEdit;
            }
            else
            {

                int index = currentS.FindIndex(t => consultS == t);

                if (index > -1)
                    currentS.RemoveAt(index);

                groupBox6.Visible = false;
                textBox3.Text = "";
            }
        }


        private void cbMedCert_Click_1(object sender, EventArgs e)
        {
            if (cbMedCert.Checked)
            {
                currentS.Add(medCert);
                groupBox9.Visible = true;
                medCertType = 1;
             /*   txtCompanyName.Visible = true;*/
            
            }
            else
            {

                int index = currentS.FindIndex(t => medCert == t);

                if (index > -1)
                    currentS.RemoveAt(index);
                groupBox9.Visible = false;
                txtCompanyName.Text = "";
                radioButton4.Checked = true;
                txtCompanyName.Text = "";
                txtCompanyName.Visible = false;
                medCertType = 0;
            }
        }

        private void setErrorCbNoValue(ref ComboBox cb, string msg)
        {
            if (cb.SelectedIndex == -1)
            {
                errorProvider1.SetError(cb, msg);
            }
        }

        private void SetErrorShowNoText(ref TextBox tb, string msg)
        {
            if (tb.Text == "")
            {
                errorProvider1.SetError(tb, msg);
            }
        }

        private void checkXray_Click(object sender, EventArgs e)
        {
            if (checkXray.Checked)
            {
                currentS.Add(xRayS);
            }
            else
            {

                int index = currentS.FindIndex(t => xRayS == t);

                if (index > -1)
                    currentS.RemoveAt(index);
            }
        }

        private void checkPackage_Click(object sender, EventArgs e)
        {
            if (checkPackage.Checked)
            {
                currentS.Add(packagesS);
            }
            else
            {

                int index = currentS.FindIndex(t => packagesS == t);

                if (index > -1)
                    currentS.RemoveAt(index);
            }
        }

      
        private void checkOthers_Click(object sender, EventArgs e)
        {
            if (checkOthers.Checked)
            {
                currentS.Add(otherS);
            }
            else
            {

                int index = currentS.FindIndex(t => otherS == t);

                if (index > -1)
                    currentS.RemoveAt(index);
            }
        }

        private void checkLab_Click(object sender, EventArgs e)
        {
            if (checkLab.Checked)
            {
                currentS.Add(labS);
            }
            else
            {
                int index = currentS.FindIndex(t => labS == t);

                if (index > -1)
                    currentS.RemoveAt(index);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lblPatientID.Visible = false;
            tbSearchId.Visible = false;
            iconButton1.Visible = false;
            txtfn.Enabled = true;
            txtMn.Enabled = true;
            txtLn.Enabled = true;
            txtAddress.Enabled = true;
            txtCn.Enabled = true;
            cbGender.Enabled = true;
            cbStatus.Enabled = true;
            txtfn.Text = "";
            txtMn.Text = "";
            txtLn.Text = "";
            txtAddress.Text = "";
            txtCn.Text = "";
            txtAge.Text = "";
            tbSearchId.Text = "";


            dateTimePicker1.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            lblPatientID.Visible = true;
            tbSearchId.Visible = true;
            iconButton1.Visible = true;
            txtfn.Enabled = false;
            txtMn.Enabled = false;
            txtLn.Enabled = false;
            txtAddress.Enabled = false;
            txtCn.Enabled = false;
            cbGender.Enabled = false;
            cbStatus.Enabled = false;
            dateTimePicker1.Enabled = false;
        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            bool isNumber = int.TryParse(tbSearchId.Text.Trim(), out _);

            if (!isNumber)
                return;

            id = int.Parse(tbSearchId.Text.Trim());

            patientDetails details = await patientDetailsController.getPatientId(id);
            if (details.id == 0)
                return;

            txtfn.Text = details.Firstname;
            txtLn.Text = details.lastname;
            txtMn.Text = details.middlename;
            dateTimePicker1.Value = DateTime.Parse(details.birthdate);
            txtAge.Text = details.age.ToString();
            txtAddress.Text = details.address;
            txtCn.Text = details.contact;
            cbGender.Text = details.gender;
            cbStatus.Text = details.civil_status;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int yrNow = DateTime.Now.Year;
            int bdate = dateTimePicker1.Value.Year;
            int age = yrNow - bdate;
            txtAge.Text = age.ToString();
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            label11.Visible = true ;

            txtCompanyName.Visible = true;
            medCertType = 2;

            if (isEdit)
                txtCompanyName.Text = companyEdit;
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            label11.Visible = false;
            txtCompanyName.Text = "";
            txtCompanyName.Visible = false;
            medCertType = 1;
        }

       
    }
}
