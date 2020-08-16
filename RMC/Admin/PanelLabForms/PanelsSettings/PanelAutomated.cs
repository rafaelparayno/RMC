using RMC.Admin.PanelLabForms.Dialogs;
using RMC.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Admin.PanelLabForms.PanelsSettings
{
    public partial class PanelAutomated : Form
    {
        List<CoordinatesList> coordinatesAutomated = new List<CoordinatesList>();
        private bool isAddingParameters = false;
        private string filePath = "";
        Graphics graphicsImg = null;
        Image img;
        public PanelAutomated()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";
             filePath = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                filePath = openFileDialog.FileName;

                pbEdited.Image = Image.FromFile(filePath);
                img = Image.FromFile(filePath);
                pbEdited.BringToFront();
                button1.Enabled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (filePath == "")
                return;
       
            selecting();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancelselecting();
        }


        private void selecting()
        {
            isAddingParameters = true;
            button1.Enabled = false;
            button2.Enabled = true;

        }

        private void cancelselecting()
        {
            isAddingParameters = false;
            button1.Enabled = true;
            button2.Enabled = false;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (filePath == "")
                return;

            if (coordinatesAutomated.Count == 0)
                return;

           
         
        }

        private Image resize(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void Draw()
        {
            Color cp = Color.LightSteelBlue ;
            Color cb = Color.LightGray;
          
            Brush brush = new SolidBrush(cb);
            Font font = new Font(new FontFamily("Times New Roman"), 20);

            Image newImg = resize(img, pbEdited.ClientSize);
            graphicsImg = Graphics.FromImage(newImg);
            foreach (CoordinatesList wew in coordinatesAutomated)
            {
                Pen p = new Pen(cp, 2);
                graphicsImg.DrawRectangle(p, wew.xCoor, wew.yCoor, 100, 40);
               // graphicsImg.DrawLine(p, wew.xCoor, wew.yCoor, wew.xCoor + 100, wew.yCoor);
                //graphicsImg.DrawString(wew.nameVar, font, brush, wew.xCoor, wew.yCoor);
            }


            pbEdited.Image = newImg;
            //newImg.Save("new.jpg");
            graphicsImg.Dispose();
            pbEdited.Invalidate();
            Update();
        }

        private void save()
        {
            /*Color cp = Color.LightSteelBlue;
            Color cb = Color.LightGray;

            Brush brush = new SolidBrush(cb);
            Font font = new Font(new FontFamily("Times New Roman"), 20);

            Image newImg = resize(img, pbEdited.ClientSize);
            Graphics graphicsImg = Graphics.FromImage(newImg);
            foreach (CoordinatesList wew in coordinatesAutomated)
            {
                Pen p = new Pen(cp, 2);
                graphicsImg.DrawRectangle(p, wew.xCoor, wew.yCoor, 100, 40);
                // graphicsImg.DrawLine(p, wew.xCoor, wew.yCoor, wew.xCoor + 100, wew.yCoor);
                //graphicsImg.DrawString(wew.nameVar, font, brush, wew.xCoor, wew.yCoor);
            }


            pbEdited.Image = newImg;
            //newImg.Save("new.jpg");
            graphicsImg.Dispose();
            pbEdited.Invalidate();
            Update();*/

        }
     

        private void pbEdited_Click(object sender, EventArgs e)
        {

            if (!isAddingParameters)
                return;


            nameAutomated form = new nameAutomated();
            form.ShowDialog();

            if (form.name == "")
                return;

            MouseEventArgs me = (MouseEventArgs)e;
            CoordinatesList cor = new CoordinatesList();
            cor.nameVar = form.name;
            cor.xCoor = me.X;
            cor.yCoor = me.Y;
            coordinatesAutomated.Add(cor);
            cancelselecting();
            Draw();
        }
    }
}
