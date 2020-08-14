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

namespace RMC.InventoryPharma.PanelRo
{
    public partial class PanelRecPo : Form
    {
        PoController poController = new PoController();
        List<string> Po = new List<string>();
        public PanelRecPo()
        {
            InitializeComponent();
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if(cbType.SelectedIndex == 0)
            {
                loadPO();
            }
            else
            {

            }
        }

        private async void loadPO()
        {
            Po = await poController.getPoActive();
            listBox1.Items.AddRange(Po.ToArray());
        }
    }
}
