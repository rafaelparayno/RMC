using RMC.Pharma.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Pharma
{
    public partial class DailySalesPharRepList : Form
    {
        public DailySalesPharRepList()
        {
            InitializeComponent();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditDailyPharSalesRep form = new AddEditDailyPharSalesRep();
            form.ShowDialog();
        }
    }
}
