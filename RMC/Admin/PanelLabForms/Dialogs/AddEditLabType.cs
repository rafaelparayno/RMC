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

namespace RMC.Admin.PanelLabForms.Dialogs
{
    public partial class AddEditLabType : Form
    {
        LabTypeController labTypeController = new LabTypeController();
        int id = 0;
        bool isEdit = false;
        public AddEditLabType()
        {
            InitializeComponent();
        }

        public AddEditLabType(int id,string name)
        {
            InitializeComponent();
            this.id = id;
            isEdit = true;
            txtName.Text = name;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
                return;


            if (isEdit)
            {
                labTypeController.update(id, txtName.Text.Trim());
            }
            else
            {
                labTypeController.save(txtName.Text.Trim());
            }


            MessageBox.Show("Succesfully Added Lab type");
            this.Close();

        }
    }
}
