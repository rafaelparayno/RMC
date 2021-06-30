using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception
{
    public partial class RequestDetailsControl : UserControl
    {
        public RequestDetailsControl()
        {
            InitializeComponent();
        }

        public Label labelPrice
        {
            get { return label2; }
            set { labelPrice = value; }
        }

        public ListView listViewLab
        {

            get { return lvLab; }
            set { listViewLab = value; }
        }

        public ListView listViewRad
        {

            get { return lvRad; }
            set { listViewRad = value; }
        }

        public ListView listViewService
        {

            get { return lvService; }
            set { listViewService = value; }
        }

    }
}
