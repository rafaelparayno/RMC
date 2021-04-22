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

namespace RMC.Admin.PanelLabForms.Dialogs
{
    public partial class AddEditService : Form
    {

        #region Variables
        ItemController itemz = new ItemController();
        ServiceController serviceController = new ServiceController();
        ConsumablesServController consumablesServController = new ConsumablesServController();
        int cbConValue = 0;
        bool isEdit = false;
        int idService = 0;
        List<int> idstobeRemove = new List<int>();
        List<consumablesServMod> consumablesModsEdit;

        #endregion

        public AddEditService()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            loadFromDbtoCb();
            initColLv();
        }

        public AddEditService(params string[] data)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            loadFromDbtoCb();
            initColLv();
            isEdit = true;
            setInitEditData(data);
        }

        #region Own Function

        private async void setInitEditData(string[] datas)
        {
            label8.Text = "Edit Service";
            idService = int.Parse(datas[0]);
            txtName.Text = datas[1];
            txtDesc.Text = datas[2];
            txtsSellingPrice.Text = datas[3];
            consumablesModsEdit = new List<consumablesServMod>();
            consumablesModsEdit = await consumablesServController.getEditedConsumables(idService);

            setLvEdited();
            txtCost.Text = await computeTotalCost() + "";
        }

        private void setLvEdited()
        {
            foreach (consumablesServMod con in consumablesModsEdit)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = con.itemid.ToString();
                lvItem.SubItems.Add(con.itemname);
                lvItem.SubItems.Add(con.qty.ToString());
                lvConsumables.Items.Add(lvItem);
            }
        }

        private void saveConsumables()
        {
            foreach (ListViewItem lvitems in lvConsumables.Items)
            {
                int itemid = int.Parse(lvitems.SubItems[0].Text);
                int qty = int.Parse(lvitems.SubItems[2].Text);

                consumablesServController.save(itemid, qty);
            }
        }

        private List<int> getRemoveId()
        {
            List<int> idRemove = new List<int>();

            foreach (consumablesServMod mod in consumablesModsEdit)
            {
                if (!isFoundItem(mod.itemid))
                {
                    idRemove.Add(mod.itemid);
                }
            }

            return idRemove;
        }

        private void removeConsumables(List<int> ids)
        {
            foreach (int itemId in ids)
            {
                consumablesServController.remove(itemId, idService);
            }
        }

        private void updateConsumables()
        {
            foreach (ListViewItem items in lvConsumables.Items)
            {
                int itemid = int.Parse(items.SubItems[0].Text);
                int qty = int.Parse(items.SubItems[2].Text);
                consumablesServController.update(idService, itemid, qty);
            }
        }

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = itemz.getComboDatas();

            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1 };

            await Task.WhenAll(Cbs);

            cbConsumables.Items.AddRange(task1.Result.ToArray());

        }

        private void addTolist(int qty)
        {
            ListViewItem lvItem = new ListViewItem();
            lvItem.Text = cbConValue.ToString();
            lvItem.SubItems.Add(cbConsumables.Text);
            lvItem.SubItems.Add(qty + "");
            lvConsumables.Items.Add(lvItem);
        }

        private bool isFoundItem(int id)
        {
            foreach (ListViewItem lvs in lvConsumables.Items)
            {
                if (int.Parse(lvs.SubItems[0].Text) == id)
                {
                    return true;
                }
            }

            return false;
        }

        private void initColLv()
        {
            lvConsumables.View = View.Details;
            lvConsumables.Columns.Add("Item Id", 150, HorizontalAlignment.Right);
            lvConsumables.Columns.Add("Item Name", 150, HorizontalAlignment.Right);
            lvConsumables.Columns.Add("Quantity", 150, HorizontalAlignment.Right);
        }

        private void updateList(int qty)
        {
            ListViewItem item = lvConsumables.FindItemWithText(cbConValue + "");
            int indexLv = lvConsumables.Items.IndexOf(item);
            int currentQty = int.Parse(lvConsumables.Items[indexLv].SubItems[2].Text);
            int updatedQty = currentQty + qty;
            lvConsumables.Items[indexLv].SubItems[2].Text = updatedQty + "";
        }


        private bool isValid()
        {
            errorProvider1.Clear();
            bool isValid = true;

            float _;

            isValid = (txtName.Text.Trim() != "" && txtName.Text != null) && isValid;
            errorHandlingIsEmpty(ref txtName, "Please Enter name");

            isValid = (float.TryParse(txtsSellingPrice.Text.Trim(), out _)) && isValid;
            numberFormatIsCorret(isValid, ref txtsSellingPrice, "Number Format is Incorrect");

            isValid = (txtsSellingPrice.Text != "") && isValid;
            errorHandlingIsEmpty(ref txtsSellingPrice, "Please Enter Price");


            return isValid;
        }

        private void errorHandlingIsEmpty(ref TextBox tb, string ergMsg)
        {
            if (tb.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(tb, ergMsg);
            }
        }

        private void numberFormatIsCorret(bool isTrue, ref TextBox tb, string errMsg)
        {
            if (!isTrue)
            {
                errorProvider1.SetError(tb, errMsg);
            }
        }

        private async Task<float> computeTotalCost()
        {
            float totalLabCost = 0;
            foreach (ListViewItem lv in lvConsumables.Items)
            {
                int id = int.Parse(lv.SubItems[0].Text);
                float unitCost = await itemz.getUnitCosts(id);
                int qty = int.Parse(lv.SubItems[2].Text);
                totalLabCost += unitCost * qty;
            }
            //  Console.WriteLine(totalLabCost);
            return totalLabCost;
        }

        #endregion


        #region Handler Events


        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private async void btnAddConsum_Click(object sender, EventArgs e)
        {
            if (cbConValue == 0)
                return;

            AddQtyConsumables form = new AddQtyConsumables();
            form.ShowDialog();

            if (form.qty == 0)
                return;

            if (isFoundItem(cbConValue))
            {
                updateList(form.qty);
            }
            else
            {
                addTolist(form.qty);
            }
            txtCost.Text = await computeTotalCost() + "";
        }

        private void cbConsumables_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbConValue = int.Parse((cbConsumables.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private async void btnRemoveConsum_Click(object sender, EventArgs e)
        {
            if (lvConsumables.Items.Count == 0)
                return;

            if (lvConsumables.SelectedItems.Count == 0)
                return;

            int index = lvConsumables.SelectedItems[0].Index;
            lvConsumables.Items.RemoveAt(index);
            txtCost.Text = await computeTotalCost() + "";
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Complete The Required Field", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isEdit)
            {
                idstobeRemove = getRemoveId();
                removeConsumables(idstobeRemove);
                updateConsumables();
                serviceController.update(idService, txtName.Text.ToString(),
                        txtDesc.Text.ToString(), float.Parse(txtsSellingPrice.Text));
            }
            else
            {
                serviceController.save(txtName.Text.Trim(), 
                    txtDesc.Text.Trim(), float.Parse(txtsSellingPrice.Text));
                saveConsumables();
            }

            MessageBox.Show("Succesfully Save Data");
            this.Close();

        }

        private void txtsSellingPrice_KeyPress(object sender, KeyPressEventArgs e)
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
