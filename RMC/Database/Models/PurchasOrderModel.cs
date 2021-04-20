using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class PurchasOrderModel
    {
        public int id { get; set; }
        public string supplierName { get; set; }
        public int supplierId { get; set; }
        public DateTime dateOrder { get; set; }
        public int orderByid { get; set; }
        public string orderbyName { get; set; }
        
    }
}
