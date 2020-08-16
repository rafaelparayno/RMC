using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMC.Database.Controllers
{
    class LoginController
    {
        dbcrud crud = new dbcrud();
        

        public int login(string username,string pass)
        {
            int roleid = 0;
            if (username == "")
            {
            
                return 0;
            }
            if (pass == "")
            {
          
                return 0;
            }

            string sql = String.Format(@"SELECT * FROM useraccounts  WHERE  Binary Username= @user and Binary Password = @pass", username, pass);
            List<MySqlParameter> listparam = new List<MySqlParameter>();
            listparam.Add(new MySqlParameter("@user", username));
            listparam.Add(new MySqlParameter("@pass", pass));

           
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, listparam);
            if (reader.Read())
            {
        
                roleid = int.Parse(reader["role_id"].ToString());
                int accid = int.Parse(reader["u_id"].ToString());
                int ischange = int.Parse(reader["is_change"].ToString());
                UserLog user = new UserLog(reader["firstname"].ToString(),
                                           reader["firstname"].ToString(), reader["firstname"].ToString(), 
                                           roleid, reader["username"].ToString(),
                                            accid, ischange); 
            }
            crud.CloseConnection();
            return roleid;
        }
    }
}
