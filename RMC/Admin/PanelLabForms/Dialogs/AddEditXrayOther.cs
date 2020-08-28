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
    public partial class AddEditXrayOther : Form
    {

        ItemController itemz = new ItemController();
        XrayControllers xrayControllers = new XrayControllers();
        ConsumablesXrayControllers consumablesXrayController = new ConsumablesXrayControllers();
        AutoDocsController autoDocsController = new AutoDocsController();
        int cbTypeValue = 0;
        int cbConValue = 0;
        bool isEdit = false;
        bool isAuto = true;
        int xrayId = 0;
        int cbAutoValue = 0;
        List<int> idstobeRemove = new List<int>();
        List<consumablesMod> consumablesModsEdit;

        public AddEditXrayOther()
        {
            InitializeComponent();
            initColLv();
            loadAllCb();
        }

        public AddEditXrayOther(params string[] data)
        {
            InitializeComponent();
            initColLv();
            loadAllCb();
            setEditState(data);
        }

        private async void setEditState(string [] data)
        {
            isEdit = true;
            xrayId = int.Parse(data[0]);
            txtName.Text = data[1];
            txtDesc.Text = data[3];
            txtSellingPrice.Text = data[4];
            cbType.Text = data[2];

            consumablesModsEdit = new List<consumablesMod>();
            consumablesModsEdit = await consumablesXrayController.getEditedConsumables(xrayId);

            if (data[5] == "")
            {
                rbNone.Checked = true;

            }
            else
            {
                rbWithAuto.Checked = true;
                cbAutomated.Text = data[5];
            }

            setLvEdited();
        }

        private void setLvEdited()
        {
            foreach (consumablesMod con in consumablesModsEdit)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = con.itemid.ToString();
                lvItem.SubItems.Add(con.itemname);
                lvItem.SubItems.Add(con.qty.ToString());
                lvConsumables.Items.Add(lvItem);
            }
        }


        private async void getImgPath()
        {
            string fullPath = await autoDocsController.getFullPath(cbAutoValue);
            if (fullPath != "")
            {
                pbAutomated.Image = Image.FromFile(fullPath);
            }

        }



        private void saveConsumables()
        {
            foreach (ListViewItem lvitems in lvConsumables.Items)
            {
                int itemid = int.Parse(lvitems.SubItems[0].Text);
                int qty = int.Parse(lvitems.SubItems[2].Text);

                  consumablesXrayController.save(itemid, qty);
            }
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadAllCb()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();

            foreach (KeyValuePair<string, int> item in StaticData.XrayTypes)
            {
                cbItems.Add(new ComboBoxItem(item.Key, item.Value));
            }

            cbType.Items.AddRange(cbItems.ToArray());
            loadFromDbtoCb();

        }

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = itemz.getComboDatas();
            Task<List<ComboBoxItem>> task2 = autoDocsController.getComboDatas();

            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1,task2 };

            await Task.WhenAll(Cbs);

            cbConsumables.Items.AddRange(task1.Result.ToArray());
            cbAutomated.Items.AddRange(task2.Result.ToArray());

        }

        private List<int> getRemoveId()
        {
            List<int> idRemove = new List<int>();

            foreach (consumablesMod mod in consumablesModsEdit)
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
                consumablesXrayController.remove(itemId, xrayId);
            }
        }

        private void updateConsumables()
        {
            foreach (ListViewItem items in lvConsumables.Items)
            {
                int itemid = int.Parse(items.SubItems[0].Text);
                int qty = int.Parse(items.SubItems[2].Text);
                consumablesXrayController.update(xrayId, itemid, qty);
            }
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

        private void addTolist(int qty)
        {
            ListViewItem lvItem = new ListViewItem();
            lvItem.Text = cbConValue.ToString();
            lvItem.SubItems.Add(cbConsumables.Text);
            lvItem.SubItems.Add(qty + "");
            lvConsumables.Items.Add(lvItem);
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

            isValid = (float.TryParse(txtSellingPrice.Text.Trim(), out _)) && isValid;
            numberFormatIsCorret(isValid, ref txtSellingPrice, "Number Format is Incorrect");

            isValid = (txtSellingPrice.Text != "") && isValid;
            errorHandlingIsEmpty(ref txtSellingPrice, "Please Enter Price");

            isValid = (cbTypeValue != 0) && isValid;
            errorCbType(ref cbType, "Please Select Type");

            return isValid;
        }

        private void errorHandlingIsEmpty(ref TextBox tb, string ergMsg)
        {
            if (tb.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(tb, ergMsg);
            }
        }

        private void errorCbType( ref ComboBox cb, string errMsg)
        {
            if (cbTypeValue == 0)
            {
                errorProvider1.SetError(cb, errMsg);
            }
        }

        private void numberFormatIsCorret(bool isTrue, ref TextBox tb, string errMsg)
        {
            if (!isTrue)
            {
                errorProvider1.SetError(tb, errMsg);
            }
        }

        private void btnAddConsum_Click(object sender, EventArgs e)
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
        }

        private void btnRemoveConsum_Click(object sender, EventArgs e)
        {
            if (lvConsumables.Items.Count == 0)
                return;

            if (lvConsumables.SelectedItems.Count == 0)
                return;

            int index = lvConsumables.SelectedItems[0].Index;
            lvConsumables.Items.RemoveAt(index);
        }

        private void cbConsumables_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbConValue = int.Parse((cbConsumables.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTypeValue = int.Parse((cbType.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void txtSellingPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
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
                xrayControllers.update(xrayId, txtName.Text.Trim(), txtDesc.Text.Trim(), 
                                        cbTypeValue, float.Parse(txtSellingPrice.Text.Trim()),cbAutoValue, isAuto);
               
            }
            else
            {
                xrayControllers.save(txtName.Text.Trim(), txtDesc.Text.Trim(), 
                                    cbTypeValue, float.Parse(txtSellingPrice.Text.Trim()),cbAutoValue,isAuto);
                saveConsumables();
            }
            MessageBox.Show("Succesfully Save Data");
            this.Close();
        }

        private void rbWithAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWithAuto.Checked)
            {
                isAuto = true;
                panelForImg.Visible = true;
                cbAutomated.Visible = true;
                label11.Visible = true;
            }
        }

        private void rbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNone.Checked)
            {
                isAuto = false;
                panelForImg.Visible = false;
                cbAutomated.Visible = false;
                label11.Visible = false;
            }
        }

        private void cbAutomated_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbAutoValue = int.Parse((cbAutomated.SelectedItem as ComboBoxItem).Value.ToString());

            getImgPath();
        }
    }
}
