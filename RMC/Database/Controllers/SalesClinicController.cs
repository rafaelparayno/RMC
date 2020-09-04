using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        public async Task<DataSet> getDataTableAllSales()
        {
            string sql = @"SELECT sales,DATE_FORMAT(date_invoice, '%M %d %Y') AS 'Date' FROM invoice 
                        WHERE invoice_id in (SELECT invoice_id FROM salesclinic)";

           return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<float> getTotalSales()
        {
            float totalSales = 0;
            string sql = @"SELECT SUM(sales) AS 'sales' FROM invoice 
                            WHERE invoice_id in (SELECT invoice_id FROM salesclinic)";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                totalSales = reader["sales"].ToString() == "" ? 0 : 
                            float.Parse(reader["sales"].ToString());
            }

            crud.CloseConnection();

            return totalSales;
        }

        public async Task<float> getSearchDays(string date)
        {
            float totalSales = 0;
            string sql = @"SELECT SUM(sales) As 'sales' FROM invoice 
                        WHERE invoice_id in (SELECT invoice_id FROM salesclinic)
                        AND date_invoice BETWEEN @date AND DATE_ADD(@date, INTERVAL 1 DAY)";

            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);

            while(await reader.ReadAsync())
            {
                totalSales = reader["sales"].ToString() == "" ? 0 : 
                            float.Parse(reader["sales"].ToString());
            }

            crud.CloseConnection();

            return totalSales;
        }

        public async Task<float> getSearchMonths(int m,int y)
        {
            float totalSales = 0;
            string sql = @"SELECT SUM(sales) As 'sales' FROM invoice 
                        WHERE invoice_id in (SELECT invoice_id FROM salesclinic)
                        AND month(date_invoice) = @m AND year(date_invoice) = @y";

            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@m", m));
            listParams.Add(new MySqlParameter("@y", y));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);

            while(await reader.ReadAsync())
            {
                totalSales = reader["sales"].ToString() == "" ? 0 :
                             float.Parse(reader["sales"].ToString());
            }

            crud.CloseConnection();

            return totalSales;
        }

        public async Task<float> getSearchYear(int y)
        {
            float totalSales = 0;
            string sql = @"SELECT SUM(sales) As 'sales' FROM invoice 
                        WHERE invoice_id in (SELECT invoice_id FROM salesclinic)
                        AND year(date_invoice) = @y";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@y", y));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                totalSales = reader["sales"].ToString() == "" ? 0 : 
                            float.Parse(reader["sales"].ToString());

            }

            crud.CloseConnection();

            return totalSales;
        }

        /*public async Task<float> getTotalCostDay(string date)
        {
            float totalCost = 0;
            List<SalesClinicTypeMod> listSalesClinicTypeMod = new List<SalesClinicTypeMod>();

            string sql = @"SELECT * FROM `salesclinic`
                    WHERE invoice_id in (SELECT invoice_id FROM invoice 
                                                WHERE invoice.date_invoice BETWEEN @date 
                     							AND DATE_ADD(@date,INTERVAL 1 DAY))";

            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);

            while(await reader.ReadAsync())
            {
                SalesClinicTypeMod s = new SalesClinicTypeMod();
                s.type = reader["type_sales"].ToString();
                s.typeid = int.Parse(reader["type_sales_id"].ToString());
                listSalesClinicTypeMod.Add(s);
            }
            crud.CloseConnection();

           *//* foreach (SalesClinicTypeMod itemsSales in listSalesClinicTypeMod)
            {
                switch (itemsSales.type)
                {
                    case
                }
            }*//*

        

            return totalCost;
        }*/
    }
}
