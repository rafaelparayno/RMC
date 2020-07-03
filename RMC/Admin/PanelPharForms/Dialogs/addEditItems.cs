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
        ItemController items = new ItemController();
        SupplierController supplier = new SupplierController();
        UnitsController unitsC = new UnitsController();
        CategoryController category = new CategoryController();
        private int isBranded = 0;
        private bool isExpiration = false;
        private int recentId = 0;
        private int Catid = 0;
        private int cbItem = 0;
        private int unitID = 0;
        private bool isEdit = false;
        private int ITEM_ID = 0;
        private Dictionary<string, int> suppliersDic = new Dictionary<string, int>();
        public addEditItems()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            recentId = items.getRecentItemID();
        
        }

        public addEditItems(params string[] datas)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            recentId = items.getRecentItemID();
            isEdit = true;
            ITEM_ID = int.Parse(datas[0]);
        }



        #region Events
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addEditItems_Load(object sender, EventArgs e)
        {
            loadAllCb();
        }



     

        private void cbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbItem = int.Parse((cbItemType.SelectedItem as ComboBoxItem).Value.ToString());
            Catid = 0;
            loadCbCat(cbItem);

            disableBrands(cbItem);


            setUpSKU(cbItem, Catid);

            
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


        private void cbUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            unitID = int.Parse((cbUnits.SelectedItem as ComboBoxItem).Value.ToString());
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

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Catid = int.Parse((cbCategory.SelectedItem as ComboBoxItem).Value.ToString());
            setUpSKU(cbItem, Catid);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Complete Required Field");
                return;
            }

            if (isEdit)
            {

            }
            else
            {
                items.Save(txtName.Text.Trim(), txtUnitPrice.Text.Trim(),
                           txtMarkup.Text.Trim(), txtSellingPrice.Text.Trim(),
                           dateExpiration.Value.ToString(), DateTime.Today.ToString(),
                           txtSku.Text.Trim(),txtDesc.Text.Trim(), isBranded.ToString(),
                           Catid.ToString(),unitID.ToString());
                
            }
            this.Close();
        }

        #endregion


        #region MyHandlers
        private void addListSupplier(string supplierName, int supplierId)
        {
            if (suppliersDic.ContainsKey(supplierName))
            {
                MessageBox.Show("The List has already an Supplier With Same name");
                return;
            }

            suppliersDic.Add(supplierName, supplierId);

            listBoxSuppliers.Items.Add(supplierName);
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


        private async void loadCbCat(int itemType)
        {
            List<ComboBoxItem> cb = new List<ComboBoxItem>();
            cb = await category.getComboDatas(itemType);

            cbCategory.Items.Clear();
            cbCategory.Items.AddRange(cb.ToArray());
            cbCategory.Enabled = true;
        }


        private void CalculateTxtSellingPrice(string unitPrice, string markup)
        {
            bool isUnitValid = float.TryParse(unitPrice, out _);
            bool isMarkupvalid = float.TryParse(markup, out _);

            if (isUnitValid && isMarkupvalid)
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

        private void setUpSKU(int itemType,int catId)
        {
            if (itemType == 0)
            {
                txtSku.Text = "";
                return;
            }

            if (catId == 0)
            {
                txtSku.Text = "";
                return;
            }


            txtSku.Text =  itemType+Catid.ToString() + fixID(recentId);
    
        }


        private string fixID(int lastid)
        {
            string id = "";
            if (lastid < 10)
            {
                id = "000" + lastid;
            }
            else if (lastid >= 10 && lastid < 100)
            {
                id = "00" + lastid;
            }
            else if (lastid > 100 && lastid < 1000)
            {
                id = "0" + lastid;
            }
            else if (lastid > 999)
            {
                id = lastid + "";
            }
            return id;
        }

        private bool isValid()
        {
            errorProvider1.Clear();
            bool isValid = true;

            isValid = (txtName.Text.Trim() != "" && txtName.Text != null) && isValid;
            errorHandlingIsEmpty(ref txtName, "Please Enter name");
         
            isValid = (unitID != 0) && isValid;
            setComboValid(ref cbUnits, "Please Pick A Units of Measurement");
            isValid = (Catid != 0) && isValid;
            setComboValid(ref cbCategory, "Please Pick A Category");

            isValid = (txtSku.Text != "") && isValid;
            errorHandlingIsEmpty(ref txtSellingPrice, "Please Enter Pick Item Type and Category");

            isValid = (txtSellingPrice.Text != "") && isValid;
            errorHandlingIsEmpty(ref txtSellingPrice, "Please Enter UnitPrice AND Markup Price");

            isValid = (listBoxSuppliers.Items.Count > 0) && isValid;
            setListBoxValid(ref listBoxSuppliers, "Please add Atleast 1 Supplier");


            return isValid;
        }

        private void errorHandlingIsEmpty(ref TextBox tb, string message)
        {
            if (tb.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(tb, message);
            }
        }

        private void setComboValid(ref ComboBox cb,string message)
        {
            if (cb.SelectedIndex == -1)
            {
                errorProvider1.SetError(cb, message);
            }
        }

        private void setListBoxValid(ref ListBox lb, string message)
        {
            if (lb.Items.Count == 0)
            {
                errorProvider1.SetError(lb, message);
            }
        }

        #endregion


    }
}
