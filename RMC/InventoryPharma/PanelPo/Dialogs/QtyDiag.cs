﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.InventoryPharma.PanelPo.Dialogs
{
    public partial class QtyDiag : Form
    {
        public int qty = 0;
        public QtyDiag()
        {
            InitializeComponent();
            qty = 0;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            qty = 0;
            this.Close();
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value == 0)
                return;

            qty = int.Parse(numericUpDown1.Value  + "");
            this.Close();
        }
    }
}