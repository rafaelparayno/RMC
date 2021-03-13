using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace RMC.Patients
{
    public partial class PatientControl : UserControl
    {
        public PatientControl()
        {
            InitializeComponent();
        }

        private string _name;
        private string _gender;
        private string _cnumber;
        private string _addresss;
        private string _age;
        private Image _icon;

        #region Properties
        [Category("Custom Props")]

        private int _patientId;

        public int PatientId
        {
            get { return _patientId; }
            set { _patientId = value; btnView.Tag = value;
                lblId.Text = lblId.Text + " :" + value; }
        }


        public string PatientName
        {
            get { return _name; }
            set { _name = value; lblName.Text = value; }
        }

        [Category("Custom Props")]
        public string Cnumber
        {
            get { return _cnumber; }
            set { _cnumber = value; lblCn.Text = value; }
        }



        [Category("Custom Props")]
        public string Address
        {
            get { return _addresss; }
            set { _addresss = value; lblAddress.Text = value; }
        }

        [Category("Custom Props")]
        public string Age
        {
            get { return _age; }
            set { _age = value;lblAge.Text = value; }
        }


        [Category("Custom Props")]
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; lblGender.Text = value; }
        }


        [Category("Custom Props")]
        public Image Icon
        {
            get { return _icon; }
            set { _icon = value;    pbDisplayPicture.Image = value; }
        }

      

        public Button btnView1
        {
            get { return btnView; }
            set { btnView1 = value; }
        }




        #endregion

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FloralWhite;
        }

        private void pbDisplayPicture_MouseClick(object sender, MouseEventArgs e)
        {      
             contextMenuStrip1.Show(pbDisplayPicture, new Point(e.X, e.Y));
        }

        private void changePhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(_patientId.ToString());
            ChangePhotoDiag form = new ChangePhotoDiag(_patientId);
            form.ShowDialog();
        }
    }
}
