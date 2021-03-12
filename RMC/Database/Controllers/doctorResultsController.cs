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
    class doctorResultsController
    {
        dbcrud crud = new dbcrud();

        public async Task save(params string[] data)
        {
            string sql = @"INSERT INTO doctor_results (cc,sfindings,assestment,procedureA,patient_id) 
                          VALUES (@cc,@sfindings,@ass,@procA,@id)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            int patientid = int.Parse(data[3]);
            listparams.Add(new MySqlParameter("@cc", data[0]));
            listparams.Add(new MySqlParameter("@sfindings", data[1]));
            listparams.Add(new MySqlParameter("@ass", data[2]));
            listparams.Add(new MySqlParameter("@procA", data[4]));
            listparams.Add(new MySqlParameter("@id", patientid));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<List<DoctorResult>> getDoctorResults(int id)
        {
            List<DoctorResult> listDoctorResultmodel = new List<DoctorResult>();
            string sql = @"SELECT * FROM `doctor_results` WHERE patient_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                DoctorResult doctoResultModel = new DoctorResult();
                doctoResultModel.id = int.Parse(reader["doctor_results_id"].ToString());
                doctoResultModel.cc = reader["cc"].ToString();
                doctoResultModel.sfindings = reader["sfindings"].ToString();
                doctoResultModel.assestment =  reader["assestment"].ToString();
                doctoResultModel.procedureA = reader["procedureA"].ToString();
                doctoResultModel.date_results = DateTime.Parse(reader["date_results"].ToString());

                listDoctorResultmodel.Add(doctoResultModel);
            }

            crud.CloseConnection();

            return listDoctorResultmodel;
        }


        public async Task<DoctorResult> getDoctorResultsSearchId(int id)
        {

            DoctorResult doctoResultModel = new DoctorResult();
            string sql = @"SELECT * FROM `doctor_results` WHERE doctor_results_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (reader.Read())
            {
                doctoResultModel.id = int.Parse(reader["doctor_results_id"].ToString());
                doctoResultModel.cc = reader["cc"].ToString();
                doctoResultModel.sfindings = reader["sfindings"].ToString();
                doctoResultModel.assestment = reader["assestment"].ToString();
                doctoResultModel.procedureA = reader["procedureA"].ToString();
                doctoResultModel.date_results = DateTime.Parse(reader["date_results"].ToString());
            }
           

            crud.CloseConnection();

            return doctoResultModel;
        }

        public async Task<List<DoctorResult>> getDoctorResults(int id, string date)
        {
            List<DoctorResult> listDoctorResultmodel = new List<DoctorResult>();
            string sql = @"SELECT * FROM `doctor_results` WHERE patient_id = @id AND date_results = @date";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                DoctorResult doctoResultModel = new DoctorResult();
                doctoResultModel.id = int.Parse(reader["doctor_results_id"].ToString());
                doctoResultModel.cc = reader["cc"].ToString();
                doctoResultModel.sfindings = reader["sfindings"].ToString();
                doctoResultModel.assestment = reader["assestment"].ToString();
                doctoResultModel.procedureA = reader["procedureA"].ToString();
                doctoResultModel.date_results = DateTime.Parse(reader["date_results"].ToString());

                listDoctorResultmodel.Add(doctoResultModel);
            }

            crud.CloseConnection();

            return listDoctorResultmodel;
        }
    }
}