using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class BackOrderController
    {
        dbcrud crud = new dbcrud();
        

        public async void save(int poid)
        {
            string sql = @"INSERT INTO back_order (po_id) values (@poid)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@poid", poid));

            await crud.ExecuteAsync(sql, listparams);

        }
    }
}
