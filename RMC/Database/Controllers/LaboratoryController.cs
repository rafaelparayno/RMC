using MySql.Data.MySqlClient;
using RMC.Components;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class LaboratoryController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDataSet()
        {
            string sql = @"SELECT laboratorylist.`laboratory_id` AS 'ID',
                        labname AS 'Name',description,price_lab,labtype_name,filename AS 'DOCS',
                        crystal_name
                        FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                        LEFT JOIN auto_docs ON laboratorylist.auto_docs_id = auto_docs.auto_docs_id 
                        LEFT JOIN automated_crystal ON laboratorylist.crystal_id_lab = automated_crystal.automated_crystal_id
                        ORDER BY laboratory_id ASC";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<labModel>getLabModelInID(int id)
        {
            labModel labModel = new labModel();
            string sql = @"SELECT * FROM `laboratorylist` WHERE laboratory_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            DbDataReader reader = await  crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                labModel.id = int.Parse(reader["laboratory_id"].ToString());

                labModel.name = reader["labname"].ToString();

                labModel.autodocsid = string.IsNullOrEmpty(reader["auto_docs_id"].ToString())? 0 : 
                                        int.Parse(reader["auto_docs_id"].ToString());

                labModel.crystal_id_lab = string.IsNullOrEmpty(reader["crystal_id_lab"].ToString()) ?
                                    0 : int.Parse(reader["crystal_id_lab"].ToString());

            }

            crud.CloseConnection();


            return labModel;
            

        }


        public async Task<labModel> getLabModelinPatientLab(int patientlabid)
        {
            labModel labModel = new labModel();
            string sql = @"SELECT * FROM `laboratorylist` WHERE laboratory_id in (SELECT patient_lab.laboratory_id FROM patient_lab WHERE patient_lab.patient_lab_id = @id)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", patientlabid));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                labModel.id = int.Parse(reader["laboratory_id"].ToString());

                labModel.name = reader["labname"].ToString();

                labModel.autodocsid = string.IsNullOrEmpty(reader["auto_docs_id"].ToString()) ? 0 :
                                        int.Parse(reader["auto_docs_id"].ToString());

                labModel.crystal_id_lab = string.IsNullOrEmpty(reader["crystal_id_lab"].ToString()) ?
                                    0 : int.Parse(reader["crystal_id_lab"].ToString());

            }

            crud.CloseConnection();


            return labModel;


        }


        public async Task<List<labModel>> getLabModel(int type)
        {
            List<labModel> listlabmodels = new List<labModel>();
            string sql = @"SELECT * FROM `laboratorylist`
                        WHERE labtype_id = @type";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@type", type));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                labModel l = new labModel();
                l.id = int.Parse(reader["laboratory_id"].ToString());
                l.name = reader["labname"].ToString();
                l.autodocsid = int.Parse(reader["auto_docs_id"].ToString());
                listlabmodels.Add(l);
            }

            crud.CloseConnection();


            return listlabmodels;
        }

        public async Task<DataSet> getDataSearch(int searchType,string searchkey)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            switch (searchType)
            {
                case 0:
                    sql = @"SELECT laboratorylist.`laboratory_id` AS 'ID',
                        labname AS 'Name',description,price_lab,labtype_name,filename AS 'DOCS',
                        crystal_name
                        FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                        LEFT JOIN auto_docs ON laboratorylist.auto_docs_id = auto_docs.auto_docs_id 
                        LEFT JOIN automated_crystal ON laboratorylist.crystal_id_lab = automated_crystal.automated_crystal_id
                        WHERE labname LIKE @key ORDER BY laboratory_id ASC";
                    break;

                case 1:
                    sql = @"SELECT laboratorylist.`laboratory_id` AS 'ID',
                        labname AS 'Name',description,price_lab,labtype_name,filename AS 'DOCS',
                        crystal_name
                        FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                        LEFT JOIN auto_docs ON laboratorylist.auto_docs_id = auto_docs.auto_docs_id 
                        LEFT JOIN automated_crystal ON laboratorylist.crystal_id_lab = automated_crystal.automated_crystal_id
                        WHERE labtype_name LIKE @key ORDER BY laboratory_id ASC";
                    
                    break;

                case 2:
                    sql = @"SELECT laboratorylist.`laboratory_id` AS 'ID',
                        labname AS 'Name',description,price_lab,labtype_name,filename AS 'DOCS',
                        crystal_name
                        FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                        LEFT JOIN auto_docs ON laboratorylist.auto_docs_id = auto_docs.auto_docs_id 
                        LEFT JOIN automated_crystal ON laboratorylist.crystal_id_lab = automated_crystal.automated_crystal_id
                        WHERE description LIKE @key ORDER BY laboratory_id ASC";

                    break;

            }

            string searches = "%" + searchkey + "%";
            listparams.Add(new MySqlParameter("@key", searches));
            return await crud.GetDataSetAsync(sql, listparams);
        }

        public async void save(params string[] datas)
        {
            List<MySqlParameter> listparam = new List<MySqlParameter>();
            string sql;
            listparam.Add(new MySqlParameter("@name", datas[0]));
            listparam.Add(new MySqlParameter("@desc", datas[1]));
            listparam.Add(new MySqlParameter("@lbid", datas[2]));
            listparam.Add(new MySqlParameter("@docsid", bool.Parse(datas[4]) == true ? int.Parse(datas[3]) : 0));
          
            listparam.Add(new MySqlParameter("@csidlab", bool.Parse(datas[6]) == true ? int.Parse(datas[5]) : 0));
            listparam.Add(new MySqlParameter("@price", float.Parse(datas[7])));
            if (datas.Length == 8)
            {
                sql = @"INSERT INTO laboratorylist (labname,description,price_lab,labtype_id,auto_docs_id,crystal_id_lab) 
                          VALUES (@name,@desc,@price,@lbid,@docsid,@csidlab)";
            }
            else
            {
                sql = @"UPDATE laboratorylist SET labname = @name, description = @desc, price_lab = @price,
                        labtype_id = @lbid, auto_docs_id = @docsid ,crystal_id_lab = @csidlab WHERE laboratory_id = @id";
                listparam.Add(new MySqlParameter("@id", int.Parse(datas[8])));
               
            }

            Console.WriteLine(sql);
            Console.WriteLine(datas[6]+ " "  + datas[5]);
            await crud.ExecuteAsync(sql, listparam);

        }

        public async void remove(int id)
        {
            string sql = @"DELETE FROM laboratorylist WHERE laboratory_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<float> getPrice(int id)
        {
            float price = 0;
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM laboratorylist WHERE laboratory_id = @id";
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);
            
            if(await reader.ReadAsync())
            {
                price = float.Parse(reader["price_lab"].ToString());

            }
            crud.CloseConnection();

            return price;

        }

        public async Task<List<ComboBoxItem>> getComboDatas()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = String.Format(@"SELECT laboratorylist.`laboratory_id`,
                        labname,description,price_lab,labtype_name
                        FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id ");

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["labname"].ToString(),
                    int.Parse(reader["laboratory_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }

    }
}
