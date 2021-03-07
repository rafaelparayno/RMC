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
    class CustomerDetailsController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<customerDetailsMod>> getDetailsList()
        {
            
            string sql = @"SELECT * FROM customer_request_details 
                            LEFT JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
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
                detailsList.Add(customerDetailsMod);
            }

            crud.CloseConnection();

            return detailsList;
        }

        public async Task<int> getLastQueue()
        {
            int lastq = 0;

            string sql = @"SELECT MAX(queue_no) AS 'last_q' FROM `customer_request_details`";
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                lastq = reader["last_q"].ToString() == "" || reader["last_q"].ToString() == null
                    ? 0 : int.Parse(reader["last_q"].ToString());
            }

            crud.CloseConnection();


            return lastq;
        }

        public async void save(params string [] data)
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
            string sql = @"SELECT * FROM customer_request_details  WHERE queue_no = @id";
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



        /*  public async Task<int> getCurrentCustomer()
          {
              int currentReq = 0;
              string sql = @"SELECT * FROM customer_request_details  WHERE customer_id = (SELECT MIN(customer_id) 
                          FROM customer_request_details WHERE req_done = 0)";


              DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

              if(await reader.ReadAsync())
              {
                  currentReq = int.Parse(reader["customer_id"].ToString());
              }

              crud.CloseConnection();

              return currentReq;
          }

          public async Task<int> nextCurrentCustomer()
          {
              int nextReq = 0;
              string sql = @"SELECT * FROM customer_request_details  WHERE customer_id = ((SELECT MIN(customer_id) 
                          FROM customer_request_details WHERE req_done = 0) + 1)";


              DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

              if (await reader.ReadAsync())
              {
                  nextReq = int.Parse(reader["customer_id"].ToString());
              }

              crud.CloseConnection();

              return nextReq;
          }



          public async void nextQueue()
          {
              string sql = @"UPDATE customer_request_details SET req_done = 1 
                          WHERE customer_id = (SELECT MIN(customer_id) 
                          FROM customer_request_details WHERE req_done = 0)";

              await crud.ExecuteAsync(sql, null);
          }*/


    }
}
