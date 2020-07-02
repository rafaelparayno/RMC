using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class AccessController
    {
        dbcrud crud = new dbcrud();


        public List<int> accesses(int role_id)
        {
            //   Stack stackAccess = new Stack();
            List<int> access = new List<int>();
            
            string sql = String.Format(@"SELECT * FROM access WHERE role_id = {0}", role_id);
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, null);
            
            while (reader.Read())
            {
                access.Add(int.Parse(reader["access"].ToString()));
            }
            crud.CloseConnection();

            return access;
        }

        public async void saveAccess(int role_id,List<int> accessList)
        {
            string tableName = access.tableName;

            string sql = String.Format("INSERT INTO {0} (access,role_id) VALUES (@access,@roleid)", tableName);
          
            foreach(int acc in accessList)
            {
                if (hasAlreadyAnAccess(acc, role_id))
                    continue;

                List<MySqlParameter> list = new List<MySqlParameter>();
                list.Add(new MySqlParameter("@access", acc));
                list.Add(new MySqlParameter("@roleid", role_id));
                await crud.ExecuteAsync(sql, list);
            }


        }

        public async void deleteAccess(int role_id,List<int> accessList)
        {
            string tableName = access.tableName;

            string sql = String.Format(@"DELETE FROM {0} WHERE role_id = @roleid AND access = @access",tableName);
            foreach (int acc in accessList)
            {
                List<MySqlParameter> list = new List<MySqlParameter>();
                list.Add(new MySqlParameter("@access", acc));
                list.Add(new MySqlParameter("@roleid", role_id));
                await crud.ExecuteAsync(sql, list);
            }
        }

        private bool hasAlreadyAnAccess(int accessIds,int roleid)
        {
            string tableName = access.tableName;
            bool alreadAccess = false;
            MySqlDataReader reader = null;
            string sql = String.Format(@"SELECT * FROM {0} WHERE role_id = @roleid AND access = @access", tableName);
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@access", accessIds));
            list.Add(new MySqlParameter("@roleid", roleid));
            crud.RetrieveRecords(sql, ref reader, list);

            if (reader.HasRows)
            {
                alreadAccess = true;
            }

            crud.CloseConnection();

            return alreadAccess;
        }
    }
}
