using MySql.Data.MySqlClient;
using RMC.Admin.PanelReportsForms.PanelsPharmaRep;
using RMC.Database.Models;
using RMC.Lab;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
     class SalesPharmaController
    {
        dbcrud crud = new dbcrud();


        public async Task<DataSet> getInvoiceInSalesPharma(int invoice_id)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@invoice_id", invoice_id));

            DataSet ds = await crud.GetDataSetAsync(@"SELECT sku,item_name as 'name',sales_qty as 'qty',(sales_qty * SellingPrice) AS 'Sales' 
                                                    FROM `salespharma` INNER JOIN itemlist 
                                                    ON salespharma.item_id = itemlist.item_id
                                                    WHERE invoice_id = @invoice_id", listparams);

            return ds;
        }

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

        public async Task<float> getTotalMedicineTodaySales()
        {
            float totalMed = 0;
            string sql = @"SELECT (salespharma.sales_qty * itemlist.SellingPrice) as 'medicinesales' FROM `salespharma` 
                            INNER JOIN invoice ON salespharma.invoice_id = invoice.invoice_id 
                            INNER JOIN itemlist ON salespharma.item_id = itemlist.item_id
                            WHERE salespharma.item_id in 
                            (SELECT itemlist.item_id FROM itemlist 
 	                            WHERE itemlist.category_id = (SELECT category.category_id FROM category WHERE category.item_type = 1)) 
                            AND DATE(invoice.date_invoice) = CURDATE()";
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while (await reader.ReadAsync())
            {
                totalMed = float.Parse(reader["medicinesales"].ToString());
            }

            crud.CloseConnection();


            return totalMed;
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

        public async Task<float> getTotalCostYears(int year)
        {
            float totalCost = 0;
            string sql;

            sql = @"SELECT SUM(sales_qty * itemlist.UnitPrice) AS 'totalCost' FROM `salespharma` 
                        INNER JOIN invoice ON salespharma.invoice_id = invoice.invoice_id 
                        LEFT JOIN itemlist ON salespharma.item_id = itemlist.item_id 
                        WHERE year(invoice.date_invoice) = @yr";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
  
            listparams.Add(new MySqlParameter("@yr", year));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while (await reader.ReadAsync())
            {
                totalCost = reader["totalCost"].ToString() == "" ? 0 : float.Parse(reader["totalCost"].ToString());
            }

            crud.CloseConnection();
            return totalCost;
        }

        public async void updateReturn(string sku,int qty,int invoice_id)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (qty > 0)
            {
                sql = @"UPDATE salespharma SET sales_qty = @qty 
                    WHERE item_id = (SELECT item_id FROM itemlist WHERE SKU = @sku) 
                    AND invoice_id = @invoice_id";

                list = new List<MySqlParameter>();
                list.Add(new MySqlParameter("@sku", sku));
                list.Add(new MySqlParameter("@qty", qty));
                list.Add(new MySqlParameter("@invoice_id", invoice_id));
            }
            else
            {
                sql = @"DELETE FROM salespharma 
                        WHERE item_id = (SELECT item_id FROM itemlist WHERE SKU = @sku) 
                        AND invoice_id = @invoice_id";

                list = new List<MySqlParameter>();
                list.Add(new MySqlParameter("@sku", sku));
            
                list.Add(new MySqlParameter("@invoice_id", invoice_id));
            }
          

            await  crud.ExecuteAsync(sql, list);
        }

        public async Task Save(string sku,int qty)
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

        public async Task<int> getOpeningBalance(int id,string date)
        {
            int sum = 0;
            string sql = @"SELECT SUM(sales_qty) AS 'addBal' FROM
                            salespharma 
                            LEFT JOIN invoice ON salespharma.invoice_id = invoice.invoice_id
                            WHERE  invoice.date_invoice  BETWEEN @day AND NOW() AND item_id = @itemid";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@day", DateTime.Parse(date)));
            list.Add(new MySqlParameter("@itemid", id));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, list);

            while (await reader.ReadAsync())
            {
                sum = reader["addBal"].ToString() == "" ? 0 : int.Parse(reader["addBal"].ToString());
            }
            crud.CloseConnection();

            return sum;
        }

        public async Task<int> getQtyInDate(int id,string date)
        {
            int sum = 0;
            string sql = @"SELECT SUM(sales_qty) As 'salesDate' FROM salespharma 
                        INNER JOIN invoice ON salespharma.`invoice_id` = invoice.`invoice_id` 
                    	 WHERE invoice.date_invoice BETWEEN @day 
                                    AND DATE_ADD(@day,INTERVAL 1 DAY)
                         AND  salespharma.`item_id` = @itemid";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@day", DateTime.Parse(date)));
            list.Add(new MySqlParameter("@itemid", id));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, list);

            while (await reader.ReadAsync())
            {
                sum = reader["salesDate"].ToString() == "" ? 0 : int.Parse(reader["salesDate"].ToString());
            }
            crud.CloseConnection();

            return sum;
        }

        public async Task<int> getAnualUnitsSold(int id,int yr)
        {
            int unitsSold = 0;
            string sql = @"SELECT SUM(sales_qty) As 'AnualConsumption' FROM salespharma 
                        INNER JOIN invoice ON salespharma.`invoice_id` = invoice.`invoice_id` 
                        WHERE year(invoice.date_invoice) = @yr AND 
                        salespharma.`item_id` = @id";

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@yr", yr));
            list.Add(new MySqlParameter("@id", id));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, list);


            while (await reader.ReadAsync())
            {
                unitsSold = reader["AnualConsumption"].ToString() == "" ? 0 : int.Parse(reader["AnualConsumption"].ToString());
            }
            crud.CloseConnection();

            return unitsSold;
        }
    }
}
