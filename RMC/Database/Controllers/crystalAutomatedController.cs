using RMC.Components;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class crystalAutomatedController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<ComboBoxItem>> getComboDatas()
        {
            List<ComboBoxItem> cbItems = new List<ComboBoxItem>();
            string sql = String.Format(@"SELECT * FROM `automated_crystal`");

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);
            while (await reader.ReadAsync())
            {
                cbItems.Add(new ComboBoxItem(reader["crystal_name"].ToString(),
                    int.Parse(reader["automated_crystal_id"].ToString())));
            }
            crud.CloseConnection();
            return cbItems;
        }

    }
}
