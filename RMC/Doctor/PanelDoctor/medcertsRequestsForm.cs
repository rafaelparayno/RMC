using RMC.Database.Controllers;
using RMC.Doctor.PanelDoctor.Diag;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Doctor.PanelDoctor
{
    public partial class medcertsRequestsForm : Form
    {

        string idRightClick = "";
        string patname = "";
        MedCertController medCertController = new MedCertController();
        public medcertsRequestsForm()
        {
            InitializeComponent();
        }


        private async Task loadGrid()
        {
            DataSet ds = await medCertController.getDataSetNotDone();

            dbServiceList.DataSource = "";
            dbServiceList.DataSource = ds.Tables[0];
            dbServiceList.AutoResizeColumns();
        }

        private async void medcertsRequestsForm_Load(object sender, EventArgs e)
        {
             await  loadGrid();
        }

        private void dbServiceList_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = dbServiceList.HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    idRightClick = dbServiceList.Rows[currentMouseOverRow].Cells[0].Value.ToString();
                    patname = dbServiceList.Rows[currentMouseOverRow].Cells[2].Value.ToString();
                    contextMenuStrip1.Show(dbServiceList, new Point(e.X, e.Y));

                }
            }
        }

        private void openMedcertFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idRightClick);
            DiagMedCertForms diagMedCertForms = new DiagMedCertForms(id,patname);
            diagMedCertForms.ShowDialog();

        }
    }
}
