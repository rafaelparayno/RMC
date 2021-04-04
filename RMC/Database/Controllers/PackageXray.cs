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
    class PackageXray
    {
        dbcrud crud = new dbcrud();


        public async Task<List<PackagesNames>> getPackagesNames(int id)
        {
            List<PackagesNames> listPackagesNames = new List<PackagesNames>();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            string sql = @"SELECT xraylist.`xray_id`,xray_name,xray_price FROM xraylist 
                            WHERE xray_id in 
                          (SELECT xray_id FROM packages_xray WHERE packages_xray.`package_id` = @id )";
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                PackagesNames namesLab = new PackagesNames();
                namesLab.id = int.Parse(reader["xray_id"].ToString());
                namesLab.name = reader["xray_name"].ToString();
                namesLab.price = float.Parse(reader["xray_price"].ToString());
                listPackagesNames.Add(namesLab);
            }


            crud.CloseConnection();

            return listPackagesNames;

        }

        public async void save(int id)
        {
            string sql = @"INSERT INTO packages_xray (xray_id,package_id) 
                           VALUES(@id,(SELECT package_id FROM packages ORDER BY package_id DESC LIMIT 1))";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void save(int id, int packageid)
        {
            if (await isFound(packageid, id))
                return;
            string sql = @"INSERT INTO packages_xray (xray_id,package_id) 
                           VALUES(@id,@packid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@packid", packageid));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async void remove(int packid, int id)
        {
            string sql = @"DELETE FROM packages_xray WHERE xray_id = @id AND package_id = @packid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@packid", packid));


            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<bool> isFound(int packid, int id)
        {
            bool isFound = false;

            string sql = @"SELECT * FROM packages_xray WHERE xray_id = @id AND package_id = @packid";


            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@packid", packid));



            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (reader.HasRows)
            {
                isFound = true;
            }
            crud.CloseConnection();

            return isFound;
        }
    }
}
