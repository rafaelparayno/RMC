using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    public class ReceivableModel 
    {
        public int id { get; set; }    
        public int qty_rect { get; set; }
        public string invoiceNo { get; set; }

        public string itemName { get; set; }
        public float unitPrice { get; set; }
        public string description { get; set; }
    }
}
