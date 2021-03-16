using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class patientVModel
    {
        public int id { get; set; }

        public int patient_id { get; set; }

        public string date_vital { get; set; }
        public string bp { get; set; }
        public string temp { get; set; }
        public string wt { get; set; }
        public string lmp { get; set; }
        public string ua { get; set; }
        public string pus { get; set; }

        public string allergies { get; set; }
    }
}
