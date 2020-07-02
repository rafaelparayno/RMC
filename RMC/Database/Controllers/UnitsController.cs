using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class UnitsController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDsActive()
        {
            string sql = String.Format(@"SELECT unit_id AS 'id', unit_name FROM unitofmeasurement WHERE is_active = 1");
            DataSet dsUnits = new DataSet();
            return dsUnits = await crud.GetDataSetAsync(sql,null);
        }

        public async void save(string unitName)
        {
            string sql = @"INSERT INTO unitofmeasurement (unit_name,is_active) 
                                VALUES (@unitname,@isactive)";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@unitname", unitName));
            list.Add(new MySqlParameter("@isactive", 1));

            await crud.ExecuteAsync(sql, list);
        }

        public async void edit(string unitName,int id)
        {
            string sql = String.Format(@"UPDATE unitofmeasurement SET unit_name = @unitname WHERE unit_id = @id");

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@unitname", unitName));
            list.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, list);
        }

        public async void Deactive(int id)
        {
            string sql = String.Format(@"UPDATE unitofmeasurement SET is_active = @isactive WHERE unit_id = @id");

            List<MySqlParameter> list = new List<MySqlParameter>();
            
            list.Add(new MySqlParameter("@id", id));
            list.Add(new MySqlParameter("@isactive", 0));
            await crud.ExecuteAsync(sql, list);
        }
    }
}
