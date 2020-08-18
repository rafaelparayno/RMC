using RMC.Admin;
using RMC.Admin.PanelLabForms;
using RMC.Components;
using RMC.InventoryPharma.PanelRo;
using RMC.Reception.PanelRequestForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ReceptionDash());
        }
    }
}
