using MySql.Data.MySqlClient;
using RMC.Components;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PackagesController
    {
        dbcrud crud = new dbcrud();

        public async Task<List<packages>> getListPackages()
        {
            List<packages> packages = new List<packages>();

            string sql = @"SELECT * FROM packages";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                packages pack = new packages();
                pack.id = int.Parse(reader["package_id"].ToString());
                pack.name = reader["package_name"].ToString();
                pack.desc = reader["package_description"].ToString();
                pack.price = float.Parse(reader["package_price"].ToString());
                packages.Add(pack);
            }
            crud.CloseConnection();

            return packages;
        }

        public async void save(string name,float price, string desc)
        {
            string sql = @"INSERT INTO packages (package_name,package_price,package_description)
                           VALUES(@name,@price,@desc)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@price", price));
            listparams.Add(new MySqlParameter("@desc", desc));


            await crud.ExecuteAsync(sql, listparams);


        }

        public async void update(string name,float price,string desc,int id)
        {
            string sql = @"UPDATE packages SET package_name = @name, package_price = @price,
                           package_description = @desc WHERE package_id = @id ";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@price", price));
            listparams.Add(new MySqlParameter("@desc", desc));
            listparams.Add(new MySqlParameter("@id", id));


            await crud.ExecuteAsync(sql, listparams);

        }


        public async Task<float> getPrice(int id)
        {
            float price = 0;
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM packages WHERE package_id = @id";
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                price = float.Parse(reader["package_price"].ToString());

            }
            crud.CloseConnection();

            return price;

        }
        public async Task<List<ComboBoxItem>> getComboDatas()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = @"SELECT * FROM `packages`";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["package_name"].ToString(),
                    int.Parse(reader["package_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }


    }
}
