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
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Components;

namespace RMC.Admin.PanelForms.dialogs
{
    public partial class addUserDialog : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wparam, int lPartam);
        bool isEdit = false;
        private int id = 0;
        private int uid = 0;
        private int roleid = 0;
        private string pos = "";
        UserracountsController useraccounts = new UserracountsController();
        RolesController roles = new RolesController();

        public addUserDialog()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            getLastId();
            loadCombo();

        }
        private async void loadCombo()
        {
            List<ComboBoxItem> cb = new List<ComboBoxItem>();
            cb = await roles.getComboDatas();
            comboBox1.Items.AddRange(cb.ToArray());
        }

        public addUserDialog(params string[] data )
        {
            InitializeComponent();
            isEdit = true;
            loadCombo();
            iconCurrentChildForm.IconChar = IconChar.UserEdit;
            label1.Text = "Edit User";
            label6.Visible = false;
            txtUsername.Visible = false;
            uid = int.Parse(data[0]);
            txtFirstName.Text = data[1];
            txtMn.Text = data[2];
            txtLn.Text = data[3];
            pos = data[4];
            comboBox1.Text = data[4];
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
            if (!isValid())
            {
                MessageBox.Show("Please Fill Firstname and Lastname", "err", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isEdit)
            {
                updateDate();
                
            }
            else
            {
                saveData();    
               // MessageBox.Show(roleid + "");
            }
            MessageBox.Show("Success Save Data");
            this.Close();
        }

        private void saveData()
        {
            string[] datas = new string[7];
            datas[0] = ToUpper(txtFirstName.Text.Trim());
            datas[1] = txtMn.Text.Trim() == "" ? "" : ToUpper(txtMn.Text.Trim());
            datas[2] = ToUpper(txtLn.Text.Trim());
           
            datas[3] = txtUsername.Text.Trim();
            datas[4] = GeneratePassword(8);
            datas[5] = 0 + "";
            datas[6] = roleid + "";
            useraccounts.saveUserAccount(datas);
        }

        private void updateDate()
        {
            useraccounts.updateUserAccounts(ToUpper(txtFirstName.Text.Trim()), ToUpper(txtLn.Text.Trim()), txtMn.Text.Trim(), roleid,uid);
        }

        private bool isValid()
        {
            bool isValid = true;
            //   isValid = (te)
            isValid = !(txtFirstName.Text.Trim() == "") && isValid;
            isValid = !(txtLn.Text.Trim() == "") && isValid;
            isValid = (comboBox1.SelectedIndex > -1) && isValid;
         
            return isValid;
        }

        private void addUserDialog_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = pos;
           
        }


        private void getLastId()
        {
          
            id = useraccounts.getRecentUserID();
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (txtLn.Text.Length > 0)
            {
                txtUsername.Text = txtLn.Text[0] + "-" + txtFirstName.Text.Trim().Split(' ')[0] + "-" + fixID(id);
            }
            else
            {
                txtUsername.Text = "";
            }
        }

        private void txtLn_TextChanged(object sender, EventArgs e)
        {
            if (txtLn.Text.Length > 0)
            {
                txtUsername.Text = txtLn.Text[0] + "-" + txtFirstName.Text.Trim().Split(' ')[0] + "-" + fixID(id);
            }
            else
            {
                txtUsername.Text = "";
            }
        }

        private string fixID(int lastid)
        {
            string id = "";
            if (lastid < 10)
            {
                id = "000" + lastid;
            }
            else if (lastid >= 10 && lastid < 100)
            {
                id = "00" + lastid;
            }
            else if (lastid > 100 && lastid < 1000)
            {
                id = "0" + lastid;
            }
            else if (lastid > 999)
            {
                id = lastid + "";
            }
            return id;
        }

        private string GeneratePassword(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }


        private string ToUpper(string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            roleid = int.Parse((comboBox1.SelectedItem as ComboBoxItem).Value.ToString());
        }
    }
}
