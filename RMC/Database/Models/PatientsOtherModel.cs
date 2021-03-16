using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class PatientsOtherModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }

        public int p_id { get; set; }

        public DateTime date_upload { get; set; }
    }
}
