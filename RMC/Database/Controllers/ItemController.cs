using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class ItemController
    {
        dbcrud crud = new dbcrud();



        public int getRecentItemID()
        {
            string sql = String.Format(@"SELECT AUTO_INCREMENT As 'Last_id'
                                        FROM information_schema.tables 
                                        WHERE table_name='itemlist' 
                                        AND table_schema= DATABASE()");
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, null);
            int last_id = 0;
            if (reader.Read())
            {
                last_id = int.Parse(reader["Last_id"].ToString());
            }
            crud.CloseConnection();
            return last_id;
        }
    }
}
