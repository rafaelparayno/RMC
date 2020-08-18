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
            
            string sql = @"SELECT * FROM customer_request_details WHERE req_done = 0 ORDER BY customer_id ASC";
            List<customerDetailsMod> detailsList = new List<customerDetailsMod>();
        /*    List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@done", 0));*/
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                customerDetailsMod customerDetailsMod = new customerDetailsMod();

                customerDetailsMod.id = int.Parse(reader["customer_id"].ToString());
                customerDetailsMod.name = reader["customer_name"].ToString();
                customerDetailsMod.age = int.Parse(reader["age"].ToString());
                customerDetailsMod.gender = reader["gender"].ToString();
                customerDetailsMod.cp_no = reader["cp_no"].ToString();
                customerDetailsMod.cs = reader["CS"].ToString();
                customerDetailsMod.address = reader["customer_address"].ToString();
                detailsList.Add(customerDetailsMod);
            }

            crud.CloseConnection();

            return detailsList;
        }


        public async void save(params string [] data)
        {
            string sql = @"INSERT INTO customer_request_details (customer_name,age,gender,CS,cp_no,customer_address,req_done)
                          VALUES (@name,@age,@gender,@cs,@cpno,@addrs,0)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@name", data[0]));
            listparams.Add(new MySqlParameter("@age", int.Parse(data[1])));
            listparams.Add(new MySqlParameter("@gender", data[2]));
            listparams.Add(new MySqlParameter("@cs", data[3]));
            listparams.Add(new MySqlParameter("@cpno", data[4]));
            listparams.Add(new MySqlParameter("@addrs", data[5]));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void update(params string[] data)
        {
            string sql = @"UPDATE customer_request_details SET customer_name = @name,
                           age = @age,gender = @gender, CS = @cs,cp_no = @cpno, 
                            customer_address = @addrs WHERE customer_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@name", data[0]));
            listparams.Add(new MySqlParameter("@age", int.Parse(data[1])));
            listparams.Add(new MySqlParameter("@gender", data[2]));
            listparams.Add(new MySqlParameter("@cs", data[3]));
            listparams.Add(new MySqlParameter("@cpno", data[4]));
            listparams.Add(new MySqlParameter("@addrs", data[5]));
            listparams.Add(new MySqlParameter("@id", int.Parse(data[6])));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<int> getCurrentCustomer()
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
        }


    }
}
