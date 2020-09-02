using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class AnalysisModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public float price { get; set; }

        public int unitsSold { get; set; }

        public float anualConsumation { get; set; }
    }
}
