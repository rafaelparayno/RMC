using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Doctor.PanelDoctor.Diag
{
    public partial class addEditPeForm : Form
    {



        #region variables

        private string general = "";
        private string eyes = "";
        private string ears = "";
        private string nose = "";
        private string mouth = "";
        private string throat = "";
        private string hematologic = "";
        private string neurological = "";
        private string cardio = "";
        private string pulmonary = "";
        private string breast = "";
        private string skin = "";
        private string gastro = "";
        private string geni = "";
        private string gyna = "";
        private string endocrine = "";

        #endregion


        public addEditPeForm()
        {
            InitializeComponent();
        }

        
        private void cbCheckAllLeft_Click(object sender, EventArgs e)
        {
            if (cbCheckAllLeft.Checked)
            {
                checkAllLeftInPE();
             
            }
            else
            {
                checkAllNotLeft();
            }
        }


        private void cbCheckAllRight_Click(object sender, EventArgs e)
        {
            if (cbCheckAllRight.Checked)
            {
                checkAllRight();
            }
            else
            {
                checkNotALlRight();
            }
        }


        #region CheckRightAllEvent

        private void checkNotALlRight()
        {

            cbCardio.Checked = false;
            cbPulmo.Checked = false;
            cbBreast.Checked = false;
            cbSkin.Checked = false;
            cbGastro.Checked = false;
            cbGen.Checked = false;
            cbGyna.Checked = false;
            cbEndo.Checked = false;

            txtCarido.Enabled = true;
            txtPulmop.Enabled = true;
            txtBreast.Enabled = true;
            txtSkin.Enabled = true;
            txtGastro.Enabled = true;
            txtGen.Enabled = true;
            txtGyna.Enabled = true;
            txtEndo.Enabled = true;
        }

        private void checkAllRight()
        {
            txtCarido.Text = "";
            txtPulmop.Text = "";
            txtBreast.Text = "";
            txtSkin.Text = "";
            txtGastro.Text = "";
            txtGen.Text = "";
            txtGyna.Text = "";
            txtEndo.Text = "";

            cbCardio.Checked = true;
            cbPulmo.Checked = true;
            cbBreast.Checked = true;
            cbSkin.Checked = true;
            cbGastro.Checked = true;
            cbGen.Checked = true;
            cbGyna.Checked = true;
            cbEndo.Checked = true;

            txtCarido.Enabled = false;
            txtPulmop.Enabled = false;
            txtBreast.Enabled = false;
            txtSkin.Enabled = false;
            txtGastro.Enabled = false;
            txtGen.Enabled = false;
            txtGyna.Enabled = false;
            txtEndo.Enabled = false;
        }

        #endregion


        #region CheckAllLeftEvent
        private void checkAllNotLeft()
        {





            //cb
            cbGeneral.Checked = false;
            cbEars.Checked = false;
            cbEyes.Checked = false;
            cbNose.Checked = false;
            cbMouth.Checked = false;
            cbThroat.Checked = false;
            cbHema.Checked = false;
            cbNeuro.Checked = false;
            //cb
            txtGeneral.Enabled = true;
            txtEars.Enabled = true;
            txtEyes.Enabled = true;
            txtNose.Enabled = true;
            txtThroat.Enabled = true;
            panelMouth.Enabled = true;
            txtHema.Enabled = true;
            txtNeuro.Enabled = true;


            //cbMO
        }


        private void checkAllLeftInPE()
        {


            //Txt
            txtGeneral.Text = "";
            txtEars.Text = "";
            txtEyes.Text = "";
            txtNose.Text = "";
            txtMouth.Text = "";
            txtThroat.Text = "";
            txtHema.Text = "";
            txtNeuro.Text = "";
            //txt


            //cb
            cbGeneral.Checked = true;
            cbEars.Checked = true;
            cbEyes.Checked = true;
            cbNose.Checked = true;
            cbMouth.Checked = true;
            cbThroat.Checked = true;
            cbHema.Checked = true;
            cbNeuro.Checked = true;
            //cb
            txtGeneral.Enabled = false;
            txtEars.Enabled = false;
            txtEyes.Enabled = false;
            txtNose.Enabled = false;
            txtThroat.Enabled = false;
            panelMouth.Enabled = false;
            txtHema.Enabled = false;
            txtNeuro.Enabled = false;


            //cbMO
        }
        #endregion



        private void cbGeneral_Click(object sender, EventArgs e)
        {
            if (cbGeneral.Checked)
            {
                txtGeneral.Enabled = false;
                txtGeneral.Text = "";
                general = "Normal";
            }
            else
            {
                txtGeneral.Enabled = true;
                general = "";
            }
        }

        private void cbEyes_Click(object sender, EventArgs e)
        {
            if (cbEyes.Checked)
            {
                txtEyes.Enabled = false;
                txtEyes.Text = "";
                eyes = "Normal";
            }
            else
            {
                eyes = "";
     
                txtEyes.Enabled = true;
            }
        }

        private void cbEars_Click(object sender, EventArgs e)
        {
            if (cbEars.Checked)
            {
                txtEars.Enabled = false;
                txtEars.Text = "";
                ears = "Normal";
            }
            else
            {
                txtEars.Enabled = true;
                ears = "";
            }
        }
    }
}
