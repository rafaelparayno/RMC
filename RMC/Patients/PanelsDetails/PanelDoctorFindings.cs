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

namespace RMC.Patients.PanelsDetails
{
    public partial class PanelDoctorFindings : Form
    {
        AccessController accessController = new AccessController();
        List<int> listAccess = new List<int>();
        public PanelDoctorFindings()
        {
            InitializeComponent();
            getAccess();
        }


        private void getAccess()
        {
            listAccess =  accessController.accesses(UserLog.getRole());

            if (listAccess.Contains(5))
            {
                iconButton3.Visible = true;
            }


        }
    }
}
