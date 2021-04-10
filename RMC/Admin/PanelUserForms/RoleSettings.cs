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

namespace RMC.Admin.PanelUserForms
{
    public partial class RoleSettings : Form
    {
        RolesController roles = new RolesController();
        AccessController access = new AccessController();
      
        List<int> currentAccess = new List<int>();
        List<int> newAccess = new List<int>();
        int roleid = 0;

         public RoleSettings()
        {
            InitializeComponent();
            loadGrid();
        }

        private void dgRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            roleid = roles.getRoleId(dgRoles.SelectedRows[0].Cells[0].Value.ToString());
            currentAccess = access.accesses(roleid);
            newAccess = access.accesses(roleid);
            checkAccess(currentAccess);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (roleid == 0)
            {
                MessageBox.Show("Please select a row before saving");
                return;
            }

            access.deleteAccess(roleid, findNoAccesses(newAccess));
            access.saveAccess(roleid, newAccess);
            MessageBox.Show("success");
        }

        private async void loadGrid()
        {
            DataSet ds = await roles.getDs();
            RefreshGrid(ds);
        }

        private void RefreshGrid(DataSet ds)
        {

            dgRoles.DataSource = "";
            dgRoles.DataSource = ds.Tables[0];
            dgRoles.AutoResizeColumns();
        }

        private void checkAccess(List<int> access)
        {
            //admin
            adminAccessCb.Checked = access.Contains(StaticData.accessValues["Admin"]);
            cbLab.Checked = access.Contains(StaticData.accessValues["labAccess"]);
            cbDoctor.Checked = access.Contains(StaticData.accessValues["doctorAccess"]);
            cbReception.Checked = access.Contains(StaticData.accessValues["receptionAcess"]);
            cbPharma.Checked = access.Contains(StaticData.accessValues["pharmaAccess"]);
            cbInventory.Checked = access.Contains(StaticData.accessValues["inventoryAccess"]);
            cbOthers.Checked = access.Contains(StaticData.accessValues["otherAccess"]);
            cbXray.Checked = access.Contains(StaticData.accessValues["xrayAccess"]);
           // adminCbTriger();
        }

      

        private bool removeNotAdmin(int access)
        {
            return access != 1;
        }

        private void adminCbTriger()
        {
            

            if (adminAccessCb.Checked)
            {
                cbDoctor.Enabled = false;
                cbPharma.Enabled = false;
                cbOthers.Enabled = false;
                cbInventory.Enabled = false;
                cbXray.Enabled = false;
                cbLab.Enabled = false;
                cbInventory.Enabled = false;
                cbReception.Enabled = false;

                cbDoctor.Checked = false;
                cbPharma.Checked = false;
                cbOthers.Checked = false;
                cbInventory.Checked = false;
                cbXray.Checked = false;
                cbLab.Checked = false;
                cbInventory.Checked = false;
                cbReception.Checked = false;
               
            }
            else
            {
                cbDoctor.Enabled = true;
                cbPharma.Enabled = true;
                cbOthers.Enabled = true;
                cbInventory.Enabled = true;
                cbXray.Enabled = true;
                cbLab.Enabled = true;
                cbInventory.Enabled = true;
                cbReception.Enabled = true;

               
            }
        }

        #region CbEvents
        private void adminAccessCb_Click(object sender, EventArgs e)
        {
            if (adminAccessCb.Checked)
            {
          //      newAccess.RemoveAll(removeNotAdmin);
                newAccess.Add(StaticData.accessValues["Admin"]);
            }
            else
            {
                int index = newAccess.FindIndex(t => StaticData.accessValues["Admin"] == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
       //     adminCbTriger();
        }

        private void cbLab_Click(object sender, EventArgs e)
        {
            if (cbLab.Checked == true)
            {
                newAccess.Add(StaticData.accessValues["labAccess"]);
            }
            else
            {

                int index = newAccess.FindIndex(t => StaticData.accessValues["labAccess"] == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
        }

        private void cbPharma_Click(object sender, EventArgs e)
        {
            if (cbPharma.Checked == true)
            {
                newAccess.Add(StaticData.accessValues["pharmaAccess"]);
            }
            else
            {

                int index = newAccess.FindIndex(t => StaticData.accessValues["pharmaAccess"] == t);

                if(index>-1)
                 newAccess.RemoveAt(index);
            }
        }

        private void cbReception_Click(object sender, EventArgs e)
        {
            if (cbReception.Checked == true)
            {
                newAccess.Add(StaticData.accessValues["receptionAcess"]);
            }
            else
            {

                int index = newAccess.FindIndex(t => StaticData.accessValues["receptionAcess"] == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
        }


        private void cbDoctor_Click(object sender, EventArgs e)
        {
            if (cbDoctor.Checked == true)
            {
                newAccess.Add(StaticData.accessValues["doctorAccess"]);
            }
            else
            {

                int index = newAccess.FindIndex(t => StaticData.accessValues["doctorAccess"] == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
        }

        private void cbInventory_Click(object sender, EventArgs e)
        {
            if (cbInventory.Checked == true)
            {
                newAccess.Add(StaticData.accessValues["inventoryAccess"]);
            }
            else
            {

                int index = newAccess.FindIndex(t => StaticData.accessValues["inventoryAccess"] == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
        }


        private void cbXray_Click(object sender, EventArgs e)
        {
            if (cbXray.Checked == true)
            {
                newAccess.Add(StaticData.accessValues["xrayAccess"]);
            }
            else
            {

                int index = newAccess.FindIndex(t => StaticData.accessValues["xrayAccess"] == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
        }

        private void cbOthers_Click(object sender, EventArgs e)
        {
            if (cbOthers.Checked)
            {
                newAccess.Add(StaticData.accessValues["otherAccess"]);
            }
            else
            {
                int index = newAccess.FindIndex(t => StaticData.accessValues["otherAccess"] == t);
                if (index > -1)
                    newAccess.RemoveAt(index);
                        
            }
        }

        #endregion

        private List<int> findNoAccesses(List<int> access)
        {
            List<int> noAccess = new List<int>();

            if (!access.Contains(StaticData.accessValues["Admin"]))
                noAccess.Add(StaticData.accessValues["Admin"]);
            
            if (!access.Contains(StaticData.accessValues["pharmaAccess"]))
                noAccess.Add(StaticData.accessValues["pharmaAccess"]);
            
            if (!access.Contains(StaticData.accessValues["labAccess"]))    
                noAccess.Add(StaticData.accessValues["labAccess"]);
            
            if (!access.Contains(StaticData.accessValues["doctorAccess"]))    
                noAccess.Add(StaticData.accessValues["doctorAccess"]);
            
            if (!access.Contains(StaticData.accessValues["receptionAcess"]))
                noAccess.Add(StaticData.accessValues["receptionAcess"]);

            if (!access.Contains(StaticData.accessValues["inventoryAccess"]))
                noAccess.Add(StaticData.accessValues["inventoryAccess"]);

            if (!access.Contains(StaticData.accessValues["xrayAccess"]))
                noAccess.Add(StaticData.accessValues["xrayAccess"]);

            if (!access.Contains(StaticData.accessValues["otherAccess"]))
                noAccess.Add(StaticData.accessValues["otherAccess"]);


            return noAccess;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                loadGrid();
            }
            else
            {
                searchGrid("%" + textBox1.Text.Trim() + "%");
            }
        }

        private async void searchGrid(string rolename)
        {
            DataSet ds = await roles.findRole(rolename);
            RefreshGrid(ds);
        }

      
    }
}
