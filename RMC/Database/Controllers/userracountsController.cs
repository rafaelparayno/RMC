using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async void saveUserAccount(Dictionary<string, string> datas)
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

    }
}
