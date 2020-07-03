using RMC.Admin.PanelPharForms.Dialogs;
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
    public partial class ItemList : Form
    {
        private Form activeForm = null;
        public ItemList()
        {
            InitializeComponent();
            openChildForm(new panelListItems());
        }

        private void ItemList_Load(object sender, EventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnViewItem_Click(object sender, EventArgs e)
        {
            openChildForm(new panelListItems());
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

        private void btnAddItem_Click_1(object sender, EventArgs e)
        {
            addEditItems frm = new addEditItems();
            frm.ShowDialog();

        }
    }
}
