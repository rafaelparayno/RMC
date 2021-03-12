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
        doctorResultsController dController = new doctorResultsController();
        PatientPrescriptionController ppController = new PatientPrescriptionController();
        DoctorRequestLabController ddController = new DoctorRequestLabController();
        DoctorRequestXrayController dxController = new DoctorRequestXrayController();
        PatientSymptomsController psController = new PatientSymptomsController();

        private int cbLabValue = 0;
        private int cbXrayValue = 0;
        private int cbSympValue = 0;
        private int cbMedsValue = 0;
        private int patientId = 0;
     

        public DoctorForm(int id)
        {
            InitializeComponent();
            initLvsCols();
            this.patientId = id;
        }

        public DoctorForm(int id,string cc)
        {
            InitializeComponent();
            initLvsCols();
            this.patientId = id;
            textBox1.Text = cc;
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
            lvMeds.Columns.Add("Dispense No", 600, HorizontalAlignment.Left);
            lvMeds.Columns.Add("Instruction", 600, HorizontalAlignment.Left);
            lvMeds.Columns.Add("Special Instruction", 600, HorizontalAlignment.Left);
        }

        private async Task loadCbs()
        {
            Task<List<ComboBoxItem>> task1 = laboratoryController.getComboDatas();
            Task<List<ComboBoxItem>> task2 = xrayControllers.getComboDatas();
            Task<List<ComboBoxItem>> task3 = itemz.getMedicinesBrandedActive();
            Task<List<ComboBoxItem>> task4 = sController.getComboDatas(UserLog.getUserId());


            Task<List<ComboBoxItem>>[] Cbs = new Task<List<ComboBoxItem>>[] { task1, task2,
                                                                              task3,task4 };

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

        private async void btnAddSymp_Click(object sender, EventArgs e)
        {
            /* if (cbSympValue == 0)
                 return;*/

            if (cbSymp.Text == "")
                return;


            bool isf = await sController.isFound(UserLog.getUserId(), cbSymp.Text.ToString().Trim());

            if (isf)
            {
                ListViewItem lv = new ListViewItem();
                lv.Text = cbSympValue.ToString();
                lv.SubItems.Add(cbSymp.Text);

                lvSymp.Items.Add(lv);

            }
            else
            {


                DialogResult dialogRes = MessageBox.Show("Are you Want to save new data?", "Validation",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialogRes == DialogResult.OK)
                {
                    await sController.save(UserLog.getUserId(), cbSymp.Text);

                    ListViewItem lv = new ListViewItem();
                    int recentid = sController.getRecentItemID() - 1;
                    lv.Text = recentid.ToString();
                    lv.SubItems.Add(cbSymp.Text);

                    lvSymp.Items.Add(lv);
                }
            }


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

        private void btnAddMeds_Click(object sender, EventArgs e)
        {
            if (cbMedsValue == 0)
                return;

            if (cbMeds.SelectedIndex == -1)
                return;

            if (txtInstructMeds.Text == "")
                return;

            ListViewItem lvItems = new ListViewItem();
            lvItems.Text = cbMedsValue.ToString();
            lvItems.SubItems.Add(cbMeds.Text);
            lvItems.SubItems.Add(textBox4.Text);
            lvItems.SubItems.Add(txtInstructMeds.Text.Trim());
            lvItems.SubItems.Add(textBox5.Text.Trim());
            lvMeds.Items.Add(lvItems);


            txtInstructMeds.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private async void iconButton1_Click(object sender, EventArgs e)
        {
            await saveDetails();
            this.Close();
        }

        private async Task saveDetails()
        {

            await dController.save(textBox1.Text.Trim(), txtSubjective.Text.Trim(), 
                                    textBox2.Text.Trim() ,patientId.ToString(), textBox3.Text.Trim());

/*
            lvMeds.Columns.Add("Meds ID", 100, HorizontalAlignment.Left);
            lvMeds.Columns.Add("Meds Name", 300, HorizontalAlignment.Left);
            lvMeds.Columns.Add("Dispense No", 600, HorizontalAlignment.Left);
            lvMeds.Columns.Add("Instruction", 600, HorizontalAlignment.Left);
            lvMeds.Columns.Add("Special Instruction", 600, HorizontalAlignment.Left);*/

            foreach (ListViewItem lvItems in lvMeds.Items)
            {
                await ppController.save(int.Parse(lvItems.SubItems[0].Text),lvItems.SubItems[3].Text,
                                        lvItems.SubItems[4].Text, lvItems.SubItems[2].Text);
            }

            foreach(ListViewItem lv in lvLab.Items)
            {

                await ddController.save(int.Parse(lv.SubItems[0].Text));
            }

            foreach (ListViewItem lv in lvXray.Items)
            {

                await dxController.save(int.Parse(lv.SubItems[0].Text));
            }

            foreach (ListViewItem lv in lvSymp.Items)
            {

                await psController.save(int.Parse(lv.SubItems[0].Text));
            }
        }

        private async void rbBranded_CheckedChanged(object sender, EventArgs e)
        {
            List<ComboBoxItem> listCbs = await itemz.getMedicinesBrandedActive();
           


            cbMeds.Items.Clear();
            cbMedsValue = 0;
            cbMeds.Items.AddRange(listCbs.ToArray());

        }

        private async void rbGeneric_CheckedChanged(object sender, EventArgs e)
        {
            List<ComboBoxItem> listCbs = await itemz.getMedicinesGenericActive();

            cbMeds.Items.Clear();
            cbMedsValue = 0;
            cbMeds.Items.AddRange(listCbs.ToArray());
        }

        private void btnRemoveMeds_Click(object sender, EventArgs e)
        {
            if (lvMeds.Items.Count == 0)
                return;

            if (lvMeds.SelectedItems.Count == 0)
                return;

            int index = lvMeds.SelectedItems[0].Index;

            lvMeds.Items.RemoveAt(index);
        }
    }
}
