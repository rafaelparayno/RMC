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

/*        private string general = "";
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
*/
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

            txtCarido.Text = "";
            txtPulmop.Text = "";
            txtBreast.Text = "";
            txtSkin.Text = "";
            txtGastro.Text = "";
            txtGen.Text = "";
            txtGyna.Text = "";
            txtEndo.Text = "";

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
            txtCarido.Text = "Normal";
            txtPulmop.Text = "Normal";
            txtBreast.Text = "Normal";
            txtSkin.Text = "Normal";
            txtGastro.Text = "Normal";
            txtGen.Text = "Normal";
            txtGyna.Text = "Normal";
            txtEndo.Text = "Normal";

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


            txtGeneral.Text = "";
            txtEars.Text = "";
            txtEyes.Text = "";
            txtNose.Text = "";
            txtMouth.Text = "";
            txtThroat.Text = "";
            txtHema.Text = "";
            txtNeuro.Text = "";


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
            txtGeneral.Text = "Normal";
            txtEars.Text = "Normal";
            txtEyes.Text = "Normal";
            txtNose.Text = "Normal";
            txtMouth.Text = "Normal";
            txtThroat.Text = "Normal";
            txtHema.Text = "Normal";
            txtNeuro.Text = "Normal";
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
                txtGeneral.Text = "Normal";
            
            }
            else
            {
                txtGeneral.Enabled = true;
                txtGeneral.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbEyes_Click(object sender, EventArgs e)
        {
            if (cbEyes.Checked)
            {
                txtEyes.Enabled = false;
                txtEyes.Text = "Normal";
               
            }
            else
            {
                txtEyes.Text = "";

                txtEyes.Enabled = true;
            }
            cbcheckAllLeftTrigger();
        }

        private void cbEars_Click(object sender, EventArgs e)
        {
            if (cbEars.Checked)
            {
                txtEars.Enabled = false;
                txtEars.Text = "Normal";
               
            }
            else
            {
                txtEars.Enabled = true;
                txtEars.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbNose_Click(object sender, EventArgs e)
        {
            if (cbNose.Checked)
            {
                txtNose.Text = "Normal";
                txtNose.Enabled = false;
             
            }
            else
            {
                txtNose.Enabled = true;
                txtNose.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbMouth_Click(object sender, EventArgs e)
        {
            if (cbMouth.Checked)
            {
                txtMouth.Text = "Normal";
                txtMouth.Enabled = false;
               
            }
            else
            {
                txtMouth.Enabled = true;
                txtMouth.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbThroat_Click(object sender, EventArgs e)
        {
            if (cbThroat.Checked)
            {
                txtThroat.Text = "Normal";
              
                txtThroat.Enabled = false;
            }
            else
            {
                txtThroat.Text = "";
                txtThroat.Enabled = true;
            }
            cbcheckAllLeftTrigger();
        }

        private void cbHema_Click(object sender, EventArgs e)
        {
            if (cbHema.Checked)
            {
                txtHema.Text = "Normal";
                txtHema.Enabled = false;

            
            }
            else
            {
                txtHema.Enabled = true;

                txtHema.Text = "";
            }
            cbcheckAllLeftTrigger();
        }

        private void cbNeuro_Click(object sender, EventArgs e)
        {
            if (cbNeuro.Checked)
            {
                txtNeuro.Text = "Normal";
                txtNeuro.Enabled = false;
         
            }
            else
            {
                txtNeuro.Enabled = true;
                txtNeuro.Text = "";
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
                txtCarido.Text = "Normal";
                txtCarido.Enabled = false;
                
            }
            else
            {
                txtCarido.Enabled = true;
                txtCarido.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbPulmo_Click(object sender, EventArgs e)
        {
            if (cbPulmo.Checked)
            {
                txtPulmop.Text = "Normal";
                txtPulmop.Enabled = false;
         
            }
            else
            {
                txtPulmop.Enabled = true;
                txtPulmop.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbBreast_Click(object sender, EventArgs e)
        {
            if (cbBreast.Checked)
            {
                txtBreast.Text = "Normal";
                txtBreast.Enabled = false;
           
            }
            else
            {
                txtBreast.Enabled = true;
                txtBreast.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbSkin_Click(object sender, EventArgs e)
        {
            if (cbSkin.Checked)
            {
                txtSkin.Text = "Normal";
                txtSkin.Enabled = false;
          
            }
            else
            {
                txtSkin.Enabled = true;
                txtSkin.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbEndo_Click(object sender, EventArgs e)
        {
            if (cbEndo.Checked)
            {
                txtEndo.Text = "Normal";
                txtEndo.Enabled = false;
          
            }
            else
            {
                txtEndo.Enabled = true;
                txtEndo.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbGyna_Click(object sender, EventArgs e)
        {
            if (cbGyna.Checked)
            {
                txtGyna.Text = "Normal";
                txtGyna.Enabled = false;
              
            }
            else
            {
                txtGyna.Enabled = true;
                txtGyna.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbGen_Click(object sender, EventArgs e)
        {
            if (cbGen.Checked)
            {
                txtGen.Text = "Normal";
                txtGen.Enabled = false;
          
            }
            else
            {
                txtGen.Enabled = true;
                txtGen.Text = "";
            }
            cbcheckAllRightTrigger();
        }

        private void cbGastro_Click(object sender, EventArgs e)
        {
            if (cbGastro.Checked)
            {
                txtGastro.Text = "Normal";
                txtGastro.Enabled = false;
              
            }
            else
            {
                txtGastro.Enabled = true;
                txtGastro.Text = "";
            }
            cbcheckAllRightTrigger();
        }


        #endregion

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbCheckAllDiag_Click(object sender, EventArgs e)
        {
            if (cbCheckAllDiag.Checked)
            {
                CheckAllDiag();
            }
            else
            {
                CheckNotAllDiag();
            }
        }

        private void CheckAllDiag()
        {
            cbXray.Checked = true;
            cbCbc.Checked = true;
            cbUrinalysis.Checked = true;
            cbStool.Checked = true;
            cbHepa.Checked = true;
            cbDrug.Checked = true;
            cbEcg.Checked = true;
            radioButton3.Checked = true;


            txtXray.Enabled = false;
            txtCbc.Enabled = false;
            txtUri.Enabled = false;
            txtStool.Enabled = false;
            txtHepa.Enabled = false;
            txtDrug.Enabled = false;
            txtEcg.Enabled = false;

        }

        private void CheckNotAllDiag()
        {
            cbXray.Checked = false;
            cbCbc.Checked = false;
            cbUrinalysis.Checked = false;
            cbStool.Checked = false;
            cbHepa.Checked = false;
            cbDrug.Checked = false;
            cbEcg.Checked = false;
       


            txtXray.Enabled = true;
            txtCbc.Enabled = true;
            txtUri.Enabled = true;
            txtStool.Enabled = true;
            txtHepa.Enabled = true;
            txtDrug.Enabled = true;
            txtEcg.Enabled = true;

        }

        private void checkAllDiagTrigger()
        {
            cbCheckAllDiag.Checked = cbXray.Checked && cbCbc.Checked && cbUrinalysis.Checked &&
                cbStool.Checked && cbStool.Checked && cbHepa.Checked && cbDrug.Checked && cbEcg.Checked;
        }

        private void cbXray_Click(object sender, EventArgs e)
        {
            if (cbXray.Checked)
            {
                txtXray.Enabled = false;
                txtXray.Text = "Normal";
            }
            else
            {
                txtXray.Enabled = true;
                txtXray.Text = "";
            }
            checkAllDiagTrigger();
        }

        private void cbCbc_Click(object sender, EventArgs e)
        {
            if (cbCbc.Checked)
            {
                txtCbc.Enabled = false;
                txtCbc.Text = "Normal";
            }
            else
            {
                txtCbc.Enabled = true;
                txtCbc.Text = "";
            }
            checkAllDiagTrigger();
        }

        private void cbUrinalysis_Click(object sender, EventArgs e)
        {
            if (cbUrinalysis.Checked)
            {
                txtUri.Enabled = false;
                txtUri.Text = "Normal";
            }
            else
            {
                txtUri.Enabled = true;
                txtUri.Text = "";
            }
            checkAllDiagTrigger();
        }

        private void cbStool_Click(object sender, EventArgs e)
        {
            if (cbStool.Checked)
            {
                txtStool.Enabled = false;
                txtStool.Text = "Normal";
            }
            else
            {
                txtStool.Enabled = true;
                txtStool.Text = "";
            }
            checkAllDiagTrigger();
        }

        private void cbHepa_Click(object sender, EventArgs e)
        {
            if (cbHepa.Checked)
            {
                txtHepa.Enabled = false;
                txtHepa.Text = "Normal";
            }
            else
            {
                txtHepa.Enabled = true;
                txtHepa.Text = "";
            }
            checkAllDiagTrigger();
        }

        private void cbDrug_Click(object sender, EventArgs e)
        {
            if (cbDrug.Checked)
            {
                txtDrug.Enabled = false;
                txtDrug.Text = "Normal";
            }
            else
            {
                txtDrug.Enabled = true;
                txtDrug.Text = "";
            }
            checkAllDiagTrigger();
        }

        private void cbEcg_Click(object sender, EventArgs e)
        {
            if (cbEcg.Checked)
            {
                txtEcg.Enabled = false;
                txtEcg.Text = "Normal";
            }
            else
            {
                txtEcg.Enabled = true;
                txtEcg.Text = "";
            }
            checkAllDiagTrigger();
        }
    }

}

