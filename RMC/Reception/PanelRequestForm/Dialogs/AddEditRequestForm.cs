using RMC.Database.Controllers;
using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class AddEditRequestForm : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        CustomerRequestsController customerRequestsController = new CustomerRequestsController();
        string gender = "Male";
        bool isEdit = false;
        int reqid = 0;
        List<int> currentS = new List<int>();
        List<int> editedS = new List<int>();

        private int consultS = 1;
        private int medCert = 2;
        private int labS = 3;
        private int xRayS = 4;
        private int packagesS = 5;
        private int medS = 6;
        private int otherS = 7;
       
   
        public AddEditRequestForm()
        {
            InitializeComponent();

        }

        public AddEditRequestForm(params string[] datas)
        {
            InitializeComponent();
            isEdit = true;
            setEditInitState(datas);
        }

        private void saveRequests()
        {
            foreach(int type in currentS)
            {
                customerRequestsController.newReq(type);
            }
        }

        private void updateRequets()
        {
            foreach (int type in currentS)
            {
                customerRequestsController.updateReq(reqid,type);
            }
        }

        private void removeRequestCostumer()
        {

            foreach (int id in idsToBeRemove())
            {
                customerRequestsController.remove(id, reqid);
            }
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
            }
            else
            {
                checkConsult.Checked = false;
            }
          
            if (typesid.Contains(medCert))
            {
                Medcert.Checked = true;
            }
            else
            {
                Medcert.Checked = false;
            }
           
            if (typesid.Contains(labS))
            {
                checkLab.Checked = true;
            }
            else
            {
                checkLab.Checked = false;
            }
        
            if (typesid.Contains(xRayS))
            {
                checkXray.Checked = true;
            }
            else
            {
                checkXray.Checked = false;
            }
     

  
            if (typesid.Contains(packagesS))
            {
                checkPackage.Checked = true;
            }
            else
            {
                checkPackage.Checked = false;
            }
      

            if (typesid.Contains(medS))
            {
                checkMeds.Checked = true;
            }
            else
            {
                checkMeds.Checked = false;
            }

            if (typesid.Contains(otherS))
            {
                checkOthers.Checked = true;
            }
            else
            {
                checkOthers.Checked = false;
            }
        }

        private async void setEditInitState(string [] datas)
        {
            label8.Text = "Edit Request";

            txtName.Text = datas[1];
            reqid = int.Parse(datas[0]);
            txtAge.Text = datas[2];
            txtcs.Text = datas[4];
            txtCpno.Text = datas[5];
            txtAddress.Text = datas[6];
            currentS = await customerRequestsController.getListTypeReq(reqid);
            editedS = await customerRequestsController.getListTypeReq(reqid);
            
            if (datas[3] == "Male")
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

            setCbsEditState(editedS);
        

        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Enter Valid Or Complete The Details Below");
                return;
            }

            if (isEdit)
            {
              
                removeRequestCostumer();
                customerDetailsController.update(txtName.Text.Trim(), txtAge.Text.Trim(),
                                           gender, txtcs.Text.Trim(), txtCpno.Text.Trim(),
                                           txtAddress.Text.Trim(),reqid.ToString());
                updateRequets();

            }
            else
            {
                customerDetailsController.save(txtName.Text.Trim(), txtAge.Text.Trim(),
                                           gender, txtcs.Text.Trim(), txtCpno.Text.Trim(),
                                           txtAddress.Text.Trim());
                saveRequests();
            }


      
            MessageBox.Show("SuccessFuly Added Request");
            this.Close();

        }

        private bool isValid()
        {
            errorProvider1.Clear();
            bool isValid = true;

            isValid = (txtName.Text.Trim() != "") && isValid;
            errorHandlingIsEmpty(ref txtName, "Please Enter Customer Name");

            isValid = (txtAge.Text.Trim() != "") && isValid;
            errorHandlingIsEmpty(ref txtAge, "Please Enter Customer age");


            isValid = (currentS.Count != 0) && isValid;
            errorHandlingList(ref groupBox8, "Please Select Atleast one");
            return isValid;
        }

        private void errorHandlingList(ref GroupBox gb, string ergMsg)
        {
            if (currentS.Count == 0)
            {
                errorProvider1.SetError(gb, ergMsg);
            }
        }

        private void errorHandlingIsEmpty(ref TextBox tb, string ergMsg)
        {
            if (tb.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(tb, ergMsg);
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked)
            {
                gender = "Male";
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked)
            {
                gender = "Female";
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
            }
            else
            {

                int index = currentS.FindIndex(t => consultS == t);

                if (index > -1)
                    currentS.RemoveAt(index);
            }
        }

        private void Medcert_Click(object sender, EventArgs e)
        {
            if (Medcert.Checked)
            {
                currentS.Add(medCert);
            }
            else
            {

                int index = currentS.FindIndex(t => medCert == t);

                if (index > -1)
                    currentS.RemoveAt(index);
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

        private void checkMeds_Click(object sender, EventArgs e)
        {
            if (checkMeds.Checked)
            {
                currentS.Add(medS);
            }
            else
            {

                int index = currentS.FindIndex(t => medS == t);

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
    }
}
