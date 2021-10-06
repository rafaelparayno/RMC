using MySql.Data.MySqlClient;
using RMC.Database.Models;
using RMC.Lab;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class LabQueueController
    {
        dbcrud crud = new dbcrud();


        public async Task save(int labid,int customerid)
        {
            string sql = @"INSERT INTO lab_queue (laboratory_id,customer_id) VALUES(@labid,@cid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@labid", labid));

            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }


        public async Task Delete(int customerid)
        {
            string sql = @"DELETE FROM lab_queue WHERE customer_id = @cid";


            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }


        public async Task updateStatus(int labid, int cid)
        {
            string sql = @"UPDATE lab_queue SET is_done_l = 1 
                            WHERE laboratory_id = @labid AND customer_id = @cid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@labid", labid));

            listparams.Add(new MySqlParameter("@cid", cid));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task updateStatus(int customerid)
        {
            string sql = @"UPDATE lab_queue SET is_done_l = 1 
                            WHERE customer_id = @cid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<List<int>> listLabQueue()
        {
            List<int> liststrings = new List<int>();


            string sql = @"SELECT customer_request_details.patient_id,CONCAT(patientdetails.firstname, ' ', patientdetails.lastname) as 'Patient_Name',customer_request_details.queue_no FROM customer_request_details
                           INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE customer_id in (SELECT customer_id FROM lab_queue WHERE lab_queue.is_done_l = 0) 
                        AND DATE(customer_request_details.date_req) = CURDATE() ORDER BY queue_no DESC";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                liststrings.Add(string.IsNullOrEmpty(reader["queue_no"].ToString()) ? 
                                0 :  int.Parse(reader["queue_no"].ToString()));
                 
            }



            crud.CloseConnection();

            return liststrings;

        }


        public async Task<List<labModel>> getReqLabByPatientID(int id)
        {
            List<labModel> listLabModel = new List<labModel>();

            string sql = @"SELECT lab_queue.customer_id,lab_queue.laboratory_id,laboratorylist.labname,labtype.labtype_name,lab_queue.is_done_l FROM `lab_queue` 
                        INNER JOIN laboratorylist ON lab_queue.laboratory_id = laboratorylist.laboratory_id 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id
                        WHERE customer_id = @id";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql,listparams);

            while(await reader.ReadAsync())
            {
                labModel l = new labModel();

                l.labID = int.Parse(reader["laboratory_id"].ToString());
                l.name = reader["labname"].ToString();
                l.labtypename = reader["labtype_name"].ToString();
                l.is_done = int.Parse(reader["is_done_l"].ToString());
                listLabModel.Add(l);
            }

            
            crud.CloseConnection();
            return listLabModel;
        }

        public async Task<bool> isDone(int customerid)
        {

            bool isDone = true;

            string sql = @"SELECT * FROM `lab_queue` WHERE customer_id = @id";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@id", customerid));


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (!reader.HasRows)
            {
                isDone = false;
               
            }

            while (await reader.ReadAsync())
            {
              
                  

                int is_done = int.Parse(reader["is_done_l"].ToString());

                if(is_done == 0)
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
