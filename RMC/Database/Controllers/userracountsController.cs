using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
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
    class UserracountsController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDs()
        {
            string sql = String.Format("SELECT * FROM {0}", useraccount.tableName);
            DataSet dsUserAccounts = new DataSet();
            return  dsUserAccounts = await crud.GetDataSetAsync(sql, null);
        }

        private async void saveUserAccount(Dictionary<string, string> datas)
        {
            string tablename = useraccount.tableName;
            string table_columns = String.Join(",",useraccount.table_columns);
            string values = String.Join(",", useraccount.table_keys);

            string sql = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", tablename, table_columns, values);
            List<MySqlParameter> list = new List<MySqlParameter>();

            foreach (KeyValuePair<string, string> entry in datas)
            {

                list.Add(new MySqlParameter(entry.Key, entry.Value));

            }
               await crud.ExecuteAsync(sql, list);
            Console.WriteLine(sql);
        }


        public async void updateUserAccounts(string firstname,string lname,string mname,int uid)
        {
            string tablename = useraccount.tableName;
            string sql = String.Format(@"UPDATE {0} SET firstname=@firstname,lastname=@lastname, 
                                        middlename=@middlename WHERE u_id = @uid", tablename);
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@firstname", firstname));
            list.Add(new MySqlParameter("@lastname", lname));
            list.Add(new MySqlParameter("@middlename", mname));
            list.Add(new MySqlParameter("@uid", uid));

            await crud.ExecuteAsync(sql, list);
        }

        public async void delete(int uid)
        {
            string tablename = useraccount.tableName;
            string sql = String.Format(@"DELETE FROM {0} WHERE u_id = @uid", tablename);
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@uid", uid));

            await crud.ExecuteAsync(sql, list);
        }

        public void save(params string[] dataInput)
        {
            Dictionary<string, string> datasUserAccounts = new Dictionary<string, string>();
            for(int i = 0; i < dataInput.Length; i++)
            {
                datasUserAccounts[useraccount.table_keys[i]] = dataInput[i];
            }

            saveUserAccount(datasUserAccounts);

        }

        public async void resetPassword(int uid)
        {
            string tablename = useraccount.tableName;
            string newPassword = GeneratePassword(8);
            string sql = String.Format(@"UPDATE {0} SET password = @password, is_change = 0 WHERE u_id = @uid", tablename);
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@uid", uid));
            list.Add(new MySqlParameter("@password", newPassword));

            await crud.ExecuteAsync(sql, list);
        }

        public int getRecentStudentId()
        {
            string sql = String.Format(@"SELECT AUTO_INCREMENT As 'Last_id'
                                        FROM information_schema.tables 
                                        WHERE table_name='useraccounts' 
                                        AND table_schema= DATABASE()");
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader);
            int last_id = 0;
            if (reader.Read())
            {
                last_id = int.Parse(reader["Last_id"].ToString());
            }
            crud.CloseConnection();
            return last_id;
        }


        private string GeneratePassword(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }

        public async Task<DataSet> searchQueryAsync(string sql,string value)
        {
          
            List<MySqlParameter> list = new List<MySqlParameter>();
    
            list.Add(new MySqlParameter("@value", value));
            DataSet dsUserAccounts = new DataSet();
            return dsUserAccounts = await crud.GetDataSetAsync(sql, list);
        }


    }
}
