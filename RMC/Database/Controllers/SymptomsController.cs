using MySql.Data.MySqlClient;
using RMC.Components;
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
    class SymptomsController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<SymptomsMod>> getSymptomsMod(int uid)
        {
            List<SymptomsMod> listSymptoms = new List<SymptomsMod>();
            string sql = @"SELECT symptoms_id,symptoms_name FROM symptoms 
                        WHERE u_id = @uid";
            List<MySqlParameter> listParams = new List<MySqlParameter>();

            listParams.Add(new MySqlParameter("@uid", uid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);

            while(await reader.ReadAsync())
            {
                SymptomsMod s = new SymptomsMod();
                s.id = int.Parse(reader["symptoms_id"].ToString());
                s.name = reader["symptoms_name"].ToString();
                listSymptoms.Add(s);

            }

            crud.CloseConnection();
            return listSymptoms;
        }

        public async Task<DataSet> getDataset(int uid)
        {
            string sql = @"SELECT symptoms_id,symptoms_name FROM symptoms 
                        WHERE u_id = @uid";
            List<MySqlParameter> listParams = new List<MySqlParameter>();

            listParams.Add(new MySqlParameter("@uid", uid));

            return await crud.GetDataSetAsync(sql, listParams);
        }

        public async Task<DataSet> getDataset(int uid,string name)
        {
            string sql = @"SELECT symptoms_id,symptoms_name FROM symptoms 
                            WHERE u_id = @uid AND symptoms_name LIKE @key";
            List<MySqlParameter> listParams = new List<MySqlParameter>();

            listParams.Add(new MySqlParameter("@uid", uid));
            string key = "%" + name + "%";
            listParams.Add(new MySqlParameter("@key", key));

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
            string sql = @"UPDATE symptoms SET symptoms_name = @name 
                            WHERE u_id = @uid AND symptoms_id = @id";

            List<MySqlParameter> listParams = new List<MySqlParameter>();

            listParams.Add(new MySqlParameter("@uid", uid));
            listParams.Add(new MySqlParameter("@name", name));
            listParams.Add(new MySqlParameter("@id", id));


            await crud.ExecuteAsync(sql, listParams);
        }


        public async Task<List<ComboBoxItem>> getComboDatas(int uid)
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
         
            string sql = @"SELECT symptoms_id,symptoms_name FROM symptoms 
                        WHERE u_id = @uid";
            List<MySqlParameter> listParams = new List<MySqlParameter>();

            listParams.Add(new MySqlParameter("@uid", uid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["symptoms_name"].ToString(),
                    int.Parse(reader["symptoms_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }


        public async Task<bool> isFound(int uid, string name)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM symptoms 
                                 WHERE u_id = @uid AND symptoms_name = @name";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@uid", uid));
            listparams.Add(new MySqlParameter("@name", name));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                isFound = true;
            }

            crud.CloseConnection();

            return isFound;
        }

        public int getRecentItemID()
        {
            string sql = String.Format(@"SELECT AUTO_INCREMENT As 'Last_id'
                                        FROM information_schema.tables 
                                        WHERE table_name='symptoms' 
                                        AND table_schema= DATABASE()");
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, null);
            int last_id = 0;
            if (reader.Read())
            {
                last_id = int.Parse(reader["Last_id"].ToString());
            }
            crud.CloseConnection();
            return last_id;
        }



    }
}
