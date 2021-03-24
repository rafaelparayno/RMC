using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Components
{
    public partial class DoctorQueueControl : UserControl
    {
        public DoctorQueueControl()
        {
            InitializeComponent();
        }

        private string _doctorName;
        private string _currentQueue;
        private string _nextQueue;

        private int _docid;

        public int DocId
        {
            get { return _docid; }
            set { _docid = value;  DoctorNamelbl.Tag = value; }
        }



        [Category("Custom Props")]
        public string DoctorName
        {
            get { return _doctorName; }
            set { _doctorName = value;  DoctorNamelbl.Text = value; }
        }


        [Category("Custom Props")]
        public string CurrentQueue
        {
            get { return _currentQueue; }
            set { _currentQueue = value; lblcQ.Text = value; }
        }


        [Category("Custom Props")]
        public string NextQueue
        {
            get { return _nextQueue; }
            set { _nextQueue = value; lblnQ.Text = value; }
        }






    }
}
