using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class DailySalesReportController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<string>> getFilesPath(int year,int month, int type)
        {
            List<string> paths = new List<string>();
            string sql = @"SELECT * FROM `daily_sales_report` 
                            WHERE type = @type AND MONTH(report_Date) = @m AND 
                            YEAR(report_Date) = @y";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@type",type)),
               (new MySqlParameter("@y",year)),
                (new MySqlParameter("@m",month))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while(await reader.ReadAsync())
            {
                paths.Add(reader["path"].ToString());
            }

            crud.CloseConnection();
            return paths;
        }

        public async Task save(int type,string path,string date)
        {
            string sql = @"INSERT INTO `daily_sales_report`(`type`, `path`,`report_Date`) VALUES (@type,@path,@date)";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@type",type)),
               (new MySqlParameter("@path",path)),
                (new MySqlParameter("@date",DateTime.Parse(date)))
            };


            await crud.ExecuteAsync(sql, mySqlParameters);

        }


        public async Task<Dictionary<string,string>> getData(int type)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            string sql = "SELECT * FROM daily_sales_report WHERE type = @type";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                  (new MySqlParameter("@type",type))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);


            while(await reader.ReadAsync())
            {
                data.Add(reader["dsp_id"].ToString(),
                    reader["report_date"].ToString());
            }

            crud.CloseConnection();


            return data;
        }

        public async Task<Dictionary<string, string>> getData(string date, int type)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            string sql = @"SELECT * FROM daily_sales_report WHERE Date(report_Date) = @date AND type = @type";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date",date)),
                  (new MySqlParameter("@type",type))
            };


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);


            while (await reader.ReadAsync())
            {
                data.Add(reader["dsp_id"].ToString(),
                    reader["report_date"].ToString());
            }

            crud.CloseConnection();


            return data;
        }


        public async Task<string> getFullPath(int id)
        {
            string sql = @"SELECT * FROM daily_sales_report WHERE dsp_id = @id";
            string path = "";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@id",id))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (await reader.ReadAsync())
            {
                path = reader["path"].ToString();
            }

            crud.CloseConnection();
            return path;
        }

        public async Task<string> findDate(int id)
        {
            string sql = @"SELECT * FROM daily_sales_report WHERE dsp_id = @id";
            string date = "";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@id",id))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if(await reader.ReadAsync())
            {
                date = reader["report_date"].ToString();
            }

            crud.CloseConnection();
            return date;
        }


        public async Task<bool> isFoundDate(string date, int type)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM daily_sales_report WHERE Date(report_Date) = @date AND type = @type";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date",date)),
                (new MySqlParameter("@type",type))
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (reader.HasRows)
            {
                isFound = true;
            }

            crud.CloseConnection();

            return isFound;
        }
    }
}
