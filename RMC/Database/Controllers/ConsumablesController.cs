using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class ConsumablesController
    {
        dbcrud crud = new dbcrud();


        public async Task<Dictionary<int,int>> getListItemConsumables(int labid)
        {
            Dictionary<int, int> consumables = new Dictionary<int, int>();

            string sql = @"SELECT * FROM consumables WHERE laboratory_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", labid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                consumables.Add(int.Parse(reader["item_id"].ToString()),
                                int.Parse(reader["consumables_qty"].ToString()));
            }

            crud.CloseConnection();


            return consumables;
        }


        public async Task<List<consumablesMod>> getEditedConsumables(int labid)
        {
            List<consumablesMod> getConsume = new List<consumablesMod>();
            string sql = @"SELECT consumables_id,consumables.item_id,consumables_qty,laboratory_id,item_name
                          FROM `consumables` LEFT JOIN itemlist ON consumables.item_id = itemlist.item_id 
                          WHERE laboratory_id = @labid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@labid", labid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                consumablesMod consumables = new consumablesMod();
                consumables.id = int.Parse(reader["consumables_id"].ToString());
                consumables.labid = int.Parse(reader["laboratory_id"].ToString());
                consumables.itemid = int.Parse(reader["item_id"].ToString());
                consumables.itemname = reader["item_name"].ToString();
                consumables.qty = int.Parse(reader["consumables_qty"].ToString());
                getConsume.Add(consumables);
            }

            crud.CloseConnection();
            return getConsume;
        }

        public async void save (int itemid,int qty)
        {
            string sql = @"INSERT INTO consumables (laboratory_id,item_id,consumables_qty)
                         VALUES((SELECT laboratory_id FROM laboratorylist ORDER BY laboratory_id DESC LIMIT 1 ),
                                @itemid,@qty)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@qty", qty));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async void update(int labid,int itemid,int qty)
        {
            string sql;
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@labid", labid));
            listparams.Add(new MySqlParameter("@qty", qty));

            if (await check(itemid, labid))
            {
                sql = @"UPDATE consumables SET consumables_qty = @qty WHERE laboratory_id = @labid AND item_id = @itemid";
                
            }
            else
            {
                sql = @"INSERT INTO consumables (laboratory_id,item_id,consumables_qty)
                         VALUES(@labid, @itemid,@qty)";
            }

            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<bool> check(int itemid,int labid)
        {
            string sql = @"SELECT * FROM consumables WHERE laboratory_id = @labid AND item_id = @itemid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@labid", labid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (reader.Read())
            {
                crud.CloseConnection();
                return true;
            }

            crud.CloseConnection();
            return false;
        }

        public async void remove(int itemId,int labid)
        {
            string sql = @"DELETE FROM consumables WHERE laboratory_id = @labid AND item_id = @itemid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@labid", labid));
            listparams.Add(new MySqlParameter("@itemid", itemId));

            await crud.ExecuteAsync(sql, listparams);
        }


    }
}
