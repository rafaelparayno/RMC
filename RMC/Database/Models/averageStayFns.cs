using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class averageStayFns
    {
        public int itemcode { get; set; }

        public float avgStay { get; set; }

        public float CumAvgStay { get; set; }

        public float percentAvgStay { get; set; }

        public string classfication { get; set; }
    }
}
