using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class TransferLogsModel
    {
        public int id { get; set; }
        public int itemid { get; set; }
        public string itemName { get; set; }
        public int qtyTransfer { get; set; }
        public int fromTo { get; set; }
        public int transferid { get; set; }
        public string transferName { get; set; }
        public DateTime date_transfer { get; set; }
        public int transferBy { get; set; }
        public int editBy { get; set; }
        public DateTime editDate { get; set; }
        
    }
}
