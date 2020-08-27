using RMC.Components;
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

namespace RMC.Lab.Panels.Diags
{
    public partial class DiagLab : Form
    {
        LaboratoryController labC = new LaboratoryController();
        LabTypeController labTypeController = new LabTypeController();
        AutoDocsController autoDocsController = new AutoDocsController();
        AutoParamController autoParamController = new AutoParamController();
        List<ListParams> listofListparams = new List<ListParams>();
        public DiagLab()
        {
            InitializeComponent();
            loadFromDbtoCb();
        }

        private async void loadFromDbtoCb()
        {
            List<ComboBoxItem> task3 = await labTypeController.getComboDatas();
            cbLabType.Items.AddRange(task3.ToArray());
        }

        private void cbLabType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cblabType = int.Parse((cbLabType.SelectedItem as ComboBoxItem).Value.ToString());
            refreshDataInCbLab(cblabType);
            cbLab.Enabled = true;
        }

        private async void refreshDataInCbLab(int labtype)
        {

            List<labModel> labModels = await labC.getLabModel(labtype);


            cbLab.DataSource = labModels;
            cbLab.DisplayMember = "name";

        }

        private void cbLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            labModel lab = (labModel)cbLab.SelectedItem;

            if(lab.autodocsid == 0 && cbLabType.SelectedIndex != -1)
            {
                panelWithAuto.Visible = false;
                groupBox1.Visible = true;
               
            }
            else
            {
                panelWithAuto.Visible = true;
                groupBox1.Visible = false;
                getDisplayAutomated(lab.autodocsid);
                displayParams(lab.autodocsid);
            }
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
            string txt = s.textbox1.Text;
            Draw(txt, s.XCoordinates, s.YCoordinates);
            
        }

        private void Draw(string txt,float x,float y)
        {
          
            Color cb = Color.LightGray;

            Brush brush = new SolidBrush(cb);
            Font font = new Font(new FontFamily("Times New Roman"), 20);

            Image newImg = pbAutomated.Image;
            Graphics graphicsImg = Graphics.FromImage(newImg);
            graphicsImg.DrawString(txt, font, brush, x, y);
     
            pbAutomated.Image = newImg;
            graphicsImg.Dispose();
            pbAutomated.Invalidate();
            Update();
        }


        private async void getDisplayAutomated(int id)
        {
            string fullPath = await autoDocsController.getFullPath(id);
            pbAutomated.Image = Image.FromFile(fullPath);
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
