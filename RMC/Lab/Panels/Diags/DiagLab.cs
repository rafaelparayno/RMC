using MySql.Data;
using RMC.Components;
using RMC.Database.Controllers;
using RMC.Database.Models;
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

namespace RMC.Lab.Panels.Diags
{
    public partial class DiagLab : Form
    {
        LaboratoryController labC = new LaboratoryController();
        LabTypeController labTypeController = new LabTypeController();
        AutoDocsController autoDocsController = new AutoDocsController();
        AutoParamController autoParamController = new AutoParamController();
        List<ListParams> listofListparams = new List<ListParams>();
        List<CoordinatesList> coordinatesAutomated = new List<CoordinatesList>();
        labModel lab = new labModel();
        Graphics graphicsImg = null;
        Image img = null;
        Image noImg = null;

        public Image imgToAdd = null;
        public string Lab = "";
        public string labType = "";
        public int labid = 0;

        public DiagLab()
        {
            InitializeComponent();
            loadFromDbtoCb();
            imgToAdd = null;
            Lab = "";
            labType = "";
            labid = 0;
            noImg = pbAutomated.Image;
        }

        private async void loadFromDbtoCb()
        {
            List<ComboBoxItem> task3 = await labTypeController.getComboDatas();
            cbLabType.Items.AddRange(task3.ToArray());
        }


        private async void refreshDataInCbLab(int labtype)
        {

            List<labModel> labModels = await labC.getLabModel(labtype);


            cbLab.DataSource = labModels;
            cbLab.DisplayMember = "name";

        }

        private async void displayParams(int id)
        {
            Task<List<ListParams>> listt = autoParamController.getListParams(id);
            await Task.WhenAll(listt);
            listofListparams = listt.Result;

            panelParam.Controls.Clear();
            foreach (ListParams l in listofListparams)
            {
                l.textbox1.TextChanged += new EventHandler(textChangeForDraw);
                l.Dock = DockStyle.Top;
               panelParam.Controls.Add(l);
            }
        
        }

        private void textChangeForDraw(object sender, EventArgs e)
        {
            int id = int.Parse(((TextBox)sender).Tag.ToString());
            ListParams s = listofListparams.Find(item => int.Parse((item.textbox1).Tag.ToString()) == id);
            CoordinatesList cor = new CoordinatesList();
       
            if(s.textbox1.Text != "")
            {
                int index = coordinatesAutomated.FindIndex(a => a.xCoor == s.XCoordinates);
           
                if (index == -1)
                {
              
                    cor.nameVar = s.textbox1.Text;
                    cor.xCoor = s.XCoordinates;
                    cor.yCoor = s.YCoordinates;
                    coordinatesAutomated.Add(cor);
                }
                else
                {
                    coordinatesAutomated[index].nameVar = s.textbox1.Text;
                }
            }
            else
            {
                int index = coordinatesAutomated.FindIndex(a => a.xCoor == s.XCoordinates);
                if (index > -1)
                    coordinatesAutomated.RemoveAt(index);
            }
            Draw();
            
        }

        private Image resize(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void Draw()
        {

            Color cb = Color.Black;

            Brush brush = new SolidBrush(cb);
            Font font = new Font(new FontFamily("Times New Roman"), 14);

            Image newImg = resize(img, pbAutomated.ClientSize);
            graphicsImg = Graphics.FromImage(newImg);
            foreach (CoordinatesList cors in coordinatesAutomated)
            {
                graphicsImg.DrawString(cors.nameVar, font, brush, cors.xCoor, cors.yCoor);
            }
            pbAutomated.Image = newImg;
     
            graphicsImg.Dispose();
            pbAutomated.Invalidate();
            Update();
        }


        private async void getDisplayAutomated(int id)
        {
            string fullPath = await autoDocsController.getFullPath(id);
            if (!File.Exists(fullPath))
                return;
            img = Image.FromFile(fullPath);
            pbAutomated.Image = img;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Lab = "";
            labType = "";
            imgToAdd = null;
            labid = 0;

            this.Close();
        }

        private void cbLabType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int cblabType = int.Parse((cbLabType.SelectedItem as ComboBoxItem).Value.ToString());
            pbAutomated.Image = null;
            refreshDataInCbLab(cblabType);
            cbLab.Enabled = true;
            coordinatesAutomated.Clear();

        }

        private void cbLab_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           lab = (labModel)cbLab.SelectedItem;

            if (lab.autodocsid == 0 && cbLabType.SelectedIndex != -1)
            {
                pbAutomated.Image = noImg;
                pbAutomated.SizeMode = PictureBoxSizeMode.StretchImage;
                panelParam.Visible = false;
                groupBox1.Visible = true;

            }
            else
            {
                pbAutomated.SizeMode = PictureBoxSizeMode.AutoSize;
                panelParam.Visible = true;
                groupBox1.Visible = false;
                getDisplayAutomated(lab.autodocsid);
                displayParams(lab.autodocsid);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";
            string filePath = "";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                filePath = openFileDialog.FileName;
                pbAutomated.SizeMode = PictureBoxSizeMode.AutoSize;
                pbAutomated.Image = Image.FromFile(filePath);
                img = Image.FromFile(filePath);
                coordinatesAutomated.Clear();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbLab.SelectedIndex == -1)
                return;
            if (cbLabType.SelectedIndex == -1)
                return;
            if (pbAutomated.Image == null)
                return;


            Lab = cbLab.Text;
            labType = cbLabType.Text;
            imgToAdd = pbAutomated.Image;
            labid = lab.id;
            this.Close();
        }
    }
}
