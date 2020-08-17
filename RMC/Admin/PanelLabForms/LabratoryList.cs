using RMC.Admin.PanelLabForms.Dialogs;
using RMC.Admin.PanelLabForms.PanelsSettings;
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
  
    public partial class LabratoryList : Form
    {
     
        public LabratoryList()
        {
            InitializeComponent();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditLab form = new AddEditLab();
            form.ShowDialog();
        }
    }
}
