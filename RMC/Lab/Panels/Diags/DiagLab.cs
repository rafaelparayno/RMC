using RMC.Components;
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

namespace RMC.Lab.Panels.Diags
{
    public partial class DiagLab : Form
    {
        LaboratoryController labC = new LaboratoryController();
        LabTypeController labTypeController = new LabTypeController();
        public DiagLab()
        {
            InitializeComponent();
            loadFromDbtoCb();
        }

        private async void loadFromDbtoCb()
        {
            List<ComboBoxItem> task3 = await labTypeController.getComboDatas();
            cbLabType.Items.AddRange(task3.ToArray());
        }

        private void cbLabType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cblabType = int.Parse((cbLabType.SelectedItem as ComboBoxItem).Value.ToString());
            refreshDataInCbLab(cblabType);
            cbLab.Enabled = true;
        }

        private async void refreshDataInCbLab(int labtype)
        {

            List<labModel> labModels = await labC.getLabModel(labtype);


            cbLab.DataSource = labModels;
            cbLab.DisplayMember = "name";

        }

        private void cbLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            labModel lab = (labModel)cbLab.SelectedItem;

            if(lab.autodocsid == 0 && cbLabType.SelectedIndex != -1)
            {
                panelWithAuto.Visible = false;
                groupBox1.Visible = true;
            }
            else
            {
                panelWithAuto.Visible = true;
                groupBox1.Visible = false;
            }
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
