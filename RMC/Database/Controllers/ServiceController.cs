﻿using MySql.Data.MySqlClient;
using RMC.Components;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class ServiceController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDataSet()
        {
            string sql = @"SELECT * FROM `service`";

            return  await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getDataSetSearch(string searchName)
        {
            string sql = @"SELECT * FROM `service` WHERE service_name LIKE @key";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string searches = "%" + searchName + "%";
            listparams.Add(new MySqlParameter("@key", searches));

            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async void save(string name,string desc,float price,int fileAttach)
        {
            string sql = @"INSERT INTO service (service_name,service_desc,price,with_file) 
                                    VALUES (@name,@desc,@price,@fileA)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@desc", desc));
            listparams.Add(new MySqlParameter("@price", price));
            listparams.Add(new MySqlParameter("@fileA", fileAttach));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void update(int id,string name, string desc, float price,int  fileAttach)
        {
            string sql = @"UPDATE service SET service_name = @name, service_desc = @desc,
                           price = @price, with_file = @fileA WHERE service_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@desc", desc));
            listparams.Add(new MySqlParameter("@price", price));
            listparams.Add(new MySqlParameter("@fileA", fileAttach));
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void remove(int id)
        {
            string sql = @"DELETE FROM service WHERE service_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);

        }


        public async Task<float> getPrice(int id)
        {
            float price = 0;
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM service WHERE service_id = @id";
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                price = float.Parse(reader["price"].ToString());

            }
            crud.CloseConnection();

            return price;

        }

        public async Task<List<ComboBoxItem>> getComboDatas()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = @"SELECT * FROM `service`";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["service_name"].ToString(),
                    int.Parse(reader["service_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }
    }
}
