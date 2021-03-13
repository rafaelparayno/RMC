using RMC.Database.Controllers;
using RMC.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace RMC.InventoryPharma.Barcodes
{
    public partial class PrintBarcodes : Form
    {


        ItemController itemZ = new ItemController();
        public PrintBarcodes()
        {
            InitializeComponent();
        }

        private async void PrintBarcodes_Load(object sender, EventArgs e)
        {
     
            DataSet ds = await itemZ.getdataSetActive();

            barcode cos = new barcode();
            cos.SetDataSource(ds);

            crystalReportViewer1.ReportSource = cos;

        }
    }
}
