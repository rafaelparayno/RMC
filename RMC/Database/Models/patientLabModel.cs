using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class patientLabModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string filename { get; set; }

        public DateTime date { get; set; }
    }
}
