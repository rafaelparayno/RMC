using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class ServiceModel
    {
        public int id { get; set; }

        public float price { get; set; }

        public string serviceName { get; set; }


        public string desc { get; set; }

        public int hasFileAttach { get; set; }

        public int isDone { get; set; }
       
    }
}
