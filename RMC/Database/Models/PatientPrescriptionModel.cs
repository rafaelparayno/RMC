using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class PatientPrescriptionModel
    {
        public int id { get; set; }

        public string medName { get; set; }

        public int itemid { get; set; }

        public string instruction { get; set; }

        public string dispenseno { get; set; }

        public string sinstruction { get; set; }

        public DateTime date { get; set; }
    }
}
