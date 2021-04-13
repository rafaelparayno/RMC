using RMC.Database.Controllers;
using RMC.Database.Models;
using RMC.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Doctor.PanelDoctor
{
    public partial class DoctorData : Form
    {
        DoctorDataController doctorDataController = new DoctorDataController();
        private string pathEdit = "";
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

            pathEdit = data.imgPath;
            if (data.imgPath == "" || data.imgPath == null)
                return;

            try
            {

                if (!File.Exists(pathEdit))
                    return;

                FileStream fileStream = new FileStream(pathEdit, FileMode.OpenOrCreate);

                pictureBox1.Image = Image.FromStream(fileStream);
                txtfilepath.Text = pathEdit;
                fileStream.Close();
                
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Error Img Path Not Found", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string GeneratePassword(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                string pathDir = CreateDirectory.CreateDir("Signatures");
            
                 await saveData(pathDir);
              

                MessageBox.Show("Succesfully Save Data");
            }
            else
            {
                MessageBox.Show("Please Dont leave blank");
            }
        }

        private async Task saveData(string pathDir)
        {
            Image img = pictureBox1.Image;

              
            string fullPath = pathDir + "Sign-" + GeneratePassword(10) + "-" + UserLog.getUserId() + ".jpg";
           await doctorDataController.save(textBox1.Text.Trim(), textBox2.Text.Trim(),
                    fullPath, UserLog.getUserId().ToString());
            if(img != null)
            {
                if (File.Exists(pathEdit))
                {
                    File.Delete(pathEdit);
                }



                img.Save(fullPath);
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            txtfilepath.Text = "";
            pictureBox1.Image = null;
        }
    }
}
