using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

        public async Task<float> getConsumedCost(string date)
        {
            float totalCost = 0;
            string sql = @"SELECT SUM(consumed_cost) as 'ItemCost' 
                        FROM `consumed_items`
                     WHERE Date(date_consumed) = @date";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() 
            { (new MySqlParameter("@date", date)) };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while(await reader.ReadAsync())
            {
                totalCost = float.TryParse(reader["ItemCost"].ToString(),out _) ? 
                    float.Parse(reader["ItemCost"].ToString()) : 0;
            }

            crud.CloseConnection();
            return totalCost;

        }


        
    }
}
