using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Components
{
    public partial class IPConfigForms : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wparam, int lPartam);
        public IPConfigForms()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtServer.Text.Trim() == "" || txtDatabase.Text.Trim() == "")
            {
                MessageBox.Show("Please enter server name and Database name", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string lines = string.Format("{0}#{1}#{2}#{3}", txtServer.Text.Trim(), txtUsername.Text.Trim(), txtPassword.Text.Trim(), txtDatabase.Text.Trim());
            System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + @"\databaseconfig.txt");
            file.WriteLine(lines);
            file.Close();
            this.Close();
            MessageBox.Show("Succesfully Save Connection");

        }
    }
}
