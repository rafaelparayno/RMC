using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PatientOthersController
    {
        dbcrud crud = new dbcrud();

        public async Task save(string path, int id,string name)
        {
            string sql = @"INSERT INTO patient_others (others_name,others_path,patient_id) 
                            VALUE (@name,@path,@id)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@path", path));


            await crud.ExecuteAsync(sql, listparams);
        }
    }
}
