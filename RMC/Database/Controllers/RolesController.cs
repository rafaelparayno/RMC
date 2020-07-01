using MySql.Data.MySqlClient;
using RMC.Components;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Database.Controllers
{
    class RolesController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getDs()
        {
            string sql = String.Format("SELECT Position FROM {0}", role.tableName);
            DataSet dgRoles = new DataSet();
            return dgRoles = await crud.GetDataSetAsync(sql, null);
        }

        public async void saveRoles(string rolename)
        {
            string tablename = role.tableName;
        
            string sql = String.Format("INSERT INTO {0} (Position) VALUES (@Position)", tablename);
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@Position", rolename));
          
            await crud.ExecuteAsync(sql, list);
        }


        public async void updateRoles(int role_id,string posname)
        {
            string tablename = role.tableName;
            string sql = String.Format("UPDATE {0} SET Position = @Position WHERE role_id = @roleid", tablename);

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@roleid", role_id));
            list.Add(new MySqlParameter("@Position", posname));

            await crud.ExecuteAsync(sql, list);
        }



        public async void delete(string pos)
        {
            string tablename = role.tableName;
            string sql = String.Format(@"DELETE FROM {0} WHERE Position = @position", tablename);
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@position", pos));

            await crud.ExecuteAsync(sql, list);
        }


        public bool hasName(string posname)
        {
            bool hasName = false;
            string sql = String.Format(@"SELECT * FROM {0} WHERE Position = '{1}'", role.tableName,posname);
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader,null);

            if (reader.HasRows)
            {
                hasName = true;
            }
            crud.CloseConnection();


            return hasName;
        }

        public void SetCombo(ref ComboBox cb)
        {
            string sql = String.Format(@"SELECT * FROM {0} ", role.tableName);
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader,null);

            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            while (reader.Read())
            {
                cbItems.Add(new ComboBoxItem(reader["Position"].ToString(), int.Parse(reader["role_id"].ToString())));
                
            }
            cb.Items.AddRange(cbItems.ToArray());
            crud.CloseConnection();
        }




        public int getRoleId(string posname)
        {
            int id = 0;
            string sql = String.Format(@"SELECT * FROM {0} WHERE Position = '{1}'", role.tableName, posname);
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader,null);

            if (reader.Read())
            {
                id = int.Parse(reader["role_id"].ToString());
            }
            crud.CloseConnection();
            return id;
        }

     
    }
}
