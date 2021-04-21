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

        public async Task save(int type,string path)
        {
            string sql = @"INSERT INTO `daily_sales_report`(`type`, `path`) VALUES (@type,@path)";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@type",type)),
               (new MySqlParameter("@path",path))
            };


            await crud.ExecuteAsync(sql, mySqlParameters);

        }


        public async Task<Dictionary<string,string>> getData()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            string sql = "SELECT * FROM daily_sales_report";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);


            while(await reader.ReadAsync())
            {
                data.Add(reader["dsp_id"].ToString(),
                    reader["report_date"].ToString());
            }

            crud.CloseConnection();


            return data;
        }


        public async Task<bool> isFoundDate(string date)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM daily_sales_report WHERE Date(report_Date) = @date";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                (new MySqlParameter("@date",date))
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
