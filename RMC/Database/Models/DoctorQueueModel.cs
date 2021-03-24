using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class DoctorQueueModel
    {

        public int id { get; set; }
        public string doctorname { get; set; }
        public int currentQueue { get; set; }
        public int nextQueue { get; set; }

    }
}
