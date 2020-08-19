using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PricesServiceController
    {
        dbcrud crud = new dbcrud();

        public async Task<float> getPrice(string name)
        {
            float price = 0;

            string sql = @"SELECT * FROM prices_service WHERE name = @name";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                price = float.Parse(reader["price_serv"].ToString());
            }

            crud.CloseConnection();

            return price;

        }

        public async void save(float price,string name)
        {
            string sql;
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@price", price));
            if (await isFound(name))
            {
                sql = @"UPDATE prices_service SET price_serv = @price WHERE name = @name";
            }
            else
            {
                sql = @"INSERT INTO prices_service (name,price_serv) VALUES (@name,@price)";
            }
             
        

            await crud.ExecuteAsync(sql, listparams);
        }

        private async Task<bool> isFound(string name)
        {
            bool isFound = false;

            string sql = @"SELECT * FROM prices_service WHERE name = @name";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                isFound = true;
            }

            crud.CloseConnection();

            return isFound;
        }
    }
}
