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

        private int adminAccess = 1;
        private int labAccess = 2;
        private int pharmaAccess = 3;
        private int receptionAcess = 4;
        private int doctorAccess = 5;

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
            if (access.Contains(adminAccess))
            {
                adminAccessCb.Checked = true;
            }
            else
            {
                adminAccessCb.Checked = false;
            }
            //admin

            //lab
            if (access.Contains(labAccess))
            {
                cbLab.Checked = true;
            }
            else
            {
                cbLab.Checked = false;
            }
            //lab

            //Doctor
            if (access.Contains(doctorAccess))
            {
                cbDoctor.Checked = true;
            }
            else
            {
                cbDoctor.Checked = false;
            }
            //Doctor


            //Reception
            if (access.Contains(receptionAcess))
            {
                cbReception.Checked = true;
            }
            else
            {
                cbReception.Checked = false;
            }
            //Reception

            //Pharma
            if (access.Contains(pharmaAccess))
            {
                cbPharma.Checked = true;
            }
            else
            {
                cbPharma.Checked = false;
            }
            //Pharma
           
        }

        private void adminAccessCb_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        #region CbEvents
        private void adminAccessCb_Click(object sender, EventArgs e)
        {
            if (adminAccessCb.Checked == true)
            {
                newAccess.Add(adminAccess);
            }
            else
            {

                int index = newAccess.FindIndex(t => adminAccess == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
        }

        private void cbLab_Click(object sender, EventArgs e)
        {
            if (cbLab.Checked == true)
            {
                newAccess.Add(labAccess);
            }
            else
            {

                int index = newAccess.FindIndex(t => labAccess == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
        }

        private void cbPharma_Click(object sender, EventArgs e)
        {
            if (cbPharma.Checked == true)
            {
                newAccess.Add(pharmaAccess);
            }
            else
            {

                int index = newAccess.FindIndex(t => pharmaAccess == t);

                if(index>-1)
                 newAccess.RemoveAt(index);
            }
        }

        private void cbReception_Click(object sender, EventArgs e)
        {
            if (cbReception.Checked == true)
            {
                newAccess.Add(receptionAcess);
            }
            else
            {

                int index = newAccess.FindIndex(t => receptionAcess == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
        }

        private void cbDoctor_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cbDoctor_Click(object sender, EventArgs e)
        {
            if (cbDoctor.Checked == true)
            {
                newAccess.Add(doctorAccess);
            }
            else
            {

                int index = newAccess.FindIndex(t => doctorAccess == t);

                if (index > -1)
                    newAccess.RemoveAt(index);
            }
        }
        #endregion

        private List<int> findNoAccesses(List<int> access)
        {
            List<int> noAccess = new List<int>();

            if (!access.Contains(adminAccess))
                noAccess.Add(adminAccess);
            
            if (!access.Contains(pharmaAccess))
                noAccess.Add(pharmaAccess);
            
            if (!access.Contains(labAccess))    
                noAccess.Add(labAccess);
            
            if (!access.Contains(doctorAccess))    
                noAccess.Add(doctorAccess);
            
            if (!access.Contains(receptionAcess))
                noAccess.Add(receptionAcess);
            

            return noAccess;
        }
    }
}
