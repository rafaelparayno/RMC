using RMC.Components;
using RMC.Database.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PackageDialog
{
    public partial class AddEditPackage : Form
    {
        LaboratoryController laboratoryController = new LaboratoryController();
        ServiceController serviceController = new ServiceController();
        XrayControllers xrayControllers = new XrayControllers();
        int cbValueLab = 0;
        int cbValueService = 0;
        int cbValueXray = 0;
      
        public AddEditPackage()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            loadFromDbtoCb();
        }


        private async void loadFromDbtoCb()
        {
            Task<List<ComboBoxItem>> task1 = laboratoryController.getComboDatas();
            Task<List<ComboBoxItem>> task2 = xrayControllers.getComboDatas();
            Task<List<ComboBoxItem>> task3 = serviceController.getComboDatas();
            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1,task2,task3 };

            await Task.WhenAll(Cbs);

            cbLab.Items.AddRange(task1.Result.ToArray());
            cbXray.Items.AddRange(task2.Result.ToArray());
            cbOther.Items.AddRange(task3.Result.ToArray());

        }
    }
}
