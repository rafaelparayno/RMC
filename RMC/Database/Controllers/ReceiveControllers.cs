using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class ReceiveControllers
    {
        dbcrud crud = new dbcrud();

        public async void save(int itemid,int qty, int poid)
        {
            string sql = @"INSERT INTO receive_orders (po_item_id,qty_ro,u_id) VALUES 
                           ((SELECT po_item_id FROM purchase_order_items WHERE item_id = @itemid AND po_id = @poid),
                            @qty,@uid)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@poid", poid));
            listparams.Add(new MySqlParameter("@qty", qty));
            listparams.Add(new MySqlParameter("@uid", UserLog.getUserId()));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<float> getSumLead(int item_id,int days)
        {
            float leadTime = 0;
            string sql = @"SELECT (SUM(date_ro - date_order)/ COUNT(ro_id)) AS 'AvgLead' FROM `receive_orders` 
                         INNER JOIN purchase_order_items ON receive_orders.po_item_id = purchase_order_items.po_item_id 
                         INNER JOIN purchase_order ON purchase_order_items.po_id = purchase_order.po_id 
                         WHERE receive_orders.po_item_id in 
                                (SELECT po_item_id FROM purchase_order_items 
                                WHERE purchase_order_items.item_id = @item_id)
                         AND date_ro BETWEEN DATE_SUB(NOW() , INTERVAL @days DAY) and NOW()";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@item_id", item_id));
            listparams.Add(new MySqlParameter("@days", days));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);
            
            if(await reader.ReadAsync())
            {
                leadTime = reader["AvgLead"].ToString() == "" ? 0 : float.Parse(reader["AvgLead"].ToString());
            }
            crud.CloseConnection(); 

            return leadTime;
        }

        public async Task<DataSet> getData()
        {
            string sql = @"SELECT itemlist.item_name As 'Item Name',receive_orders.qty_ro AS 'quantity Receive',receive_orders.date_ro AS 'date Receive', 
                        CONCAT(useraccounts.firstname,' ',useraccounts.lastname) AS 'Receive By' 
                        FROM `receive_orders` INNER JOIN purchase_order_items ON receive_orders.po_item_id = purchase_order_items.po_item_id 
                        INNER JOIN itemlist ON purchase_order_items.item_id = itemlist.item_id 
                        INNER JOIN useraccounts ON receive_orders.u_id = useraccounts.u_id";


            return await crud.GetDataSetAsync(sql, null);
        }


        public async Task<DataSet> getData(string date)
        {
            string sql = @"SELECT itemlist.item_name As 'Item Name',receive_orders.qty_ro AS 'quantity Receive',receive_orders.date_ro AS 'date Receive', 
                        CONCAT(useraccounts.firstname,' ',useraccounts.lastname) AS 'Receive By' 
                        FROM `receive_orders` INNER JOIN purchase_order_items ON receive_orders.po_item_id = purchase_order_items.po_item_id 
                        INNER JOIN itemlist ON purchase_order_items.item_id = itemlist.item_id 
                        INNER JOIN useraccounts ON receive_orders.u_id = useraccounts.u_id 
                        WHERE date_ro = @date";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            return await crud.GetDataSetAsync(sql, listparams);
        }

    }
}
