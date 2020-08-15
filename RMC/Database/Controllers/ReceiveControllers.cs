using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
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
    }
}
