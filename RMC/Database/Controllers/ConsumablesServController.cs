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
    class ConsumablesServController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<consumablesServMod>> getEditedConsumables(int servid)
        {
            List<consumablesServMod> getConsume = new List<consumablesServMod>();
            string sql = @"SELECT consumables_service_id,consumables_service.item_id,c_service_qty,service_id,item_name
                          FROM `consumables_service` LEFT JOIN itemlist ON consumables_service.item_id = itemlist.item_id 
                          WHERE service_id = @servid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@servid", servid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                consumablesServMod consumables = new consumablesServMod();
                consumables.id = int.Parse(reader["consumables_service_id"].ToString());
                consumables.service_id = int.Parse(reader["service_id"].ToString());
                consumables.itemid = int.Parse(reader["item_id"].ToString());
                consumables.itemname = reader["item_name"].ToString();
                consumables.qty = int.Parse(reader["c_service_qty"].ToString());
                getConsume.Add(consumables);
            }

            crud.CloseConnection();
            return getConsume;
        }

        public async void save(int itemid, int qty)
        {
            string sql = @"INSERT INTO consumables_service (service_id,item_id,c_service_qty)
                         VALUES((SELECT service_id FROM service ORDER BY service_id DESC LIMIT 1 ),
                                @itemid,@qty)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@qty", qty));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async void update(int sid, int itemid, int qty)
        {
            string sql;
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@sid", sid));
            listparams.Add(new MySqlParameter("@qty", qty));

            if (await check(itemid, sid))
            {
                sql = @"UPDATE consumables_service SET c_service_qty = @qty WHERE service_id = @sid AND item_id = @itemid";

            }
            else
            {
                sql = @"INSERT INTO consumables_service (service_id,item_id,c_service_qty)
                         VALUES(@sid, @itemid,@qty)";
            }

            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<bool> check(int itemid, int sid)
        {
            string sql = @"SELECT * FROM consumables_service WHERE service_id = @sid AND item_id = @itemid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@sid", sid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (reader.Read())
            {
                crud.CloseConnection();
                return true;
            }

            crud.CloseConnection();
            return false;
        }

        public async void remove(int itemId, int sid)
        {
            string sql = @"DELETE FROM consumables_service WHERE service_id = @sid AND item_id = @itemid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@sid", sid));
            listparams.Add(new MySqlParameter("@itemid", itemId));

            await crud.ExecuteAsync(sql, listparams);
        }
    }
}
