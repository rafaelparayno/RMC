using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class AutoDocsController
    {
        dbcrud crud = new dbcrud();

        public async void save(string name,string path)
        {
            string sql = @"INSERT INTO auto_docs (filename,path) VALUES(@name,@path)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@path", path));

            await crud.ExecuteAsync(sql, listparams);
        }
    }
}
