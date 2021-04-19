using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    public class PharmaStocksController
    {
        dbcrud crud = new dbcrud();

        private bool check(int id)
        {
            bool stocks = false;

            string sql = @"SELECT * FROM pharmastocks WHERE item_id = @id";
            MySqlDataReader reader = null;
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@id", id));

            crud.RetrieveRecords(sql, ref reader, list);

            if (reader.Read())
            {
                stocks = true;
            }

            crud.CloseConnection();

            return stocks;
        }


        public async Task<int> getStocks(int id)
        {
            int stocks = 0;
            string sql = "SELECT * FROM pharmastocks WHERE item_id = @id";
            MySqlDataReader reader = null;
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@id", id));
            crud.RetrieveRecords(sql, ref reader, list);

            if (await reader.ReadAsync())
            {
                stocks = int.Parse(reader["pharma_stocks"].ToString());
            }

            crud.CloseConnection();

            return stocks;
        }

        public async Task addStocks(int id,int qty)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (check(id))
            {
                //update
                sql = @"UPDATE pharmastocks SET pharma_stocks = pharma_stocks + @qty WHERE item_id = @id";
                list.Add(new MySqlParameter("@qty", qty));
                list.Add(new MySqlParameter("@id", id));
            }
            else
            {
                sql = @"INSERT INTO pharmastocks (item_id,pharma_stocks) VALUES (@id,@qty)";

                list.Add(new MySqlParameter("@id", id));
                list.Add(new MySqlParameter("@qty", qty));
            }
            await crud.ExecuteAsync(sql, list);
        }


        public async Task Save(int id,int qty)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (check(id))
            {
                //update
                sql = @"UPDATE pharmastocks SET pharma_stocks = @qty WHERE item_id = @id";
                list.Add(new MySqlParameter("@qty", qty));
                list.Add(new MySqlParameter("@id", id));
            }
            else
            {
                sql = @"INSERT INTO pharmastocks (item_id,pharma_stocks) VALUES (@id,@qty)";
       
                list.Add(new MySqlParameter("@id", id));
                list.Add(new MySqlParameter("@qty", qty));
            }
            await crud.ExecuteAsync(sql, list);
        }

        public async Task SaveSKU(string sku, int qty)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();
            
                //update
            sql = @"UPDATE pharmastocks SET pharma_stocks = `pharma_stocks` - @qty 
                    WHERE pharmastocks.item_id 
                    IN(SELECT item_id FROM itemlist WHERE SKU = @sku )";
            list.Add(new MySqlParameter("@qty", qty));
            list.Add(new MySqlParameter("@sku", sku));
          
            await crud.ExecuteAsync(sql, list);
        }
    }
}
