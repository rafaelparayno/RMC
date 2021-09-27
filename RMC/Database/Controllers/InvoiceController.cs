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

        public async Task<float> getInvoiceSale(int cusid)
        {
            float sales = 0;

            string sql = @"SELECT * FROM invoice WHERE invoice_id IN 
                            (SELECT invoice_id FROM salesclinic WHERE customer_id = @cid)";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@cid", cusid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while(await reader.ReadAsync())
            {
                sales = reader["sales"].ToString() == "" ? 0 : float.Parse(reader["sales"].ToString());
            }

            crud.CloseConnection();

            return sales;
        }


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

        public async Task Delete(int id)
        {
            string sql = @"DELETE FROM invoice WHERE invoice_id in 
                                (SELECT invoice_id FROM salesclinic WHERE customer_id = @id)";
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@id", id));

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

        public async Task<float> getSalesDate(string date)
        {
            float sales = 0;

            string sql = @"SELECT SUM(sales) as 'totalSales' FROM `invoice` 
                        WHERE invoice_id in 
                            (SELECT invoice_id FROM salesclinic) 
                                AND Date(date_invoice) = @date";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@date", date)) };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while(await reader.ReadAsync())
            {
                sales = string.IsNullOrEmpty(reader["totalSales"].ToString()) ?
                    0 : float.Parse(reader["totalSales"].ToString());
            }

            crud.CloseConnection();

            return sales;
        }


      
    }
}
