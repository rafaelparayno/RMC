using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class ServiceController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDataSet()
        {
            string sql = @"SELECT * FROM `service`";

            return  await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getDataSetSearch(string searchName)
        {
            string sql = @"SELECT * FROM `service` WHERE service_name LIKE @key";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string searches = "%" + searchName + "%";
            listparams.Add(new MySqlParameter("@key", searches));

            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async void save(string name,string desc,float price)
        {
            string sql = @"INSERT INTO service (service_name,service_desc,price) VALUES (@name,@desc,@price)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@desc", desc));
            listparams.Add(new MySqlParameter("@price", price));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void update(int id,string name, string desc, float price)
        {
            string sql = @"UPDATE service SET service_name = @name, service_desc = @desc,
                           price = @price WHERE service_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@desc", desc));
            listparams.Add(new MySqlParameter("@price", price));
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void remove(int id)
        {
            string sql = @"DELETE FROM service WHERE service_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);

        }
    }
}
