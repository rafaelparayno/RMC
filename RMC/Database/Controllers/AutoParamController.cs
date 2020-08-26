using MySql.Data.MySqlClient;
using RMC.Components;
using RMC.Lab;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class AutoParamController
    {
        dbcrud crud = new dbcrud();

        public async void save(string name,float xcor,float ycor)
        {
            string sql = @"INSERT INTO auto_docs_param (paramname,xcoor,ycoor,auto_docs_id) 
                           VALUES(@name,@xcor,@ycor,
                           (SELECT auto_docs_id FROM auto_docs ORDER BY auto_docs_id DESC LIMIT 1))";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@xcor", xcor));
            listparams.Add(new MySqlParameter("@ycor", ycor));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<List<ListParams>> getListParams(int id)
        {
            List<ListParams> listofparamers = new List<ListParams>();
            string sql = @"SELECT * FROM auto_docs_param WHERE auto_docs_id = @id";
            List<MySqlParameter> listMysqlparam = new List<MySqlParameter>();
            listMysqlparam.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listMysqlparam);

            while(await reader.ReadAsync())
            {
                ListParams l = new ListParams();
                l.ID = int.Parse(reader["autodocsparmaid"].ToString());
                l.ParamName = reader["paramname"].ToString();
                l.XCoordinates = float.Parse(reader["xcoor"].ToString());
                l.YCoordinates = float.Parse(reader["ycoor"].ToString());
                listofparamers.Add(l);
            }

            crud.CloseConnection();

            return listofparamers;

        }


    }
}
