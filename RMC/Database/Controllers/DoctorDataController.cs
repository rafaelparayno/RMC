
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
    class DoctorDataController
    {
        dbcrud crud = new dbcrud();



        public async void save(params string [] data)
        {
            int uid = int.Parse(data[3]);

            string sql = await isFound(uid) ?
                @"UPDATE doctor_data SET pr_no = @pr, license_no = @li, signature_path = @path 
                                                WHERE u_id = @id" : 
                @"INSERT INTO doctor_data (pr_no,license_no,signature_path,u_id) 
                            VALUES(@pr,@li,@path,@id)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@pr", data[0]));
            listparams.Add(new MySqlParameter("@li", data[1]));
            listparams.Add(new MySqlParameter("@path", data[2]));
            listparams.Add(new MySqlParameter("@id",int.Parse(data[3])));

            await crud.ExecuteAsync(sql,listparams);
        }

        public async Task<DoctorDataModel> getDoctorData(int uid)
        {
            DoctorDataModel data = new DoctorDataModel();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", uid));

            DbDataReader reader = await crud.RetrieveRecordsAsync("SELECT * FROM doctor_data WHERE u_id = @id",listparams);


            if(await reader.ReadAsync())
            {
                data.id = int.Parse(reader["doctor_data_id"].ToString());
                data.pr = reader["pr_no"].ToString();
                data.license = reader["license_no"].ToString();
                data.imgPath = reader["signature_path"].ToString();
            }

            crud.CloseConnection();

            return data;

        }

        public async Task<bool> isFound(int uid)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM doctor_data WHERE u_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", uid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                isFound = true;
            }

            crud.CloseConnection();

            return isFound;
        }


    }
}
