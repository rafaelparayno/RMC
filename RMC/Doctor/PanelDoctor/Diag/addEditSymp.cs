using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Doctor.PanelDoctor.Diag
{
    public partial class addEditSymp : Form
    {

        SymptomsController symptomsController = new SymptomsController();
        private int id = 0;
        public addEditSymp()
        {
            InitializeComponent();
        }

        public addEditSymp(params string[] data)
        {
            InitializeComponent();
            initEditState(data);
        }

        private void initEditState(string [] data)
        {
            this.id = int.Parse(data[0]);
            txtRole.Text = data[1];
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSaveUser_Click(object sender, EventArgs e)
        {
            if(txtRole.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Symptoms Name");
                return;
            }

            if(id == 0)
            {
                await symptomsController.save(UserLog.getUserId(), txtRole.Text.Trim());
            }
            else
            {
                await symptomsController.update(UserLog.getUserId(), txtRole.Text.Trim(), id);
            }

            MessageBox.Show("succesfully Save data");
            this.Close();
        }
    }
}
