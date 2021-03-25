using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class RadioQueueController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int customerid, int radioid)
        {
            string sql = @"INSERT INTO radio_queue (xray_id,customer_id) VALUES(@id,@cid)";


            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", radioid));
            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }
    }
}
