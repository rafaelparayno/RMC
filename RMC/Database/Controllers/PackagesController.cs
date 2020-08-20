﻿using MySql.Data.MySqlClient;
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

            string sql = @"SELECT * FROM `packages`";

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
    }
}