using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class MedCertController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int id)
        {

            string sql = @"INSERT INTO medcert_request (customer_id) VALUES (@id)";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", id)) };


            await crud.ExecuteAsync(sql, mySqlParameters);
        }



        public async Task setDone(int id)
        {
            string sql = @"UPDATE medcert_request SET is_done_cert = 1 WHERE medcert_request_id = @id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", id)) };

            await crud.ExecuteAsync(sql, mySqlParameters);
        }
    }
}
