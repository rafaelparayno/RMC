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
    public partial class addEditCategory : Form
    {
        int itemType = 0;
        private bool isEdit = false;
        private int id = 0;

        CategoryController category = new CategoryController();
        public addEditCategory()
        {
            InitializeComponent();
            LoadCombo();
        }

        public addEditCategory(params string[] datas)
        {
            InitializeComponent();
            LoadCombo();
            id = int.Parse(datas[0]);
            comboBox1.Text = datas[2];
            txtCatName.Text = datas[1];
            label1.Text = "Edit Categories";
            isEdit = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemType = int.Parse((comboBox1.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Complete Data");
                return;
            }

            if (isEdit)
            {
                category.Edit(txtCatName.Text.Trim(), itemType,id);
            }
            else
            {
                category.Save(txtCatName.Text.Trim(), itemType);
            }
            MessageBox.Show("Succesfully Save Data");

            this.Close();
        }

        private bool isValid()
        {
            bool isvalid = true;

            isvalid = (txtCatName.Text.Trim() != "") && isvalid;

            isvalid = (itemType > 0) && isvalid;


            return isvalid;
        }

        private void LoadCombo()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();

            foreach (KeyValuePair<string, int> item in StaticData.DataItemType)
            {
                cbItems.Add(new ComboBoxItem(item.Key, item.Value));
            }

            comboBox1.Items.AddRange(cbItems.ToArray());
        }
    }
}
