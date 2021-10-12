using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
   public class itemModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public int stocks { get; set; }
        public int subCategory { get; set; }
        public string Category { get; set; }
        public int isBrand { get; set; }
        public string UnitName { get; set; }
        public string ExpirationDate { get; set; }
        public string DateAdded { get; set; }
        public float unitPrice { get; set; }
        public float markupPrice { get; set; }
        public float sellingPrice { get; set; }
        public string sku { get; set; }
        public string description { get; set; }
    }
}
