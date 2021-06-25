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

namespace RMC.Admin.PanelPharForms.Dialogs
{
    public partial class addEditOtherPlaces : Form
    {
        PlacesTransferController placesTransferController = new PlacesTransferController();

        private int id = 0;


        public addEditOtherPlaces()
        {
            InitializeComponent();
        }

        public addEditOtherPlaces(int id,string name)
        {
            InitializeComponent();
            this.id = id;
            txtPlaceName.Text = name;
        }


        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPlaceName.Text.Trim()))
            {
                MessageBox.Show("Please Fill The Data", "validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPlaceName.Focus();
                return;
            }

            if (await placesTransferController.isFound(txtPlaceName.Text.Trim()))
            {
                MessageBox.Show("Already Has an existing place name");
                return;
            }


            if(id == 0)
            {
               await placesTransferController.save(txtPlaceName.Text.Trim());
            }
            else
            {
                await placesTransferController.edit(txtPlaceName.Text.Trim(),id);
            }

            MessageBox.Show("Succesfully Save Data");
            this.Close();
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
