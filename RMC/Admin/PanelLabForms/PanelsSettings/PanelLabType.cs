using RMC.Admin.PanelLabForms.Dialogs;
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

namespace RMC.Admin.PanelLabForms.PanelsSettings
{
    public partial class PanelLabType : Form
    {
        LabTypeController labTypeController = new LabTypeController();
        public PanelLabType()
        {
            InitializeComponent();
            loadGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEditLabType form = new AddEditLabType();
            form.ShowDialog();
            loadGrid();

        }

        private async void loadGrid()
        {
            DataSet ds = await labTypeController.getDataSet();
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {

            dgLabTypes.DataSource = "";
            dgLabTypes.DataSource = ds.Tables[0];
            dgLabTypes.AutoResizeColumns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgLabTypes.SelectedRows.Count == 0)
                return;

            int id = int.Parse(dgLabTypes.SelectedRows[0].Cells[0].Value.ToString());
            string name = dgLabTypes.SelectedRows[0].Cells[1].Value.ToString();
            AddEditLabType form = new AddEditLabType(id,name);
            form.ShowDialog();

            loadGrid();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgLabTypes.SelectedRows.Count == 0)
                return;

            DialogResult diag = MessageBox.Show("Remove From the list", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            int id = int.Parse(dgLabTypes.SelectedRows[0].Cells[0].Value.ToString());

            if(diag == DialogResult.Yes)
            {
                labTypeController.remove(id);
            }

            loadGrid();
        }
    }
}
