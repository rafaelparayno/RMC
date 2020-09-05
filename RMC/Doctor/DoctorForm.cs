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

namespace RMC.Doctor
{
    public partial class DoctorForm : Form
    {
        SymptomsController sController = new SymptomsController();
        ItemController itemz = new ItemController();
        LaboratoryController laboratoryController = new LaboratoryController();
        XrayControllers xrayControllers = new XrayControllers();

        private int cbLabValue = 0;
        private int cbXrayValue = 0;
        private int cbSympValue = 0;
        private int cbMedsValue = 0;
        public DoctorForm()
        {
            InitializeComponent();
            initLvsCols();
        }

        private void initLvsCols()
        {
            lvLab.View = View.Details;
            lvXray.View = View.Details;
            lvSymp.View = View.Details;
            lvMeds.View = View.Details;


            lvLab.Columns.Add("Lab ID", 100, HorizontalAlignment.Left);
            lvLab.Columns.Add("Lab Name", 500, HorizontalAlignment.Left);

            lvXray.Columns.Add("Xray ID", 100, HorizontalAlignment.Left);
            lvXray.Columns.Add("Xray Name", 500, HorizontalAlignment.Left);

            lvSymp.Columns.Add("Symptom ID", 100, HorizontalAlignment.Left);
            lvSymp.Columns.Add("Symptom Name", 500, HorizontalAlignment.Left);

            lvMeds.Columns.Add("Meds ID", 100, HorizontalAlignment.Left);
            lvMeds.Columns.Add("Meds Name", 300, HorizontalAlignment.Left);
            lvMeds.Columns.Add("Instruction", 600, HorizontalAlignment.Left);
        }

        private async Task loadCbs()
        {
            Task<List<ComboBoxItem>> task1 = laboratoryController.getComboDatas();
            Task<List<ComboBoxItem>> task2 = xrayControllers.getComboDatas();
            Task<List<ComboBoxItem>> task3 = itemz.getMedicinesActive();
            Task<List<ComboBoxItem>> task4 = sController.getComboDatas(UserLog.getUserId());


            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1, task2,
                                                                            task3, task4 };

            await Task.WhenAll(Cbs);
            cbLab.Items.AddRange(task1.Result.ToArray());
            cbXray.Items.AddRange(task2.Result.ToArray());
            cbMeds.Items.AddRange(task3.Result.ToArray());
            cbSymp.Items.AddRange(task4.Result.ToArray());
      
        }

        private async void DoctorForm_Load(object sender, EventArgs e)
        {
           await  loadCbs();
        }

        private void cbSymp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSympValue = int.Parse((cbSymp.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void cbLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbLabValue = int.Parse((cbLab.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void cbXray_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbXrayValue = int.Parse((cbXray.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void cbMeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbMedsValue = int.Parse((cbMeds.SelectedItem as ComboBoxItem).Value.ToString());
        }

        private void btnAddSymp_Click(object sender, EventArgs e)
        {
            if (cbSympValue == 0)
                return;

            ListViewItem lv = new ListViewItem();
            lv.Text = cbSympValue.ToString();
            lv.SubItems.Add(cbSymp.Text);

            lvSymp.Items.Add(lv);
        }

        private void btnRemSymp_Click(object sender, EventArgs e)
        {
            if (lvSymp.Items.Count == 0)
                return;

            if (lvSymp.SelectedItems.Count == 0)
                return;

            int index = lvSymp.SelectedItems[0].Index;
            lvSymp.Items.RemoveAt(index);
        }

        private void btnAddLab_Click(object sender, EventArgs e)
        {
            if (cbLabValue == 0)
                return;

            ListViewItem lvItems = new ListViewItem();
            lvItems.Text = cbLabValue.ToString();
            lvItems.SubItems.Add(cbLab.Text);

            lvLab.Items.Add(lvItems);
        }

        private void btnRemoveLab_Click(object sender, EventArgs e)
        {
            if (lvLab.Items.Count == 0)
                return;

            if (lvLab.SelectedItems.Count == 0)
                return;


            int index = lvLab.SelectedItems[0].Index;

            lvLab.Items.RemoveAt(index);
        }

        private void btnAddX_Click(object sender, EventArgs e)
        {
            if (cbXrayValue == 0)
                return;

            ListViewItem lvItems = new ListViewItem();
            lvItems.Text = cbXrayValue.ToString();
            lvItems.SubItems.Add(cbXray.Text);

            lvXray.Items.Add(lvItems);
        }

        private void btnRemX_Click(object sender, EventArgs e)
        {
            if (lvXray.Items.Count == 0)
                return;

            if (lvXray.SelectedItems.Count == 0)
                return;

            int index = lvXray.SelectedItems[0].Index;

            lvXray.Items.RemoveAt(index);
        }
    }
}
