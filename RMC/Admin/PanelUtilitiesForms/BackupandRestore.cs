using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RMC.Components;
using RMC.Database;
using RMC.Utilities;

namespace RMC.Admin.PanelUtilitiesForms
{
    public partial class BackupandRestore : Form
    {

        private string dateClicked = string.Empty;
        private string timeClicked = string.Empty;
        loading loading = new loading();
        public BackupandRestore()
        {
            InitializeComponent();
        }

        private void editUser_Click(object sender, EventArgs e)
        {
            DialogResult form1 = MessageBox.Show("Do you really want to back up data?",
                  "Exit", MessageBoxButtons.YesNo);


            if (form1 == DialogResult.Yes)
            {

                backgroundWorker1.RunWorkerAsync();
               
                csBackupAndRestore.DoBackup();
                MessageBox.Show("Back Up Data Success");


               
                reloadBackup();
            }
        }


        private void reloadBackup()
        {
            csBackupAndRestore.CreateDirectory();
            string filePathServer = ReadFileServerPath.FetchServerLocation();
            string newDir = String.Format(@"{0}{1}\", filePathServer, "RMC-backup");
            DirectoryInfo d = new DirectoryInfo(newDir);
            FileInfo[] Files = d.GetFiles("*.sql");
            Array.Sort(Files, (f1, f2) => f1.Name.CompareTo(f2.Name));

            int backupIndex = 1;

            lv_Backup.Items.Clear();

            for (int i = Files.Length - 1; i >= 0; i--)
            {

                string[] dateTime = Files[i].Name.Split(new[] { "--" }, StringSplitOptions.None);
                string dateParse = dateTime[1] + "-" + dateTime[2] + "-" + dateTime[3];
                string timeParse = dateTime[4] + ":" + dateTime[5] + ":" + dateTime[6] + " " + dateTime[7].Replace(".sql", "");


                if (dateParse != datePick_Logs.Value.ToString("yyyy-MM-dd"))
                {
                    continue;
                }


                ListViewItem lv = new ListViewItem();
                lv.Text = dateParse;
                lv.SubItems.Add(timeParse);

                lv_Backup.Items.Add(lv);
                backupIndex++;
            }


        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                Thread.Sleep(10);
                backgroundWorker1.WorkerReportsProgress = true;
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void ResetPassword_Click(object sender, EventArgs e)
        {
            if (dateClicked != "" && timeClicked != "")
            {
                csBackupAndRestore.DoRestore(dateClicked, timeClicked);
                MessageBox.Show("Recover Data Success");
                reloadBackup();

            }
            else
            {
                MessageBox.Show("Please Choose Date to recover", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void datePick_Logs_ValueChanged(object sender, EventArgs e)
        {
            reloadBackup();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            datePick_Logs.Value = DateTime.Now;
        }

        private void lv_Backup_Click(object sender, EventArgs e)
        {
            if (lv_Backup.SelectedItems.Count == 1)
            {
                dateClicked = lv_Backup.SelectedItems[0].SubItems[0].Text;
                timeClicked = lv_Backup.SelectedItems[0].SubItems[1].Text;

                ResetPassword.Enabled = true;
            }
            else
            {
                ResetPassword.Enabled = false;
            }
        }
    }
}
