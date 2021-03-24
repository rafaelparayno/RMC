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
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
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

       

       /* private async Task<int> getCurrentQueue()
        {
           return  await customerDetailsController.getCurrentCustomer();
        }

        private async Task<int> getNextQueue()
        {
            return await customerDetailsController.nextCurrentCustomer();
        }*/


        private async void refreshQue()
        {
            Cdoctors = await uc.listDoctorOnlinesModel();
            populateDocs();


            /*  int q  =  await getCurrentQueue();
              int nq = await getNextQueue();*/
            /*  lastQue = q;
              setQueue(q);
              setNextQ(nq);*/
        }

      /*  private void setQueue(int cQ)
        {
            if (cQ == 0)
            {
                label3.Text = String.Format("No# : {0}", "None");
            }
            else
            {
                label3.Text = String.Format("No# : {0}", cQ);
            }

        
        }*/

       /* private void setNextQ(int nQ)
        {
            if (nQ == 0)
            {
                label4.Text = String.Format("No# : {0}", "None");
            }
            else
            {
                label4.Text = String.Format("No# : {0}", nQ);
            }
        }*/

        private async void timer1_Tick(object sender, EventArgs e)
        {
            /* List<int> currentDocsQ = await doctorQueueController.getQueueDoc();
             populateDocs(currentDocsQ);*/

            List<DoctorQueueModel> newDocQue = await uc.listDoctorOnlinesModel();

            foreach (DoctorQueueModel d in newDocQue)
            {
                int currentq = await doctorQueueController.getCurrentDoctorQ(d.id);



                DoctorQueueModel s = Cdoctors.Find(item => item.id == d.id);

                if(s.currentQueue != currentq)
                {
                    speech(s.doctorname, currentq.ToString());
                }

            }

                /*
                            int getCurrentDQ = currentDocsQ.Last();
                            int getNDQ = await doctorQueueController.getCurrentQ();
                            if (getCurrentDQ != getNDQ)
                            {
                                if (getNDQ != 0)
                                    speech("Doctor", getNDQ.ToString());
                            }*/

                refreshQue();


            /*  int getQ = await getCurrentQueue();
              int getNQ = await getNextQueue();*/
            /*if (lastQue != getQ)
            {
                lastQue = getQ;
                setQueue(getQ);
                setNextQ(getNQ);
                playSounds();
            }
           */



        }

    }
}
