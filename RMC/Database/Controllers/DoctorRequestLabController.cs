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
    class DoctorRequestLabController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int labid)
        {
            string sql = @"INSERT INTO doc_req_reql (laboratory_id,doctor_results_id) 
                          VALUES (@lid,(SELECT doctor_results_id FROM doctor_results ORDER BY doctor_results_id DESC LIMIT 1))";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@lid", labid));

            await crud.ExecuteAsync(sql, listparams);

        }
        public async Task<List<labModel>> getLabModelDoctorResult(int docres)
        {
            List<labModel> listLabModel = new List<labModel>();

            string sql = @"SELECT * FROM `doc_req_reql` 
                            INNER JOIN laboratorylist ON doc_req_reql.laboratory_id = laboratorylist.laboratory_id
                            WHERE doctor_results_id = @docreq";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@docreq", docres));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while (await reader.ReadAsync())
            {
                labModel labModel = new labModel();

                labModel.id = int.Parse(reader["docresreql_id"].ToString());

                labModel.name = reader["labname"].ToString();
                listLabModel.Add(labModel);
            }


            crud.CloseConnection();


            return listLabModel;
        }


        public async Task<List<labModel>> getLabModelDoctorResult(string date)
        {
            List<labModel> listLabModel = new List<labModel>();

            string sql = @"SELECT * FROM `doc_req_reql` 
                            INNER JOIN laboratorylist ON doc_req_reql.laboratory_id = laboratorylist.laboratory_id
                            INNER JOIN doctor_result ON doc_req_reql.doctor_result_id = doctor_result.doctor_result_id
                            WHERE doctor_result.date_results = @date";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while (await reader.ReadAsync())
            {
                labModel labModel = new labModel();

                labModel.id = int.Parse(reader["docresreql_id"].ToString());

                labModel.name = reader["labname"].ToString();
                listLabModel.Add(labModel);
            }


            crud.CloseConnection();

            return listLabModel;
        }

    }
}
