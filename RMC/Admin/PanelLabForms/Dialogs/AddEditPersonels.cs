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

        public AddEditPersonels(int id,string name,string prof,string licno)
        {
            InitializeComponent();
            isEdit = true;
            this.idEdit = id;
            textBox1.Text = name;
            cbOther.Text = prof;
            textBox2.Text = licno;
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
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private string GeneratePassword(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }

        private async Task saveData( string pathDir)
        {
            Image img = pictureBox1.Image;


            string fullPath = pathDir + "Sign-" + GeneratePassword(10) + ".jpg";
            await personelsController.save(cbOther.Text, textBox1.Text.Trim(), fullPath,textBox2.Text.Trim());
            if (img != null)
                img.Save(fullPath);
           
               

        }

        private async Task editData()
        {
            Image img = pictureBox1.Image;
            
            await personelsController.edit(cbOther.Text, textBox1.Text.Trim(), 
                idEdit.ToString(),textBox2.Text.Trim());
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
               
                await saveData(pathDir);
               
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

            isValid = !(cbOther.SelectedIndex == -1) && isValid;
         
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
