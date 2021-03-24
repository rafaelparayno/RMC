using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class UserracountsController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDs()
        {
            string sql = String.Format(@"SELECT u_id,firstname,middlename,lastname,username,password,Position,is_change FROM `{0}` 
                                            LEFT JOIN  roles ON roles.role_id = useraccounts.role_id WHERE Position != 'SuperAdmin'", useraccount.tableName);
            DataSet dsUserAccounts = new DataSet();
            return  dsUserAccounts = await crud.GetDataSetAsync(sql, null);
        }

        public string[] getDataByUsername(string username)
        {
            string[] datas = new string[8];
            string sql = String.Format("SELECT * FROM {0} WHERE username = @username", useraccount.tableName);
            List<MySqlParameter> list = new List<MySqlParameter>();
       
            list.Add(new MySqlParameter("@username", username));
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, list);

            if (reader.Read())
            {
                datas[0] = reader[0].ToString();
                datas[1] = reader[1].ToString();
                datas[2] = reader[2].ToString();
                datas[3] = reader[3].ToString();
                datas[4] = reader[4].ToString();
                datas[5] = reader[5].ToString();
                datas[6] = reader[6].ToString();
                datas[7] = reader[7].ToString();
            }


            crud.CloseConnection();

            return datas;
        }

        public async Task<List<string>> listDoctorOnlines()
        {
            List<string> listDoctors = new List<string>();

            string Sql = @"SELECT * FROM useraccounts WHERE isOnline = 1 AND role_id in(SELECT role_id FROM access WHERE access = 5)";

            DbDataReader reader = await crud.RetrieveRecordsAsync(Sql, null);

            while (await reader.ReadAsync())
            {
                string uid = reader["u_id"].ToString();
                string fn = reader["firstname"].ToString();
                string ln = reader["lastname"].ToString();

                listDoctors.Add($"ID-{uid}-{fn} {ln}");

            }

            crud.CloseConnection();
            return listDoctors;
        }

        public async Task<Dictionary<int,string>> listDoctorOnlinesDic()
        {
            Dictionary<int, string> listDoctors = new Dictionary<int, string>();

            string Sql = @"SELECT * FROM useraccounts WHERE isOnline = 1 AND role_id in(SELECT role_id FROM access WHERE access = 5)";

            DbDataReader reader = await crud.RetrieveRecordsAsync(Sql, null);

            while (await reader.ReadAsync())
            {
                int uid =int.Parse(reader["u_id"].ToString());
                string fn = reader["firstname"].ToString();
                string ln = reader["lastname"].ToString();


                listDoctors.Add(uid, fn + " " + ln);

            }

            crud.CloseConnection();
            return listDoctors;
        }

        public async void saveUserAccount(params string[] dataInput)
        {
            string tablename = useraccount.tableName;
            string table_columns = String.Join(",",useraccount.table_columns);
            string sql = String.Format(@"INSERT INTO {0} ({1}) VALUES (@firstname,@middlename,@lastname,@username,@password,@is_change,@role_id,@isOnline)", tablename, table_columns);
            List<MySqlParameter> list = new List<MySqlParameter>();

            list.Add(new MySqlParameter(useraccount.table_keys[0], dataInput[0]));
            list.Add(new MySqlParameter(useraccount.table_keys[1], dataInput[1]));
            list.Add(new MySqlParameter(useraccount.table_keys[2], dataInput[2]));
            list.Add(new MySqlParameter(useraccount.table_keys[3], dataInput[3]));
            list.Add(new MySqlParameter(useraccount.table_keys[4], dataInput[4]));
            list.Add(new MySqlParameter(useraccount.table_keys[5], int.Parse(dataInput[5])));
            list.Add(new MySqlParameter(useraccount.table_keys[6], int.Parse(dataInput[6])));
            list.Add(new MySqlParameter(useraccount.table_keys[7], int.Parse(dataInput[7])));
            await crud.ExecuteAsync(sql, list);
           
        }


        public async void updateUserAccounts(string firstname,string lname,string mname,int roleid,int uid)
        {
            string tablename = useraccount.tableName;
            string sql = String.Format(@"UPDATE {0} SET firstname=@firstname,lastname=@lastname, 
                                        middlename=@middlename,role_id = @roleid WHERE u_id = @uid", tablename);
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@firstname", firstname));
            list.Add(new MySqlParameter("@lastname", lname));
            list.Add(new MySqlParameter("@middlename", mname));
            list.Add(new MySqlParameter("@roleid", roleid));
            list.Add(new MySqlParameter("@uid", uid));

            await crud.ExecuteAsync(sql, list);
        }


        public async void assignRole(int uid,int roleid)
        {
            string tablename = useraccount.tableName;
            string sql = String.Format(@"UPDATE {0} SET role_id=@roleid 
                                        WHERE u_id = @uid", tablename);
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@uid", uid));
            list.Add(new MySqlParameter("@roleid", roleid));

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


        public async void changePassword(int uid,string newPass)
        {
            string tablename = useraccount.tableName;
            string newPassword = newPass;
            string sql = String.Format(@"UPDATE {0} SET password = @password, is_change = 1 WHERE u_id = @uid", tablename);
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@uid", uid));
            list.Add(new MySqlParameter("@password", newPassword));

            await crud.ExecuteAsync(sql, list);
        }

        public int getRecentUserID()
        {
            string sql = String.Format(@"SELECT AUTO_INCREMENT As 'Last_id'
                                        FROM information_schema.tables 
                                        WHERE table_name='useraccounts' 
                                        AND table_schema= DATABASE()");
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader,null);
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

        public async Task updateStatus(int value,int uid)
        {
            string sql = @"UPDATE useraccounts SET isOnline = @isol WHERE u_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@isol", value));
            listparams.Add(new MySqlParameter("@id", uid));

            await crud.ExecuteAsync(sql, listparams);
            
        }


    }
}
