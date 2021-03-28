using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class labModel
    {
        public int id { get; set; }

        public int labID { get; set; }

        public string name { get; set; }

        public int autodocsid { get; set; }

        public string labtypename { get; set; }

        public int crystal_id_lab { get; set; }

        public int is_done { get; set; }
    }
}
