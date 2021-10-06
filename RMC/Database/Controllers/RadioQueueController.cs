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
    class RadioQueueController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int customerid, int radioid)
        {
            string sql = @"INSERT INTO radio_queue (xray_id,customer_id) VALUES(@id,@cid)";


            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", radioid));
            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task Delete(int customerid)
        {
            string sql = @"DELETE FROM radio_queue WHERE customer_id = @cid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

          
            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);

        }


        public async Task updateStatus(int xrayid, int patientid)
        {
            string sql = @"UPDATE radio_queue SET is_done_x = 1 
                            WHERE xray_id = @id AND customer_id in 
                                (SELECT customer_id FROM customer_request_details WHERE patient_id = @pid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", xrayid));

            listparams.Add(new MySqlParameter("@pid", patientid));

            await crud.ExecuteAsync(sql, listparams);
        }
        
        
            public async Task updateStatus(int customerid)
        {
            string sql = @"UPDATE radio_queue SET is_done_x = 1 
                            WHERE customer_id = @cid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@cid", customerid));


            await crud.ExecuteAsync(sql, listparams);
        }


        public async Task<List<int>> listRadQueue()
        {
            List<int> liststrings = new List<int>();


            string sql = @"SELECT customer_request_details.patient_id,CONCAT(patientdetails.firstname, ' ', patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details
                           INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM radio_queue WHERE radio_queue.is_done_x = 0) 
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

        public async Task<List<xraymodel>> getReqLabByPatientID(int id)
        {
            List<xraymodel> listxraymodel = new List<xraymodel>();

            string sql = @"SELECT  radio_queue.xray_id,radio_queue.is_done_x,xraylist.xray_name,xraylist.xray_type FROM `radio_queue` 
                        INNER JOIN xraylist ON radio_queue.xray_id = xraylist.xray_id 
                        WHERE customer_id = @id";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                xraymodel l = new xraymodel();

                l.id = int.Parse(reader["xray_id"].ToString());
                l.name = reader["xray_name"].ToString();
                l.type = int.Parse(reader["xray_type"].ToString());
                l.is_done = int.Parse(reader["is_done_x"].ToString());
                listxraymodel.Add(l);
            }



            crud.CloseConnection();
            return listxraymodel;
                
        }

        public async Task<bool> isDone(int customerid)
        {
            bool isDone = true;


            string sql = @"SELECT * FROM `radio_queue` WHERE customer_id = @id";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@id", customerid));


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);



            if (!reader.HasRows)
            {
                isDone = false;

            }

            while (await reader.ReadAsync())
            {
                int is_done = int.Parse(reader["is_done_x"].ToString());

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
