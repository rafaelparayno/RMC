﻿using MySql.Data.MySqlClient;

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
            string sql = @"SELECT * FROM `laboratorylist` 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                        LEFT JOIN auto_docs ON laboratorylist.auto_docs_id = auto_docs.auto_docs_id";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async void save(params string[] datas)
        {
            string sql = @"INSERT INTO laboratorylist (labname,description,price_lab,labtype_id,auto_docs_id) 
                          VALUES (@name,@desc,@price,@lbid,@docsid)";
            List<MySqlParameter> listparam = new List<MySqlParameter>();
            listparam.Add(new MySqlParameter("@name", datas[0]));
            listparam.Add(new MySqlParameter("@desc", datas[1]));
            listparam.Add(new MySqlParameter("@lbid", datas[2]));
            listparam.Add(new MySqlParameter("@docsid", bool.Parse(datas[4]) == true ? int.Parse(datas[3]) : 0 ));
            listparam.Add(new MySqlParameter("@price", float.Parse(datas[5])));

            await crud.ExecuteAsync(sql, listparam);
        }
    }
}
