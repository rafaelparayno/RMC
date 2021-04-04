using RMC.Components;
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

namespace RMC.Admin.PackageDialog
{
    public partial class AddEditPackage : Form
    {


        #region Variables
        PackagesController packagesController = new PackagesController();
        LaboratoryController laboratoryController = new LaboratoryController();
        ServiceController serviceController = new ServiceController();
        XrayControllers xrayControllers = new XrayControllers();
        PackageLabController packageLabController = new PackageLabController();
        PackageXray packageXray = new PackageXray();
        PackageOthers packageOthers = new PackageOthers();


        List<int> idsRemoveInLabpack = new List<int>();
        List<int> idsRemoveInXraypack = new List<int>();
        List<int> idsRemoveInOtherspack = new List<int>();

        List<PackagesNames> packagesNamesLab = new List<PackagesNames>();
        List<PackagesNames> packagesNamesXray = new List<PackagesNames>();
        List<PackagesNames> packagesNamesOther = new List<PackagesNames>();


        int cbValueLab = 0;
        int cbValueService = 0;
        int cbValueXray = 0;
        int idPack = 0;
        bool isEdit = false;

        #endregion

        public AddEditPackage()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            loadFromDbtoCb();
            initColLv();
        }

        public AddEditPackage(params string[] data)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            loadFromDbtoCb();
            initColLv();
            initEditState(data);
        }


        #region OwnFunctions
        private async void getLvlabData(int id)
        {
            Task<List<PackagesNames>> task1 = packageLabController.getPackagesLab(id);
            Task<List<PackagesNames>> task2 = packageXray.getPackagesNames(id);
            Task<List<PackagesNames>> task3 = packageOthers.getPackagesNames(id);
            Task<List<PackagesNames>>[] lvsItems = new Task<List<PackagesNames>>[] { task1,task2, task3 };
            await Task.WhenAll(lvsItems);

            packagesNamesLab = task1.Result;
            packagesNamesXray = task2.Result;
            packagesNamesOther = task3.Result;
        }

        private void getInitStateLvs()
        {
            foreach(PackagesNames n in packagesNamesLab)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = n.id.ToString();
                lvItem.SubItems.Add(n.name);
                lvItem.SubItems.Add(n.price.ToString());

                lvLab.Items.Add(lvItem);
            }

            foreach (PackagesNames n in packagesNamesXray)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = n.id.ToString();
                lvItem.SubItems.Add(n.name);
                lvItem.SubItems.Add(n.price.ToString());

                lvXray.Items.Add(lvItem);
            }

            foreach (PackagesNames n in packagesNamesOther)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = n.id.ToString();
                lvItem.SubItems.Add(n.name);
                lvItem.SubItems.Add(n.price.ToString());

                lvService.Items.Add(lvItem);
            }

            AddSuggestedPrice();

        }

        private void initEditState(string [] data)
        {
            isEdit = true;
            idPack = int.Parse(data[0]);
            txtName.Text = data[1];
            txtPriceSave.Text = data[2];
            txtDesc.Text = data[3];

            getLvlabData(idPack);

            getInitStateLvs();
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


        private void saveLv()
        {
            foreach(ListViewItem lvitems in lvLab.Items)
            {
                int id = int.Parse(lvitems.SubItems[0].Text);
                packageLabController.save(id);
            }

            foreach (ListViewItem lvitems in lvXray.Items)
            {
                int id = int.Parse(lvitems.SubItems[0].Text);
                packageXray.save(id);
            }

            foreach (ListViewItem lvitems in lvService.Items)
            {
                int id = int.Parse(lvitems.SubItems[0].Text);
                packageOthers.save(id);
            }
        }

        private void saveLv(int packid)
        {
            foreach (ListViewItem lvitems in lvLab.Items)
            {
                int id = int.Parse(lvitems.SubItems[0].Text);
                packageLabController.save(id,packid);
            }

            foreach (ListViewItem lvitems in lvXray.Items)
            {
                int id = int.Parse(lvitems.SubItems[0].Text);
                packageXray.save(id,packid );
            }

            foreach (ListViewItem lvitems in lvService.Items)
            {
                int id = int.Parse(lvitems.SubItems[0].Text);
                packageOthers.save(id, packid);
            }
        }

        private void initlvIdRemove()
        {
            foreach(PackagesNames n in packagesNamesLab)
            {
                if (!lvLabFound(n.id))
                {
                    idsRemoveInLabpack.Add(n.id);
                }
            }

            foreach (PackagesNames n in packagesNamesXray)
            {
                if (!lvXrayFound(n.id))
                {
                    idsRemoveInXraypack.Add(n.id);
                }
            }


            foreach (PackagesNames n in packagesNamesOther)
            {
                if (!lvOthersFound(n.id))
                {
                    idsRemoveInOtherspack.Add(n.id);
                }
            }
        }

        private void removelv()
        {
            initlvIdRemove();
            foreach(int id in idsRemoveInLabpack)
            {
                packageLabController.remove(idPack, id);
            }

            foreach (int id in idsRemoveInXraypack)
            {
                packageXray.remove(idPack, id);
            }


            foreach (int id in idsRemoveInOtherspack)
            {
                packageOthers.remove(idPack, id);
            }
        }

        private bool lvLabFound(int id)
        {
            foreach (ListViewItem items in lvLab.Items)
            {
                if (id == int.Parse(items.SubItems[0].Text))
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


        private bool isValid()
        {
            errorProvider1.Clear();
            bool isValid = true;

            float _;

            isValid = (txtName.Text.Trim() != "") && isValid;
            isTextNull(ref txtName, "Please Fill the Field");
            isValid = (txtPriceSave.Text.Trim() != "") && isValid;
            isTextNull(ref txtPriceSave, "Please Fill the Field");

            isValid = (float.TryParse(txtPriceSave.Text.Trim(), out _)) && isValid;
            isFormatPriceCorrect(isValid, ref txtPriceSave, "Inccorect Number Format");


            return isValid;
        }

        #endregion


        #region Handlers
        private void isFormatPriceCorrect(bool isvalid, ref TextBox tb, string msg)
        {
            if (!isvalid)
            {
                errorProvider1.SetError(tb, msg);
            }
        }

        private void isTextNull(ref TextBox tb, string msg)
        {
            if (tb.Text == "")
            {
                errorProvider1.SetError(tb, msg);
            }
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

            if (isEdit)
            {
                removelv();
               
                packagesController.update(txtName.Text.Trim(), float.Parse(txtPriceSave.Text.Trim()), txtDesc.Text.Trim(),idPack);
                saveLv(idPack);
            }
            else
            {
             
                packagesController.save(txtName.Text.Trim(), float.Parse(txtPriceSave.Text.Trim()), txtDesc.Text.Trim());
                saveLv();
            }
            MessageBox.Show("Succesfully Save Data");
            this.Close();

        }

        private void txtPriceSave_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

    }
}
