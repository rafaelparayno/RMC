﻿using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class ItemController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDsActive()
        {
            string sql = "SELECT * FROM `itemlist";
       
            DataSet dgItems = new DataSet();
            return dgItems = await crud.GetDataSetAsync(sql, null);

            
        }

        public int getRecentItemID()
        {
            string sql = String.Format(@"SELECT AUTO_INCREMENT As 'Last_id'
                                        FROM information_schema.tables 
                                        WHERE table_name='itemlist' 
                                        AND table_schema= DATABASE()");
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, null);
            int last_id = 0;
            if (reader.Read())
            {
                last_id = int.Parse(reader["Last_id"].ToString());
            }
            crud.CloseConnection();
            return last_id;
        }

        public async void Save(params string [] datas)
        {
            string sql = String.Format(@"INSERT INTO itemlist (item_name,UnitPrice,MarkupPrice,
                                                                SellingPrice,ExpirationDate,DateAdded,
                                                                SKU,Description,isBranded,category_id,
                                                                unit_id,is_active) VALUES 
                                       (@name,@unit,@markup,@sellprice,@exp,@DateAdd,@sku,
                                        @desc,@isBrand,@catid,@unitid,@isactive)");

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@name", datas[0]));
            float unit = float.Parse(datas[1]);
            list.Add(new MySqlParameter("@unit", unit));
            float markup = float.Parse(datas[2]);
            list.Add(new MySqlParameter("@markup", markup));
            float selling = float.Parse(datas[3]);
            list.Add(new MySqlParameter("@sellprice", selling));
            DateTime dateExp = DateTime.Parse(datas[4]);
            list.Add(new MySqlParameter("@exp",dateExp));
            list.Add(new MySqlParameter("@DateAdd", datas[5]));
            list.Add(new MySqlParameter("@sku", datas[6]));
            list.Add(new MySqlParameter("@desc", datas[7]));
            int isBrand = int.Parse(datas[8]);
            list.Add(new MySqlParameter("@isBrand", isBrand));
            int catid = int.Parse(datas[9]);
            list.Add(new MySqlParameter("@catid", catid));
            int unitd = int.Parse(datas[10]);
            list.Add(new MySqlParameter("@unitid", unitd));
          
            list.Add(new MySqlParameter("@isactive", 1));

            await crud.ExecuteAsync(sql, list);


        }
    }
}
