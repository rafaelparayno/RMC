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

namespace RMC.Admin.PanelPharForms.Dialogs
{
    public partial class addEditItems : Form
    {
        SupplierController supplier = new SupplierController();
        UnitsController unitsC = new UnitsController();
        CategoryController category = new CategoryController();
        private int isBranded = 0;
        private bool isExpiration = false;
        private Dictionary<string, int> suppliersDic = new Dictionary<string, int>();
        public addEditItems()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addEditItems_Load(object sender, EventArgs e)
        {
            loadAllCb();
        }

        private void loadAllCb()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();

            foreach (KeyValuePair<string, int> item in StaticData.DataItemType)
            {
                cbItems.Add(new ComboBoxItem(item.Key, item.Value));
            }

            cbItemType.Items.AddRange(cbItems.ToArray());
            loadFromDbtoCb();

        }

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = supplier.getComboDatas();
            Task<List<ComboBoxItem>> task2 = unitsC.getComboDatas();
            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1, task2 };

            await Task.WhenAll(Cbs);

            cbUnits.Items.AddRange(task2.Result.ToArray());
            cbSuppliers.Items.AddRange(task1.Result.ToArray());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void cbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cbItem = int.Parse((cbItemType.SelectedItem as ComboBoxItem).Value.ToString());

            loadCbCat(cbItem);

            disableBrands(cbItem);

        }

        private async void loadCbCat(int itemType)
        {
            List<ComboBoxItem> cb = new List<ComboBoxItem>();
            cb = await category.getComboDatas(itemType);

            cbCategory.Items.Clear();
            cbCategory.Items.AddRange(cb.ToArray());
            cbCategory.Enabled = true;
        }

        private void disableBrands(int itemType)
        {
            if (itemType == 1)
            {
                gbBrands.Visible = true;
                isBranded = 0;
            }
            else
            {
                gbBrands.Visible = false;
            }
        }

        private void rbBrand_Click(object sender, EventArgs e)
        {
            if (rbBrand.Checked)
            {
                isBranded = 1;
            }
        }

        private void rbGeneric_MouseClick(object sender, MouseEventArgs e)
        {
            if (rbGeneric.Checked)
            {
                isBranded = 2;
            }
        }

        private void rbExpiration_Click(object sender, EventArgs e)
        {
            if (rbExpiration.Checked)
            {
                label15.Visible = true;
                dateExpiration.Visible = true;
                isExpiration = true;
            }
        }

        private void rbNone_Click(object sender, EventArgs e)
        {
            if (rbNone.Checked)
            {
                label15.Visible = false;
                dateExpiration.Visible = false;
                isExpiration = false;
            }
        }

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMarkup_KeyPress(object sender, KeyPressEventArgs e)
        {
            string validKeys = "0123456789.";
            if (validKeys.IndexOf(e.KeyChar) < 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSellingPrice_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void CalculateTxtSellingPrice(string unitPrice,string markup)
        {
            bool isUnitValid = float.TryParse(unitPrice, out _);
            bool isMarkupvalid = float.TryParse(markup, out _);

            if(isUnitValid && isMarkupvalid)
            {
                float unitP = float.Parse(unitPrice);
                float markupP = float.Parse(markup);

                double sellingPrice = unitP + markupP;

                txtSellingPrice.Text = Math.Round(sellingPrice, 2).ToString();
            }
            else
            {
                txtSellingPrice.Text = "";
            }
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateTxtSellingPrice(txtUnitPrice.Text.Trim(), txtMarkup.Text.Trim());
        }

        private void txtMarkup_TextChanged(object sender, EventArgs e)
        {
            CalculateTxtSellingPrice(txtUnitPrice.Text.Trim(), txtMarkup.Text.Trim());
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            if (cbSuppliers.SelectedIndex == -1)
                return;

            addListSupplier(cbSuppliers.Text, int.Parse((cbSuppliers.SelectedItem as ComboBoxItem).Value.ToString()));
        }

        private void btnRemoveSupplier_Click(object sender, EventArgs e)
        {
            if (listBoxSuppliers.Items.Count == 0)
                return;

            suppliersDic.Remove(listBoxSuppliers.SelectedItem.ToString());
            listBoxSuppliers.Items.RemoveAt(listBoxSuppliers.SelectedIndex);
        }

        private void addListSupplier(string supplierName,int supplierId)
        {
            if(suppliersDic.ContainsKey(supplierName))
            {
                MessageBox.Show("The List has already an Supplier With Same name");
                return;
            }

            suppliersDic.Add(supplierName, supplierId);

            listBoxSuppliers.Items.Add(supplierName);
        }
    }
}
