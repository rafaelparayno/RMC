using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class LabQueueController
    {
        dbcrud crud = new dbcrud();


        public async Task save(int labid,int customerid)
        {
            string sql = @"INSERT INTO lab_queue (laboratory_id,customer_id) VALUES(@labid,@cid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@labid", labid));

            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }


    }
}
