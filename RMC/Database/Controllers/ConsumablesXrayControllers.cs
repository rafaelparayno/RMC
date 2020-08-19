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
    class ConsumablesXrayControllers
    {
        dbcrud crud = new dbcrud();


        public async Task<List<consumablesMod>> getEditedConsumables(int xrayid)
        {
            List<consumablesMod> getConsume = new List<consumablesMod>();
            string sql = @"SELECT consumables_xrays_id,consumables_xrays.item_id,consumables_x_qty,xray_id,item_name
                          FROM `consumables_xrays` LEFT JOIN itemlist ON consumables_xrays.item_id = itemlist.item_id 
                          WHERE xray_id = @xrayid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@xrayid", xrayid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                consumablesMod consumables = new consumablesMod();
                consumables.id = int.Parse(reader["consumables_xrays_id"].ToString());
                consumables.labid = int.Parse(reader["xray_id"].ToString());
                consumables.itemid = int.Parse(reader["item_id"].ToString());
                consumables.itemname = reader["item_name"].ToString();
                consumables.qty = int.Parse(reader["consumables_x_qty"].ToString());
                getConsume.Add(consumables);
            }

            crud.CloseConnection();
            return getConsume;
        }

        public async void save(int itemid, int qty)
        {
            string sql = @"INSERT INTO consumables_xrays (xray_id,item_id,consumables_x_qty)
                         VALUES((SELECT xray_id FROM xraylist ORDER BY xray_id DESC LIMIT 1 ),
                                @itemid,@qty)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@qty", qty));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async void update(int xrayid, int itemid, int qty)
        {
            string sql;
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@xrayid", xrayid));
            listparams.Add(new MySqlParameter("@qty", qty));

            if (await check(itemid, xrayid))
            {
                sql = @"UPDATE consumables_xrays SET consumables_x_qty = @qty WHERE xray_id = @xrayid AND item_id = @itemid";

            }
            else
            {
                sql = @"INSERT INTO consumables_xrays (xray_id,item_id,consumables_x_qty)
                         VALUES(@xrayid, @itemid,@qty)";
            }

            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<bool> check(int itemid, int xrayid)
        {
            string sql = @"SELECT * FROM consumables_xrays WHERE xray_id = @xrayid AND item_id = @itemid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@xrayid", xrayid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (reader.Read())
            {
                crud.CloseConnection();
                return true;
            }

            crud.CloseConnection();
            return false;
        }

        public async void remove(int itemId, int xrayid)
        {
            string sql = @"DELETE FROM consumables_xrays WHERE xray_id = @xrayid AND item_id = @itemid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@xrayid", xrayid));
            listparams.Add(new MySqlParameter("@itemid", itemId));

            await crud.ExecuteAsync(sql, listparams);
        }
    }
}
