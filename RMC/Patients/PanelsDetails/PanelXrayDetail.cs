﻿using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Xray.Panels.RepDiags;
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
    public partial class PanelXrayDetail : Form
    {
        private int id = 0;
        PatientXrayController patientXrayController = new PatientXrayController();
        XrayControllers xrayControllers = new XrayControllers();
        List<patientXrayModel> listXrayModel = new List<patientXrayModel>();
        public PanelXrayDetail(int id)
        {
            InitializeComponent();
            this.id = id;
            initListCols();
            getData();
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private async void getData()
        {
            listXrayModel = await patientXrayController.getPatientXray(id);
            refreshLvs();
        }

        private async Task searchData()
        {
            listXrayModel = await patientXrayController.getPatientXray(id,
                                dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            refreshLvs();
        }


        private void refreshLvs()
        {
            lvLabDetails.Items.Clear();
            foreach(patientXrayModel pmodel in listXrayModel)
            {
                ListViewItem lvitem = new ListViewItem();
                lvitem.Text = pmodel.id.ToString();
                lvitem.SubItems.Add(pmodel.name);
                lvitem.SubItems.Add(getType(pmodel.type));
                lvitem.SubItems.Add(pmodel.date.ToString("dddd, dd MMMM yyyy"));
                lvitem.SubItems.Add(pmodel.fileName);
                lvLabDetails.Items.Add(lvitem);
            }
        }

        private string getType(int type)
        {
            string typeStr = "";

            foreach(KeyValuePair<string,int> k in StaticData.XrayTypes)
            {
                if(type == k.Value)
                {

                    return k.Key;
                }
            }

            return typeStr;
        }

        private void initListCols()
        {
            lvLabDetails.View = View.Details;

            lvLabDetails.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Name", 200, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Type", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Date", 300, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("File name", 300, HorizontalAlignment.Left);
        }

        private  void lvLabDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLabDetails.Items.Count == 0)
                return;

            if (lvLabDetails.SelectedItems.Count == 0)
                return;

            int id = int.Parse(lvLabDetails.SelectedItems[0].Text);

          // await showImg(id);
        }

       /* private async Task showImg(int id)
        {
            string path = await patientXrayController.getFullPath(id);

            try
            {
                pbEdited.Image = Image.FromFile(path);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("There was an error opening the image. \n" +
                           "Please check the path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }*/


        private async void iconButton1_Click(object sender, EventArgs e)
        {
          await searchData();
        }

        private void lvLabDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                var focusedItem = lvLabDetails.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }

            }
        }

        private async void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvLabDetails.SelectedItems.Count == 0)
                return;

            if (lvLabDetails.Items.Count == 0)
                return;

            pictureBox1.Show();
            pictureBox1.Update();
            int selectedIds = int.Parse(lvLabDetails.SelectedItems[0].SubItems[0].Text);
            xraymodel xraymodel = await xrayControllers.getxrayModelinPatientLab(selectedIds);
            pictureBox1.Hide();
            string filenameExt = lvLabDetails.SelectedItems[0].SubItems[4].Text.Split('.')[1];

            if (xraymodel.is_crystal > 0)
            {

                RoetDiagForm roetDiagForm = new RoetDiagForm(id,xraymodel.id,selectedIds,0);
                roetDiagForm.ShowDialog();
            }
            else
            {
                ViewImageXray viewImageXray = new ViewImageXray(id,xraymodel.id,selectedIds,xraymodel.name,filenameExt,0);
                viewImageXray.ShowDialog();
               /* ViewImageFile viewImageFile = new ViewImageFile(id, labModel.id, selectedIds, lvLabDetails.SelectedItems[0].SubItems[1].Text);
                viewImageFile.ShowDialog();*/
            }
        }

        private async void editDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvLabDetails.SelectedItems.Count == 0)
                return;

            if (lvLabDetails.Items.Count == 0)
                return;

            pictureBox1.Show();
            pictureBox1.Update();
            int selectedIds = int.Parse(lvLabDetails.SelectedItems[0].SubItems[0].Text);
            xraymodel xraymodel = await xrayControllers.getxrayModelinPatientLab(selectedIds);
            pictureBox1.Hide();


            if (xraymodel.autodocsid > 0)
            {
                AddAutomatedXray addAutomatedXray = new AddAutomatedXray(xraymodel.id, id, selectedIds, 0);
                addAutomatedXray.ShowDialog();

            }

            if (xraymodel.is_crystal == 1)
            {
                XrayDynamicValue xrayDynamicValue = new XrayDynamicValue(id, xraymodel.id, selectedIds,0);
                xrayDynamicValue.ShowDialog();
            }

            if (xraymodel.autodocsid == 0 && xraymodel.is_crystal == 0)
            {
                AddXrayUploading addXrayUploading = new AddXrayUploading(xraymodel.id, id,selectedIds,0);
                addXrayUploading.ShowDialog();
              
            }
        }
    }
}
