using RMC.Database.Controllers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using RMC.Components;
using RMC.Database.Models;
using Org.BouncyCastle.Utilities;

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class ShowQueue : Form
    {
       
        DoctorQueueController doctorQueueController = new DoctorQueueController();
        LabQueueController labQueueController = new LabQueueController();
        UserracountsController uc = new UserracountsController();
        SpeechSynthesizer _ss = new SpeechSynthesizer();
        List<DoctorQueueModel> Cdoctors;
        List<int> listQueStrings = new List<int>();


        public ShowQueue()
        {
            InitializeComponent();
         
            _ss.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);
            timer1.Start();
        }

        private void speech(string place, string queueNo)
        {
            bool task1 = playSounds();
            if (task1)
                _ss.SpeakAsync($"Currently Serving in {place} is Queue Number {queueNo}");

        }

       private bool playSounds()
        {
            bool _isStopped = false;
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + @"\notfy.wav");
            player.PlaySync();
            _isStopped = true;
            return _isStopped;
        }

        private async void populateDocs()
        {
            panelDoctor.Controls.Clear();
            foreach(DoctorQueueModel d in Cdoctors)
            {
                DoctorQueueControl dc = new DoctorQueueControl();

                int currentq = await  doctorQueueController.getCurrentDoctorQ(d.id);
                int nextQ = await doctorQueueController.getNextDoctorQ(d.id);
                int index = Cdoctors.FindIndex(item => item.id == d.id);


                Cdoctors[index].currentQueue = currentq;
                Cdoctors[index].nextQueue = nextQ;

                dc.DoctorName = d.doctorname;
                dc.DocId = d.id;
                dc.CurrentQueue = currentq.ToString();
                dc.NextQueue = nextQ == 0 ? "" : nextQ.ToString();
                dc.Dock = DockStyle.Left;
                panelDoctor.Controls.Add(dc);
            }
        }

        private void populateLabQueue()
        {

            panelLab.Controls.Clear();
            foreach(int s in listQueStrings)
            {
                Label l = new Label();
                l.Dock = DockStyle.Top;
                l.Text = s.ToString();
                l.Height = 50;
                l.Width = 300;
                l.TextAlign = ContentAlignment.TopCenter;
                l.Font = new Font("Microsoft Sans Serif", 36);
                panelLab.Controls.Add(l);
            }


            Label l2 = new Label();
            l2.Dock = DockStyle.Top;
            l2.Text = "Currently";
            l2.Height = 50;
            l2.Width = 300;
            l2.TextAlign = ContentAlignment.TopCenter;
            l2.Font = new Font("Microsoft Sans Serif", 24);
            panelLab.Controls.Add(l2);

        }



        private async Task refreshQue()
        {

            Task<List<DoctorQueueModel>> task1 = uc.listDoctorOnlinesModel();
            Task<List<int>> task2 = labQueueController.listLabQueue();
            Task[] taskargs = new Task[] { task1, task2 };

            await Task.WhenAll(taskargs);

            Cdoctors = task1.Result;
            listQueStrings = task2.Result;


            populateDocs();
            populateLabQueue();

         
        }

        private async Task LabSounds()
        {
            List<int> newListLabQueue = await labQueueController.listLabQueue();

            if (newListLabQueue.Count == 0)
                return;

            int newQ = newListLabQueue.Select(s => s).Min();
            int lastQ = listQueStrings.Select(l => l).Min();

            if(newQ != lastQ)
            {
                speech("Laboratory", newQ.ToString());
            }

        }

        private async Task doctorSounds()
        {
            List<DoctorQueueModel> newDocQue = await uc.listDoctorOnlinesModel();

            foreach (DoctorQueueModel d in newDocQue)
            {
                int currentq = await doctorQueueController.getCurrentDoctorQ(d.id);



                DoctorQueueModel s = Cdoctors.Find(item => item.id == d.id);

                if (s.currentQueue != currentq)
                {
                    speech(s.doctorname, currentq.ToString());
                }

            }
        }
  

        private async void timer1_Tick(object sender, EventArgs e)
        {

            await doctorSounds();
            await LabSounds();     
            await refreshQue();

        }

        private async  void ShowQueue_Load(object sender, EventArgs e)
        {
            await refreshQue();
        }
    }
}
