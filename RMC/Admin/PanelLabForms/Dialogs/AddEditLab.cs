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

namespace RMC.Admin.PanelLabForms.Dialogs
{
    public partial class AddEditLab : Form
    {
        ItemController itemz = new ItemController();
        AutoDocsController autoDocsController = new AutoDocsController();
        bool isAuto = true;
        int cbAutoValue = 0;

        int cbConValue = 0;
        
        public AddEditLab()
        {
            InitializeComponent();
            loadFromDbtoCb();
            initColLv();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = itemz.getComboDatas();
            Task<List<ComboBoxItem>> task2 = autoDocsController.getComboDatas();
            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1, task2 };

            await Task.WhenAll(Cbs);

            cbConsumables.Items.AddRange(task1.Result.ToArray());
            cbAutomated.Items.AddRange(task2.Result.ToArray());
          
        }

        private void initColLv()
        {
            lvConsumables.View = View.Details;
            lvConsumables.Columns.Add("Item Id", 150, HorizontalAlignment.Right);
            lvConsumables.Columns.Add("Item Name", 150, HorizontalAlignment.Right);
            lvConsumables.Columns.Add("Quantity", 150, HorizontalAlignment.Right);
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
            foreach(ListViewItem lvs in lvConsumables.Items)
            {
                if(int.Parse(lvs.SubItems[0].Text) == id)
                {
                    return true;
                }
            }

            return false;
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

        private void cbAutomated_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAutoValue = int.Parse((cbAutomated.SelectedItem as ComboBoxItem).Value.ToString());

            getImgPath();
        }

        private async void getImgPath()
        {
            string fullPath = await autoDocsController.getFullPath(cbAutoValue);
            if(fullPath != "")
            {
                pbAutomated.Image = Image.FromFile(fullPath);
            }
         
        }
    }
}
