using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class DoctorResult
    {
        public int id { get; set; }
        
        public int patient_id { get; set; }

        public int doctor_id { get; set; }

        public string cc { get; set; }

        public string sfindings { get; set; }

        public string assestment { get; set; }

        public string procedureA { get; set; }

        public DateTime date_results { get; set; }
    }
}
