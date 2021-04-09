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
            txtMouth.Enabled = false;
            txtHema.Enabled = false;
            txtNeuro.Enabled = false;


            //cbMO
        }
        #endregion

        #region CbsLefts
        private void cbcheckAllLeftTrigger()
        {
            cbCheckAllLeft.Checked = cbGeneral.Checked && cbEyes.Checked && cbEars.Checked &&
                                   cbNose.Checked && cbMouth.Checked && cbThroat.Checked &&
                                   cbHema.Checked && cbNeuro.Checked;
        }

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
            cbcheckAllLeftTrigger();
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
            cbcheckAllLeftTrigger();
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
            cbcheckAllLeftTrigger();
        }

        private void cbNose_Click(object sender, EventArgs e)
        {
            if (cbNose.Checked)
            {
                txtNose.Text = "";
                txtNose.Enabled = false;
                nose = "Normal";
            }
            else
            {
                txtNose.Enabled = true;
                nose = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbMouth_Click(object sender, EventArgs e)
        {
            if (cbMouth.Checked)
            {
                txtMouth.Text = "";
                txtMouth.Enabled = false;
                mouth = "Normal";
            }
            else
            {
                txtMouth.Enabled = true;
                mouth = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbThroat_Click(object sender, EventArgs e)
        {
            if (cbThroat.Checked)
            {
                txtThroat.Text = "";
                throat = "Normal";
                txtThroat.Enabled = false;
            }
            else
            {
                throat = "";
                txtThroat.Enabled = true;
            }
            cbcheckAllLeftTrigger();
        }

        private void cbHema_Click(object sender, EventArgs e)
        {
            if (cbHema.Checked)
            {
                txtHema.Text = "";
                txtHema.Enabled = false;

                hematologic = "Normal";
            }
            else
            {
                txtHema.Enabled = true;

                hematologic = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbNeuro_Click(object sender, EventArgs e)
        {
            if (cbNeuro.Checked)
            {
                txtNeuro.Text = "";
                txtNeuro.Enabled = false;
                neurological = "Normal";
            }
            else
            {
                txtNeuro.Enabled = true;
                neurological = "";
            }
            cbcheckAllLeftTrigger();
        }

        #endregion

        #region CbsRight
        private void cbcheckAllRightTrigger()
        {
            cbCheckAllRight.Checked = cbCardio.Checked && cbPulmo.Checked && cbBreast.Checked &&
                cbSkin.Checked && cbGastro.Checked && cbGen.Checked && cbGyna.Checked && cbEndo.Checked;
        }

        private void cbCardio_Click(object sender, EventArgs e)
        {
            if (cbCardio.Checked)
            {
                txtCarido.Text = "";
                txtCarido.Enabled = false;
                cardio = "Normal";
            }
            else
            {
                txtCarido.Enabled = true;
                cardio = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbPulmo_Click(object sender, EventArgs e)
        {
            if (cbPulmo.Checked)
            {
                txtPulmop.Text = "";
                txtPulmop.Enabled = false;
                cardio = "Normal";
            }
            else
            {
                txtPulmop.Enabled = true;
                pulmonary = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbBreast_Click(object sender, EventArgs e)
        {
            if (cbBreast.Checked)
            {
                txtBreast.Text = "";
                txtBreast.Enabled = false;
                breast = "Normal";
            }
            else
            {
                txtBreast.Enabled = true;
                breast = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbSkin_Click(object sender, EventArgs e)
        {
            if (cbSkin.Checked)
            {
                txtSkin.Text = "";
                txtSkin.Enabled = false;
                skin = "Normal";
            }
            else
            {
                txtSkin.Enabled = true;
                skin = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbEndo_Click(object sender, EventArgs e)
        {
            if (cbEndo.Checked)
            {
                txtEndo.Text = "";
                txtEndo.Enabled = false;
                endocrine = "Normal";
            }
            else
            {
                txtEndo.Enabled = true;
                endocrine = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbGyna_Click(object sender, EventArgs e)
        {
            if (cbGyna.Checked)
            {
                txtGyna.Text = "";
                txtGyna.Enabled = false;
                gyna = "Normal";
            }
            else
            {
                txtGyna.Enabled = true;
                gyna = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbGen_Click(object sender, EventArgs e)
        {
            if (cbCardio.Checked)
            {
                txtGen.Text = "";
                txtGen.Enabled = false;
                geni = "Normal";
            }
            else
            {
                txtGen.Enabled = true;
                geni = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbGastro_Click(object sender, EventArgs e)
        {
            if (cbGastro.Checked)
            {
                txtGastro.Text = "";
                txtGastro.Enabled = false;
                gastro = "Normal";
            }
            else
            {
                txtGastro.Enabled = true;
                gastro = "";
            }
            cbcheckAllRightTrigger();
        }

        #endregion


    }

}

