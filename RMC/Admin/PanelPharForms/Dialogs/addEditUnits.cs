using RMC.Database.Controllers;
using System;

using System.Windows.Forms;

namespace RMC.Admin.PanelPharForms.Dialogs
{
    public partial class addEditUnits : Form
    {
        UnitsController unitsC = new UnitsController();
        bool isEdit = false;
        private int id = 0;
        public addEditUnits()
        {
            InitializeComponent();
        }

        public addEditUnits(string name,int id)
        {
            InitializeComponent();
            txtUnits.Text = name;
            isEdit = true;
            this.id = id;
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

                unitsC.edit(txtUnits.Text.Trim(), id);
                
            }
            else
            {
                unitsC.save(txtUnits.Text.Trim());
            }

            MessageBox.Show("Succesfully Save Data");
            this.Close();
        }

        private bool isValid()
        {
            bool isValid = true;

            isValid = (txtUnits.Text.Trim() != "") && isValid;

            return isValid;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
