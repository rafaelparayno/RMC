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

namespace RMC.Admin.PanelPharForms.Dialogs
{
    public partial class AddEditSuppliers : Form
    {

        SupplierController suppliers = new SupplierController();
        bool isEdit = false;
        private int id = 0;
        public AddEditSuppliers()
        {
            InitializeComponent();
        }


        public AddEditSuppliers(params string[] datas)
        {
            InitializeComponent();
            label1.Text = "Edit Suppliers";
            isEdit = true;
            id = int.Parse(datas[0]);
        
            txtSupplierName.Text = datas[1];
            txtSupplierNum.Text = datas[2];
            txtLocation.Text = datas[3];
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Complete Data");
                return;
            }


            if (isEdit)
            {
              
                suppliers.Edit(txtSupplierName.Text, txtSupplierNum.Text, txtLocation.Text, id);
            }
            else
            {
                suppliers.save(txtSupplierName.Text.Trim(), txtSupplierNum.Text.Trim(), txtLocation.Text);
            }
           

            MessageBox.Show("Succesfully Save Data");
            this.Close();
        }


        private bool isValid()
        {
            bool isValid = true;

            isValid = (txtLocation.Text.Trim() != "") && isValid;

            isValid = (txtSupplierName.Text.Trim() != "") && isValid;

            isValid = (txtSupplierNum.Text.Trim() != "") && isValid;


            return isValid;
        }

       
    }
}
