using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class patientDetails
    {

        public int id { get; set; }
        public string Firstname { get; set; }

        public string middlename { get; set; }

        public string lastname { get; set; }

        public string FullName
        {
            get
            {
                return $"{ Firstname } { lastname}";
            }
        }

        public int age { get; set; }

        public string gender { get; set; }

        public string address { get; set; }

        public string contact { get; set; }

        public string birthdate { get; set; }

        public string civil_status { get; set; }
    }
}
