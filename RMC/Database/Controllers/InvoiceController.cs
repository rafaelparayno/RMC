using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    
   public class InvoiceController
    {
        dbcrud crud = new dbcrud();

        public async Task Save(float sales)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();
           
            sql = @"INSERT INTO invoice (sales) VALUES (@sales)";

            list.Add(new MySqlParameter("@sales", sales));

            await crud.ExecuteAsync(sql, list);
        }

      
    }
}
