using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    
   public class InvoiceController
    {
        dbcrud crud = new dbcrud();


        public async Task<int> getLatestNo()
        {
            int invoice = 0; 
            string sql = @"SELECT * FROM invoice ORDER BY invoice.invoice_id DESC LIMIT 1";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            if(await reader.ReadAsync())
            {
                invoice = string.IsNullOrEmpty(reader["invoice_id"].ToString()) ? 0 :
                    int.Parse(reader["invoice_id"].ToString());
            }
            crud.CloseConnection();

            return invoice;
        }

        public async Task Save(float sales)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();
           
            sql = @"INSERT INTO invoice (sales) VALUES (@sales)";

            list.Add(new MySqlParameter("@sales", sales));

            await crud.ExecuteAsync(sql, list);
        }

        public async Task<DataSet> getInvoicePharma()
        {
            string sql = @"SELECT DISTINCT(invoice.invoice_id),sales,date_invoice FROM invoice 
                        INNER JOIN salespharma ON invoice.invoice_id = salespharma.invoice_id";
          

            return await crud.GetDataSetAsync(sql, null);
        }


        public async Task<DataSet> getInvoicePharma(string date)
        {
            string sql = @"SELECT DISTINCT(invoice.invoice_id),sales,date_invoice FROM invoice 
                        INNER JOIN salespharma ON invoice.invoice_id = salespharma.invoice_id
                        WHERE Date(date_invoice) = @date";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@date", date)) };

            return await crud.GetDataSetAsync(sql, mySqlParameters);
        }

      
    }
}
