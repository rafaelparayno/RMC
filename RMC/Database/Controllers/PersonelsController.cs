using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PersonelsController
    {
        dbcrud crud = new dbcrud();

        public async Task save(params string [] data)
        {
            string sql = @"INSERT INTO `personels`( `profession`, `personel_name`, `signature_path`) 
                        VALUES (@prof,@name,@path)";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@prof", data[0]));
            mySqlParameters.Add(new MySqlParameter("@name", data[1]));
            mySqlParameters.Add(new MySqlParameter("@path", data[2]));

            await crud.ExecuteAsync(sql, mySqlParameters);
        }

        public async Task<DataSet> getDataset()
        {
            string sql = @"SELECT profession,personel_name,is_active FROM personels";


            return await crud.GetDataSetAsync(sql, null);
        }


    }
}
