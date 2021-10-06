using MySql.Data.MySqlClient;
using RMC.Database.Models;
using RMC.Lab;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class CustomerDetailsController
    {
        dbcrud crud = new dbcrud();

        public int getRecentItemID()
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



        public async Task<List<customerDetailsMod>> getDetailsList()
        {
            
            string sql = @"SELECT * FROM customer_request_details 
                            LEFT JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE DATE(date_req) = CURDATE()
                            ORDER BY queue_no ASC";
            List<customerDetailsMod> detailsList = new List<customerDetailsMod>();
       
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                customerDetailsMod customerDetailsMod = new customerDetailsMod();

                customerDetailsMod.id = int.Parse(reader["customer_id"].ToString());
                customerDetailsMod.quueu_no = int.Parse(reader["queue_no"].ToString());
                customerDetailsMod.fname = reader["firstname"].ToString();
                customerDetailsMod.mname = reader["middlename"].ToString();
                customerDetailsMod.lname = reader["lastname"].ToString();
                customerDetailsMod.age = int.Parse(reader["age"].ToString());
                customerDetailsMod.gender = reader["gender"].ToString();
                customerDetailsMod.address = reader["address"].ToString();
                customerDetailsMod.cp_no = reader["contactnumber"].ToString();
                customerDetailsMod.address = reader["address"].ToString();
                customerDetailsMod.isPaid = int.Parse(reader["is_paid"].ToString());
                detailsList.Add(customerDetailsMod);
            }

            crud.CloseConnection();

            return detailsList;
        }

        public async Task<List<customerDetailsMod>> getDetailsList(string date)
        {

            string sql = @"SELECT * FROM customer_request_details 
                            LEFT JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE DATE(date_req) = Date(@date)
                            ORDER BY queue_no ASC";
            List<customerDetailsMod> detailsList = new List<customerDetailsMod>();
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@date", date));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                customerDetailsMod customerDetailsMod = new customerDetailsMod();

                customerDetailsMod.id = int.Parse(reader["customer_id"].ToString());
                customerDetailsMod.quueu_no = int.Parse(reader["queue_no"].ToString());
                customerDetailsMod.fname = reader["firstname"].ToString();
                customerDetailsMod.mname = reader["middlename"].ToString();
                customerDetailsMod.lname = reader["lastname"].ToString();
                customerDetailsMod.age = int.Parse(reader["age"].ToString());
                customerDetailsMod.gender = reader["gender"].ToString();
                customerDetailsMod.address = reader["address"].ToString();
                customerDetailsMod.cp_no = reader["contactnumber"].ToString();
                customerDetailsMod.address = reader["address"].ToString();
                customerDetailsMod.isPaid = int.Parse(reader["is_paid"].ToString());
                detailsList.Add(customerDetailsMod);
            }

            crud.CloseConnection();

            return detailsList;
        }

        public async Task<int> getLastQueue()
        {
            int lastq = 0;

            string sql = @"SELECT MAX(queue_no) AS 'last_q' FROM `customer_request_details`  WHERE DATE(date_req) = CURDATE()";
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                lastq = reader["last_q"].ToString() == "" || reader["last_q"].ToString() == null
                    ? 0 : int.Parse(reader["last_q"].ToString());
            }

            crud.CloseConnection();


            return lastq;
        }

        public async Task<int> getCustomerIdLast()
        {
            int lastq = 0;

            string sql = @"SELECT * FROM `customer_request_details` ORDER BY customer_request_details.customer_id DESC";
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            if (await reader.ReadAsync())
            {
                lastq = int.Parse(reader["customer_id"].ToString());
            }

            crud.CloseConnection();


            return lastq;
        }

        public async Task setPaid(int customerid,int status)
        {
            string sql = @"UPDATE customer_request_details SET is_paid = @status WHERE customer_id  = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();


            listparams.Add(new MySqlParameter("@id", customerid));
            listparams.Add(new MySqlParameter("@status", status));


            await crud.ExecuteAsync(sql, listparams);
        }

       

        public async Task save(params string [] data)
        {
            string sql = @"INSERT INTO customer_request_details (is_paid,queue_no,patient_id)
                          VALUES (0,@q,@id)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

       
            listparams.Add(new MySqlParameter("@q", int.Parse(data[0])));
            listparams.Add(new MySqlParameter("@id", int.Parse(data[1])));


            await crud.ExecuteAsync(sql, listparams);
        }



        public async Task<int> getPatientIDinQueue(int qno)
        {
            int currentReq = 0;
            string sql = @"SELECT * FROM customer_request_details  WHERE queue_no = @id  AND DATE(date_req) = CURDATE()";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", qno));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                currentReq = int.Parse(reader["patient_id"].ToString());
            }

            crud.CloseConnection();

            return currentReq;
        }

        public async Task<int> getCustomerIdinQueue(int qno)
        {
            int currentReq = 0;
            string sql = @"SELECT * FROM customer_request_details  WHERE queue_no = @id  AND DATE(date_req) = CURDATE()";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", qno));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                currentReq = int.Parse(reader["customer_id"].ToString());
            }

            crud.CloseConnection();

            return currentReq;
        }



        public async Task<int> getIsPaid(int qno)
        {
            int isPaid = 0;
            string sql = @"SELECT * FROM customer_request_details  WHERE queue_no = @id  AND DATE(date_req) = CURDATE()";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", qno));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                isPaid = int.Parse(reader["is_paid"].ToString());
            }

            crud.CloseConnection();

            return isPaid;
        }

        public async Task<DataSet> getLabQueue()
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name' ,
                            customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM lab_queue WHERE lab_queue.is_done_l = 0) 
                        AND DATE(customer_request_details.date_req) = CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }


        public async Task<DataSet> getLabQueue(string search)
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM lab_queue WHERE lab_queue.is_done_l = 0) 
                        AND DATE(customer_request_details.date_req) = CURDATE() AND CONCAT(patientdetails.firstname,' ',patientdetails.lastname) LIKE @key";

            string key = "%" + search + "%";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@key",key))};

            return await crud.GetDataSetAsync(sql, mySqlParameters);
        }

        public async Task<DataSet> getLabPending()
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name'
                    ,customer_request_details.queue_no,customer_request_details.date_req FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM lab_queue WHERE lab_queue.is_done_l = 0) 
                        AND DATE(customer_request_details.date_req) != CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getLabPending(string search)
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name'
                    ,customer_request_details.queue_no,customer_request_details.date_req FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM lab_queue WHERE lab_queue.is_done_l = 0) 
                        AND DATE(customer_request_details.date_req) != CURDATE() AND CONCAT(patientdetails.firstname,' ',patientdetails.lastname) LIKE @key";

            string key = "%" + search + "%";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@key", key)) };

            return await crud.GetDataSetAsync(sql, mySqlParameters);
        }

        public async Task<DataSet> getLabQueueDone()
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id, CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM lab_queue WHERE lab_queue.is_done_l = 1) 
                        AND DATE(customer_request_details.date_req) = CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }


        public async Task<DataSet> getLabQueueDone(string search)
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM lab_queue WHERE lab_queue.is_done_l = 1) 
                        AND DATE(customer_request_details.date_req) = CURDATE()  
                        AND CONCAT(patientdetails.firstname,' ',patientdetails.lastname) LIKE @key";

            string key = "%" + search + "%";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@key", key)) };

            return await crud.GetDataSetAsync(sql, mySqlParameters);
        }



        public async Task<DataSet> getRadioQueue()
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM radio_queue WHERE radio_queue.is_done_x = 0) 
                        AND DATE(customer_request_details.date_req) = CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getRadioPending()
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM radio_queue WHERE radio_queue.is_done_x = 0) 
                        AND DATE(customer_request_details.date_req) != CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getRadioQueue(string search)
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM radio_queue WHERE radio_queue.is_done_x = 0) 
                        AND DATE(customer_request_details.date_req) = CURDATE() 
                         AND CONCAT(patientdetails.firstname,' ',patientdetails.lastname) LIKE @key";

            string key = "%" + search + "%";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@key", key)) };

            return await crud.GetDataSetAsync(sql, mySqlParameters);
        }

        public async Task<DataSet> getRadioQueueDone()
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM radio_queue WHERE radio_queue.is_done_x = 1) 
                        AND DATE(customer_request_details.date_req) = CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }


        public async Task<DataSet> getRadioQueueDone(string search)
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM radio_queue WHERE radio_queue.is_done_x = 1) 
                        AND DATE(customer_request_details.date_req) = CURDATE() 
                            AND CONCAT(patientdetails.firstname,' ',patientdetails.lastname) LIKE @key";

            string key = "%" + search + "%";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@key", key)) };

            return await crud.GetDataSetAsync(sql, mySqlParameters);
        }


        public async Task<DataSet> getServiceQueue()
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',
                        customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM others_queue WHERE others_queue.is_done_o = 0) 
                        AND DATE(customer_request_details.date_req) = CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getServicePending()
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',
                        customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM others_queue WHERE others_queue.is_done_o = 0) 
                        AND DATE(customer_request_details.date_req) != CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getServiceQueue(string search)
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',
                        customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM others_queue WHERE lab_queue.is_done_o = 0) 
                        AND DATE(customer_request_details.date_req) = CURDATE() AND CONCAT(patientdetails.firstname,' ',patientdetails.lastname) LIKE @key";

            string key = "%" + search + "%";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@key", key)) };

            return await crud.GetDataSetAsync(sql, mySqlParameters);
        }

        public async Task<DataSet> getServiceQueueDone()
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM others_queue WHERE others_queue.is_done_o = 1) 
                        AND DATE(customer_request_details.date_req) = CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }


        public async Task<DataSet> getServiceQueueDone(string search)
        {
            string sql = @"SELECT customer_request_details.patient_id,customer_request_details.customer_id,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details 
                         INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM others_queue WHERE others_queue.is_done_o = 1) 
                        AND DATE(customer_request_details.date_req) = CURDATE() 
                            AND CONCAT(patientdetails.firstname,' ',patientdetails.lastname) LIKE @key";

            string key = "%" + search + "%";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@key", key)) };

            return await crud.GetDataSetAsync(sql, mySqlParameters);
        }


        public async Task Delete(int customerid)
        {
            string sql = @"DELETE FROM customer_request_details WHERE customer_id = @cid";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@cid",customerid))
            };

            await crud.ExecuteAsync(sql, mySqlParameters);
        }
    }
}
