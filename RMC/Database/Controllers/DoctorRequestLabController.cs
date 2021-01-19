using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    }
}
