using MySql.Data.MySqlClient;
using RMC.Components;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class AutoDocsController
    {
        dbcrud crud = new dbcrud();

        public async void save(string name,string path)
        {
            string sql = @"INSERT INTO auto_docs (filename,path) VALUES(@name,@path)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@name", name));
            listparams.Add(new MySqlParameter("@path", path));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<List<ComboBoxItem>> getComboDatas()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = String.Format(@"SELECT * FROM auto_docs");

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["filename"].ToString(),
                    int.Parse(reader["auto_docs_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }
    }
}
