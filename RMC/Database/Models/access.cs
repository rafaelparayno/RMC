using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class access
    {
        public static string tableName = "access";
        public static string[] table_columns = { "acesss","role_id" };
        public static string[] table_keys = { "@acesss", "@role_id" };
    }
}
