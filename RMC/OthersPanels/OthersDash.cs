using RMC.OthersPanels.panels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.OthersPanels
{
    public partial class OthersDash : Form
    {
        private Form activeForm = null;

        public OthersDash()
        {
            InitializeComponent();
        }


        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();


            childForm.Show();
        }


        private void btnQueue_Click(object sender, EventArgs e)
        {
            openChildForm(new othersQueue());
        }
    }
}
