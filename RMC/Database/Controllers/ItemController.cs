using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
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
    class ItemController
    {
        dbcrud crud = new dbcrud();



        public async Task<DataSet> getdataSetActive()
        {
            string sql = @"SELECT item_id,item_name ,UnitPrice , MarkupPrice ,
                                SellingPrice, SKU, Description,isBranded,category_name,unit_name,
                                Convert(ExpirationDate,varchar(50)),DateAdded FROM itemlist 
                                LEFT JOIN category ON `category`.category_id  = `itemlist`.category_id 
                                LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id WHERE itemlist.is_active = 1";
            DataSet dgSuppliers = new DataSet();
            return dgSuppliers = await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getdatasetActiveWithStock()
        {
            string sql = @"SELECT itemlist.item_id,item_name,(labitemstocks.`clinic_stocks` + pharmastocks.`pharma_stocks`) AS total , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                            WHERE itemlist.is_active = 1";
    

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getDataWithSupplierIdTotalStocks(int id)
        {
            string sql = @"SELECT itemlist.item_id,item_name,(labitemstocks.`clinic_stocks` + pharmastocks.`pharma_stocks`) AS total , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                            WHERE itemlist.item_id in (SELECT item_id 
                                                FROM supplier_items 
                                                WHERE supplier_id in (SELECT supplier_id 
                                                                      FROM suppliers 
                                                                      WHERE supplier_id = @id)) 
                            AND itemlist.is_active = 1";
            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@id", id));

            return await crud.GetDataSetAsync(sql, listParams);
        }

        public async Task<DataSet> getDataWithSupplierIdPharmaStocks(int id)
        {
            string sql = @"SELECT itemlist.item_id,item_name, pharmastocks.`pharma_stocks` , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id  
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                            WHERE itemlist.item_id in (SELECT item_id 
                                                FROM supplier_items 
                                                WHERE supplier_id in (SELECT supplier_id 
                                                                      FROM suppliers 
                                                                      WHERE supplier_id = @id)) 
                            AND itemlist.is_active = 1";
            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@id", id));

            return await crud.GetDataSetAsync(sql, listParams);
        }

        public async Task<DataSet> getDataWithSupplierIdClinicStocks(int id)
        {
            string sql = @"SELECT itemlist.item_id,item_name, labitemstocks.`clinic_stocks` , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id
                            WHERE itemlist.item_id in (SELECT item_id 
                                                FROM supplier_items 
                                                WHERE supplier_id in (SELECT supplier_id 
                                                                      FROM suppliers 
                                                                      WHERE supplier_id = @id)) 
                            AND itemlist.is_active = 1";
            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@id", id));

            return await crud.GetDataSetAsync(sql, listParams);
        }

        public async Task<DataSet> getDataSetWithStockPharma()
        {
            string sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                            WHERE itemlist.is_active = 1";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getDataSetWithStockClinic()
        {
            string sql = @"SELECT itemlist.item_id,item_name,labitemstocks.clinic_stocks , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id 
                            WHERE itemlist.is_active = 1";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getDsSearchActivePharma(int searchType, string keySearch)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                            WHERE itemlist.is_active = @isactive AND item_name LIKE @key";
                    break;
                case 1:
                    sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                                WHERE itemlist.is_active = @isactive AND SKU LIKE @key";
                    break;
                case 2:
                    sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                                WHERE itemlist.is_active = @isactive AND Description LIKE @key";
                    break;
            }
            listparams.Add(new MySqlParameter("@isactive", 1));
            string searches = "%" + keySearch + "%";
            listparams.Add(new MySqlParameter("@key", searches));
            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async Task<DataSet> getDataSearchInReturnPharma(int id, int searchType, string keySearch)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT itemlist.item_id,item_name, pharmastocks.`pharma_stocks` , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id  
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                            WHERE itemlist.item_id in (SELECT item_id 
                                                FROM supplier_items 
                                                WHERE supplier_id in (SELECT supplier_id 
                                                                      FROM suppliers 
                                                                      WHERE supplier_id = @id)) 
                            AND itemlist.is_active = 1 AND item_name LIKE @key";
                    break;
                case 1:
                    sql = @"SELECT itemlist.item_id,item_name, pharmastocks.`pharma_stocks` , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id  
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                            WHERE itemlist.item_id in (SELECT item_id 
                                                FROM supplier_items 
                                                WHERE supplier_id in (SELECT supplier_id 
                                                                      FROM suppliers 
                                                                      WHERE supplier_id = @id))  
                            AND itemlist.is_active = 1 AND SKU LIKE @key";
                    break;

            }
            listparams.Add(new MySqlParameter("@id", id));
            string searches = "%" + keySearch + "%";
            listparams.Add(new MySqlParameter("@key", searches));
            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async Task<DataSet> getDataSearchInReturnClinic(int id, int searchType, string keySearch)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT itemlist.item_id,item_name, labitemstocks.`clinic_stocks` , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id
                            WHERE itemlist.item_id in (SELECT item_id 
                                                FROM supplier_items 
                                                WHERE supplier_id in (SELECT supplier_id 
                                                                      FROM suppliers 
                                                                      WHERE supplier_id = @id)) 
                            AND itemlist.is_active = 1 AND item_name LIKE @key";
                    break;
                case 1:
                    sql = @"SELECT itemlist.item_id,item_name, labitemstocks.`clinic_stocks` , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id
                            WHERE itemlist.item_id in (SELECT item_id 
                                                FROM supplier_items 
                                                WHERE supplier_id in (SELECT supplier_id 
                                                                      FROM suppliers 
                                                                      WHERE supplier_id = @id)) 
                            AND itemlist.is_active = 1 AND SKU LIKE @key";
                    break;

            }
            listparams.Add(new MySqlParameter("@id", id));
            string searches = "%" + keySearch + "%";
            listparams.Add(new MySqlParameter("@key", searches));
            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async Task<DataSet> getDataWithSupplierIdTotalStocksWithSearch(int id, int searchType, string keySearch)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT itemlist.item_id,item_name,(labitemstocks.`clinic_stocks` + pharmastocks.`pharma_stocks`) AS total , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                            WHERE itemlist.item_id in (SELECT item_id 
                                                FROM supplier_items 
                                                WHERE supplier_id in (SELECT supplier_id 
                                                                      FROM suppliers 
                                                                      WHERE supplier_id = @id)) 
                            AND itemlist.is_active = 1 AND item_name LIKE @key";
                    break;
                case 1:
                    sql = @"SELECT itemlist.item_id,item_name,(labitemstocks.`clinic_stocks` + pharmastocks.`pharma_stocks`) AS total , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                            WHERE itemlist.item_id in (SELECT item_id 
                                                FROM supplier_items 
                                                WHERE supplier_id in (SELECT supplier_id 
                                                                      FROM suppliers 
                                                                      WHERE supplier_id = @id)) 
                            AND itemlist.is_active = 1 AND SKU LIKE @key";
                    break;

            }
            listparams.Add(new MySqlParameter("@id", id));
            string searches = "%" + keySearch + "%";
            listparams.Add(new MySqlParameter("@key", searches));
            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async Task<DataSet> getDsSearchActiveClinic(int searchType, string keySearch)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT itemlist.item_id,item_name,labitemstocks.clinic_stocks , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id 
                            WHERE itemlist.is_active = @isactive AND item_name LIKE @key";
                    break;
                case 1:
                    sql = @"SELECT itemlist.item_id,item_name,labitemstocks.clinic_stocks , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id 
                                WHERE itemlist.is_active = @isactive AND SKU LIKE @key";
                    break;
                case 2:
                    sql = @"SELECT itemlist.item_id,item_name,labitemstocks.clinic_stocks , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id 
                                WHERE itemlist.is_active = @isactive AND Description LIKE @key";
                    break;
            }
            listparams.Add(new MySqlParameter("@isactive", 1));
            string searches = "%" + keySearch + "%";
            listparams.Add(new MySqlParameter("@key", searches));
            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async Task<DataSet> getDsSearchActive(int searchType, string keySearch)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT item_id,item_name ,UnitPrice , MarkupPrice ,
                                SellingPrice, SKU, Description,isBranded,category_name,unit_name,
                                Convert(ExpirationDate,varchar(50)),DateAdded FROM itemlist 
                                LEFT JOIN category ON `category`.category_id  = `itemlist`.category_id 
                                LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                                WHERE itemlist.is_active = @isactive AND item_name LIKE @key";
                    break;
                case 1:
                    sql = @"SELECT item_id,item_name ,UnitPrice , MarkupPrice ,
                                SellingPrice, SKU, Description,isBranded,category_name,unit_name,
                                Convert(ExpirationDate,varchar(50)),DateAdded FROM itemlist 
                                LEFT JOIN category ON `category`.category_id  = `itemlist`.category_id 
                                LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                                WHERE itemlist.is_active = @isactive AND SKU LIKE @key";
                    break;
                case 2:
                    sql = @"SELECT item_id,item_name ,UnitPrice , MarkupPrice ,
                                SellingPrice, SKU, Description,isBranded,category_name,unit_name,
                                Convert(ExpirationDate,varchar(50)),DateAdded FROM itemlist 
                                LEFT JOIN category ON `category`.category_id  = `itemlist`.category_id 
                                LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                                WHERE itemlist.is_active = @isactive AND Description LIKE @key";
                    break;
            }
            listparams.Add(new MySqlParameter("@isactive", 1));
            string searches = "%" + keySearch + "%";
            listparams.Add(new MySqlParameter("@key", searches));
            return await crud.GetDataSetAsync(sql, listparams);
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
            if(int.Parse(datas[11])== 1)
            {
                list.Add(new MySqlParameter("@exp", dateExp));
            }
            else
            {
                list.Add(new MySqlParameter("@exp", null));
            }
            DateTime now = DateTime.Today;
            list.Add(new MySqlParameter("@DateAdd", now));
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

        public async void Edit(params string[] datas)
        {
            string sql = @"UPDATE itemlist SET item_name = @name, UnitPrice = @unitp, MarkupPrice = @markup,
                            SellingPrice = @selllprice, ExpirationDate = @exp, SKU = @sku, 
                            Description = @desc, isBranded = @isBranded, category_id = @catid, unit_id = @unitid 
                            WHERE item_id = @id";
            int ID = int.Parse(datas[0]);
            float unitPrice = float.Parse(datas[2]);
            float markupPrice = float.Parse(datas[3]);
            float sellingPrice = float.Parse(datas[4]);
            DateTime expDate = DateTime.Parse(datas[5]);
            int isbrand = int.Parse(datas[8]);
            int catid = int.Parse(datas[9]);
            int unitid = int.Parse(datas[10]);

            List<MySqlParameter> listParam = new List<MySqlParameter>();
            listParam.Add(new MySqlParameter("@name", datas[1]));
            listParam.Add(new MySqlParameter("@unitp", unitPrice));
            listParam.Add(new MySqlParameter("@markup", markupPrice));
            listParam.Add(new MySqlParameter("@selllprice", sellingPrice));
            if (int.Parse(datas[11]) == 1)
            {
                listParam.Add(new MySqlParameter("@exp", expDate));
            }
            else
            {
                listParam.Add(new MySqlParameter("@exp", null));
            }
            listParam.Add(new MySqlParameter("@sku", datas[6]));
            listParam.Add(new MySqlParameter("@desc", datas[7]));
            listParam.Add(new MySqlParameter("@isBranded", isbrand));
            listParam.Add(new MySqlParameter("@catid", catid));
            listParam.Add(new MySqlParameter("@unitid", unitid));
            listParam.Add(new MySqlParameter("@id", ID));

            await crud.ExecuteAsync(sql, listParam);
        }

        public async void Deactivate(int id)
        {
            string sql = @"UPDATE itemlist SET is_active = @isactive
                            WHERE item_id = @id";

            List<MySqlParameter> listParam = new List<MySqlParameter>();
            listParam.Add(new MySqlParameter("@isactive", 0));
            listParam.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listParam);
           
        }

        public int recentAddID()
        {
           
            string sql = "SELECT * FROM `itemlist` ORDER BY `itemlist`.`item_id` DESC";
            
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, null);
            int newid = 0;
            if ( reader.Read())
            {
                newid = int.Parse(reader["item_id"].ToString());
            }
            crud.CloseConnection();
           

            return newid;

        }


        public int getCategory(int itemid)
        {
            int catid = 0;
            string sql = String.Format(@"SELECT * FROM itemlist WHERE item_id = @id");
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@id", itemid));
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, list);

            if (reader.Read())
            {
                catid = int.Parse(reader["category_id"].ToString());
            }
            crud.CloseConnection();


            return catid;
        }

        public List<ItemList> Details(string keySearch)
        {
            List<ItemList> items = new List<ItemList>();
            string sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks, SKU, Description,
                            isBranded,item_type,category_name,unit_name,UnitPrice,MarkupPrice,SellingPrice
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                            WHERE itemlist.is_active = @isactive AND SKU LIKE @key";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@isactive", 1));
            string searches = "%" + keySearch + "%";
            listparams.Add(new MySqlParameter("@key", searches));
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, listparams);
            if (reader.Read())
            {
                items.Add(new ItemList(reader["item_id"].ToString(), reader["item_name"].ToString(),
                    reader["pharma_stocks"].ToString() == null ? "": reader["pharma_stocks"].ToString()
                    , reader["item_type"].ToString(),
                    reader["category_name"].ToString(), reader["isBranded"].ToString(),
                    reader["unit_name"].ToString(), reader["UnitPrice"].ToString(),
                    reader["MarkupPrice"].ToString(), reader["SellingPrice"].ToString(),
                    reader["SKU"].ToString(), reader["Description"].ToString()));
            }
            crud.CloseConnection();
            return items;
        }

        public async Task<List<ComboBoxItem>> getComboDatas()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = String.Format(@"SELECT * FROM itemlist 
                                       WHERE is_active = @isactive");
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@isactive", 1));
            
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, list);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["item_name"].ToString(),
                    int.Parse(reader["item_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }
    }
}
