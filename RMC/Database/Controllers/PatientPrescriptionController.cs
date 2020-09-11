using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PatientPrescriptionController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int item_id,string instruction)
        {
            string sql = @"INSERT INTO patient_prescription (item_id,instruction,doctor_results_id) 
                            VALUES (@itemid,@instruction, 
                            (SELECT doctor_results_id FROM doctor_results ORDER BY doctor_results_id DESC LIMIT 1)) ";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", item_id));
            listparams.Add(new MySqlParameter("@instruction", instruction));

            await crud.ExecuteAsync(sql, listparams);
        }
    }
}
