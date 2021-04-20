using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PoItemController
    {
        dbcrud crud = new dbcrud();

        public async Task Save(int itemid,int qty)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"INSERT INTO purchase_order_items (po_id,item_id,quantity_order) VALUES 
                        ((SELECT po_id FROM purchase_order ORDER BY po_id DESC LIMIT 1),@itemid,@qty)";
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@qty", qty));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<List<PoModel>> getPoNo(int po_no)
        {
            List<PoModel> purchaseOrder = new List<PoModel>();


            string sql = @"SELECT itemlist.item_id,itemlist.item_name,quantity_order,suppliers.supplier_name 
                          FROM `purchase_order_items` LEFT JOIN itemlist ON purchase_order_items.item_id = itemlist.item_id 
                          LEFT JOIN purchase_order ON purchase_order_items.po_id = purchase_order.po_id
                          LEFT JOIN suppliers ON purchase_order.supplier_id = suppliers.supplier_id 
                          WHERE purchase_order_items.po_id = @po_id";

            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@po_id", po_no));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);
            while (await reader.ReadAsync())
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


        public async Task<List<PoModel>> getPoNoWithOrigStocks(int po_no)
        {
            List<PoModel> purchaseOrder = new List<PoModel>();


            string sql = @"SELECT purchase_order_items.item_id,itemlist.item_name,COALESCE((purchase_order_items.quantity_order + receive_orders.qty_ro), purchase_order_items.quantity_order) AS 'qty' FROM `purchase_order_items`
                            INNER JOIN itemlist ON purchase_order_items.item_id = itemlist.item_id
                            LEFT JOIN receive_orders ON purchase_order_items.po_item_id = receive_orders.po_item_id
                            WHERE purchase_order_items.po_id = @po_id";

            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@po_id", po_no));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);
            while (await reader.ReadAsync())
            {
                PoModel newPo = new PoModel();
                newPo.item_id = int.Parse(reader["item_id"].ToString());
                newPo.item_name = reader["item_name"].ToString();
                newPo.quantity_order = int.Parse(reader["qty"].ToString());
                purchaseOrder.Add(newPo);
            }

            crud.CloseConnection();
            return purchaseOrder;
        }

        public async Task updateOrderQty(int itemid,int poid,int qty)
        {
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"UPDATE purchase_order_items SET quantity_order = @qty WHERE item_id = @itemid AND po_id = @poid";
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@qty", qty));
            listparams.Add(new MySqlParameter("@poid", poid));

            await crud.ExecuteAsync(sql, listparams);
        }
    }
}
