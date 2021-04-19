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
    public partial class OthersPlaces : Form
    {
        PlacesTransferController placesTransferController = new PlacesTransferController();
        public OthersPlaces()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            addEditOtherPlaces forms = new addEditOtherPlaces();
            forms.ShowDialog();
            await loadGrid();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (dgCategory.Rows.Count == 0)
                return;

            if (dgCategory.SelectedRows.Count == 0)
                return;


            int id = int.Parse(dgCategory.SelectedRows[0].Cells[0].Value.ToString());

            addEditOtherPlaces forms = new addEditOtherPlaces(id,
                                                              dgCategory.SelectedRows[0].Cells[1].Value.ToString());
            forms.ShowDialog();
            await loadGrid();
        }

        private async Task loadGrid()
        {
            DataSet ds = await placesTransferController.getDataset();

            dgCategory.DataSource = "";
            dgCategory.DataSource = ds.Tables[0];
            dgCategory.AutoResizeColumns();
        }

        private async void OthersPlaces_Load(object sender, EventArgs e)
        {
            await loadGrid();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (dgCategory.Rows.Count == 0)
                return;

            if (dgCategory.SelectedRows.Count == 0)
                return;


            DialogResult dialogResult = MessageBox.Show("Are you Sure To Delete this Selected Data", "Delation",
                                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(dialogResult == DialogResult.Yes)
            {
                int id = int.Parse(dgCategory.SelectedRows[0].Cells[0].Value.ToString());
                await placesTransferController.delete(id);
                MessageBox.Show("Succesfully Deleted Data");
            }

            await loadGrid();
        }
    }
}
