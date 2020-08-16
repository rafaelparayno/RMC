using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class LabTypeController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDataSet()
        {
            string sql = @"SELECT `labtype_id` AS 'id',labtype_name AS 'name' FROM labtype WHERE is_active = 1";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async void save(string name)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"INSERT INTO labtype (labtype_name,is_active) VALUES (@name,1)";

            listparams.Add(new MySqlParameter("@name", name));

            await crud.ExecuteAsync(sql,listparams);
        }

        public async void update(int id,string name)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"UPDATE labtype SET labtype_name = @name WHERE labtype_id = @id";

            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void remove(int id)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"UPDATE labtype SET is_active = @active WHERE labtype_id = @id";

            listparams.Add(new MySqlParameter("@active", 0));
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }




    }
}
