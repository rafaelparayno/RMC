using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    }
}
