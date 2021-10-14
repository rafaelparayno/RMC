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
    class ReceiveControllers
    {
        dbcrud crud = new dbcrud();

        public async Task save(int itemid,int qty, int poid,string invoice_no,int isCash,string checkNo,string dateCheck)
        {
            string sql = @"INSERT INTO receive_orders (po_item_id,qty_ro,u_id,invoice_no,isCash,check_no,date_check) VALUES 
                           ((SELECT po_item_id FROM purchase_order_items WHERE item_id = @itemid AND po_id = @poid),
                            @qty,@uid,@no,@isCash,@chno,@date_check)";


            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", itemid));
            listparams.Add(new MySqlParameter("@poid", poid));
            listparams.Add(new MySqlParameter("@qty", qty));
            listparams.Add(new MySqlParameter("@uid", UserLog.getUserId()));
            listparams.Add(new MySqlParameter("@no", invoice_no));
            listparams.Add(new MySqlParameter("@isCash", isCash));
            listparams.Add(new MySqlParameter("@chno", checkNo));
            listparams.Add(new MySqlParameter("@date_check", dateCheck == "" ? null : dateCheck));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task<float> getSumLead(int item_id,int days)
        {
            float leadTime = 0;
            string sql = @"SELECT (SUM(date_ro - date_order)/ COUNT(ro_id)) AS 'AvgLead' FROM `receive_orders` 
                         INNER JOIN purchase_order_items ON receive_orders.po_item_id = purchase_order_items.po_item_id 
                         INNER JOIN purchase_order ON purchase_order_items.po_id = purchase_order.po_id 
                         WHERE receive_orders.po_item_id in 
                                (SELECT po_item_id FROM purchase_order_items 
                                WHERE purchase_order_items.item_id = @item_id)
                         AND date_ro BETWEEN DATE_SUB(NOW() , INTERVAL @days DAY) and NOW()";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@item_id", item_id));
            listparams.Add(new MySqlParameter("@days", days));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);
            
            if(await reader.ReadAsync())
            {
                leadTime = reader["AvgLead"].ToString() == "" ? 0 : float.Parse(reader["AvgLead"].ToString());
            }
            crud.CloseConnection(); 

            return leadTime;
        }

        public async Task<DataSet> getData()
        {
            string sql = @"SELECT itemlist.item_name As 'Item Name',receive_orders.qty_ro AS 'quantity Receive',invoice_no,receive_orders.date_ro AS 'date Receive', 
                        CONCAT(useraccounts.firstname,' ',useraccounts.lastname) AS 'Receive By' 
                        FROM `receive_orders` INNER JOIN purchase_order_items ON receive_orders.po_item_id = purchase_order_items.po_item_id 
                        INNER JOIN itemlist ON purchase_order_items.item_id = itemlist.item_id 
                        INNER JOIN useraccounts ON receive_orders.u_id = useraccounts.u_id 
                        ORDER BY ro_id ASC";


            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<int> getReceive(int id ,string d)
        {
            int totalrec = 0;
            string sql = @"SELECT SUM(qty_ro) AS 'totalRec' FROM `receive_orders` 
                         INNER JOIN purchase_order_items 
                                ON receive_orders.po_item_id = purchase_order_items.po_item_id
                         WHERE receive_orders.po_item_id in 
                                (SELECT po_item_id FROM purchase_order_items 
                                WHERE purchase_order_items.item_id = @id)
                         AND date_ro = @date";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(d)));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);
            while(await reader.ReadAsync())
            {
                totalrec = reader["totalRec"].ToString() == "" ? 0 : int.Parse(reader["totalRec"].ToString());
            }

            crud.CloseConnection();
            return totalrec;
        }

        public async Task<int> getReceive(int id, int year,int m)
        {
            string month = m > 9 ? m.ToString() : $"0{m}";
            string date = $"{year}-{month}-01";
            int totalrec = 0;
            string sql = @"SELECT SUM(qty_ro) AS 'totalRec' FROM `receive_orders` 
                         INNER JOIN purchase_order_items 
                                ON receive_orders.po_item_id = purchase_order_items.po_item_id
                         WHERE receive_orders.po_item_id in 
                                (SELECT po_item_id FROM purchase_order_items 
                                WHERE purchase_order_items.item_id = @id)
                         AND date_ro BETWEEN @d 
                        AND DATE_ADD(CURDATE(),INTERVAL 1 day)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@d", date));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);
            while (await reader.ReadAsync())
            {
                totalrec = reader["totalRec"].ToString() == "" ? 0 : int.Parse(reader["totalRec"].ToString());
            }

            crud.CloseConnection();
            return totalrec;
        }



        public async Task<DataSet> getData(string date)
        {
            string sql = @"SELECT itemlist.item_name As 'Item Name',receive_orders.qty_ro AS 'quantity Receive',invoice_no,receive_orders.date_ro AS 'date Receive', 
                        CONCAT(useraccounts.firstname,' ',useraccounts.lastname) AS 'Receive By' 
                        FROM `receive_orders` INNER JOIN purchase_order_items ON receive_orders.po_item_id = purchase_order_items.po_item_id 
                        INNER JOIN itemlist ON purchase_order_items.item_id = itemlist.item_id 
                        INNER JOIN useraccounts ON receive_orders.u_id = useraccounts.u_id 
                        WHERE date_ro = @date ORDER BY ro_id ASC";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            return await crud.GetDataSetAsync(sql, listparams);
        }


        public async Task<List<ReceivableModel>> getReceiveModel(string invoiceNo)
        {
            List<ReceivableModel> receivableModels = new List<ReceivableModel>();


            string sql = @"SELECT ro_id,itemlist.item_name,itemlist.UnitPrice,
                            itemlist.Description,receive_orders.qty_ro, 
                            receive_orders.invoice_no,qty_ro 
                            FROM receive_orders 
                            INNER JOIN purchase_order_items 
                                    ON receive_orders.po_item_id = purchase_order_items.po_item_id 
                       		INNER JOIN itemlist 
                                    ON purchase_order_items.item_id = itemlist.item_id  
                            WHERE invoice_no = @ino";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();

            mySqlParameters.Add(new MySqlParameter("@ino", invoiceNo));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while(await reader.ReadAsync())
            {
                ReceivableModel m = new ReceivableModel();
                m.id = int.Parse(reader["ro_id"].ToString());
                m.qty_rect = int.Parse(reader["qty_ro"].ToString());
                m.itemName = reader["item_name"].ToString();
                m.invoiceNo = reader["invoice_no"].ToString();
                m.unitPrice = float.Parse(reader["UnitPrice"].ToString());
                m.description = reader["Description"].ToString();

                receivableModels.Add(m);
            }

            crud.CloseConnection();

            return receivableModels;
        }

    }
}
