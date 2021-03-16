using RMC.Patients.PanelsDetails.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Patients.PanelsDetails
{
    public partial class PanelOtherFiles : Form
    {
        int patient_id = 0;
        public PanelOtherFiles(int patient_id)
        {
            InitializeComponent();
            this.patient_id = patient_id;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddViewOther form = new AddViewOther(patient_id);
            form.ShowDialog();
        }
    }
}
