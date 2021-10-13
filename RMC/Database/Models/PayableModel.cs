using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    public class PayableModel
    {
        public int id { get; set; }
        public float amount { get; set; }
        public string invoice_no { get; set; }
        public string payableDue { get; set; }
      
        public bool isPaid { get; set; }

    }
}
