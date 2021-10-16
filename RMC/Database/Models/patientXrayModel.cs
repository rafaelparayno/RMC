using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class patientXrayModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public int type { get; set; }

        public DateTime date { get; set; }

        public string fileName { get; set; }
    }
}
