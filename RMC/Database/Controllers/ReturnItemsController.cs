using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class ReturnItemsController
    {
        dbcrud crud = new dbcrud();

        public async void save(int qtyReturn,string reason,int itemid,int supplierid)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@qty", qtyReturn));
            listparams.Add(new MySqlParameter("@reason", reason));
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@sid", supplierid));
            listparams.Add(new MySqlParameter("@uid", UserLog.getUserId()));

            string sql = @"INSERT INTO return_items (qty_return,reason,item_id,supplier_id,u_id) 
                            VALUES(@qty,@reason,@itemid,@sid,@uid)";

            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<DataSet> getReturnItems()
        {
            string sql = @"SELECT itemlist.item_id,itemlist.item_name,suppliers.supplier_name,return_items.qty_return AS 'qtyRturn',
                        return_items.reason,date_return FROM `return_items` 
                        INNER JOIN itemlist ON return_items.item_id = itemlist.item_id
                        INNER JOIN suppliers ON return_items.supplier_id = suppliers.supplier_id
                        INNER JOIN useraccounts ON return_items.u_id = useraccounts.u_id";

           return await crud.GetDataSetAsync(sql, null);
        }



    }
}
