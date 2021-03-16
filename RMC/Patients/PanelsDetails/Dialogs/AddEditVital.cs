using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Patients.PanelsDetails.Dialogs
{
    public partial class AddEditVital : Form
    {
        private int count = 0;
        PatientVController patientVController = new PatientVController();
        int patid = 0;
        int vid = 0;
        public AddEditVital(int pat_id)
        {
            InitializeComponent();
             this.patid = pat_id;
        }

        public AddEditVital(int pat_id,int v_id)
        {
            InitializeComponent();
            this.patid = pat_id;
            this.vid = v_id;
            setInitEditState(v_id);
        }

        private async void setInitEditState(int vid)
        {
            patientVModel pv = await patientVController.getDetailsID(vid);

            txtbp.Text = pv.bp;
            txtlmp.Text = pv.lmp;
            txttemp.Text = pv.temp;
            txtwt.Text = pv.wt;
            txtrbc.Text = pv.allergies;
     
            dateTimePicker1.Value = DateTime.Parse(pv.date_vital);
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Insert atleast One Data");
                return;
            }

            if(vid == 0)
            {
                patientVController.save(patid.ToString(), dateTimePicker1.Value.ToString("yyyy/MM/dd"),
                                        txtbp.Text.Trim(), txttemp.Text.Trim(), txtwt.Text.Trim(),
                                         txtlmp.Text.Trim(), txtrbc.Text.Trim());
            }
            else
            {
                patientVController.update(vid.ToString(), dateTimePicker1.Value.ToString("yyyy/MM/dd"),
                                  txtbp.Text.Trim(), txttemp.Text.Trim(), txtwt.Text.Trim(),
                                   txtlmp.Text.Trim(), txtrbc.Text.Trim());
            }
            MessageBox.Show("Succesfully Save Data");
            this.Close();
        }

        private bool isValid()
        {
            bool isValid = true;
            count = 0;

            if(txtbp.Text != "")
            {
                count++;
            }

            if (txttemp.Text != "")
            {
                count++;
            }

            if (txtwt.Text != "")
            {
                count++;
            }

            if (txtrbc.Text != "")
            {
                count++;
            }

            if (txtlmp.Text != "")
            {
                count++;
            }

            isValid = count > 0 && isValid;

            return isValid;
        }
    }
}
