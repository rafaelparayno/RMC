using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class SalesClinicController
    {
        dbcrud crud = new dbcrud();
        public async void Save(string type, int id)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();

            sql = @"INSERT INTO salesclinic(invoice_id, type_sales,type_sales_id) VALUES
                    ((SELECT invoice_id FROM invoice ORDER BY invoice_id DESC LIMIT 1),
                    @type, @id)";

            list.Add(new MySqlParameter("@type", type));
            list.Add(new MySqlParameter("@id", id));
            await crud.ExecuteAsync(sql, list);
        }
    }
}
