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
        public async Task Save(string type, int id)
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

        public async Task<float> getSumInMonth(int m, int y)
        {
            float totalSales = 0;
            string sql = @"SELECT SUM(sales) As 'sales' FROM invoice 
                        WHERE invoice_id in (SELECT invoice_id FROM salesclinic) 
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

        public async Task<float> getTotalTodayConsultation()
        {

            float totalSalesConsulation = 0;
            string sql = @"SELECT (COUNT(sales_clinic_id) * prices_service.price_serv ) AS 'totalConsulation' FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
                            INNER JOIN prices_service ON prices_service.prices_service_id = 1
                            WHERE type_sales = 'Service' AND type_sales_id = 1
                            AND DATE(invoice.date_invoice) = CURDATE()";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            if (await reader.ReadAsync())
            {
                totalSalesConsulation = reader["totalConsulation"].ToString() == "" ? 0 :
                            float.Parse(reader["totalConsulation"].ToString());

            }

            crud.CloseConnection();

            return totalSalesConsulation;
        }

        public async Task<float> getTotalConsultation(string date)
        {
        
            float totalSalesConsulation = 0;
            string sql = @"SELECT (COUNT(sales_clinic_id) * prices_service.price_serv ) AS 'totalConsulation' FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
                            INNER JOIN prices_service ON prices_service.prices_service_id = 1
                            WHERE type_sales = 'Service' AND type_sales_id = 1
                            AND DATE(invoice.date_invoice) = @date";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date",date))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (await reader.ReadAsync())
            {
                totalSalesConsulation = reader["totalConsulation"].ToString() == "" ? 0 :
                            float.Parse(reader["totalConsulation"].ToString());

            }

            crud.CloseConnection();

            return totalSalesConsulation;
        }

        public async Task<float> getMedCertTotalToday()
        {

            //TODO
            float totalMedCert = 0;
            string sql = @"SELECT (COUNT(sales_clinic_id) * prices_service.price_serv ) AS 'medCert' FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
                            INNER JOIN prices_service ON prices_service.prices_service_id = 2
                            WHERE type_sales = 'Service' AND type_sales_id = 2
                            AND DATE(invoice.date_invoice) = CURDATE()";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            if (await reader.ReadAsync())
            {
                totalMedCert = reader["medCert"].ToString() == "" ? 0 :
                            float.Parse(reader["medCert"].ToString());

            }

            crud.CloseConnection();

            return totalMedCert;
        }

        public async Task<float> getMedCertTotal(string date)
        {

            //TODO
            float totalMedCert = 0;
            string sql = @"SELECT (COUNT(sales_clinic_id) * prices_service.price_serv ) AS 'medCert' FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
                            INNER JOIN prices_service ON prices_service.prices_service_id = 2
                            WHERE type_sales = 'Service' AND type_sales_id = 2
                            AND DATE(invoice.date_invoice) = @date";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date",date))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (await reader.ReadAsync())
            {
                totalMedCert = reader["medCert"].ToString() == "" ? 0 :
                            float.Parse(reader["medCert"].ToString());

            }

            crud.CloseConnection();

            return totalMedCert;
        }


        public async Task<float> getTotalTodayLaboratory()
        {

            float totalSalesConsulation = 0;
            string sql = @"SELECT SUM(laboratorylist.price_lab) as totalLab FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
							INNER JOIN laboratorylist ON laboratorylist.laboratory_id = salesclinic.type_sales_id
                            WHERE type_sales = 'Laboratory' 
                            AND DATE(invoice.date_invoice) = CURDATE()";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            if (await reader.ReadAsync())
            {
                totalSalesConsulation = reader["totalLab"].ToString() == "" ? 0 :
                            float.Parse(reader["totalLab"].ToString());

            }

            crud.CloseConnection();

            return totalSalesConsulation;
        }

        public async Task<float> getTotalLaboratory(string date)
        {

            float totalSalesConsulation = 0;
            string sql = @"SELECT SUM(laboratorylist.price_lab) as totalLab FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
							INNER JOIN laboratorylist ON laboratorylist.laboratory_id = salesclinic.type_sales_id
                            WHERE type_sales = 'Laboratory' 
                            AND DATE(invoice.date_invoice) = @date";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date",date))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (await reader.ReadAsync())
            {
                totalSalesConsulation = reader["totalLab"].ToString() == "" ? 0 :
                            float.Parse(reader["totalLab"].ToString());

            }

            crud.CloseConnection();

            return totalSalesConsulation;
        }


        public async Task<float> getTotalTodayPackages()
        {

            float totalPackages = 0;
            string sql = @"SELECT SUM(packages.package_price) as totalPackages FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
							INNER JOIN packages ON packages.package_id = salesclinic.type_sales_id
                            WHERE type_sales = 'Packages' 
                            AND DATE(invoice.date_invoice) = CURDATE()";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            if (await reader.ReadAsync())
            {
                totalPackages = reader["totalPackages"].ToString() == "" ? 0 :
                            float.Parse(reader["totalPackages"].ToString());

            }

            crud.CloseConnection();

            return totalPackages;
        }

        public async Task<float> getTotalPackages(string date)
        {

            float totalPackages = 0;
            string sql = @"SELECT SUM(packages.package_price) as totalPackages FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
							INNER JOIN packages ON packages.package_id = salesclinic.type_sales_id
                            WHERE type_sales = 'Packages' 
                            AND DATE(invoice.date_invoice) = @date";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date",date))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (await reader.ReadAsync())
            {
                totalPackages = reader["totalPackages"].ToString() == "" ? 0 :
                            float.Parse(reader["totalPackages"].ToString());

            }

            crud.CloseConnection();

            return totalPackages;
        }


        public async Task<float> getTotalTodayOtherServices()
        {

            float totalPackages = 0;
            string sql = @"SELECT SUM(service.price) AS totalOthers FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
							INNER JOIN service ON service.service_id = salesclinic.type_sales_id
                            WHERE type_sales = 'OtherServices' 
                            AND DATE(invoice.date_invoice) = CURDATE()";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            if (await reader.ReadAsync())
            {
                totalPackages = reader["totalOthers"].ToString() == "" ? 0 :
                            float.Parse(reader["totalOthers"].ToString());

            }

            crud.CloseConnection();

            return totalPackages;
        }


        public async Task<float> getTotalOtherServices(string date)
        {

            float totalPackages = 0;
            string sql = @"SELECT SUM(service.price) AS totalOthers FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
							INNER JOIN service ON service.service_id = salesclinic.type_sales_id
                            WHERE type_sales = 'OtherServices' 
                            AND DATE(invoice.date_invoice) = @date";


            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date",date))
            };



            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (await reader.ReadAsync())
            {
                totalPackages = reader["totalOthers"].ToString() == "" ? 0 :
                            float.Parse(reader["totalOthers"].ToString());

            }

            crud.CloseConnection();

            return totalPackages;
        }

        public async Task<float> getTotalTodayXray()
        {

            float totalSalesConsulation = 0;
            string sql = @"SELECT SUM(xraylist.xray_price) as 'totalSales' FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
							INNER JOIN xraylist ON xraylist.xray_id = salesclinic.type_sales_id
                            WHERE type_sales = 'Radio' 
                            AND DATE(invoice.date_invoice) = CURDATE()";



            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            if (await reader.ReadAsync())
            {
                totalSalesConsulation = reader["totalSales"].ToString() == "" ? 0 :
                            float.Parse(reader["totalSales"].ToString());

            }

            crud.CloseConnection();

            return totalSalesConsulation;
        }


        public async Task<float> getTotalXray(string date)
        {

            float totalSalesConsulation = 0;
            string sql = @"SELECT SUM(xraylist.xray_price) as 'totalSales' FROM `salesclinic` 
                            INNER JOIN invoice on salesclinic.invoice_id = invoice.invoice_id
							INNER JOIN xraylist ON xraylist.xray_id = salesclinic.type_sales_id
                            WHERE type_sales = 'Radio' 
                            AND DATE(invoice.date_invoice) = @date";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date",date))
            };


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (await reader.ReadAsync())
            {
                totalSalesConsulation = reader["totalSales"].ToString() == "" ? 0 :
                            float.Parse(reader["totalSales"].ToString());

            }

            crud.CloseConnection();

            return totalSalesConsulation;
        }


    }
}
