using RMC.Admin.PackageDialog;
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

namespace RMC.Admin
{
    public partial class Promos : Form
    {
        PackagesController packagesController = new PackagesController();
        List<packages> listPackages = new List<packages>();

        public Promos()
        {
            InitializeComponent();
            initLvs();
            loadPackages();
        }

        private void initLvs()
        {
            lvPackages.View = View.Details;
            lvPackages.Columns.Add("ID",80 ,HorizontalAlignment.Left);
            lvPackages.Columns.Add("Package Name", 400, HorizontalAlignment.Left);
            lvPackages.Columns.Add("Package Price", 150, HorizontalAlignment.Left);
            lvPackages.Columns.Add("Package Description", 150, HorizontalAlignment.Left);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditPackage form = new AddEditPackage();
            form.ShowDialog();
            loadPackages();
        }

        private async void loadPackages()
        {
            listPackages = await packagesController.getListPackages();
            refreshLv(listPackages);

        }

        private void refreshLv(List<packages> packs)
        {
            lvPackages.Items.Clear();
            foreach(packages p in packs)
            {
                ListViewItem lvitems = new ListViewItem();
                lvitems.Text = p.id.ToString();
                lvitems.SubItems.Add(p.name);
                lvitems.SubItems.Add(p.price.ToString());
                lvitems.SubItems.Add(p.desc);
                lvPackages.Items.Add(lvitems);
            }
        
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (lvPackages.Items.Count == 0)
                return;

            if (lvPackages.SelectedItems.Count == 0)
                return;

            AddEditPackage form = new AddEditPackage(lvPackages.SelectedItems[0].SubItems[0].Text,
                                                    lvPackages.SelectedItems[0].SubItems[1].Text,
                                                    lvPackages.SelectedItems[0].SubItems[2].Text,
                                                    lvPackages.SelectedItems[0].SubItems[3].Text);
            form.ShowDialog();
            loadPackages();
        }
    }
}
