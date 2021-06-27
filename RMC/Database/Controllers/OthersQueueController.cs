using MySql.Data.MySqlClient;
using RMC.Database.Models;
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

        public async Task Delete( int customerid)
        {
            string sql = @"DELETE FROM others_queue WHERE customer_id = @cid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }


        public async Task updateStatus(int service_id, int patientid,int status)
        {
            string sql = @"UPDATE others_queue SET is_done_o = @status
                            WHERE service_id = @serviceid AND customer_id in 
                                (SELECT customer_id FROM customer_request_details WHERE patient_id = @pid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@serviceid", service_id));

            listparams.Add(new MySqlParameter("@pid", patientid));
            listparams.Add(new MySqlParameter("@status", status));

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


        public async Task<List<ServiceModel>> getReqServiceByPatientID(int id)
        {
            List<ServiceModel> listServiceModel = new List<ServiceModel>();

            string sql = @"SELECT others_queue.customer_id,others_queue.service_id,service.service_name,others_queue.is_done_o FROM `others_queue` 
                        INNER JOIN service ON others_queue.service_id = service.service_id 
                        WHERE customer_id IN (SELECT customer_id  FROM customer_request_details WHERE customer_request_details.patient_id = @id
                                                    AND  DATE(date_req) = CURDATE()) ";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                ServiceModel s = new ServiceModel();

                s.id = int.Parse(reader["service_id"].ToString());
                s.serviceName = reader["service_name"].ToString();
              
                s.isDone = int.Parse(reader["is_done_o"].ToString());
                listServiceModel.Add(s);
            }

            crud.CloseConnection();
            return listServiceModel;
        }


        public async Task<bool> isDone(int customerid)
        {

            bool isDone = true;

            string sql = @"SELECT * FROM `others_queue` WHERE customer_id = @id";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@id", customerid));


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (!reader.HasRows)
            {
                isDone = false;

            }

            while (await reader.ReadAsync())
            {



                int is_done = int.Parse(reader["is_done_o"].ToString());

                if (is_done == 0)
                {
                    isDone = false;
                    break;
                }
            }

            crud.CloseConnection();
            return isDone;

        }

    }
}
