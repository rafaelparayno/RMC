using RMC.Components;
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

namespace RMC.Admin.PackageDialog
{
    public partial class AddEditPackage : Form
    {
        LaboratoryController laboratoryController = new LaboratoryController();
        ServiceController serviceController = new ServiceController();
        XrayControllers xrayControllers = new XrayControllers();
        int cbValueLab = 0;
        int cbValueService = 0;
        int cbValueXray = 0;
        int total = 0;
      
        public AddEditPackage()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            loadFromDbtoCb();
            initColLv();
        }


        private void AddSuggestedPrice()
        {
            float totalPrice = 0;


            foreach(ListViewItem item in lvLab.Items)
            {
                totalPrice += float.Parse(item.SubItems[2].Text);
            }

            foreach (ListViewItem item in lvXray.Items)
            {
                totalPrice += float.Parse(item.SubItems[2].Text);
            }

            foreach (ListViewItem item in lvService.Items)
            {
                totalPrice += float.Parse(item.SubItems[2].Text);
            }

            txtSellingPrice.Text = "" + totalPrice;

        }

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = laboratoryController.getComboDatas();
            Task<List<ComboBoxItem>> task2 = xrayControllers.getComboDatas();
            Task<List<ComboBoxItem>> task3 = serviceController.getComboDatas();
            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1,task2,task3 };

            await Task.WhenAll(Cbs);

            cbLab.Items.AddRange(task1.Result.ToArray());
            cbXray.Items.AddRange(task2.Result.ToArray());
            cbOther.Items.AddRange(task3.Result.ToArray());

        }

        private void cbLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValueLab = int.Parse((cbLab.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void cbXray_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValueXray = int.Parse((cbXray.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void cbOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValueService = int.Parse((cbOther.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void initColLv()
        {
            lvLab.View = View.Details;
            lvLab.Columns.Add("Lab Id", 150, HorizontalAlignment.Right);
            lvLab.Columns.Add("Lab Name", 200, HorizontalAlignment.Right);
            lvLab.Columns.Add("Price", 150, HorizontalAlignment.Right);

            lvXray.View = View.Details;
            lvXray.Columns.Add("Id", 150, HorizontalAlignment.Right);
            lvXray.Columns.Add("Name", 200, HorizontalAlignment.Right);
            lvXray.Columns.Add("Price", 150, HorizontalAlignment.Right);

            lvService.View = View.Details;
            lvService.Columns.Add("Services Id", 150, HorizontalAlignment.Right);
            lvService.Columns.Add("Services Name", 200, HorizontalAlignment.Right);
            lvService.Columns.Add("Price", 150, HorizontalAlignment.Right);
        }

        private async void btnAddLab_Click(object sender, EventArgs e)
        {
            if (cbValueLab == 0)
                return;

          

            if (!lvLabFound(cbValueLab))
            {

                float price = await laboratoryController.getPrice(cbValueLab);
                ListViewItem lvitems = new ListViewItem();
                lvitems.Text = cbValueLab.ToString();
                lvitems.SubItems.Add(cbLab.Text);
                lvitems.SubItems.Add(price.ToString());


                lvLab.Items.Add(lvitems);

                AddSuggestedPrice();
            }

          
        }

        private async void btnAddXray_Click(object sender, EventArgs e)
        {
            if (cbValueXray == 0)
                return;

           


            if (!lvXrayFound(cbValueXray))
            {

                float price = await xrayControllers.getPrice(cbValueXray);


                ListViewItem lvitems = new ListViewItem();
                lvitems.Text = cbValueXray.ToString();
                lvitems.SubItems.Add(cbXray.Text);
                lvitems.SubItems.Add(price.ToString());


                lvXray.Items.Add(lvitems);

                AddSuggestedPrice();
            }

          
        }

        private async void btnAddOther_Click(object sender, EventArgs e)
        {
            if (cbValueService == 0)
                return;

            

            if (!lvOthersFound(cbValueService))
            {
                float price = await serviceController.getPrice(cbValueService);

                ListViewItem lvitems = new ListViewItem();
                lvitems.Text = cbValueService.ToString();
                lvitems.SubItems.Add(cbOther.Text);
                lvitems.SubItems.Add(price.ToString());

                lvService.Items.Add(lvitems);

                AddSuggestedPrice();
            }
        }

        private bool lvLabFound(int id)
        {
            foreach(ListViewItem items in lvLab.Items)
            {
                if(id == int.Parse(items.SubItems[0].Text))
                {
                    return true;
                }
            }
            return false;
        }


        private bool lvXrayFound(int id)
        {
            foreach (ListViewItem items in lvXray.Items)
            {
                if (id == int.Parse(items.SubItems[0].Text))
                {
                    return true;
                }
            }
            return false;
        }


        private bool lvOthersFound(int id)
        {
            foreach (ListViewItem items in lvService.Items)
            {
                if (id == int.Parse(items.SubItems[0].Text))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnRemoveLab_Click(object sender, EventArgs e)
        {
            if (lvLab.Items.Count == 0)
                return;

            if (lvLab.SelectedItems.Count == 0)
                return;

            int index = lvLab.SelectedItems[0].Index;

            lvLab.Items[index].Remove();

            AddSuggestedPrice();
        }

        private void btnRemoveXray_Click(object sender, EventArgs e)
        {
            if (lvXray.Items.Count == 0)
                return;

            if (lvXray.SelectedItems.Count == 0)
                return;

            int index = lvXray.SelectedItems[0].Index;

            lvXray.Items[index].Remove();

            AddSuggestedPrice();
        }

        private void btnRemoveOther_Click(object sender, EventArgs e)
        {
            if (lvService.Items.Count == 0)
                return;

            if (lvService.SelectedItems.Count == 0)
                return;

            int index = lvService.SelectedItems[0].Index;

            lvService.Items[index].Remove();


            AddSuggestedPrice();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Complete the required field");
                return;
            }
        }

        private bool isValid()
        {
            errorProvider1.Clear();
            bool isValid = true;

            isValid = (txtName.Text.Trim() != "") && isValid;
            isTextNull(ref txtName, "Please Fill the Field")
;
            isValid = (txtPriceSave.Text.Trim() != "") && isValid;
            isTextNull(ref txtPriceSave, "Please Fill the Field")
;

            return isValid;
        }

        private void isTextNull(ref TextBox tb,string msg)
        {
            if(tb.Text == "")
            {
                errorProvider1.SetError(tb, msg);
            }
        }


    }
}
