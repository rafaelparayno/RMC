using RMC.Database.Controllers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Reception.PanelRequestForm.Dialogs
{
    public partial class ShowQueue : Form
    {
        CustomerDetailsController customerDetailsController = new CustomerDetailsController();
        int lastQue = 0;
        public ShowQueue()
        {
            InitializeComponent();
            refreshQue();
            timer1.Start();
        }

       private void playSounds()
        {
            string current = Directory.GetCurrentDirectory();
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + @"\notfy.wav");
            player.Play();
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
