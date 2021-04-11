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
    }
}
