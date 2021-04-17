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

using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Globalization;
using System.Net;

namespace RMC.Patients
{
    public partial class TakePhotoForm : Form
    {
     /*   private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] snapshotCapabilities;*/
        private ArrayList listCamera = new ArrayList();
        public string pathFolder = "";
        public TakePhotoForm()
        {
            InitializeComponent();
        }
    }
}
