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

namespace RMC.Components
{
    public partial class VoidForm : Form
    {
        public bool isFound = false;

        private List<int> useraccess = new List<int>();
        AccessController accessesController = new AccessController();
        LoginController loginController = new LoginController();

        public VoidForm()
        {
            InitializeComponent();
            useraccess = new List<int>();
            isFound = false;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            isFound = false;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string username = txtPlaceName.Text.Trim();
            string password = textBox1.Text.Trim();

            int roleid = loginController.isAcceptVoid(username, password);


            useraccess = accessesController.accesses(roleid);

            if(useraccess.Contains(1))
            {
                isFound = true;


                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect password or username or The user you enter doesnt have the right access");;
            }

          
        }
    }
}
