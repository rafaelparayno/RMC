
using RMC.Doctor.PanelDoctor.Diag;
using RMC.Lab.Panels.Diags;
using System;
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
            Application.Run(new splashscreen());
        }
    }
}
