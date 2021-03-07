using RMC.Admin.PanelForms.dialogs;
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

namespace RMC.Admin.PanelForms
{
    public partial class UserList : Form
    {
        UserracountsController userracounts = new UserracountsController();
        public UserList()
        {
            InitializeComponent();
        }

   

        private void CloseChild_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region ButtonEvents
        private void addUser_Click(object sender, EventArgs e)
        {
            addUserDialog frm = new addUserDialog();
            frm.ShowDialog();
            loadGrid();
        }

        private void editUser_Click(object sender, EventArgs e)
        {
            if(dgUserAccounts.Rows.Count > 0)
            {
                
                addUserDialog frm = new addUserDialog(dgUserAccounts.SelectedRows[0].Cells[0].Value.ToString(),
                                                        dgUserAccounts.SelectedRows[0].Cells[1].Value.ToString(),
                                                         dgUserAccounts.SelectedRows[0].Cells[2].Value.ToString(),
                                                         dgUserAccounts.SelectedRows[0].Cells[3].Value.ToString(),
                                                         dgUserAccounts.SelectedRows[0].Cells[6].Value.ToString());
                frm.ShowDialog();
                loadGrid();
            }
           
        }

        private void ResetPassword_Click(object sender, EventArgs e)
        {
            if(dgUserAccounts.Rows.Count== 0)
                return;

            int id = int.Parse(dgUserAccounts.SelectedRows[0].Cells[0].Value.ToString());
            string userName = dgUserAccounts.SelectedRows[0].Cells[4].Value.ToString();
            DialogResult diag = MessageBox.Show("Do you want to reset this " + userName + " Password?",
                        "Exit", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                userracounts.resetPassword(id);
                MessageBox.Show("Succesfully Reset Password an Account");
                loadGrid();
            }
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            if (dgUserAccounts.Rows.Count == 0)
                return;

            if (dgUserAccounts.SelectedRows.Count == 0)
                return;

            string userName = dgUserAccounts.SelectedRows[0].Cells[4].Value.ToString();
            int id = int.Parse(dgUserAccounts.SelectedRows[0].Cells[0].Value.ToString());
            DialogResult diag = MessageBox.Show("Do you want to Delete this " + userName + " account?",
                        "Exit", MessageBoxButtons.YesNo);

            if (diag == DialogResult.Yes)
            {
                userracounts.delete(id);
                MessageBox.Show("Succesfully Delete an Account");
                loadGrid();

            }
        }

        #endregion

        private async void loadGrid()
        {
            DataSet ds = await userracounts.getDs();
            RefreshGrid(ds);
        }


        private void RefreshGrid(DataSet ds)
        {
            dgUserAccounts.DataSource = "";
            dgUserAccounts.DataSource = ds.Tables[0];
            dgUserAccounts.AutoResizeColumns();
            encryptPasswordGrid();
          //  label1.Visible = false;

        }

        private void UserList_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void encryptPasswordGrid()
        {
            foreach (DataGridViewRow dr in dgUserAccounts.Rows)
            {
                if (int.Parse(dr.Cells["is_change"].Value.ToString()) == 1)
                {
                    dr.Cells["password"].Value = "*********";
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text.Trim()== "")
            {
                loadGrid();
            }
            else
            {
                searchGrid(comboBox1.SelectedIndex,textBox1.Text.Trim());
            }
        }

        private async void searchGrid(int searchKey,string value)
        {
            string sql = "";
            switch (searchKey)
            {
                case 0:
                    sql = "SELECT * FROM useraccounts WHERE u_id = @value";
                  
                    break;
                case 1:
                    sql = "SELECT * FROM useraccounts WHERE firstname LIKE @value";
                    value = "%" + value + "%";
                    break;
                case 2:
                    sql = "SELECT * FROM useraccounts WHERE lastname LIKE @value";
                    value = "%" + value + "%";
                    break;
                /*  case 3:
                      search = "u_id";
                      break;*/
                default:
                    sql = "";
                    break;
            }

           

            DataSet ds = await userracounts.searchQueryAsync(sql, value);
            RefreshGrid(ds);
        }
    }
}
