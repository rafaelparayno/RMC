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

namespace RMC.Patients.PanelsDetails
{
    public partial class PanelPatientDetails : Form
    {
        int id = 0;
        private bool isSave = false;
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        public PanelPatientDetails(int id)
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = DateTime.Now;
            this.id = id;
            initEditState();
        }

        private async void initEditState()
        {
            if (id > 0)
            {
                patientDetails details = await patientDetailsController.getPatientId(id);
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
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int yrNow = DateTime.Now.Year;
            int bdate = dateTimePicker1.Value.Year;
            int age = yrNow - bdate;
            txtAge.Text = age.ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("PLease Complete Required Field");
                return;
            }

            if (isSave)
                return;

            if(id == 0)
            {
               await patientDetailsController.save(txtfn.Text.Trim(), txtMn.Text.Trim(),
                                              txtLn.Text.Trim(), dateTimePicker1.Value.ToString("yyyy/MM/dd"),
                                              txtAge.Text.Trim(), cbGender.SelectedItem.ToString(),txtCn.Text.Trim(),
                                              cbStatus.SelectedItem.ToString(), txtAddress.Text.Trim());
                isSave = true;
               
            }
            else
            {
                await patientDetailsController.update(txtfn.Text.Trim(), txtMn.Text.Trim(),
                                            txtLn.Text.Trim(), dateTimePicker1.Value.ToString("yyyy/MM/dd"),
                                            txtAge.Text.Trim(), cbGender.SelectedItem.ToString(), txtCn.Text.Trim(),
                                            cbStatus.SelectedItem.ToString(), txtAddress.Text.Trim(),id.ToString());
               
            }
            MessageBox.Show("Succesfully Save Data");
      

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


            return isValid;
        }

        private void setErrorCbNoValue(ref ComboBox cb,string msg)
        {
            if(cb.SelectedIndex == -1)
            {
                errorProvider1.SetError(cb, msg);
            }
        }

        private void SetErrorShowNoText(ref TextBox tb, string msg)
        {
            if(tb.Text == "")
            {
                errorProvider1.SetError(tb, msg);
            }
        }
    }
}
