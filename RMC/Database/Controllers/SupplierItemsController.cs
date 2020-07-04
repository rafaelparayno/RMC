using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
   public class SupplierItemsController
    {
        dbcrud crud = new dbcrud();

        public async void Save(int item_id,List<int> suppliers_id)
        {
            string sql = String.Format(@"INSERT INTO supplier_items (item_id,supplier_id) VALUES (@itemid,@supid)");

            foreach( int supid in suppliers_id)
            {
                if (hasAlreadyAnAccess(item_id, supid))
                    continue;

                List<MySqlParameter> list = new List<MySqlParameter>();
                list.Add(new MySqlParameter("@itemid", item_id));
                list.Add(new MySqlParameter("@supid", supid));
                await crud.ExecuteAsync(sql, list);
            }
        }


        private bool hasAlreadyAnAccess(int itemid, int supid)
        {
      
            bool alreadAccess = false;
            MySqlDataReader reader = null;
            string sql = String.Format(@"SELECT * FROM supplier_items WHERE item_id = @itemid AND supplier_id = @supid");
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@itemid", itemid));
            list.Add(new MySqlParameter("@supid", supid));
            crud.RetrieveRecords(sql, ref reader, list);

            if (reader.HasRows)
            {
                alreadAccess = true;
            }

            crud.CloseConnection();

            return alreadAccess;
        }
    }
}
