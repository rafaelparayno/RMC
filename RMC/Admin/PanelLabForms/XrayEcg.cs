using RMC.Admin.PanelLabForms.Dialogs;
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
    public partial class XrayEcg : Form
    {
        public XrayEcg()
        {
            InitializeComponent();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditXrayOther form = new AddEditXrayOther();
            form.ShowDialog();
        }
    }
}
