using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Reception.PanelRequestForm.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception.PanelRequestForm
{
    public partial class PanelRequestForm : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        List<customerDetailsMod> customerDetailsModsList;
        public PanelRequestForm()
        {
            InitializeComponent();
            initLvCol();
            getData();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditRequestForm form = new AddEditRequestForm();
            form.ShowDialog();
            getData();
        }

        private void initLvCol()
        {
            lvCustomerDetails.View = View.Details;
            lvCustomerDetails.Columns.Add("QUEUE NO", 150, HorizontalAlignment.Left);
            lvCustomerDetails.Columns.Add("Name", 200, HorizontalAlignment.Left);
            lvCustomerDetails.Columns.Add("age", 80, HorizontalAlignment.Left);
            lvCustomerDetails.Columns.Add("gender", 100, HorizontalAlignment.Left);
            lvCustomerDetails.Columns.Add("CS", 100, HorizontalAlignment.Left);
            lvCustomerDetails.Columns.Add("CP# no", 100, HorizontalAlignment.Left);
            lvCustomerDetails.Columns.Add("Address", 200, HorizontalAlignment.Left);
        }

        private async void getData()
        {
            lvCustomerDetails.Items.Clear();
            customerDetailsModsList = new List<customerDetailsMod>() ;

            customerDetailsModsList = await customerDetailsController.getDetailsList();
            RefreshGrid(customerDetailsModsList);
          
        }

        private void RefreshGrid(List<customerDetailsMod> customers)
        {
            foreach (customerDetailsMod c in customers)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = c.id.ToString();
                lvItem.SubItems.Add(c.name);
                lvItem.SubItems.Add(c.age.ToString());
                lvItem.SubItems.Add(c.gender);
                lvItem.SubItems.Add(c.cs);
                lvItem.SubItems.Add(c.cp_no);
                lvItem.SubItems.Add(c.address);

                lvCustomerDetails.Items.Add(lvItem);
            }
        }

        private void btnNextReq_Click(object sender, EventArgs e)
        {
            ReceptionPayment form = new ReceptionPayment();
            form.ShowDialog();

            /*customerDetailsController.nextQueue();*/
            getData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvCustomerDetails.Items.Count == 0)
                return;

            if (lvCustomerDetails.SelectedItems.Count == 0)
                return;

            AddEditRequestForm form = new AddEditRequestForm(lvCustomerDetails.SelectedItems[0].SubItems[0].Text,
                                                            lvCustomerDetails.SelectedItems[0].SubItems[1].Text,
                                                            lvCustomerDetails.SelectedItems[0].SubItems[2].Text,
                                                            lvCustomerDetails.SelectedItems[0].SubItems[3].Text,
                                                            lvCustomerDetails.SelectedItems[0].SubItems[4].Text,
                                                            lvCustomerDetails.SelectedItems[0].SubItems[5].Text,
                                                            lvCustomerDetails.SelectedItems[0].SubItems[6].Text);
            form.ShowDialog();
            getData();
        }
    }
}
