using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class consumablesMod
    {
        public int id { get; set; }

        public int labid { get; set; }

        public int itemid { get; set; }

        public string itemname { get; set; }
        public int qty { get; set; }
    }
}
