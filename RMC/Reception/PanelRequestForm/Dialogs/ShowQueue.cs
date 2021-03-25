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

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class ShowQueue : Form
    {
       
        DoctorQueueController doctorQueueController = new DoctorQueueController();
        UserracountsController uc = new UserracountsController();
        SpeechSynthesizer _ss = new SpeechSynthesizer();
        List<DoctorQueueModel> Cdoctors;


        public ShowQueue()
        {
            InitializeComponent();
            refreshQue();
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

 

        private async void refreshQue()
        {
            Cdoctors = await uc.listDoctorOnlinesModel();
            populateDocs();


         
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
                 
             refreshQue();

        }

    }
}
