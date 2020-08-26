using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Lab
{
    public partial class ListParams : UserControl
    {
        public ListParams()
        {
            InitializeComponent();
        }


        /*  public Button btnView1
          {
              get { return btnView; }
              set { btnView1 = value; }
          }*/

        private float _xcoor;
        private float _ycoor;
        private string _paramname;

        private int _id;

        #region Properties
        [Category("Custom Props")]
        public int ID
        {
            get { return _id; }
            set { _id = value; txtParam.Tag = value; }
        }


        [Category("Custom Props")]
        public float XCoordinates
        {
            get { return _xcoor; }
            set { _xcoor = value; }
        }

        [Category("Custom Props")]
        public float YCoordinates
        {
            get { return _ycoor; }
            set { _ycoor = value; }
        }

        [Category("Custom Props")]
        public string ParamName
        {
            get { return _paramname; }
            set { _paramname = value; lblParamName.Text = value; }
        }

        [Category("Custom Props")]
        public TextBox textbox1
        {
            get { return txtParam; }
            set { textbox1 = value; }
        } 
        #endregion

    }
}
