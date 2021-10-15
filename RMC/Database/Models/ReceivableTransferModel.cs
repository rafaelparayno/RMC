using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    public class ReceivableTransferModel
    {
        public int id { get; set; }

        public float amount { get; set; }

        public string invoice { get; set; }
        
        public string dateTransfer { get; set; }

        public int isPaid { get; set; }

        public string checkNo { get; set; }

        public string checkDate { get; set; }

        public string dueDate { get; set; }

        public int pid { get; set; }

        public string namep { get; set; }

        public float amountPaid { get; set; }

    }
}
