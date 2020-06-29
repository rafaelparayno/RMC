using System;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using RMC.Database.DbSettings;
using System.Threading.Tasks;

namespace RMC.Database
{
    class dbcrud
    {
        string cnString = "";
        MySqlConnection cn;
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter adptr = new MySqlDataAdapter();
        DataSet ds = new DataSet();
        string SERVER = dbConfigFile.FetchDatabaseLocation()[0];
        string USERNAME = dbConfigFile.FetchDatabaseLocation()[1];
        string PASSWORD = dbConfigFile.FetchDatabaseLocation()[2];
        string DATABASE = dbConfigFile.FetchDatabaseLocation()[3];
        //globalVariables global = new globalVariables();
        public dbcrud()
        {
            cnString = String.Format("SERVER={0};Database={1};Uid={2};Pwd={3}", SERVER, DATABASE, USERNAME, PASSWORD);
            cn = new MySqlConnection(cnString);
        }
        public Task<DataSet> GetDataSetAsync(string sSQL)
        {
            return Task.Run(() =>
            {
                try
                {
                    cn.Open();
                    cmd = new MySqlCommand(sSQL, cn);
                    adptr = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    adptr.Fill(ds);
                    cn.Close();
                    return ds;
                }
                catch (Exception e)
                {
                    cn.Close();
                    MessageBox.Show("" + e.Message);
                    return null;
                }

            });
        }


        public async void ExecuteQuery(string sql)
        {
            try
            {
                cn.Open();
                cmd = new MySqlCommand(sql, cn);
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
            }
            cn.Close();
        }

        public async Task<int> ExecuteAsync(string sql)
        {
            try
            {
                await cn.OpenAsync().ConfigureAwait(false);
                cmd = new MySqlCommand(sql, cn);
                //  cn.Close();
                return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);

            }
            catch (Exception e)
            {

                MessageBox.Show("" + e.Message);
                return 0;

            }
            finally
            {
                cn.Close();
            }


        }


        public MySqlDataReader RetrieveRecords(string sql, ref MySqlDataReader reader)
        {
            try
            {
                cn.Open();
                cmd = new MySqlCommand(sql, cn);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e.Message);
                return null;
            }
        }

        public void CloseConnection()
        {
            cn.Close();
        }
    }
}
