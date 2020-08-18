﻿using RMC.Components;
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
    public partial class AddEditLab : Form
    {

        #region Vars
        ItemController itemz = new ItemController();
        AutoDocsController autoDocsController = new AutoDocsController();
        LabTypeController labTypeController = new LabTypeController();
        LaboratoryController laboratoryController = new LaboratoryController();
        bool isAuto = true;
        bool isEdit = false;
        int cbAutoValue = 0;
        int cbLabTypeValue = 0;
        int cbConValue = 0;
        int id = 0;
        #endregion 

        public AddEditLab()
        {
            InitializeComponent();
            loadFromDbtoCb();
            initColLv();
            this.DoubleBuffered = true;
        }


        public AddEditLab(params string[] datas)
        {
            InitializeComponent();
            loadFromDbtoCb();
            initColLv();
            this.DoubleBuffered = true;
            setEditState(datas);
        }


        #region Own Functions
        private void updateList(int qty)
        {
            ListViewItem item = lvConsumables.FindItemWithText(cbConValue + "");
            int indexLv = lvConsumables.Items.IndexOf(item);
            int currentQty = int.Parse(lvConsumables.Items[indexLv].SubItems[2].Text);
            int updatedQty = currentQty + qty;
            lvConsumables.Items[indexLv].SubItems[2].Text = updatedQty + "";
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

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = itemz.getComboDatas();
            Task<List<ComboBoxItem>> task2 = autoDocsController.getComboDatas();
            Task<List<ComboBoxItem>> task3 = labTypeController.getComboDatas();
            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1, task2, task3 };

            await Task.WhenAll(Cbs);

            cbConsumables.Items.AddRange(task1.Result.ToArray());
            cbAutomated.Items.AddRange(task2.Result.ToArray());
            cbLabType.Items.AddRange(task3.Result.ToArray());

        }

        private void initColLv()
        {
            lvConsumables.View = View.Details;
            lvConsumables.Columns.Add("Item Id", 150, HorizontalAlignment.Right);
            lvConsumables.Columns.Add("Item Name", 150, HorizontalAlignment.Right);
            lvConsumables.Columns.Add("Quantity", 150, HorizontalAlignment.Right);
        }

        private async void getImgPath()
        {
            string fullPath = await autoDocsController.getFullPath(cbAutoValue);
            if (fullPath != "")
            {
                pbAutomated.Image = Image.FromFile(fullPath);
            }

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

            isValid = (cbLabTypeValue != 0) && isValid;
            errrorCbLabType(isValid, ref cbLabType, "Please Choose a Lab Type");

            return isValid;
        }

        private void errrorCbLabType(bool valid, ref ComboBox cb, string erMsg)
        {
            if (!valid)
            {
                errorProvider1.SetError(cb, erMsg);
            }
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

        private void setEditState(string [] datas)
        {
            label8.Text = "Edit Laboratory";
            isEdit = true;
            id = int.Parse(datas[0]);
            txtName.Text = datas[1];
            txtDesc.Text = datas[2];
            txtSellingPrice.Text = datas[3];
            cbLabType.Text = datas[4];
            if (datas[5] == "")
            {
                rbNone.Checked = true;

            }
            else
            {
                rbWithAuto.Checked = true;
                cbAutomated.Text = datas[5];
            }
        
        }

        #endregion

        #region HandlerEvents
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
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

        private void cbConsumables_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbConValue = int.Parse((cbConsumables.SelectedItem as ComboBoxItem).Value.ToString());
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Complete The Required Field","Validation" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isEdit)
            {
                laboratoryController.save(txtName.Text.Trim(), txtDesc.Text.Trim(),
                  cbLabTypeValue.ToString(), cbAutoValue.ToString(), isAuto.ToString(), txtSellingPrice.Text.Trim(),id.ToString());
            }
            else
            {
                laboratoryController.save(txtName.Text.Trim(), txtDesc.Text.Trim(), 
                    cbLabTypeValue.ToString(), cbAutoValue.ToString(), isAuto.ToString(),txtSellingPrice.Text.Trim());
            }
            MessageBox.Show("Succesfully Save Laboratory");
            this.Close();
        }


        private void txtSellingPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbAutomated_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            cbAutoValue = int.Parse((cbAutomated.SelectedItem as ComboBoxItem).Value.ToString());

            getImgPath();
        }

        private void cbLabType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbLabTypeValue = int.Parse((cbLabType.SelectedItem as ComboBoxItem).Value.ToString());
        }
        #endregion

    }
}
