﻿using RMC.Database.Controllers;
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

namespace RMC.Patients.PanelsDetails
{
    public partial class PanelPatientMedcert : Form
    {
        PatientMedcertController medcertController = new PatientMedcertController();
        List<MedCertModel> medCertModels = new List<MedCertModel>();
        private int patid = 0;
        public PanelPatientMedcert(int patid)
        {
            InitializeComponent();
            this.patid = patid;
            initLvsCols();
        }

        private async void PanelPatientMedcert_Load(object sender, EventArgs e)
        {
            medCertModels = await medcertController.listMedCert(patid);
            populateLv();
        }

        private void initLvsCols()
        {
            lvLabDetails.View = View.Details;
            lvLabDetails.Columns.Add("ID", 100, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Med Type", 200, HorizontalAlignment.Left);
            lvLabDetails.Columns.Add("Date Taken", 300, HorizontalAlignment.Left);
       
        }

        private void populateLv()
        {
            lvLabDetails.Items.Clear();
            foreach (MedCertModel m in medCertModels)
            {
                ListViewItem listViews = new ListViewItem();

                listViews.Text = m.id.ToString();
                string typeName = m.type == 1 ? "MedCert" : "Pre Employment";
                listViews.SubItems.Add(m.type.ToString(typeName));
                listViews.SubItems.Add(m.date.ToString("MMMM dd, yyyy"));

                lvLabDetails.Items.Add(listViews);
            }
        }
    }
}
