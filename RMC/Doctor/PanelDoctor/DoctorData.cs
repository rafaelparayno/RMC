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

namespace RMC.Doctor.PanelDoctor
{
    public partial class DoctorData : Form
    {
        DoctorDataController doctorDataController = new DoctorDataController();
        public DoctorData()
        {
            InitializeComponent();
            getData();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private async void getData()
        {
            DoctorDataModel data = await doctorDataController.getDoctorData(UserLog.getUserId());

            textBox1.Text = data.pr;
            textBox2.Text = data.license;


            txtfilepath.Text = data.imgPath;


            if (data.imgPath == "" || data.imgPath == null)
                return;

            try
            {     
                Image img = Image.FromFile(data.imgPath);
                pictureBox1.Image = img;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Error Img Path Not Found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                doctorDataController.save(textBox1.Text.Trim(), textBox2.Text.Trim(), 
                    txtfilepath.Text.Trim(), UserLog.getUserId().ToString());

                MessageBox.Show("Succesfully Save Data");
            }
            else
            {
                MessageBox.Show("Please Dont leave blank");
            }
        }


        private bool isValid()
        {
            bool isValid = true;


            isValid = textBox1.Text.Trim() != "" && isValid;

            isValid = textBox2.Text.Trim() != "" && isValid;



            return isValid;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";
            string filePath = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                filePath = openFileDialog.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.Image = Image.FromFile(filePath);
                txtfilepath.Text = filePath;
            }
        }
    }
}
