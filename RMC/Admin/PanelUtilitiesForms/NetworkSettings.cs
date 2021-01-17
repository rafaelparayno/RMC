using RMC.Components;
using RMC.Database.DbSettings;
using RMC.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelUtilitiesForms
{
    public partial class NetworkSettings : Form
    {
        public NetworkSettings()
        {
            InitializeComponent();
            initLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            IPConfigForms frm = new IPConfigForms();
            frm.ShowDialog();
            initLoad();
        }

        private void NetworkSettings_Load(object sender, EventArgs e)
        {
            


        }


        private void initLoad()
        {
            string SERVER = dbConfigFile.FetchDatabaseLocation()[0];
            string USERNAME = dbConfigFile.FetchDatabaseLocation()[1];
           
            string DATABASE = dbConfigFile.FetchDatabaseLocation()[3];

            string filePathServer = ReadFileServerPath.FetchServerLocation();

            label10.Text = "Server: " + SERVER;

            label1.Text = "Database: " + DATABASE;

            label2.Text = "Username : " + USERNAME;

            txtfilepath.Text = filePathServer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                txtfilepath.Text = folderBrowserDialog.SelectedPath;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtfilepath.Text.Trim() == "")
            {
                MessageBox.Show("Please Select A Proper File Path", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"\fileserver.txt");
            file.WriteLine(txtfilepath.Text);
            file.Close();

        }
    }
}
