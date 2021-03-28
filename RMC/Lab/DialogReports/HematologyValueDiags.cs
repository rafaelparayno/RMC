using CrystalDecisions.Shared;
using RMC.NewReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Lab.DialogReports
{
    public partial class HematologyValueDiags : Form
    {
       // List<string> values = new List<string>();
        List<TextBoxParamsCrystal> textBoxParamsCrystals = new List<TextBoxParamsCrystal>();
        Dictionary<string, string> valuesInReports;
        public HematologyValueDiags()
        {
            InitializeComponent();
            loadValues();
            loadTxts();
        }

        public void loadValues()
        {
            Hematology hema = new Hematology();

            foreach(ParameterField parameterField in hema.ParameterFields)
            {
                TextBoxParamsCrystal textBoxParams = new TextBoxParamsCrystal();
                if (parameterField.Name == "patientName")
                    continue;
                if (parameterField.Name == "age")
                    continue;
                if (parameterField.Name == "sex")
                    continue;

                textBoxParams.NameLabel = parameterField.Name;
                textBoxParamsCrystals.Add(textBoxParams);
                //values.Add(parameterField.Name);
            }
            
        }


        private void loadTxts()
        {
            mainPanel.Controls.Clear();
            foreach(TextBoxParamsCrystal t in textBoxParamsCrystals)
            {
                t.Dock = DockStyle.Top;
                mainPanel.Controls.Add(t);
            }
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            valuesInReports = new Dictionary<string, string>();
            foreach (TextBoxParamsCrystal t in textBoxParamsCrystals)
            {
                valuesInReports.Add(t.NameLabel, t.Value);
            }


          
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
