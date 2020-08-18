using RMC.Database.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class AddEditRequestForm : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        string gender = "Male";
        bool isEdit = false;
        int reqid = 0;
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

        private void setEditInitState(string [] datas)
        {
            label8.Text = "Edit Request";

            txtName.Text = datas[1];
            reqid = int.Parse(datas[0]);
            txtAge.Text = datas[2];
            txtcs.Text = datas[4];
            txtCpno.Text = datas[5];
            txtAddress.Text = datas[6];
            if(datas[3] == "Male")
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

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
               customerDetailsController.update(txtName.Text.Trim(), txtAge.Text.Trim(),
                                           gender, txtcs.Text.Trim(), txtCpno.Text.Trim(),
                                           txtAddress.Text.Trim(),reqid.ToString());
            }
            else
            {
                customerDetailsController.save(txtName.Text.Trim(), txtAge.Text.Trim(),
                                           gender, txtcs.Text.Trim(), txtCpno.Text.Trim(),
                                           txtAddress.Text.Trim());
            }

            MessageBox.Show("SuccessFuly Added Request");
            this.Close();

        }

        private bool isValid()
        {
            bool isValid = true;

            isValid = (txtName.Text.Trim() != "") && isValid;


            isValid = (txtAge.Text.Trim() != "") && isValid;

            return isValid;
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
    }
}
