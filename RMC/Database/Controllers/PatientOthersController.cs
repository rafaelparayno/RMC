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

        public async Task<List<PatientsOtherModel>> getPatientsOthers(int id)
        {
            List<PatientsOtherModel> listsp = new List<PatientsOtherModel>();

            string sql = @"SELECT * FROM patient_others WHERE patient_id = @id";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while(await reader.ReadAsync())
            {
                PatientsOtherModel p = new PatientsOtherModel();
                p.id = int.Parse(reader["patient_others_id"].ToString());
                p.name = reader["others_name"].ToString();
                p.path = reader["others_path"].ToString();
                p.date_upload = DateTime.Parse(reader["date_others"].ToString());

                listsp.Add(p);
            }

            crud.CloseConnection();
            return listsp;
        }


        public async Task<string> getPathName(int id)
        {
            string path = "";


            string sql = @"SELECT CONCAT(others_path,others_name) AS 'path' 
                            FROM patient_others WHERE patient_others_id = @id";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                path = reader["path"].ToString();
            }

            crud.CloseConnection();

            return path;
        }
    }
}
