using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class customerDetailsMod
    {
        public int id { get; set; }

        public string name
        {
            get { return fname + " " + mname + " " + lname; }
            set { name = value; }
        }

    

        public string fname { get; set; }

        public string lname { get; set; }

        public string mname { get; set; }

        public int age { get; set; }

        public int quueu_no { get; set; }

        public string gender { get; set; }

        public string cs { get; set; }

        public string cp_no { get; set; }

        public string address { get; set; }
    }
}
