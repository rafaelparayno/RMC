using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class OthersQueueController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int serviceid, int customerid)
        {
            string sql = @"INSERT INTO others_queue (service_id,customer_id) VALUES(@serviceid,@cid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@serviceid", serviceid));

            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }


        public async Task updateStatus(int service_id, int patientid)
        {
            string sql = @"UPDATE others_queue SET is_done_o = 1 
                            WHERE service_id = @serviceid AND customer_id in 
                                (SELECT customer_id FROM customer_request_details WHERE patient_id = @pid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@serviceid", service_id));

            listparams.Add(new MySqlParameter("@pid", patientid));

            await crud.ExecuteAsync(sql, listparams);
        }



        public async Task<List<int>> listServiceQueue()
        {
            List<int> liststrings = new List<int>();


            string sql = @"SELECT customer_request_details.patient_id,
                            CONCAT(patientdetails.firstname, ' ', patientdetails.lastname) as 'Patient_Name',
                            customer_request_details.queue_no FROM customer_request_details
                           INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM others_queue WHERE others_queue.is_done_o = 0) 
                        AND DATE(customer_request_details.date_req) = CURDATE() ORDER BY queue_no DESC";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while (await reader.ReadAsync())
            {
                liststrings.Add(string.IsNullOrEmpty(reader["queue_no"].ToString()) ?
                                0 : int.Parse(reader["queue_no"].ToString()));

            }



            crud.CloseConnection();

            return liststrings;

        }

    }
}
