using RMC.Database.Controllers;
using RMC.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelLabForms.Dialogs
{
    public partial class AddEditPersonels : Form
    {
        PersonelsController personelsController = new PersonelsController();
        bool isEdit = false;
        public AddEditPersonels()
        {
            InitializeComponent();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cbOther_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddEditPersonels_Load(object sender, EventArgs e)
        {

        }

        private async Task saveData(int genRand, string pathDir)
        {
            Image img = pictureBox1.Image;


            string fullPath = pathDir + "Sign-" + genRand + ".jpg";
            await personelsController.save(textBox1.Text.Trim(), cbOther.Text, fullPath);
            if (img != null)
                img.Save(fullPath);

        }

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!isValid())
            {
                MessageBox.Show("Please Fill the Data", "validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!isEdit)
            {
                string pathDir = CreateDirectory.CreateDir("Signatures");
                Random r = new Random();
                int genRand = r.Next(10, 50);
                await saveData(genRand, pathDir);
                MessageBox.Show("Succesfully Save Data");
                this.Close();
            }
            else
            {

            }
        }


        private bool isValid()
        {

            bool isValid = true;


         
            isValid = !(string.IsNullOrEmpty(cbOther.Text.Trim())) && isValid;

            isValid = !(string.IsNullOrEmpty(textBox1.Text.Trim())) && isValid;

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
         
                pictureBox1.Image = Image.FromFile(filePath);
                txtfilepath.Text = filePath;
            }
        }
    }
}
