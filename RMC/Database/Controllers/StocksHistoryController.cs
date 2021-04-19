using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    public class StocksHistoryController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getStockHis()
        {
            string sql = @"SELECT action,quantity,item_name,date_stock_his AS 'Date',
                            Concat(firstname,' ',lastname) AS 'Name' FROM `stockshistory` 
                            LEFT JOIN itemlist on itemlist.item_id = stockshistory.item_id
                            LEFT JOIN useraccounts ON useraccounts.u_id = stockshistory.u_id 
                            ORDER BY hist_id ASC";
     
           return await crud.GetDataSetAsync(sql, null);
        }
        public async Task<DataSet> getStockHis(string date)
        {
            string sql = @"SELECT action,quantity,item_name,date_stock_his AS 'Date',
                            Concat(firstname,' ',lastname) AS 'Name' FROM `stockshistory` 
                            LEFT JOIN itemlist on itemlist.item_id = stockshistory.item_id
                            LEFT JOIN useraccounts ON useraccounts.u_id = stockshistory.u_id 
                            WHERE date_stock_his = @date ORDER BY hist_id ASC";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async void Save(string action,int qty,int uid,int item_id)
        {
            string sql;
            if (action == "")
                return;

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@action", action));
            list.Add(new MySqlParameter("@qty", qty));
            list.Add(new MySqlParameter("@uid", uid));
            list.Add(new MySqlParameter("@itemid", item_id));

            sql = @"INSERT INTO stockshistory (action,quantity,u_id,item_id) VALUES (@action,@qty,@uid,@itemid)";
         
            await crud.ExecuteAsync(sql, list);
        }
    }
}
