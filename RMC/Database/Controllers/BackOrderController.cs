using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class BackOrderController
    {
        dbcrud crud = new dbcrud();

        public async Task<List<string>> getBoActive()
        {
            List<string> poActive = new List<string>();
            string sql = @"SELECT back_order.`po_id` FROM back_order WHERE po_id in 
                            (SELECT purchase_order.po_id 
                            FROM purchase_order WHERE po_id In 
                                (SELECT purchase_order_items.po_id 
                                FROM purchase_order_items 
                                WHERE quantity_order > 0))";


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while (await reader.ReadAsync())
            {
                poActive.Add("PO# " + reader["po_id"].ToString());
            }
            crud.CloseConnection();
            return poActive;
        }


        public async Task<List<string>> getBoActive(string date)
        {
            List<string> poActive = new List<string>();
            string sql = @"SELECT back_order.`po_id` FROM back_order WHERE po_id in 
                            (SELECT purchase_order.po_id 
                            FROM purchase_order WHERE po_id In 
                                (SELECT purchase_order_items.po_id 
                                FROM purchase_order_items 
                                WHERE quantity_order > 0) AND Date(date_order) = @date)";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@date", date)) };



            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while (await reader.ReadAsync())
            {
                poActive.Add("PO# " + reader["po_id"].ToString());
            }
            crud.CloseConnection();
            return poActive;
        }


        public async void save(int poid)
        {
            string sql = @"INSERT INTO back_order (po_id) values (@poid)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@poid", poid));

            await crud.ExecuteAsync(sql, listparams);

        }
    }
}
