using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PatientLabController
    {
        dbcrud crud = new dbcrud();

        public async void save(int pid,int lid,string filename,string path)
        {
            string sql = @"INSERT INTO patient_lab (patient_id,laboratory_id,filename,path) 
                          VALUES (@id,@lid,@fname,@path)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", pid));
            listparams.Add(new MySqlParameter("@lid", lid));
            listparams.Add(new MySqlParameter("@fname", filename));
            listparams.Add(new MySqlParameter("@path", path));


            await crud.ExecuteAsync(sql, listparams);

        }
    }
}
