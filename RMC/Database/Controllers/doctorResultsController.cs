using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            string sql = @"INSERT INTO `doctor_results`( `cc`, `sfindings`, `assestment`, `procedureA`,`patient_id`,`user_id`)
                          VALUES (@cc,@sfindings,@ass,@procA,@id,@uid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            int patientid = int.Parse(data[3]);
            int userid = int.Parse(data[5]);
            listparams.Add(new MySqlParameter("@cc", data[0]));
            listparams.Add(new MySqlParameter("@sfindings", data[1]));
            listparams.Add(new MySqlParameter("@ass", data[2]));
            listparams.Add(new MySqlParameter("@procA", data[4]));
            listparams.Add(new MySqlParameter("@id", patientid));
            listparams.Add(new MySqlParameter("@uid", userid));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task update(params string[] data)
        {
            string sql = @"UPDATE doctor_results SET cc = @cc , sfindings = @sfindings, assestment = @ass 
                           , procedureA = @procA WHERE patient_id = @id AND doctor_results_id = @resid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            int patientid = int.Parse(data[3]);
            int resid = int.Parse(data[5]);
            listparams.Add(new MySqlParameter("@cc", data[0]));
            listparams.Add(new MySqlParameter("@sfindings", data[1]));
            listparams.Add(new MySqlParameter("@ass", data[2]));
            listparams.Add(new MySqlParameter("@procA", data[4]));
            listparams.Add(new MySqlParameter("@id", patientid));
            listparams.Add(new MySqlParameter("@resid", resid));


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
                doctoResultModel.doctor_id = int.Parse(reader["user_id"].ToString());
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
                doctoResultModel.patient_id = id;
                doctoResultModel.cc = reader["cc"].ToString();
                doctoResultModel.sfindings = reader["sfindings"].ToString();
                doctoResultModel.assestment = reader["assestment"].ToString();
                doctoResultModel.procedureA = reader["procedureA"].ToString();
                doctoResultModel.doctor_id = int.Parse(reader["user_id"].ToString());
               
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

        public async Task<DataSet> getPatientDetailsWithReq()
        {
            string sql = @"SELECT doctor_results.doctor_results_id AS 'ID',
                            patientdetails.patient_id,CONCAT(firstname,' ',middlename,' ',lastname) as 'patientname' FROM `doctor_results` 
                            INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id  
                                WHERE  DATE(doctor_results.date_results) = CURDATE() AND (EXISTS(
                                        SELECT 
                                                *
                                            FROM
                                                doc_res_req
                                            WHERE
                                                doc_res_req.doctor_results_id 
                                            = doctor_results.doctor_results_id) OR EXISTS(
                                        SELECT 
                                                *
                                            FROM
                                                doc_req_reql
                                            WHERE
                                                doc_req_reql.doctor_results_id   
                                            = doctor_results.doctor_results_id))";

            


            return await crud.GetDataSetAsync(sql, null);
            
        }
    }
}
