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
    public partial class Units : Form
    {
        UnitsController unitsC = new UnitsController();
        public Units()
        {
            InitializeComponent();
        }

        private void Units_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private async void loadGrid()
        {
            DataSet ds = await unitsC.getDsActive();
            RefreshGrid(ds);
        }

        private void RefreshGrid(DataSet ds)
        {
            dgUnits.DataSource = "";
            dgUnits.DataSource = ds.Tables[0];
            dgUnits.AutoResizeColumns();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            addEditUnits frm = new addEditUnits();
            frm.ShowDialog();
            loadGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgUnits.Rows.Count == 0)
                return;


            int id = int.Parse(dgUnits.SelectedRows[0].Cells[0].Value.ToString());
            addEditUnits frm = new addEditUnits(dgUnits.SelectedRows[0].Cells[1].Value.ToString(),id);
            frm.ShowDialog();
            loadGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgUnits.Rows.Count == 0)
                return;


            int id = int.Parse(dgUnits.SelectedRows[0].Cells[0].Value.ToString());

            DialogResult diag = MessageBox.Show("Do you want to Remove this Unit",
                      "Exit", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                unitsC.Deactive(id);
                MessageBox.Show("Succesfully Delete an Unit");
                loadGrid();

            }
        }
    }
}
