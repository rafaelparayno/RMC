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

namespace RMC.Admin.PanelLabForms.Dialogs
{
    public partial class SaveAutomated : Form
    {
        string dir = "";
        public string FileName = "";
        public SaveAutomated(string Dir)
        {
            InitializeComponent();
            dir = Dir;
            FileName = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
                return;
            DialogResult diag;
            if (isNameSame(txtName.Text.Trim()))
            {
                diag = MessageBox.Show("Overwrite the image with same name?", "Same Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if(DialogResult.OK == diag)
                {
                    FileName = txtName.Text.Trim();
                }
                else
                {
                    FileName = "";
                  
                }
            }
            else
            {
                FileName = txtName.Text.Trim();
            }
            this.Close();
        }

        private bool isNameSame(string newName)
        {
            DirectoryInfo d = new DirectoryInfo(String.Format(@"{0}\", dir));
            FileInfo[] Files = d.GetFiles("*.jpg");
          
            for (int i = Files.Length - 1; i >= 0; i--)
            {
               
                if(Files[i].Name.Split('.')[0] == newName)
                {
                 
                    return true;
                }
            }
            return false;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
