using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class LaboratoryController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDataSet()
        {
            string sql = @"SELECT laboratorylist.`laboratory_id` AS 'ID',
                        labname AS 'Name',description,price_lab,labtype_name,filename AS 'DOCS'
                        FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                        LEFT JOIN auto_docs ON laboratorylist.auto_docs_id = auto_docs.auto_docs_id";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getDataSearch(int searchType,string searchkey)
        {
            string sql = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            switch (searchType)
            {
                case 0:
                    sql = @"SELECT laboratorylist.`laboratory_id` AS 'ID',
                        labname AS 'Name',description,price_lab,labtype_name,filename AS 'DOCS'
                        FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                        LEFT JOIN auto_docs ON laboratorylist.auto_docs_id = auto_docs.auto_docs_id
                        WHERE labname LIKE @key";
                    break;

                case 1:
                    sql = @"SELECT laboratorylist.`laboratory_id` AS 'ID',
                        labname AS 'Name',description,price_lab,labtype_name,filename AS 'DOCS'
                        FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                        LEFT JOIN auto_docs ON laboratorylist.auto_docs_id = auto_docs.auto_docs_id
                        WHERE labtype_name LIKE @key";
                    
                    break;

                case 2:
                    sql = @"SELECT laboratorylist.`laboratory_id` AS 'ID',
                        labname AS 'Name',description,price_lab,labtype_name,filename AS 'DOCS'
                        FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                        LEFT JOIN auto_docs ON laboratorylist.auto_docs_id = auto_docs.auto_docs_id
                        WHERE description LIKE @key";

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
            listparam.Add(new MySqlParameter("@price", float.Parse(datas[5])));
            if (datas.Length == 6)
            {
                sql = @"INSERT INTO laboratorylist (labname,description,price_lab,labtype_id,auto_docs_id) 
                          VALUES (@name,@desc,@price,@lbid,@docsid)";
            }
            else
            {
                sql = @"UPDATE laboratorylist SET labname = @name, description = @desc, price_lab = @price,
                        labtype_id = @lbid, auto_docs_id = @docsid WHERE laboratory_id = @id";
                listparam.Add(new MySqlParameter("@id", int.Parse(datas[6])));
               
            }

            await crud.ExecuteAsync(sql, listparam);
        }

        public async void remove(int id)
        {
            string sql = @"DELETE FROM laboratorylist WHERE laboratory_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }
        
    }
}
