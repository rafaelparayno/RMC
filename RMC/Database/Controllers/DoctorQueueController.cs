using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        public async Task<DataSet> getDataSetDocQ(int uid)
        {
            string sql = @"SELECT customer_request_details.queue_no,patientdetails.patient_id,
                            CONCAT(patientdetails.firstname,' ',patientdetails.lastname) AS 'patientname',age,gender,med_cert_type,cc_doctor FROM `doctor_queue`
                                            INNER JOIN customer_request_details ON doctor_queue.customer_id = customer_request_details.customer_id
                                            INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id 
                                            WHERE doctor_queue.is_done = 0 AND u_id = @id AND DATE(customer_request_details.date_req) = CURDATE()";
            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@id", uid));
            return await crud.GetDataSetAsync(sql, listParams);
        }

        public async Task updateDoctorQueue(int uid,int id)
        {
            string sql = @"UPDATE doctor_queue SET u_id = @uid WHERE doctor_queue.customer_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();



            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@uid", uid));


            await crud.ExecuteAsync(sql, listparams);
        }

        public async void Save(int queu_no,string cc,int medCertType,string compname)
        {
            string sql = await isFound(queu_no) ? @"UPDATE doctor_queue SET cc_doctor = @cc ,
                                                   med_cert_type = @md , company_name = @cmpname 
                                                  WHERE customer_id = @q" :
                @"INSERT INTO doctor_queue (customer_id,cc_doctor,med_cert_type,company_name) 
                    VALUES  (@q,@cc,@md,@cmpname)";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@q", queu_no));
            list.Add(new MySqlParameter("@cc", cc));
            list.Add(new MySqlParameter("@md", medCertType));
            list.Add(new MySqlParameter("@cmpname", compname));

            await crud.ExecuteAsync(sql, list);
        }

        public async Task setDone(int queu_no)
        {
            string sql = "UPDATE doctor_queue SET is_done = 1 WHERE customer_id IN (SELECT customer_id FROM customer_request_details WHERE queue_no = @q) AND DATE(date_q) = CURDATE()";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@q", queu_no));

            await crud.ExecuteAsync(sql, list);
        }

        public async Task<List<int>> getQueueDoc()
        {
            List<int> listQueue = new List<int>();

            string sql = @"SELECT * FROM doctor_queue
                        INNER JOIN customer_request_details ON doctor_queue.customer_id = customer_request_details.customer_id
                         WHERE is_done = 0 AND DATE(date_q) = CURDATE() ORDER BY queue_no DESC";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                listQueue.Add(int.Parse(reader["queue_no"].ToString()));
            }

            crud.CloseConnection();
            return listQueue;
        }

        public async Task<int> getCurrentDoctorQ(int uid)
        {
            int CURRENTQ = 0;

            string sql = @"SELECT MIN(queue_no) as 'current' FROM doctor_queue 
                        INNER JOIN customer_request_details ON doctor_queue.customer_id = customer_request_details.customer_id
                        WHERE is_done = 0 AND DATE(date_q) = CURDATE() AND u_id = @uid";


            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@uid", uid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                CURRENTQ = reader["current"].ToString() == "" ? 0 : int.Parse(reader["current"].ToString());
            }

            crud.CloseConnection();


            return CURRENTQ;
        }

        public async Task<int> getNextDoctorQ(int uid)
        {
            int NEXTQ = 0;
            bool hasNext = false;
            string sql = @"SELECT * FROM doctor_queue 
                        INNER JOIN customer_request_details ON doctor_queue.customer_id = customer_request_details.customer_id
                        WHERE is_done = 0 AND DATE(date_q) = CURDATE() AND u_id = @uid";


            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@uid", uid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (reader.Read())
            {
                hasNext = true;
                if (hasNext)
                {
                    if ( reader.Read())
                    {
                        NEXTQ = int.Parse(reader["queue_no"].ToString());
                    }
                }
            }

          
           

            crud.CloseConnection();


            return NEXTQ;
        }

        public async Task<int> getCurrentQ()
        {
            int currentQ = 0;


            DbDataReader reader = await crud.RetrieveRecordsAsync("SELECT MIN(queue_no) AS 'current' FROM doctor_queue INNER JOIN customer_request_details ON doctor_queue.customer_id = customer_request_details.customer_id WHERE is_done = 0  AND DATE(date_q) = CURDATE()", null);

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
            string sql = @"SELECT * FROM `doctor_queue`  WHERE customer_id in(SELECT customer_id FROM customer_request_details 
                          WHERE customer_request_details.queue_no = @id) AND DATE(date_q) = CURDATE()";
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

        public async Task<int> getMedCertType(int queue_no)
        {
            int medType = 0;
            string sql = @"SELECT * FROM `doctor_queue`  WHERE customer_id in(SELECT customer_id FROM customer_request_details 
                          WHERE customer_request_details.queue_no = @id) AND DATE(date_q) = CURDATE()";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", queue_no));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                medType = int.Parse(reader["med_cert_type"].ToString());
            }

            crud.CloseConnection();

            return medType;
        }


        public async Task<int> getDoctorID(int cusid)
        {
            int doctorid = 0;
            string sql = @"SELECT * FROM `doctor_queue`  WHERE customer_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", cusid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                doctorid = int.Parse(reader["u_id"].ToString());
            }

            crud.CloseConnection();

            return doctorid;
        }

        public async Task<string> getCompanyName(int queue_no)
        {
            string companyName = "";
            string sql = @"SELECT * FROM `doctor_queue`  WHERE customer_id in(SELECT customer_id FROM customer_request_details 
                          WHERE customer_request_details.queue_no = @id) AND DATE(date_q) = CURDATE()";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", queue_no));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                companyName = reader["company_name"].ToString();
            }

            crud.CloseConnection();

            return companyName;
        }

        public async Task<string> getCompanyNameByCustomeId(int customerid)
        {
            string companyName = "";
            string sql = @"SELECT * FROM `doctor_queue`  WHERE customer_id = @id ";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", customerid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                companyName = reader["company_name"].ToString();
            }

            crud.CloseConnection();

            return companyName;
        }

        public async Task<int> getMedCertByCustomeId(int customerid)
        {
            int medType = 0;
            string sql = @"SELECT * FROM `doctor_queue`  WHERE customer_id = @id ";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", customerid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                medType = int.Parse(reader["med_cert_type"].ToString());
            }

            crud.CloseConnection();

            return medType;
        }

        public async Task Remove(int req_id)
        {
            
            string sql2 = "SELECT * FROM doctor_queue WHERE customer_id = @q AND is_done = 1";

            string sql = @"DELETE FROM doctor_queue WHERE customer_id = @q";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@q", req_id));

            DbDataReader dbDataReader = await crud.RetrieveRecordsAsync(sql2, list);

            if (dbDataReader.HasRows)
                return;

             await crud.ExecuteAsync(sql, list);
        }

        public async Task<bool> isDone(int queue_no)
        {
            bool isDone = false;
            string sql = @"SELECT * FROM `doctor_queue` 
                            WHERE customer_id in (SELECT customer_id FROM customer_request_details 
                                                    WHERE customer_request_details.queue_no = @id) 
                            AND is_done = 1 AND DATE(date_q) = CURDATE()";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", queue_no));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                isDone = true;
            }

            crud.CloseConnection();

            return isDone;
        }


        private async Task<bool> isFound(int q)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM `doctor_queue` WHERE customer_id = @id";
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
