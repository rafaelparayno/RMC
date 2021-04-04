using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class CustomerRequestsController
    {
        dbcrud crud = new dbcrud();
        
        public async Task<List<int>> getListTypeReq(int customerId)
        {
            List<int> types = new List<int>();
            string sql = @"SELECT * FROM customer_requests WHERE customer_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", customerId));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                int type = int.Parse(reader["request_type"].ToString());

                types.Add(type);
            }


            crud.CloseConnection();
            return types;
        }

        public async Task newReq(int type)
        {
                string sql = @"INSERT INTO customer_requests (request_type,customer_id)
                          VALUES (@type,(SELECT customer_id FROM customer_request_details ORDER BY customer_id DESC LIMIT 1))";

                List<MySqlParameter> listparams = new List<MySqlParameter>();
                listparams.Add(new MySqlParameter("@type", type));

                await crud.ExecuteAsync(sql, listparams);
        }

        public async void updateReq(int customer_id, int type)
        {
            if (await isFound(customer_id, type))
                return;

            string sql = @"INSERT INTO customer_requests (request_type,customer_id)
                          VALUES (@type,@cid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@type", type));
            listparams.Add(new MySqlParameter("@cid", customer_id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void remove(int type,int customer_id)
        {
            string sql = @"DELETE FROM customer_requests WHERE customer_id = @id AND request_type = @type";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", customer_id));
            listparams.Add(new MySqlParameter("@type", type));


            await crud.ExecuteAsync(sql, listparams);
        }

        private async Task<bool> isFound(int customerid,int typeid)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM customer_requests WHERE customer_id = @id AND request_type = @type";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", customerid));
            listparams.Add(new MySqlParameter("@type", typeid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if(await reader.ReadAsync())
            {
                isFound = true;
            }

            crud.CloseConnection();

            return isFound;
        }

        public int getRecentID()
        {
            string sql = String.Format(@"SELECT AUTO_INCREMENT As 'Last_id'
                                        FROM information_schema.tables 
                                        WHERE table_name='customer_request_details' 
                                        AND table_schema= DATABASE()");
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, null);
            int last_id = 0;
            if (reader.Read())
            {
                last_id = int.Parse(reader["Last_id"].ToString());
            }
            crud.CloseConnection();
            return last_id;
        }
    }
}
