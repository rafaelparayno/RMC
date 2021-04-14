using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            string sql = @"SELECT personels_id,profession,personel_name,is_active FROM personels";


            return await crud.GetDataSetAsync(sql, null);
        }


        public async Task edit(params string [] data)
        {
            string sql = @"UPDATE personels SET profession = @prof ,personel_name = @name WHERE personels_id = @id";


            int id = int.Parse(data[2]);
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@prof", data[0]));
            mySqlParameters.Add(new MySqlParameter("@name", data[1]));
            mySqlParameters.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, mySqlParameters);
        }

        public async Task updateStatus(int id,string prof)
        {
            string sql = @"SELECT * FROM personels WHERE profession = @prof AND is_active = 1 ";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@prof", prof));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);
            int idToChange = 0;
            if(await reader.ReadAsync())
            {
                idToChange = int.Parse(reader["personels_id"].ToString());
            }

            crud.CloseConnection();

            List<MySqlParameter> mySqlParameters1 = new List<MySqlParameter>() { (new MySqlParameter("@toChange", idToChange)) };

            string sql2 = @"UPDATE personels SET is_active = 0 WHERE personels_id = @toChange";


            sql = @"UPDATE personels SET is_active = 1 WHERE personels_id = @id";

            mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", id)) };

            List<Task> tasks = new List<Task>();
            tasks.Add(crud.ExecuteAsync(sql2, mySqlParameters1));
            tasks.Add(crud.ExecuteAsync(sql, mySqlParameters));

            await Task.WhenAll(tasks);
        }


        public async Task<PersonelModel> getImgName(string prof)
        {
            PersonelModel personelModel = new PersonelModel();

            string sql = @"SELECT * FROM personels WHERE is_active = 1 AND profession = @prof";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@prof", prof));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (await reader.ReadAsync())
            {
                personelModel.id = int.Parse(reader["personels_id"].ToString());

                personelModel.name = reader["personel_name"].ToString();

                personelModel.profession = reader["profession"].ToString();

                personelModel.imgPath = reader["signature_path"].ToString();
                
            }

            crud.CloseConnection();

            return personelModel;
        }

        public async Task<string> imgPath(int id)
        {
            string path = "";
            string sql = @"SELECT * FROM personels WHERE personels_id = @id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", id)) };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if(await reader.ReadAsync())
            {

                path = reader["signature_path"].ToString();
            }

            crud.CloseConnection();

            return path;

        }

    }
}
