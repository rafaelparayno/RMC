using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
     public class ClinicStocksController
    {
        dbcrud crud = new dbcrud();
        private bool check(int id)
        {
            bool stocks = false;

            string sql = @"SELECT * FROM labitemstocks WHERE item_id = @id";
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
            string sql = "SELECT * FROM labitemstocks WHERE item_id = @id";
            MySqlDataReader reader = null;
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@id", id));
            crud.RetrieveRecords(sql, ref reader, list);

            if (await reader.ReadAsync())
            {
                stocks = int.Parse(reader["clinic_stocks"].ToString());
            }

            crud.CloseConnection();

            return stocks;
        }


        public async void Save(int id, int qty)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (check(id))
            {
                //update
                sql = @"UPDATE pharmastocks SET clinic_stocks = @qty WHERE item_id = @id";
                list.Add(new MySqlParameter("@qty", qty));
                list.Add(new MySqlParameter("@id", id));
            }
            else
            {
                sql = @"INSERT INTO labitemstocks (item_id,clinic_stocks) VALUES (@id,@qty)";

                list.Add(new MySqlParameter("@id", id));
                list.Add(new MySqlParameter("@qty", qty));
            }
            await crud.ExecuteAsync(sql, list);
        }
    }
}
