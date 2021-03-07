using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class DoctorQueueController
    {

        dbcrud crud = new dbcrud();


        public async void Save(int queu_no,string cc)
        {
            string sql = await isFound(queu_no) ? "UPDATE doctor_queue SET cc_doctor = @cc WHERE queue_no = @q" : 
                @"INSERT INTO doctor_queue (queue_no,cc_doctor) VALUES  (@q,@cc)";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@q", queu_no));
            list.Add(new MySqlParameter("@cc", cc));

            await crud.ExecuteAsync(sql, list);
        }

        public async Task<string> getCC(int queue_no)
        {
            string cc = "";
            string sql = @"SELECT * FROM `doctor_queue` WHERE queue_no = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", queue_no));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                cc = reader["cc_doctor"].ToString();
            }

            crud.CloseConnection();

            return cc;
        }

        public async void Remove(int req_id)
        {
            string sql = @"DELETE FROM doctor_queue WHERE queue_no in 
                            (SELECT queue_no FROM customer_request_details WHERE customer_id = @q)";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@q", req_id));

            await crud.ExecuteAsync(sql, list);
        }




        private async Task<bool> isFound(int q)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM `doctor_queue` WHERE queue_no = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", q));

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
