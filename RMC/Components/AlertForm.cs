using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Components
{
    public partial class AlertForm : Form
    {

        public AlertForm(string title, string Message)
        {
            InitializeComponent();

            lblMessage.Text = Message;

            lblTitle.Text = title;

     
        }


        public AlertForm(string title,string Message,int fontSize)
        {
            InitializeComponent();

            lblMessage.Text = Message;

            lblTitle.Text = title;

            lblMessage.Font = new Font("Tahoma", fontSize, FontStyle.Regular);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
