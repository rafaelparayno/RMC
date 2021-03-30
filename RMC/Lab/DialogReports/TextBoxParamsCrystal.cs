using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Lab.DialogReports
{
    public partial class TextBoxParamsCrystal : UserControl
    {
        public TextBoxParamsCrystal()
        {
            InitializeComponent();
        }

        private string _name;
        private string _value;




        [Category("Custom Props")]
        public string Value
        {
            get { return _value; }
            set { _value = value; txtParam.Text = value; }
        }


        [Category("Custom Props")]
        public string NameLabel
        {
            get { return _name; }
            set { _name = value; lblParamName.Text = value; }
        }

        [Category("Custom Props")]
        public TextBox textbox1
        {
            get { return txtParam; }
            
        }

        private void txtParam_TextChanged(object sender, EventArgs e)
        {
            _value = txtParam.Text.Trim();
        }
    }

    }
