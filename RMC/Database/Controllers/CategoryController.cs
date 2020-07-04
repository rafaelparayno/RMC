using MySql.Data.MySqlClient;
using RMC.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class CategoryController
    {
        dbcrud crud = new dbcrud();
        

        public async Task<DataSet> getDataSetActivet()
        {
            string sql = @"SELECT category_id AS 'id' ,category_name AS 'Category',item_type 
               FROM category WHERE is_active = 1";
          

            return await crud.GetDataSetAsync(sql, null);
        }

        public async void Save(string catName,int type)
        {
            string sql = @"INSERT INTO category (category_name,item_type,is_active) VALUES 
                            (@name,@type,@isactive)";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@name", catName));
            list.Add(new MySqlParameter("@type", type));
            list.Add(new MySqlParameter("@isactive", 1));

             await crud.ExecuteAsync(sql, list);
        }

        public async void Edit(string catName,int type,int id)
        {
            string sql = @"UPDATE category SET category_name = @name, item_type = @type 
                            WHERE category_id = @id";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@name", catName));
            list.Add(new MySqlParameter("@type", type));
            list.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, list);
        }
        

        public async void Deactivate(int id)
        {
            string sql = @"UPDATE category SET is_active = @isactive 
                            WHERE category_id = @id";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@isactive", 0));
            list.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, list);

        }

        public async Task<List<ComboBoxItem>> getComboDatas(int itemType)
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = String.Format(@"SELECT * FROM category 
                                       WHERE is_active = @isactive 
                                        AND item_type = @item");
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@isactive", 1));
            list.Add(new MySqlParameter("@item", itemType));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, list);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["category_name"].ToString(),
                    int.Parse(reader["category_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }

        public int getItemType(int catid)
        {
            int type = 0;
            string sql = String.Format(@"SELECT * FROM category WHERE category_id = @id");
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@id", catid));
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, list);

            if (reader.Read())
            {
                type = int.Parse(reader["item_type"].ToString());
            }
            crud.CloseConnection();


            return type;
        }


    }
}
