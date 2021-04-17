using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using AForge.Video;
using System.Diagnostics;
using AForge.Video.DirectShow;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Globalization;
using System.Net;
using RMC.Database.Controllers;
using RMC.Utilities;

namespace RMC.Patients
{
    public partial class TakePhotoForm : Form
    {

       
        string pathDir = "";
        int patient_id = 0;
        PatientDetailsController patientDetailsController = new PatientDetailsController();
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        
        private static bool needSnapshot = false;

        public TakePhotoForm(int id)
        {
            InitializeComponent();
            loadCameras();
            this.patient_id = id;
        }

      

        private void loadCameras()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
                cbCameras.Items.Add(filterInfo.Name);
            cbCameras.SelectedIndex = 0;

            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cbCameras.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pbAutomated.Image = (Bitmap)eventArgs.Frame.Clone();
            if (needSnapshot)
            {
                this.Invoke(new CaptureSnapshotManifast(UpdateCaptureSnapshotManifast), (Bitmap)eventArgs.Frame.Clone());
            }
        }

        private void TakePhotoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }

        public delegate void CaptureSnapshotManifast(Bitmap image);
        public void UpdateCaptureSnapshotManifast(Bitmap image)
        {
            try
            {
                needSnapshot = false;
                pictureBox1.Image = image;
                pictureBox1.Update();

             
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                needSnapshot = true;
            else
                MessageBox.Show("No Camera Open", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveImgPath(int random)
        {

            Image img = pictureBox1.Image;

            if(img != null)
                img.Save(pathDir + "DP-" + random + "-" + patient_id + ".jpg");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No Image to Save");
                return;
            }

            pathDir = CreateDirectory.CreateDir("PatientsImg");
            Random r = new Random();
            int genRand = r.Next(10, 50);
            saveImgPath(genRand);
            patientDetailsController.saveIMG(pathDir + "DP-" + genRand + "-" + patient_id + ".jpg", patient_id);
            MessageBox.Show("Successfully Save Image");
        }
    }
}
