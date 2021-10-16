﻿using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Lab;
using RMC.Utilities;
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

namespace RMC.Xray.Panels.RepDiags
{
    public partial class AddAutomatedXray : Form
    {
        XrayControllers xrayControllers = new XrayControllers();

        PatientXrayController patientXrayController = new PatientXrayController();
        /*PatientLabController patientLabController = new PatientLabController();*/
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        List<ListParams> listofListparams = new List<ListParams>();
        AutoParamController autoParamController = new AutoParamController();
        xraymodel xraymodel = new xraymodel();
        ItemController itemController = new ItemController();
        List<CoordinatesList> coordinatesAutomated = new List<CoordinatesList>();
        AutoDocsController autoDocsController = new AutoDocsController();
        RadioQueueController radioQueueController = new RadioQueueController();

        ConsumablesXrayControllers consumablesXray = new ConsumablesXrayControllers();
        ClinicStocksController clinicStocks = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();
        Dictionary<int, int> consumables = new Dictionary<int, int>();
        Graphics graphicsImg = null;
        Image img = null;
        /*   Image noImg = null;*/
        private int patientid = 0;
        public Image imgToAdd = null;
        int xid = 0;
        private int patient_xray_id = 0;
        private bool isEdited = false;
        private int cusid = 0;
        

        public AddAutomatedXray(int xid, int patientid,int cusid)
        {
            InitializeComponent();
            this.xid = xid;
            this.patientid = patientid;
            this.cusid = cusid;
            getXrayModel();
        }

        public AddAutomatedXray(int xid, int patientid,int patient_xray_id,int xtra)
        {
            InitializeComponent();
            this.xid = xid;
            this.patientid = patientid;
            isEdited = true;
            this.patient_xray_id = patient_xray_id;
            this.cusid = xtra;
            getXrayModel();
        }

        public async void getXrayModel()
        {
            xraymodel = await xrayControllers.getLabModelById(xid);

            label1.Text = xraymodel.name;
            pbAutomated.SizeMode = PictureBoxSizeMode.AutoSize;
            panelParam.Visible = true;

        
            getDisplayAutomated(xraymodel.autodocsid);
            displayParams(xraymodel.autodocsid);
        }

        private async void displayParams(int id)
        {
            Task<List<ListParams>> listt = autoParamController.getListParams(id);
            await Task.WhenAll(listt);
            listofListparams = listt.Result;

            panelParam.Controls.Clear();
            foreach (ListParams l in listofListparams)
            {
                l.textbox1.TextChanged += new EventHandler(textChangeForDraw);
                l.Dock = DockStyle.Top;
                panelParam.Controls.Add(l);
            }

        }

        private void textChangeForDraw(object sender, EventArgs e)
        {
            int id = int.Parse(((TextBox)sender).Tag.ToString());
            ListParams s = listofListparams.Find(item => int.Parse((item.textbox1).Tag.ToString()) == id);
            CoordinatesList cor = new CoordinatesList();

            if (s.textbox1.Text != "")
            {
                int index = coordinatesAutomated.FindIndex(a => a.xCoor == s.XCoordinates);

                if (index == -1)
                {

                    cor.nameVar = s.textbox1.Text;
                    cor.xCoor = s.XCoordinates;
                    cor.yCoor = s.YCoordinates;
                    coordinatesAutomated.Add(cor);
                }
                else
                {
                    coordinatesAutomated[index].nameVar = s.textbox1.Text;
                }
            }
            else
            {
                int index = coordinatesAutomated.FindIndex(a => a.xCoor == s.XCoordinates);
                if (index > -1)
                    coordinatesAutomated.RemoveAt(index);
            }
            Draw();

        }

        private Image resize(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void Draw()
        {

            Color cb = Color.Black;

            Brush brush = new SolidBrush(cb);
            Font font = new Font(new FontFamily("Times New Roman"), 14);

            Image newImg = resize(img, pbAutomated.ClientSize);
            graphicsImg = Graphics.FromImage(newImg);
            foreach (CoordinatesList cors in coordinatesAutomated)
            {
                graphicsImg.DrawString(cors.nameVar, font, brush, cors.xCoor, cors.yCoor);
            }
            pbAutomated.Image = newImg;

            graphicsImg.Dispose();
            pbAutomated.Invalidate();
            Update();
        }


        private async void getDisplayAutomated(int id)
        {
            string fullPath = await autoDocsController.getFullPath(id);

            if (!File.Exists(fullPath))
                return;

            img = Image.FromFile(fullPath);
            pbAutomated.Image = img;
        }

        private async Task processConsumables()
        {
            consumables = await consumablesXray.getListItemConsumables(xid);
            List<Task> listTasks = new List<Task>();
            foreach (KeyValuePair<int, int> kp in consumables)
            {
                int currentStocks = await clinicStocks.getStocks(kp.Key);
                int stocktosave = currentStocks - kp.Value;
                float unitCost = await itemController.getUnitCosts(kp.Key);
                float totalCost = unitCost * kp.Value;
                stocktosave = stocktosave > 0 ? stocktosave : 0;
                listTasks.Add(clinicStocks.Save(kp.Key, stocktosave));
                listTasks.Add(consumeditems.save(kp.Key, kp.Value,totalCost));

            }

            await Task.WhenAll(listTasks);

        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            patientDetails patientDetails = await patientDetailsController.getPatientId(patientid);


            if (!isEdited)
            {
                await radioQueueController.updateStatus(xid, cusid);

            
                string newFilePath2 = filePathSaving.saveXray(patientDetails.lastname + "-" + patientDetails.id);
                string filePath = newFilePath2;
                string datenow = DateTime.Now.ToString("yyyy--MM--dd");
                string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
                string combine = datenow + "--" + timenow;

                saveImginPath(filePath, "Xray-" + patientDetails.id + "-" + xid + "-" + combine);
                await patientXrayController.save(patientDetails.id, xid,
                                 "Xray-" + patientDetails.id + "-" + xid + "-" + combine + ".jpg", filePath, cusid);
                await processConsumables();
            }
            else
            {
                string path = patient_xray_id == 0 ?
                await patientXrayController.getFullPath(patientDetails.id, xid, cusid)
               : await patientXrayController.getFullPath(patient_xray_id);

                saveImginPathEdited(path);
            }
           

            MessageBox.Show("succesfully Save Data");
            this.Close();
        }


        private void saveImginPathEdited(string path)
        {
            Image newImg = pbAutomated.Image;
            newImg.Save(path);
            newImg.Dispose();
        }

        private void saveImginPath(string path, string fileName)
        {
            Image newImg = pbAutomated.Image;
            newImg.Save(path + fileName + ".jpg");
            newImg.Dispose();
        }

    }
}
