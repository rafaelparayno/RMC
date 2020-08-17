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
       // int counter = 0;
        Graphics graphicsImg = null;
        Image img;
        PictureBox org;
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
                org = new PictureBox();
                org.Image = pbEdited.Image;
                coordinatesAutomated.Clear();
                lbParams.Items.Clear();
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
            pbEdited.Cursor = Cursors.Hand;
        }

        private void cancelselecting()
        {
            isAddingParameters = false;
            button1.Enabled = true;
            button2.Enabled = false;
            pbEdited.Cursor = Cursors.Default;
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
          ///  Font font = new Font(new FontFamily("Times New Roman"), 20);
            
            Image newImg = resize(img, pbEdited.ClientSize);
            graphicsImg = Graphics.FromImage(newImg);
            foreach (CoordinatesList cors in coordinatesAutomated)
            {
                Pen p = new Pen(cp, 2);
                graphicsImg.DrawRectangle(p, cors.xCoor, cors.yCoor, 120, 20);
               // graphicsImg.DrawLine(p, wew.xCoor, wew.yCoor, wew.xCoor + 100, wew.yCoor);
                //graphicsImg.DrawString(wew.nameVar, font, brush, wew.xCoor, wew.yCoor);
            }


            pbEdited.Image = newImg;
          //  img = newImg;
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


            nameAutomated form = new nameAutomated(coordinatesAutomated);
            form.ShowDialog();

            if (form.name == "")
            {
                cancelselecting();
                return;
            }

            MouseEventArgs me = (MouseEventArgs)e;
            CoordinatesList cor = new CoordinatesList();
          
            cor.nameVar = form.name;
            cor.xCoor = me.X;
            cor.yCoor = me.Y;
            coordinatesAutomated.Add(cor);
            AddToLb(form.name);
           /* counter++;*/
            Draw();
            cancelselecting();
        }

        private Image ZoomPicture(Image img,Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * size.Width), Convert.ToInt32(img.Height * size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (filePath == "")
                return;

            if(trackBar1.Value != 0)
            {
              //  pbEdited.Image = null;
                pbEdited.Image = ZoomPicture(img, new Size(trackBar1.Value, trackBar1.Value));
            }
        }

        private void AddToLb(string name)
        {
            lbParams.Items.Add(name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lbParams.Items.Count == 0)
                return;

            if (lbParams.SelectedIndex == -1)
                return;

            string selectText = lbParams.GetItemText(lbParams.SelectedItem);
            lbParams.Items.RemoveAt(lbParams.SelectedIndex);

            int index = coordinatesAutomated.FindIndex(a => a.nameVar == selectText);
            coordinatesAutomated.RemoveAt(index);       
             Draw();
        }
    }
}
