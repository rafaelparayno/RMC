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
    class DoctorQueueController
    {

        dbcrud crud = new dbcrud();


        public async Task<DataSet> getDataSetDocQ()
        {
            string sql = @"SELECT doctor_queue.queue_no,patientdetails.patient_id,
                            CONCAT(patientdetails.firstname,' ',patientdetails.lastname) AS 'patientname',age,gender FROM `doctor_queue`
                                            INNER JOIN customer_request_details ON doctor_queue.queue_no = customer_request_details.queue_no
                                            INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id 
                                            WHERE doctor_queue.is_done = 0";
            return await crud.GetDataSetAsync(sql, null);
        }

        public async void Save(int queu_no,string cc)
        {
            string sql = await isFound(queu_no) ? "UPDATE doctor_queue SET cc_doctor = @cc WHERE queue_no = @q" : 
                @"INSERT INTO doctor_queue (queue_no,cc_doctor) VALUES  (@q,@cc)";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@q", queu_no));
            list.Add(new MySqlParameter("@cc", cc));

            await crud.ExecuteAsync(sql, list);
        }

        public async void setDone(int queu_no)
        {
            string sql = "UPDATE doctor_queue SET is_done = 1 WHERE queue_no = @q";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@q", queu_no));

            await crud.ExecuteAsync(sql, list);
        }

        public async Task<List<int>> getQueueDoc()
        {
            List<int> listQueue = new List<int>();

            string sql = @"SELECT * FROM doctor_queue WHERE is_done = 0 ORDER BY queue_no DESC";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                listQueue.Add(int.Parse(reader["queue_no"].ToString()));
            }

            crud.CloseConnection();
            return listQueue;
        }

        public async Task<int> getCurrentQ()
        {
            int currentQ = 0;


            DbDataReader reader = await crud.RetrieveRecordsAsync("SELECT MIN(queue_no) AS 'current' FROM doctor_queue WHERE is_done = 0", null);

            if (await reader.ReadAsync())
            {
                currentQ = reader["current"].ToString() == "" ? 0 : int.Parse(reader["current"].ToString());
            }

            crud.CloseConnection();
            return currentQ;
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
