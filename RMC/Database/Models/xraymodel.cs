using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class xraymodel
    {
        public int id { get; set; }


        public int xrayid { get; set; }

        public string name { get; set; }

        public int type { get; set; }

        public int autodocsid { get; set; }

        public int is_crystal { get; set; }

        public int is_done { get; set; }
    }
}
