using RMC.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelLabForms.Dialogs
{
    public partial class AddEditXrayOther : Form
    {
        public AddEditXrayOther()
        {
            InitializeComponent();
            loadAllCb();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadAllCb()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();

            foreach (KeyValuePair<string, int> item in StaticData.XrayTypes)
            {
                cbItems.Add(new ComboBoxItem(item.Key, item.Value));
            }

            cbType.Items.AddRange(cbItems.ToArray());
            //loadFromDbtoCb();

        }
    }
}
