using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class XrayControllers
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDataSet()
        {
            string sql = @"SELECT * FROM `xraylist`";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getSearchDataset(string searchkey)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            
            string sql = @"SELECT * FROM `xraylist` WHERE xray_name LIKE @key";
            string key = "%" + searchkey + "%";
            listparams.Add(new MySqlParameter("@key", key));

           return  await crud.GetDataSetAsync(sql, listparams);
        }

        public async void save(string name,string desc,int type,float price)
        {
            string sql = @"INSERT INTO xraylist (xray_name,xray_type,description,xray_price)
                           VALUES (@name,@type,@desc,@price)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@desc", desc));
            listparams.Add(new MySqlParameter("@type", type));
            listparams.Add(new MySqlParameter("@price", price));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async void update(int id,string name,string desc,int type,float price)
        {

            string sql = @"UPDATE xraylist SET xray_name = @name, xray_type = @type,
                          description = @desc , xray_price = @price WHERE xray_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@desc", desc));
            listparams.Add(new MySqlParameter("@type", type));
            listparams.Add(new MySqlParameter("@price", price));
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void remove(int id)
        {
            string sql = @"DELETE FROM xraylist WHERE xray_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);

        }
    }
}
