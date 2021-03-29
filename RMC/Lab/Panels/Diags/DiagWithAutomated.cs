﻿using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
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

namespace RMC.Lab.Panels.Diags
{
    public partial class DiagWithAutomated : Form
    {
        LaboratoryController laboratoryController = new LaboratoryController();
        PatientLabController patientLabController = new PatientLabController();
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        List<ListParams> listofListparams = new List<ListParams>();
        AutoParamController autoParamController = new AutoParamController();
        labModel lab = new labModel();
        List<CoordinatesList> coordinatesAutomated = new List<CoordinatesList>();
        AutoDocsController autoDocsController = new AutoDocsController();
        LabQueueController labQueueController = new LabQueueController();
        ConsumablesController consumablesController = new ConsumablesController();
        ClinicStocksController clinicStocks = new ClinicStocksController();
        ConsumedItems consumeditems = new ConsumedItems();
        Dictionary<int, int> consumables = new Dictionary<int, int>();
        Graphics graphicsImg = null;
        Image img = null;
        /*   Image noImg = null;*/
        private int patientid = 0;
        public Image imgToAdd = null;


        int labId = 0;
        public  DiagWithAutomated(int labId,int patientid)
        {
            InitializeComponent();
            this.labId = labId;
            this.patientid = patientid;
            getLabmodel();
        }

        public async void getLabmodel()
        {
            lab = await laboratoryController.getLabModelInID(labId);

            label1.Text = lab.name;
            pbAutomated.SizeMode = PictureBoxSizeMode.AutoSize;
            panelParam.Visible = true;
    
            getDisplayAutomated(lab.autodocsid);
            displayParams(lab.autodocsid);
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

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async Task processConsumables()
        {
            consumables = await consumablesController.getListItemConsumables(labId);
            List<Task> listTasks = new List<Task>();
            foreach (KeyValuePair<int, int> kp in consumables)
            {
                int currentStocks = await clinicStocks.getStocks(kp.Key);
                int stocktosave = currentStocks - kp.Value;
                stocktosave = stocktosave > 0 ? stocktosave : 0;
                listTasks.Add(clinicStocks.Save(kp.Key, stocktosave));
                listTasks.Add(consumeditems.save(kp.Key, kp.Value));

            }

            await Task.WhenAll(listTasks);

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
        
            patientDetails  patientmod = await patientDetailsController.getPatientId(patientid);

            await labQueueController.updateStatus(labId, patientmod.id);
            CreateDirectory.CreateDir(patientmod.lastname + "-" + patientmod.id);
            string newFilePath2 = CreateDirectory.CreateDir(patientmod.lastname + "-" + patientmod.id + "\\" + "LabFiles");
            string filePath = newFilePath2;
            string datenow = DateTime.Now.ToString("yyyy--MM--dd");
            string timenow = DateTime.Now.ToString("HH--mm--ss--tt");
            string combine = datenow + "--" + timenow;
            saveImginPath(filePath, "Lab-" + patientmod.id + "-" + labId + "-" + combine);
            await patientLabController.save(patientmod.id,labId,
                             "Lab-" + patientmod.id + "-" + labId + "-" + combine + ".jpg", filePath);
            await processConsumables();

            MessageBox.Show("succesfully Save Data");
            this.Close();
        }

        private void saveImginPath( string path, string fileName)
        {
            Image newImg = pbAutomated.Image;
            newImg.Save(path + fileName + ".jpg");
            newImg.Dispose();
        }
    }
}