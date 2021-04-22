using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class ConsumedItems
    {
        dbcrud crud = new dbcrud();

        public async Task save(int itemid,int qty,float consumedCost)
        {
            string sql = @"INSERT INTO consumed_items (item_id,consumed_qty,consumed_cost) 
                         VALUES(@itemid,@qty,@cost)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@qty", qty));
            listparams.Add(new MySqlParameter("@cost", consumedCost));

            await crud.ExecuteAsync(sql, listparams);

        }


        
    }
}
