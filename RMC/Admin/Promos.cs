using RMC.Admin.PackageDialog;
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
        public Promos()
        {
            InitializeComponent();
            initLvs();
        }

        private void initLvs()
        {
            lvPackages.View = View.Details;
            lvPackages.Columns.Add("ID",80 ,HorizontalAlignment.Left);
            lvPackages.Columns.Add("Package Name", 500, HorizontalAlignment.Left);
            lvPackages.Columns.Add("Package Price", 150, HorizontalAlignment.Left);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditPackage form = new AddEditPackage();
            form.ShowDialog();
        }
    }
}
