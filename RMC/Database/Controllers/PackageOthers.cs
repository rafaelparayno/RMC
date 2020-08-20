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
    class PackageOthers
    {

        dbcrud crud = new dbcrud();


        public async Task<List<PackagesNames>> getPackagesNames(int id)
        {
            List<PackagesNames> listPackagesNames = new List<PackagesNames>();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            string sql = @"SELECT service.`service_id`,service_name,price FROM service
                            WHERE service_id in 
                          (SELECT service_id FROM packages_others WHERE packages_others.`package_id` = @id )";
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                PackagesNames namesLab = new PackagesNames();
                namesLab.id = int.Parse(reader["service_id"].ToString());
                namesLab.name = reader["service_name"].ToString();
                namesLab.price = float.Parse(reader["price"].ToString());
                listPackagesNames.Add(namesLab);
            }


            crud.CloseConnection();

            return listPackagesNames;

        }

        public async void save(int id)
        {
            string sql = @"INSERT INTO packages_others (service_id,package_id) 
                           VALUES(@id,(SELECT package_id FROM packages ORDER BY package_id DESC LIMIT 1))";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void save(int id, int packageid)
        {
            string sql = @"INSERT INTO packages_others (service_id,package_id) 
                           VALUES(@id,@packid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@packid", packageid));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async void remove(int packid, int id)
        {
            string sql = @"DELETE FROM packages_others WHERE service_id = @id AND package_id = @packid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@packid", packid));


            await crud.ExecuteAsync(sql, listparams);
        }
    }
}
