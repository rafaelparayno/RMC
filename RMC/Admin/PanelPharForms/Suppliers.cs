using RMC.Admin.PanelPharForms.Dialogs;
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

namespace RMC.Admin.PanelPharForms
{
    public partial class Suppliers : Form
    {
        SupplierController suppliers = new SupplierController();
        public Suppliers()
        {
            InitializeComponent();
            loadGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEditSuppliers frm = new AddEditSuppliers();
            frm.ShowDialog();
            loadGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgSuppliers.Rows.Count == 0)
                return;


            AddEditSuppliers frm = new AddEditSuppliers(dgSuppliers.SelectedRows[0].Cells[0].Value.ToString(),
                                                        dgSuppliers.SelectedRows[0].Cells[1].Value.ToString(),
                                                        dgSuppliers.SelectedRows[0].Cells[2].Value.ToString(),
                                                        dgSuppliers.SelectedRows[0].Cells[3].Value.ToString());
            frm.ShowDialog();
            loadGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgSuppliers.Rows.Count == 0)
                return;

            int id = int.Parse( dgSuppliers.SelectedRows[0].Cells[0].Value.ToString());

            DialogResult diag = MessageBox.Show("Do you want to Remove this Suppliers",
                        "Exit", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                suppliers.deactivate(id);
                MessageBox.Show("Succesfully Delete an Supplier");
                loadGrid();

            }

        }

        private async void loadGrid()
        {
            DataSet ds = await suppliers.getdataSetActive();
            RefreshGrid(ds);
        }

        private void RefreshGrid(DataSet ds)
        {
            dgSuppliers.DataSource = "";
            dgSuppliers.DataSource = ds.Tables[0];
            dgSuppliers.AutoResizeColumns();
          

        }


    }

}
