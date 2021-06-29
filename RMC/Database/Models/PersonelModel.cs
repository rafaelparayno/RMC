using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class PersonelModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string licno { get; set; }

        public string profession { get; set; }

        public string imgPath { get; set; }

        public int is_active { get; set; }

    }
}
