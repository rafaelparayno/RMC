using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    }
}
