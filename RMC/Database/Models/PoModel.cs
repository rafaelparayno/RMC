using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class PoModel
    {
      
        public int item_id { get; set; }
        public string item_name { get; set; }

        public int quantity_order { get; set; }
        public float unitCosts { get; set; }
        public float sellingPrice { get; set; }
        public float totalCost { get; set; }
        public string desc { get; set; }
      
    }
}
