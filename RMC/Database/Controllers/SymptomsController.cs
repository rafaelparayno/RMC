using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class SymptomsController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDataset(int uid)
        {
            string sql = @"SELECT * FROM symptoms WHERE u_id = @uid";
            List<MySqlParameter> listParams = new List<MySqlParameter>();

            listParams.Add(new MySqlParameter("@uid", uid));

            return await crud.GetDataSetAsync(sql, listParams);
        }


        public async Task save(int uid,string name)
        {
            string sql = @"INSERT INTO symptoms (u_id,symptoms_name) 
                           VALUES (@uid,@name)";

            List<MySqlParameter> listParams = new List<MySqlParameter>();

            listParams.Add(new MySqlParameter("@uid", uid));
            listParams.Add(new MySqlParameter("@name", name));


            await crud.ExecuteAsync(sql, listParams);
        }

        public async Task update(int uid,string name,int id)
        {
            string sql = @"UPDATE symptoms SET symptoms_name = @name WHERE u_id = @uid AND symptoms_id = @id";

            List<MySqlParameter> listParams = new List<MySqlParameter>();

            listParams.Add(new MySqlParameter("@uid", uid));
            listParams.Add(new MySqlParameter("@name", name));
            listParams.Add(new MySqlParameter("@id", name));


            await crud.ExecuteAsync(sql, listParams);
        }


        
    }
}
