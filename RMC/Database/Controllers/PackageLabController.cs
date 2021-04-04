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
    class PackageLabController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<PackagesNames>> getPackagesLab(int id)
        {
            List<PackagesNames> listPackagesNames = new List<PackagesNames>();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            string sql = @"SELECT laboratorylist.`laboratory_id`,labname,price_lab FROM laboratorylist 
                            WHERE laboratory_id in 
                          (SELECT laboratory_id FROM packages_lab WHERE packages_lab.`package_id` = @id )";
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                PackagesNames namesLab = new PackagesNames();
                namesLab.id = int.Parse(reader["laboratory_id"].ToString());
                namesLab.name = reader["labname"].ToString();
                namesLab.price = float.Parse(reader["price_lab"].ToString());
                listPackagesNames.Add(namesLab);
            }


            crud.CloseConnection();

            return listPackagesNames;

        }

        public async void save(int labid)
        {
            string sql = @"INSERT INTO packages_lab (laboratory_id,package_id) 
                           VALUES(@labid,(SELECT package_id FROM packages ORDER BY package_id DESC LIMIT 1))";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@labid", labid));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void save(int labid,int packageid)
        {
            if (await isFound(packageid, labid))
                return;

            string sql = @"INSERT INTO packages_lab (laboratory_id,package_id) 
                           VALUES(@labid,@packid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@labid", labid));
            listparams.Add(new MySqlParameter("@packid", packageid));

            await crud.ExecuteAsync(sql, listparams);
        
        }

        public async void remove(int packid,int labid)
        {
            string sql = @"DELETE FROM packages_lab WHERE laboratory_id = @labid AND package_id = @packid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@labid", labid));
            listparams.Add(new MySqlParameter("@packid", packid));


            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<bool> isFound(int packid, int labid)
        {
            bool isFound = false;

            string sql = @"SELECT * FROM packages_lab WHERE laboratory_id = @labid AND package_id = @packid";


            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@labid", labid));
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
