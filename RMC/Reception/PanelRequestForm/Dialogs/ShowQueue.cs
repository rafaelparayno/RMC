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

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class ShowQueue : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        DoctorQueueController doctorQueueController = new DoctorQueueController();
        UserracountsController uc = new UserracountsController();
        SpeechSynthesizer _ss = new SpeechSynthesizer();
        List<int> currentDocsQ = new List<int>();
     
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

        private async void populateDocs(List<int> currentDocsQ)
        {
            panelDoctor.Controls.Clear();
            Dictionary<int,string> doctors = await uc.listDoctorOnlinesDic();


            foreach(KeyValuePair<int,string> valuePair in doctors)
            {
                DoctorQueueControl dc = new DoctorQueueControl();

                int currentq = await  doctorQueueController.getCurrentDoctorQ(valuePair.Key);
                int nextQ = await doctorQueueController.getNextDoctorQ(valuePair.Key);
                dc.DoctorName = valuePair.Value;
                dc.DocId = valuePair.Key;
                dc.CurrentQueue = currentq.ToString();
                dc.NextQueue = nextQ == 0 ? "" : nextQ.ToString();

                panelDoctor.Controls.Add(dc);

            }
            /*    foreach (int i in currentDocsQ)
                {
                    Label l = new Label();
                    l.Dock = DockStyle.Top;
                    l.AutoSize = false;
                    l.Size = new Size(345, 70);
                    l.Font = new Font("Tahoma", 32, FontStyle.Regular);
                    l.TextAlign = ContentAlignment.TopCenter;
                    l.Text = i.ToString();
                    panelDoctor.Controls.Add(l);
                }


    */


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
            currentDocsQ = await doctorQueueController.getQueueDoc();
            populateDocs(currentDocsQ);


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

       /*     int getCurrentDQ = currentDocsQ.Last();
            int getNDQ = await doctorQueueController.getCurrentQ();
            if(getCurrentDQ != getNDQ)
            {
                if(getNDQ != 0)
                  speech("Doctor", getNDQ.ToString());
            }

            refreshQue();*/


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
