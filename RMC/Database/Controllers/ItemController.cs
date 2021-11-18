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


        public async Task<float> getUnitCosts(int id)
        {
            float unitCost = 0;

            string sql = @"SELECT * FROM itemlist WHERE item_id = @id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id",id))};

            DbDataReader reader =  await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while(await reader.ReadAsync())
            {
                unitCost = float.Parse(reader["unitPrice"].ToString());
            }
            crud.CloseConnection();



            return unitCost;
        }

        public async Task updateUnitCost(int itemid, double price)
        {
            

            string sql = @"UPDATE itemlist SET unitPrice = @price  WHERE item_id = @id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() 
            { 
                (new MySqlParameter("@id", itemid)),
                 (new MySqlParameter("@price", price))
            };


            await crud.ExecuteAsync(sql, mySqlParameters);
        }


        public async Task updateSellingAndMarkup(double Selling, double Markup,int id)
        {


            string sql = @"UPDATE itemlist SET SellingPrice = @price,MarkupPrice = @mark  WHERE item_id = @id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@price", Selling)),
                 (new MySqlParameter("@mark", Markup)),
                 (new MySqlParameter("@id", id)),
            };


            await crud.ExecuteAsync(sql, mySqlParameters);
        }

        public async Task updateExpDate(string expDate,int id)
        {

            string sql = @"UPDATE itemlist SET ExpirationDate = @date  WHERE item_id = @id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date", expDate)),
                 (new MySqlParameter("@id", id)),
            };


            await crud.ExecuteAsync(sql, mySqlParameters);
        }

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

        public async Task<DataSet> getdataSetPharma()
        {
            string sql = @"SELECT itemlist.item_id,item_name,
                                SellingPrice, pharmastocks.`pharma_stocks`,SKU, Description,isBranded,category_name,unit_name,                               
                                Convert(ExpirationDate,varchar(50)),DateAdded FROM itemlist 
                                LEFT JOIN category ON `category`.category_id  = `itemlist`.category_id 
                                LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                                LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                                WHERE itemlist.is_active = 1";
            DataSet dgSuppliers = new DataSet();
            return dgSuppliers = await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getDsSearchPharmaActive(int searchType, string keySearch)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT itemlist.item_id,item_name,
                                SellingPrice, pharmastocks.`pharma_stocks`,SKU, Description,isBranded,category_name,unit_name,                               
                                Convert(ExpirationDate,varchar(50)),DateAdded FROM itemlist 
                                LEFT JOIN category ON `category`.category_id  = `itemlist`.category_id 
                                LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                                LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                                WHERE itemlist.is_active = 1 AND item_name LIKE @key";
                    break;
                case 1:
                    sql = @"SELECT itemlist.item_id,item_name,
                                SellingPrice, pharmastocks.`pharma_stocks`,SKU, Description,isBranded,category_name,unit_name,                               
                                Convert(ExpirationDate,varchar(50)),DateAdded FROM itemlist 
                                LEFT JOIN category ON `category`.category_id  = `itemlist`.category_id 
                                LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                                LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                                WHERE itemlist.is_active = 1 SKU LIKE @key";
                    break;
                case 2:
                    sql = @"SELECT itemlist.item_id,item_name,
                                SellingPrice, pharmastocks.`pharma_stocks`,SKU, Description,isBranded,category_name,unit_name,                               
                                Convert(ExpirationDate,varchar(50)),DateAdded FROM itemlist 
                                LEFT JOIN category ON `category`.category_id  = `itemlist`.category_id 
                                LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                                LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                                WHERE itemlist.is_active = 1 AND Description LIKE @key";
                    break;
            }
            string searches = "%" + keySearch + "%";
            listparams.Add(new MySqlParameter("@key", searches));
            return await crud.GetDataSetAsync(sql, listparams);
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

        public async Task<DataSet> getdatasetActiveExpiration()
        {
            string sql = @"SELECT item_id,item_name,SKU,Description,ExpirationDate FROM `itemlist` WHERE ExpirationDate 
                            BETWEEN ExpirationDate and NOW() AND itemlist.is_active = 1";


            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<float> getSumExpiredItems(int m,int year)
        {
            float totalExpired = 0;
            string sql = @"SELECT SUM((pharmastocks.pharma_stocks * itemlist.UnitPrice)) as 'sumexp' FROM `itemlist`
                            INNER JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                            WHERE MONTH(itemlist.ExpirationDate) = @m AND YEAR(itemlist.ExpirationDate) = @y 
                            AND itemlist.is_active = 1";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@m",m)),
                (new MySqlParameter("@y",year))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while(await reader.ReadAsync())
            {
                totalExpired = string.IsNullOrEmpty(reader["sumexp"].ToString()) ?
                    0 : float.Parse(reader["sumexp"].ToString());
            }

            crud.CloseConnection();
            return totalExpired; 
        }

        public async Task<DataSet> getdatasetActiveExpirationWithDate(int days)
        {
            string sql = @"SELECT item_id,item_name,SKU,Description,ExpirationDate FROM `itemlist` WHERE ExpirationDate 
                             BETWEEN NOW() AND DATE_ADD(NOW() , INTERVAL @day DAY) AND itemlist.is_active = 1";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@day", days));

            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async Task<List<itemModel>> getDataWithSupplierIdTotalStocks(int id)
        {
            List<itemModel> itemModels = new List<itemModel>();
            string sql = @"SELECT itemlist.item_id,item_name,COALESCE((labitemstocks.`clinic_stocks` + pharmastocks.`pharma_stocks`),pharmastocks.`pharma_stocks`) AS total , 
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
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);

            while(await reader.ReadAsync())
            {
                itemModel itemModel = new itemModel();
                itemModel.id = int.Parse(reader["item_id"].ToString());
                itemModel.name = reader["item_name"].ToString();
                itemModel.stocks = reader["total"].ToString() == "" ? 0 : int.Parse(reader["total"].ToString());
                itemModel.unitPrice = float.Parse(reader["UnitPrice"].ToString());
                itemModel.sku = reader["SKU"].ToString();
                itemModel.description = reader["Description"].ToString();
                itemModel.isBrand = int.Parse(reader["isBranded"].ToString());
                itemModel.subCategory = int.Parse(reader["item_type"].ToString());
                itemModel.Category = reader["category_name"].ToString();
                itemModel.UnitName = reader["unit_name"].ToString();
                itemModels.Add(itemModel);
            }

            crud.CloseConnection();
            return itemModels;
        //    return await crud.GetDataSetAsync(sql, listParams);
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
            string sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks,UnitPrice , SKU, Description,isBranded,category.item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                            WHERE itemlist.is_active = 1";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getDataSetWithStockClinic()
        {
            string sql = @"SELECT itemlist.item_id,item_name,labitemstocks.clinic_stocks,UnitPrice , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id 
                            WHERE itemlist.is_active = 1";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getMedicinesActives()
        {
            string sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks , SKU, Description,category_name,isBranded
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                            WHERE itemlist.is_active = 1 AND item_type = 1";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getMedicinesActives(string searchkey)
        {
            string sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks , SKU, Description,category_name,isBranded
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                            WHERE itemlist.is_active = 1 AND item_type = 1 AND Description LIKE @key 
                            OR item_name LIKE @key OR category_name LIKE @key";


            List<MySqlParameter> listMySqlParameters = new List<MySqlParameter>();
            string key = "%" + searchkey + "%";
            listMySqlParameters.Add(new MySqlParameter("@key", searchkey));
            

            return await crud.GetDataSetAsync(sql, listMySqlParameters);
        }

        public async Task<DataSet> getDsSearchActivePharma(int searchType, string keySearch)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks,UnitPrice , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                            WHERE itemlist.is_active = @isactive AND item_name LIKE @key";
                    break;
                case 1:
                    sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks,UnitPrice , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                                WHERE itemlist.is_active = @isactive AND SKU LIKE @key";
                    break;
                case 2:
                    sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks,UnitPrice , SKU, Description,isBranded,item_type,category_name,unit_name 
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

        public async Task<List<itemModel>> getDataWithSupplierIdTotalStocksWithSearch(int id, int searchType, string keySearch)
        {
            List<itemModel> itemModels = new List<itemModel>();
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT itemlist.item_id,item_name,COALESCE((labitemstocks.`clinic_stocks` + pharmastocks.`pharma_stocks`),pharmastocks.`pharma_stocks`) AS total , 
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
                    sql = @"SELECT itemlist.item_id,item_name,COALESCE((labitemstocks.`clinic_stocks` + pharmastocks.`pharma_stocks`),pharmastocks.`pharma_stocks`) AS total , 
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
         
            
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                itemModel itemModel = new itemModel();
                itemModel.id = int.Parse(reader["item_id"].ToString());
                itemModel.name = reader["item_name"].ToString();
                itemModel.stocks = reader["total"].ToString() == "" ? 0 : int.Parse(reader["total"].ToString());
                itemModel.unitPrice = float.Parse(reader["UnitPrice"].ToString());
                itemModel.sku = reader["SKU"].ToString();
                itemModel.description = reader["Description"].ToString();
                itemModel.isBrand = int.Parse(reader["isBranded"].ToString());
                itemModel.subCategory = int.Parse(reader["item_type"].ToString());
                itemModel.Category = reader["category_name"].ToString();
                itemModel.UnitName = reader["unit_name"].ToString();
                itemModels.Add(itemModel);
            }

            crud.CloseConnection();
            return itemModels;
        }

        public async Task<DataSet> getDsSearchActiveClinic(int searchType, string keySearch)
        {

            
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            switch (searchType)
            {
                case 0:
                    sql = @"SELECT itemlist.item_id,item_name,labitemstocks.clinic_stocks,UnitPrice , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id 
                            WHERE itemlist.is_active = @isactive AND item_name LIKE @key";
                    break;
                case 1:
                    sql = @"SELECT itemlist.item_id,item_name,labitemstocks.clinic_stocks,UnitPrice , SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id 
                                WHERE itemlist.is_active = @isactive AND SKU LIKE @key";
                    break;
                case 2:
                    sql = @"SELECT itemlist.item_id,item_name,labitemstocks.clinic_stocks,UnitPrice , SKU, Description,isBranded,item_type,category_name,unit_name 
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

        public async Task Save(params string [] datas)
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



        public async Task Edit(params string[] datas)
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


        public async Task<itemModel> getDataModel(string sku)
        {
            itemModel item = new itemModel();
            string sql = @"SELECT itemlist.item_id,item_name,pharmastocks.pharma_stocks, SKU, Description,
                            isBranded,UnitPrice,MarkupPrice,SellingPrice
                            FROM itemlist 
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id
                          WHERE itemlist.is_active = 1 AND SKU = @sku";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
     
            listparams.Add(new MySqlParameter("@sku", sku));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams); 
           
            if (reader.Read())
            {
                item.id = int.Parse(reader["item_id"].ToString());
                item.name = reader["item_name"].ToString();
                item.stocks = reader["pharma_stocks"].ToString() == "" ? 0 : 
                    int.Parse(reader["pharma_stocks"].ToString());
                item.sellingPrice = float.Parse(reader["SellingPrice"].ToString());
                item.sku = reader["SKU"].ToString();
                item.unitPrice = float.Parse(reader["UnitPrice"].ToString());
                item.markupPrice = float.Parse(reader["MarkupPrice"].ToString());

            }
            crud.CloseConnection();
            return item;
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

        public async Task<List<ComboBoxItem>> getMedicinesBrandedActive()
        {

            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = @"SELECT * FROM itemlist 
                         WHERE category_id in (SELECT category_id FROM category WHERE item_type = 1)
                         AND is_active = 1 AND isBranded = 1";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["item_name"].ToString(),
                    int.Parse(reader["item_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }

        public async Task<List<ComboBoxItem>> getMedicinesGenericActive()
        {

            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = @"SELECT * FROM itemlist 
                         WHERE category_id in (SELECT category_id FROM category WHERE item_type = 1)
                         AND is_active = 1 AND isBranded = 2";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["item_name"].ToString(),
                    int.Parse(reader["item_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }

        public async Task<bool> isFoundSKU(string sku)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM `itemlist` WHERE is_active = 1 AND SKU = @sku";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@sku", sku));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                isFound = true;
            }

            crud.CloseConnection();

            return isFound;
        }


        public async Task<itemModel> getModel(int id)
        {
            itemModel m = new itemModel();


            string sql = @"SELECT itemlist.item_id,item_name,COALESCE((labitemstocks.`clinic_stocks` + pharmastocks.`pharma_stocks`),pharmastocks.`pharma_stocks`) AS total , 
                            UnitPrice,SKU, Description,isBranded,item_type,category_name,unit_name 
                            FROM itemlist LEFT JOIN category ON `category`.category_id = `itemlist`.category_id 
                            LEFT JOIN unitofmeasurement ON unitofmeasurement.unit_id = itemlist.unit_id 
                            LEFT JOIN labitemstocks ON itemlist.item_id = labitemstocks.item_id
                            LEFT JOIN pharmastocks ON itemlist.item_id = pharmastocks.item_id 
                            WHERE itemlist.item_id = @id  AND itemlist.is_active = 1";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",id)
            };


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);


            if(await reader.ReadAsync())
            {
                m.id = int.Parse(reader["item_id"].ToString());
                m.name = reader["item_name"].ToString();
                m.stocks = reader["total"].ToString() == "" ? 0 : int.Parse(reader["total"].ToString());
                m.unitPrice = float.Parse(reader["UnitPrice"].ToString());
                m.sku = reader["SKU"].ToString();
                m.description = reader["Description"].ToString();
                m.isBrand = int.Parse(reader["isBranded"].ToString());
                m.subCategory = int.Parse(reader["item_type"].ToString());
                m.Category = reader["category_name"].ToString();
                m.UnitName = reader["unit_name"].ToString();

            }

            crud.CloseConnection();


            return m;

        }
    }
}
