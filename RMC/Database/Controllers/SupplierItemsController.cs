using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
   public class SupplierItemsController
    {
        dbcrud crud = new dbcrud();

        public async Task<Dictionary<string,int>> suppliersInItem(int itemid)
        {
            Dictionary<string, int> supDic = new Dictionary<string, int>();
            
            string sql = String.Format(@"SELECT supplier_item_id, supplier_name,supplier_items.`supplier_id` FROM `supplier_items` 
                                    LEFT JOIn suppliers ON suppliers.supplier_id = supplier_items.supplier_id 
                                    WHERE item_id = @id");
            List<MySqlParameter> listParam = new List<MySqlParameter>();
            listParam.Add(new MySqlParameter("@id", itemid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParam);

            while (await reader.ReadAsync())
            {
                supDic.Add(reader["supplier_name"].ToString(), int.Parse(reader["supplier_id"].ToString()));
            }

            crud.CloseConnection();
            return supDic;

        }


        public async Task Save(int item_id,int supplier_id)
        {
           
            if (hasAlreadyTheSupplier(item_id, supplier_id))
                return;

            string sql = String.Format(@"INSERT INTO supplier_items (item_id,supplier_id) VALUES (@itemid,@supid)");
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@itemid", item_id));
            list.Add(new MySqlParameter("@supid", supplier_id));
            await crud.ExecuteAsync(sql, list);
        }


        public async Task Save(int item_id,List<int> suppliers_id)
        {
            string sql = String.Format(@"INSERT INTO supplier_items (item_id,supplier_id) VALUES (@itemid,@supid)");

            foreach( int supid in suppliers_id)
            {
                if (hasAlreadyTheSupplier(item_id, supid))
                    continue;

                List<MySqlParameter> list = new List<MySqlParameter>();
                list.Add(new MySqlParameter("@itemid", item_id));
                list.Add(new MySqlParameter("@supid", supid));
                await crud.ExecuteAsync(sql, list);
            }
        }



        public async Task Delete(int itemid, List<int> suppliersId)
        {
            string sql = String.Format(@"DELETE FROM supplier_items WHERE item_id = @itemid 
                                        AND  supplier_id = @supplierid");

            foreach (int supid in suppliersId)
            {
                List<MySqlParameter> list = new List<MySqlParameter>();
                list.Add(new MySqlParameter("@itemid", itemid));
                list.Add(new MySqlParameter("@supplierid", supid));
                await crud.ExecuteAsync(sql, list);
            }
        }


        private bool hasAlreadyTheSupplier(int itemid, int supid)
        {
      
            bool alreadAccess = false;
            MySqlDataReader reader = null;
            string sql = String.Format(@"SELECT * FROM supplier_items WHERE item_id = @itemid AND supplier_id = @supid");
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@itemid", itemid));
            list.Add(new MySqlParameter("@supid", supid));
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
