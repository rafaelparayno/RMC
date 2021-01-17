
using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class rtsController
    {
        dbcrud crud = new dbcrud();


        public async void save(int qtyReturn, string reason, string sku, int invoiceid)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@qty", qtyReturn));
            listparams.Add(new MySqlParameter("@reason", reason));
     
            listparams.Add(new MySqlParameter("@invoiceid", invoiceid));
            listparams.Add(new MySqlParameter("@sku", sku));
            listparams.Add(new MySqlParameter("@uid", UserLog.getUserId()));

            string sql = @"INSERT INTO return_items_shop (qty_return,reason,item_id,invoice_id,u_id) 
                            VALUES(@qty,@reason,(SELECT item_id FROM itemlist WHERE SKU = @sku),@invoiceid,@uid)";

            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<DataSet> getReturnItems()
        {
            string sql = @"SELECT itemlist.item_id,itemlist.item_name,return_items_shop.qty_return AS 'qtyRturn',
                        return_items_shop.reason,date_return,CONCAT(useraccounts.firstname,' ', useraccounts.lastname) AS 'Issued' FROM `return_items_shop` 
                        INNER JOIN itemlist ON return_items_shop.item_id = itemlist.item_id
                        INNER JOIN useraccounts ON return_items_shop.u_id = useraccounts.u_id";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getReturnItems(string date)
        {
            string sql = @"SELECT itemlist.item_id,itemlist.item_name,return_items_shop.qty_return AS 'qtyRturn',
                        return_items_shop.reason,date_return,CONCAT(useraccounts.firstname,' ', useraccounts.lastname) AS 'Issued' FROM `return_items_shop` 
                        INNER JOIN itemlist ON return_items_shop.item_id = itemlist.item_id
                        INNER JOIN useraccounts ON return_items_shop.u_id = useraccounts.u_id 
                        WHERE return_items_shop.`date_return` = @date";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));


            return await crud.GetDataSetAsync(sql, listparams);
        }
    }
}
