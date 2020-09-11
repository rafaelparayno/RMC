using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class doctorResultsController
    {
        dbcrud crud = new dbcrud();

        public async Task save(params string[] data)
        {
            string sql = @"INSERT INTO doctor_results (cc,sfindings,assestment) 
                          VALUES (@cc,@sfindings,@ass)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@cc", data[0]));
            listparams.Add(new MySqlParameter("@sfindings", data[1]));
            listparams.Add(new MySqlParameter("@ass", data[2]));

            await crud.ExecuteAsync(sql, listparams);

        }
    }
}
