using RMC.Database.Controllers;
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

namespace RMC.Admin.PanelLabForms.Dialogs
{
    public partial class AddEditPersonels : Form
    {
        PersonelsController personelsController = new PersonelsController();
        bool isEdit = false;
        string pathEdit = "";
        private int idEdit = 0;
        public AddEditPersonels()
        {
            InitializeComponent();

        }

        public AddEditPersonels(int id,string name,string prof)
        {
            InitializeComponent();
            isEdit = true;
            this.idEdit = id;
            textBox1.Text = name;
            cbOther.Text = prof;
           
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cbOther_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void AddEditPersonels_Load(object sender, EventArgs e)
        {
            if (isEdit)
            {
                try
                {
                    pathEdit = await personelsController.imgPath(idEdit);

                    if (!File.Exists(pathEdit))
                        return;

                    FileStream fileStream = new FileStream(pathEdit, FileMode.OpenOrCreate);

                    pictureBox1.Image = Image.FromStream(fileStream);
                    txtfilepath.Text = pathEdit;
                    fileStream.Close();

                }
                catch(IOException ex)
                {

                }
            }
        }

        private async Task saveData(int genRand, string pathDir)
        {
            Image img = pictureBox1.Image;


            string fullPath = pathDir + "Sign-" + genRand + ".jpg";
            await personelsController.save(cbOther.Text, textBox1.Text.Trim(), fullPath);
            if (img != null)
                img.Save(fullPath);
           
               

        }

        private async Task editData()
        {
            Image img = pictureBox1.Image;
            
            await personelsController.edit(cbOther.Text, textBox1.Text.Trim(), idEdit.ToString());
            if (img != null)
            {

                if (File.Exists(pathEdit))
                    File.Delete(pathEdit);


                img.Save(pathEdit);

                
            }
           
        
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
               
            }
            else
            {
                await editData();
            }
            MessageBox.Show("Succesfully Save Data");
            this.Close();
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
