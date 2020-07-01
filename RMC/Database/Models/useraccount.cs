using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    public class useraccount
    {
        public static string tableName = "useraccounts";
        public static string[] table_columns = { "firstname", "middlename", "lastname", "position", "username", "password" };
        public static string[] table_keys = { "@firstname", "@middlename", "@lastname", "@position", "@username", "@password" };
    }
}
