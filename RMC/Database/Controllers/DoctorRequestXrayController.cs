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
    class DoctorRequestXrayController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int xray)
        {
            string sql = @"INSERT INTO doc_res_req (xray_id,doctor_results_id) 
                          VALUES (@xrayid,(SELECT doctor_results_id FROM doctor_results ORDER BY doctor_results_id DESC LIMIT 1))";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@xrayid", xray));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<List<xraymodel>> getXrayData(int docresult_id)
        {
            string sql = @"SELECT * FROM doc_res_req 
                    INNER JOIN doctor_results ON doc_res_req.doctor_results_id = doctor_results.doctor_results_id
                    INNER JOIN xraylist ON doc_res_req.xray_id = xraylist.xray_id
                    WHERE doctor_results.doctor_results_id	= @dsid";

            List<MySqlParameter> listparam = new List<MySqlParameter>();
            listparam.Add(new MySqlParameter("@dsid", docresult_id));
            List<xraymodel> listXrayModel = new List<xraymodel>();


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparam);

            while (await reader.ReadAsync())
            {
                xraymodel xraymodelX = new xraymodel();

                xraymodelX.id = int.Parse(reader["xray_id"].ToString());

                xraymodelX.name = reader["xray_name"].ToString();

                listXrayModel.Add(xraymodelX);
            }

            crud.CloseConnection();

            return listXrayModel;
        }


        public async Task<List<xraymodel>> getXrayData(string date)
        {
            string sql = @"SELECT * FROM doc_res_req 
                    INNER JOIN doctor_results ON doc_res_req.doctor_results_id = doctor_results.doctor_results_id
                    INNER JOIN xraylist ON doc_res_req.xray_id = xraylist.xray_id
                    WHERE doctor_results.date_results	= @date";

            List<MySqlParameter> listparam = new List<MySqlParameter>();
            listparam.Add(new MySqlParameter("@date", DateTime.Parse(date)));
            List<xraymodel> listXrayModel = new List<xraymodel>();


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparam);

            while (await reader.ReadAsync())
            {
                xraymodel xraymodelX = new xraymodel();

                xraymodelX.id = int.Parse(reader["xray_id"].ToString());

                xraymodelX.name = reader["xray_name"].ToString();
                listXrayModel.Add(xraymodelX);
            }

            crud.CloseConnection();

            return listXrayModel;
        }

    }
}
