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
    public partial class PanelSignaturePersonels : Form
    {
        PersonelsController personelsController = new PersonelsController();
        public PanelSignaturePersonels()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            AddEditPersonels addEditPersonels = new AddEditPersonels();
            addEditPersonels.ShowDialog();
            await loadData();
        }


        private async Task loadData()
        {
            DataSet dataSet = await personelsController.getDataset();


            dgLabTypes.DataSource = "";
            dgLabTypes.DataSource = dataSet.Tables[0];

            dgLabTypes.AutoResizeColumns();
        }

        private async void PanelSignaturePersonels_Load(object sender, EventArgs e)
        {
            await loadData();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (dgLabTypes.SelectedRows.Count == 0)
                return;

            if (dgLabTypes.Rows.Count == 0)
                return;


            int id = int.Parse(dgLabTypes.SelectedRows[0].Cells[0].Value.ToString());
            AddEditPersonels addEditPersonels = new AddEditPersonels(id,
                                                    dgLabTypes.SelectedRows[0].Cells[1].Value.ToString(),
                                                    dgLabTypes.SelectedRows[0].Cells[2].Value.ToString());
            addEditPersonels.ShowDialog();
            await loadData();
        }
    }
}
