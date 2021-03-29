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
    class XrayControllers
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDataSet()
        {
            string sql = @"SELECT xray_id,xray_name,xray_type,description,xray_price,filename FROM `xraylist` 
                    LEFT JOIN auto_docs ON xraylist.auto_docs_id = auto_docs.auto_docs_id";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<List<xraymodel>> getLabModel(int type)
        {
            List<xraymodel> listxraymodels = new List<xraymodel>();
            string sql = @"SELECT * FROM `xraylist`
                        WHERE xray_type = @type";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@type", type));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                xraymodel l = new xraymodel();
                l.id = int.Parse(reader["xray_id"].ToString());
                l.name = reader["xray_name"].ToString();
                l.autodocsid = int.Parse(reader["auto_docs_id"].ToString());
                listxraymodels.Add(l);
            }

            crud.CloseConnection();


            return listxraymodels;
        }

        public async Task<DataSet> getSearchDataset(string searchkey)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            
            string sql = @"SELECT * FROM `xraylist` WHERE xray_name LIKE @key";
            string key = "%" + searchkey + "%";
            listparams.Add(new MySqlParameter("@key", key));

           return  await crud.GetDataSetAsync(sql, listparams);
        }

        public async void save(string name,string desc,int type,float price,int idauto,bool isAuto,int crystal)
        {
            string sql = @"INSERT INTO xraylist (xray_name,xray_type,description,xray_price,auto_docs_id,is_crystal)
                           VALUES (@name,@type,@desc,@price,@auto,@cr)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@desc", desc));
            listparams.Add(new MySqlParameter("@type", type));
            listparams.Add(new MySqlParameter("@price", price));
            listparams.Add(new MySqlParameter("@auto", isAuto ?  idauto : 0));
            listparams.Add(new MySqlParameter("@cr", crystal));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async void update(int id,string name,string desc,int type,float price, int idauto, bool isAuto,int crystal)
        {

            string sql = @"UPDATE xraylist SET xray_name = @name, xray_type = @type,
                          description = @desc , xray_price = @price, auto_docs_id = @auto,is_crystal = @cr WHERE xray_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@desc", desc));
            listparams.Add(new MySqlParameter("@type", type));
            listparams.Add(new MySqlParameter("@price", price));
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@auto", isAuto ? idauto : 0));
            listparams.Add(new MySqlParameter("@cr", crystal));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void remove(int id)
        {
            string sql = @"DELETE FROM xraylist WHERE xray_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);

        }


        public async Task<float> getPrice(int id)
        {
            float price = 0;
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM xraylist WHERE xray_id = @id";
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                price = float.Parse(reader["xray_price"].ToString());

            }
            crud.CloseConnection();

            return price;

        }

        public async Task<List<ComboBoxItem>> getComboDatas()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = @"SELECT * FROM `xraylist`";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["xray_name"].ToString(),
                    int.Parse(reader["xray_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }

    }
}
