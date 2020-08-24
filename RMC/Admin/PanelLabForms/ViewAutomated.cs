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

namespace RMC.Admin.PanelLabForms
{
    public partial class ViewAutomated : Form
    {

        AutoDocsController autod = new AutoDocsController();
        public ViewAutomated()
        {
            InitializeComponent();
            getDataFromDb();
        }

        private async void getDataFromDb()
        {
            DataSet ds = await autod.getDs();
            refreshGrid(ds);
        }

        private void refreshGrid(DataSet ds)
        {
            dgUserAccounts.DataSource = "";
            dgUserAccounts.DataSource = ds.Tables[0];
            dgUserAccounts.AutoResizeColumns();
        }

  

        private async void dgUserAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgUserAccounts.Rows.Count == 0)
                return;
            if (dgUserAccounts.SelectedRows.Count == 0)
                return;

            int id = int.Parse(dgUserAccounts.SelectedRows[0].Cells[0].Value.ToString());
            string fullPath = await autod.getFullPath(id);
            Console.WriteLine(fullPath);
            if (fullPath != "")
            {
                pbEdited.Image = Image.FromFile(fullPath);
            }
        }
    }
}
