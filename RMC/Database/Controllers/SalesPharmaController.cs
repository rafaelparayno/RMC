using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    public class SalesPharmaController
    {
        dbcrud crud = new dbcrud();

        public async void Save(string sku,int qty)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();

            /*  sql = @"INSERT INTO salespharma (invoice_id,item_id) VALUES 
                      (@invoice,(SELECT item_id FROM itemlist WHERE sku = @sku))";*/

            sql = @"INSERT INTO salespharma(invoice_id, item_id,sales_qty) VALUES
                    ((SELECT invoice_id FROM invoice ORDER BY invoice_id DESC LIMIT 1),
                    (SELECT item_id FROM itemlist WHERE sku = @sku), @qty)";
            
          
            // list.Add(new MySqlParameter("@invoice", invoice_id));
            list.Add(new MySqlParameter("@sku", sku));
            list.Add(new MySqlParameter("@qty", qty));
            await crud.ExecuteAsync(sql, list);
        }
    }
}
