using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelPharForms
{
    public partial class Parameters : Form
    {
        private Form activeForm = null;
        public Parameters()
        {
            InitializeComponent();
          
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            openChildForm(new Suppliers());
        }
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();

         
            childForm.Show();
        }

        private void btnItemTypes_Click(object sender, EventArgs e)
        {
            openChildForm(new ItemType());
        }

        private void Parameters_Load(object sender, EventArgs e)
        {
            openChildForm(new Suppliers());
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            openChildForm(new ItemCategories());
        }
    }
}
