using MySql.Data.MySqlClient;
using RMC.Admin.PanelReportsForms.PanelsPharmaRep;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
     class SalesPharmaController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<salesPharmacyModel>> getDataAllSales()
        {
            List<salesPharmacyModel> salesPharmas = new List<salesPharmacyModel>();
            string sql = @"SELECT * FROM `invoice` 
                          WHERE invoice_id in (SELECT invoice_id FROM salespharma)";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while (await reader.ReadAsync())
            {
                salesPharmacyModel s = new salesPharmacyModel();
                s.id = int.Parse(reader["invoice_id"].ToString());
                s.sales = float.Parse(reader["sales"].ToString());
                s.dateInvoice = DateTime.Parse(reader["date_invoice"].ToString());
                salesPharmas.Add(s);
            }

            crud.CloseConnection();

            return salesPharmas;
        }

        public async Task<float> getTotalCost()
        {
            float totalCost = 0;
            string sql = @"SELECT SUM(sales_qty * itemlist.UnitPrice) AS 'totalCost' FROM `salespharma` 
                        INNER JOIN invoice ON salespharma.invoice_id = invoice.invoice_id
                        LEFT JOIN itemlist ON salespharma.item_id = itemlist.item_id";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while (await reader.ReadAsync())
            {
                totalCost = float.Parse(reader["totalCost"].ToString());
            }

            crud.CloseConnection();
            return totalCost;
        }

        public async Task<List<salesPharmacyModel>> getSearchDays(string d,string d2)
        {
            List<salesPharmacyModel> salesPharmas = new List<salesPharmacyModel>();
            string sql;

            sql = d == d2 ? @"SELECT DISTINCT(invoice.invoice_id),sales,date_invoice FROM `invoice` 
                        INNER JOIN salespharma ON invoice.invoice_id = salespharma.invoice_id 
                        WHERE invoice.date_invoice BETWEEN @dt1 AND DATE_ADD(@dt1,INTERVAL 1 DAY) ORDER BY `invoice`.`date_invoice` ASC" :
                        @"SELECT DISTINCT(invoice.invoice_id),sales,date_invoice FROM `invoice` 
                        INNER JOIN salespharma ON invoice.invoice_id = salespharma.invoice_id 
                        WHERE invoice.date_invoice BETWEEN @dt1 AND @d2 ORDER BY `invoice`.`date_invoice` ASC";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@dt1", DateTime.Parse(d)));

            if(d != d2) listparams.Add(new MySqlParameter("@d2", DateTime.Parse(d2)));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                salesPharmacyModel s = new salesPharmacyModel();
                s.id = int.Parse(reader["invoice_id"].ToString());
                s.sales = float.Parse(reader["sales"].ToString());
                s.dateInvoice = DateTime.Parse(reader["date_invoice"].ToString());
                salesPharmas.Add(s);
            }

            crud.CloseConnection();

            return salesPharmas;
        }


        public async Task<List<salesPharmacyModel>> getSearchMonths(int d, int d2, int y)
        {
            List<salesPharmacyModel> salesPharmas = new List<salesPharmacyModel>();
            string sql;

            sql = d == d2 ? @"SELECT DISTINCT(invoice.invoice_id),sales,date_invoice FROM `invoice` 
                        INNER JOIN salespharma ON invoice.invoice_id = salespharma.invoice_id 
                        WHERE month(invoice.date_invoice) = @dt1 AND year(invoice.date_invoice) = @y
                        ORDER BY `invoice`.`date_invoice` ASC" :
                        @"SELECT DISTINCT(invoice.invoice_id),sales,date_invoice FROM `invoice` 
                        INNER JOIN salespharma ON invoice.invoice_id = salespharma.invoice_id 
                        WHERE month(invoice.date_invoice) BETWEEN @dt1 AND @d2 AND year(invoice.date_invoice) = @y
                        ORDER BY `invoice`.`date_invoice` ASC";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@dt1", d));
            listparams.Add(new MySqlParameter("@y", y));

            if (d != d2) listparams.Add(new MySqlParameter("@d2", d2));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                salesPharmacyModel s = new salesPharmacyModel();
                s.id = int.Parse(reader["invoice_id"].ToString());
                s.sales = float.Parse(reader["sales"].ToString());
                s.dateInvoice = DateTime.Parse(reader["date_invoice"].ToString());
                salesPharmas.Add(s);
            }

            crud.CloseConnection();

            return salesPharmas;
        }

        public async Task<float> getSumInMonth(int m,int y)
        {
            float totalSales = 0;
            string sql = @"SELECT SUM(sales) As 'sales' FROM invoice 
                        WHERE invoice_id in (SELECT invoice_id FROM salespharma) 
                        AND month(date_invoice) = @m AND year(date_invoice) = @y";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@m", m));
            listparams.Add(new MySqlParameter("@y", y));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                totalSales = reader["sales"].ToString() == "" ? 0 : float.Parse(reader["sales"].ToString());

            }

            crud.CloseConnection();

            return totalSales;
        }


        public async Task<float> getSumInYears(int yr)
        {
            float totalSales = 0;
            string sql = @"SELECT SUM(sales) As 'sales' FROM invoice 
                        WHERE invoice_id in (SELECT invoice_id FROM salespharma) 
                        AND year(date_invoice) = @y";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@y", yr));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                totalSales = reader["sales"].ToString() == "" ? 0 : float.Parse(reader["sales"].ToString());

            }

            crud.CloseConnection();

            return totalSales;
        }


        public async Task<float> getTotalCostMonths(int m1,int yr)
        {
            float totalCost = 0;
            string sql;

            sql =  @"SELECT SUM(sales_qty * itemlist.UnitPrice) AS 'totalCost' FROM `salespharma` 
                        INNER JOIN invoice ON salespharma.invoice_id = invoice.invoice_id 
                        LEFT JOIN itemlist ON salespharma.item_id = itemlist.item_id 
                        WHERE month(invoice.date_invoice) = @m AND year(invoice.date_invoice) = @yr";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@m", m1));
            listparams.Add(new MySqlParameter("@yr", yr));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while (await reader.ReadAsync())
            {
                totalCost = reader["totalCost"].ToString() == "" ? 0 : float.Parse(reader["totalCost"].ToString());
            }

            crud.CloseConnection();
            return totalCost;
        }


        public async Task<float> getTotalCostDays(string d,string d2)
        {
            float totalCost = 0;
            string sql;

            sql = d == d2 ? @"SELECT SUM(sales_qty * itemlist.UnitPrice) AS 'totalCost' FROM `salespharma` 
                        INNER JOIN invoice ON salespharma.invoice_id = invoice.invoice_id
                        LEFT JOIN itemlist ON salespharma.item_id = itemlist.item_id
                        WHERE invoice.date_invoice BETWEEN @dt1 AND DATE_ADD(@dt1,INTERVAL 1 DAY) " :
                      @"SELECT SUM(sales_qty * itemlist.UnitPrice) AS 'totalCost' FROM `salespharma` 
                        INNER JOIN invoice ON salespharma.invoice_id = invoice.invoice_id 
                        LEFT JOIN itemlist ON salespharma.item_id = itemlist.item_id 
                        WHERE invoice.date_invoice BETWEEN @dt1 AND @d2";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@dt1", DateTime.Parse(d)));

            if (d != d2) listparams.Add(new MySqlParameter("@d2", DateTime.Parse(d2)));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while (await reader.ReadAsync())
            {
                totalCost = reader["totalCost"].ToString() == "" ? 0 : float.Parse(reader["totalCost"].ToString());
            }

            crud.CloseConnection();
            return totalCost;
        }


       


        public async void Save(string sku,int qty)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();

            sql = @"INSERT INTO salespharma(invoice_id, item_id,sales_qty) VALUES
                    ((SELECT invoice_id FROM invoice ORDER BY invoice_id DESC LIMIT 1),
                    (SELECT item_id FROM itemlist WHERE sku = @sku), @qty)";
            
          
            // list.Add(new MySqlParameter("@invoice", invoice_id));
            list.Add(new MySqlParameter("@sku", sku));
            list.Add(new MySqlParameter("@qty", qty));
            await crud.ExecuteAsync(sql, list);
        }

        public async Task<int> getSalesInDaysItemId(int days,int id)
        {
            int sum = 0;
            string sql = @"SELECT SUM(sales_qty) As 'AverageSales' FROM salespharma 
                        INNER JOIN invoice ON salespharma.`invoice_id` = invoice.`invoice_id` 
                        WHERE invoice.`date_invoice` BETWEEN DATE_SUB(NOW() , INTERVAL @day DAY) and NOW() AND 
                        salespharma.`item_id` = @itemid";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@day", days));
            list.Add(new MySqlParameter("@itemid", id));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, list);

            while (await reader.ReadAsync())
            {
                sum =  reader["AverageSales"].ToString() == "" ? 0 : int.Parse(reader["AverageSales"].ToString());
            }
            crud.CloseConnection();
          
            return sum;
        }
    }
}
