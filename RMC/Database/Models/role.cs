using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    public class role
    {
        public static string tableName = "roles";
        public static string[] table_columns = { "Position" };
        public static string[] table_keys = { "@Position" };
    }
}
