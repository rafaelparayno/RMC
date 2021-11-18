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
    class PatientDetailsController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<patientDetails>> getPatientDetails()
        {
            List<patientDetails> listpatient = new List<patientDetails>();
            string sql = @"SELECT * FROM `patientdetails` WHERE is_active_patient = 1";
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while (await reader.ReadAsync())
            {
                patientDetails p = new patientDetails();
                p.id = int.Parse(reader["patient_id"].ToString());
                p.Firstname = reader["firstname"].ToString();
                p.middlename = reader["middlename"].ToString();
                p.lastname = reader["lastname"].ToString();
                p.birthdate = reader["birthdate"].ToString();
                p.age = int.Parse(reader["age"].ToString());
                p.gender = reader["gender"].ToString();
                p.address = reader["address"].ToString();
                p.contact = reader["contactnumber"].ToString();
                p.imgPath = reader["img_path"].ToString();

                listpatient.Add(p);

            }


            crud.CloseConnection();

            return listpatient;
        }

        public async Task<List<patientDetails>> getSearchPatient(string searchkey,int cb)
        {
            List<patientDetails> listpatient = new List<patientDetails>();
            string sql = "";
            string searches = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (cb)
            {
                case 0:
                    sql = @"SELECT * FROM patientdetails WHERE patient_id  = @searchKey And is_active_patient = 1";
                    listparams.Add(new MySqlParameter("@searchKey", int.Parse(searchkey)));
                    break;

                case 1:
                    sql = @"SELECT * FROM `patientdetails` where CONCAT(firstname,' ',lastname) LIKE @key And is_active_patient = 1";
                    searches = "%" + searchkey + "%";
                    listparams.Add(new MySqlParameter("@key", searches));
                    break;

            }

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                patientDetails p = new patientDetails();
                p.id = int.Parse(reader["patient_id"].ToString());
                p.Firstname = reader["firstname"].ToString();
                p.middlename = reader["middlename"].ToString();
                p.lastname = reader["lastname"].ToString();
                p.birthdate = reader["birthdate"].ToString();
                p.age = int.Parse(reader["age"].ToString());
                p.gender = reader["gender"].ToString();
                p.address = reader["address"].ToString();
                p.contact = reader["contactnumber"].ToString();
                p.imgPath = reader["img_path"].ToString();

                listpatient.Add(p);
            }

            crud.CloseConnection();

            return listpatient;
        }


        public async Task<patientDetails> getPatientId(int id)
        {
            patientDetails patientDetails = new patientDetails();
            string sql = @"SELECT * FROM patientdetails WHERE patient_id = @id And is_active_patient = 1";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if(await reader.ReadAsync())
            {
                patientDetails.id = int.Parse(reader["patient_id"].ToString());
                patientDetails.Firstname = reader["firstname"].ToString();
                patientDetails.middlename = reader["middlename"].ToString();
                patientDetails.lastname = reader["lastname"].ToString();

                patientDetails.age = int.Parse(reader["age"].ToString());
                patientDetails.gender = reader["gender"].ToString();
                patientDetails.address = reader["address"].ToString();
                patientDetails.contact = reader["contactnumber"].ToString();
                patientDetails.birthdate = reader["birthdate"].ToString();
                patientDetails.civil_status = reader["civil_status"].ToString();
                patientDetails.imgPath = reader["img_path"].ToString();
            }
            crud.CloseConnection();

            return patientDetails;
        }


        public async Task<patientDetails> getPatientQueueNo(int queue_no)
        {
            patientDetails patientDetails = new patientDetails();
            string sql = @"SELECT * FROM patientdetails WHERE patient_id in 
                    (SELECT patient_id FROM customer_request_details WHERE queue_no = @queue_no AND DATE(date_req) = CURDATE() ) And is_active_patient = 1";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@queue_no", queue_no));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                patientDetails.id = int.Parse(reader["patient_id"].ToString());
                patientDetails.Firstname = reader["firstname"].ToString();
                patientDetails.middlename = reader["middlename"].ToString();
                patientDetails.lastname = reader["lastname"].ToString();

                patientDetails.age = int.Parse(reader["age"].ToString());
                patientDetails.gender = reader["gender"].ToString();
                patientDetails.address = reader["address"].ToString();
                patientDetails.contact = reader["contactnumber"].ToString();
                patientDetails.birthdate = reader["birthdate"].ToString();
                patientDetails.civil_status = reader["civil_status"].ToString();
                patientDetails.imgPath = reader["img_path"].ToString();
            }
            crud.CloseConnection();

            return patientDetails;
        }


        public async Task<patientDetails> getPatientFName(string fname)
        {
            patientDetails patientDetails = new patientDetails();
            string sql = @"SELECT * FROM patientdetails WHERE firstname = @name And is_active_patient = 1";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", fname));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                patientDetails.id = int.Parse(reader["patient_id"].ToString());
                patientDetails.Firstname = reader["firstname"].ToString();
                patientDetails.middlename = reader["middlename"].ToString();
                patientDetails.lastname = reader["lastname"].ToString();

                patientDetails.age = int.Parse(reader["age"].ToString());
                patientDetails.gender = reader["gender"].ToString();
                patientDetails.address = reader["address"].ToString();
                patientDetails.contact = reader["contactnumber"].ToString();
                patientDetails.birthdate = reader["birthdate"].ToString();
                patientDetails.civil_status = reader["civil_status"].ToString();
                patientDetails.imgPath = reader["img_path"].ToString();
            }
            crud.CloseConnection();

            return patientDetails;
        }

        public async Task<patientDetails> getPatientLName(string lname)
        {
            patientDetails patientDetails = new patientDetails();
            string sql = @"SELECT * FROM patientdetails WHERE lastname = @name And is_active_patient = 1";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", lname));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                patientDetails.id = int.Parse(reader["patient_id"].ToString());
                patientDetails.Firstname = reader["firstname"].ToString();
                patientDetails.middlename = reader["middlename"].ToString();
                patientDetails.lastname = reader["lastname"].ToString();

                patientDetails.age = int.Parse(reader["age"].ToString());
                patientDetails.gender = reader["gender"].ToString();
                patientDetails.address = reader["address"].ToString();
                patientDetails.contact = reader["contactnumber"].ToString();
                patientDetails.birthdate = reader["birthdate"].ToString();
                patientDetails.civil_status = reader["civil_status"].ToString();
                patientDetails.imgPath = reader["img_path"].ToString();
            }
            crud.CloseConnection();

            return patientDetails;
        }

    

        public async Task save(params string[] data)
        {
            string sql = @"INSERT INTO patientdetails 
                        (firstname,middlename,lastname,birthdate,age,gender,contactnumber,civil_status,address) 
                        VALUES(@fn,@mn,@ln,@bdate,@age,@gender,@cn,@status,@add)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@fn", data[0]));
            listparams.Add(new MySqlParameter("@mn", data[1]));
            listparams.Add(new MySqlParameter("@ln", data[2]));
            listparams.Add(new MySqlParameter("@bdate", DateTime.Parse(data[3])));
            listparams.Add(new MySqlParameter("@age", int.Parse(data[4])));
            listparams.Add(new MySqlParameter("@gender", data[5]));
            listparams.Add(new MySqlParameter("@cn", data[6]));
            listparams.Add(new MySqlParameter("@status", data[7]));
            listparams.Add(new MySqlParameter("@add", data[8]));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void saveIMG(string path,int id)
        {
            string sql = @"UPDATE patientdetails SET img_path = @path WHERE patient_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@path",path));
           
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task updateAcvite(int status,int id)
        {
            string sql = @"UPDATE patientdetails SET is_active_patient = @status WHERE patient_id = @id";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@status", status));

            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task update(params string[] data)
        {
            string sql = @"UPDATE patientdetails 
                         SET firstname = @fn,middlename = @mn,lastname = @ln,birthdate = @bdate , 
                         age = @age ,gender = @gender,contactnumber = @cn ,civil_status = @status,
                         address = @add WHERE patient_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@fn", data[0]));
            listparams.Add(new MySqlParameter("@mn", data[1]));
            listparams.Add(new MySqlParameter("@ln", data[2]));
            listparams.Add(new MySqlParameter("@bdate", DateTime.Parse(data[3])));
            listparams.Add(new MySqlParameter("@age", int.Parse(data[4])));
            listparams.Add(new MySqlParameter("@gender", data[5]));
            listparams.Add(new MySqlParameter("@cn", data[6]));
            listparams.Add(new MySqlParameter("@status", data[7]));
            listparams.Add(new MySqlParameter("@add", data[8]));
            listparams.Add(new MySqlParameter("@id", int.Parse(data[9])));

            await crud.ExecuteAsync(sql, listparams);
        }

        public int getRecentPID()
        {
            string sql = String.Format(@"SELECT AUTO_INCREMENT As 'Last_id'
                                        FROM information_schema.tables 
                                        WHERE table_name='patientdetails' 
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
    }
}
