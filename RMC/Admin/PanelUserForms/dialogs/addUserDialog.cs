using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace RMC.Admin.PanelForms.dialogs
{
    public partial class addUserDialog : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wparam, int lPartam);
        bool isEdit = false;

        public addUserDialog()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public addUserDialog(string[] data )
        {
            InitializeComponent();
            isEdit = true;
            iconCurrentChildForm.IconChar = IconChar.UserEdit;
            label1.Text = "Edit User";
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {

        }
    }
}
