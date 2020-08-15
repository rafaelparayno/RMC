using MySql.Data.MySqlClient;
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
    class PoController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<PoModel>> getPoNo(int po_no)
        {
            List<PoModel> purchaseOrder = new List<PoModel>();


            string sql = @"SELECT itemlist.item_id,itemlist.item_name,quantity_order,suppliers.supplier_name 
                          FROM `purchase_order` LEFT JOIN itemlist ON purchase_order.item_id = itemlist.item_id 
                          LEFT JOIN suppliers ON purchase_order.supplier_id = suppliers.supplier_id WHERE po_no = @po_no";

            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@po_no", po_no));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);
            while(await reader.ReadAsync())
            {
                PoModel newPo = new PoModel();
                newPo.item_id = int.Parse(reader["item_id"].ToString());
                newPo.item_name = reader["item_name"].ToString();
                newPo.quantity_order = int.Parse(reader["quantity_order"].ToString());
                purchaseOrder.Add(newPo);
            }

            crud.CloseConnection();
            return purchaseOrder;
        }

        public async Task<DataSet> getDsOfPono(int po_no)
        {
            string sql = @"SELECT itemlist.item_id,itemlist.item_name,quantity_order,suppliers.supplier_name 
                          FROM `purchase_order` LEFT JOIN itemlist ON purchase_order.item_id = itemlist.item_id 
                          LEFT JOIN suppliers ON purchase_order.supplier_id = suppliers.supplier_id WHERE po_no = @po_no";

            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@po_no", po_no));

           return await crud.GetDataSetAsync(sql, listParams);
        }
        public async void save(int supplierid, int uid)
        {
            List<MySqlParameter> list = new List<MySqlParameter>();

            string sql = @"INSERT INTO `purchase_order` (supplier_id,u_id) 
                            VALUES (@sid,@uid)";

            
            list.Add(new MySqlParameter("@sid", supplierid));
            list.Add(new MySqlParameter("@uid", uid));

            await crud.ExecuteAsync(sql, list);
        }

        public int getLastPoNo()
        {
            string sql = String.Format(@"SELECT AUTO_INCREMENT As 'Last_id'
                                        FROM information_schema.tables 
                                        WHERE table_name='purchase_order' 
                                        AND table_schema= DATABASE()");
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql, ref reader, null);
            int last_id = 0;
            if (reader.Read())
            {
                last_id = int.Parse(reader["Last_id"].ToString());
            }
            crud.CloseConnection();

            return last_id;
        }

        public async Task<List<string>> getPoActive()
        {
            List<string> poActive = new List<string>();
            string sql = @"SELECT DISTINCT(purchase_order.po_id) FROM `purchase_order` 
                            LEFT JOIN purchase_order_items ON purchase_order.po_id =  purchase_order_items.po_id
                            WHERE quantity_order > 0";


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while (await reader.ReadAsync())
            {
                poActive.Add("PO# " +reader["po_id"].ToString());
            }
            crud.CloseConnection();
            return poActive;
        }
    }
}
