using RMC.Database.Controllers;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Pharma.Dialogs
{
    public partial class ItemViewDetailsSku : Form
    {
        string sku = "";
        ItemController itemController = new ItemController();
        public ItemViewDetailsSku(string sku)
        {
            InitializeComponent();
            this.sku = sku;
        }

        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void ItemViewDetailsSku_Load(object sender, EventArgs e)
        {
            itemModel item = await itemController.getDataModel(sku);
            textBox1.Text = item.name;
            textBox2.Text = item.sku;
            textBox3.Text = item.stocks.ToString();
            textBox4.Text = item.unitPrice.ToString();
            textBox5.Text = item.sellingPrice.ToString();
            textBox6.Text = item.markupPrice.ToString();
        }
    }
}
