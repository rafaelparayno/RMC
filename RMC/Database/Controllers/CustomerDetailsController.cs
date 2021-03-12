﻿using MySql.Data.MySqlClient;
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

        public async Task setPaid(int customerid)
        {
            string sql = @"UPDATE customer_request_details SET is_paid = 1 WHERE customer_id  = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();


            listparams.Add(new MySqlParameter("@id", customerid));


            await crud.ExecuteAsync(sql, listparams);
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



    }
}
