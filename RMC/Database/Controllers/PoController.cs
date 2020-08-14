using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PoController
    {
        dbcrud crud = new dbcrud();

        public async void save(int itemid,int supplierid, int qtyOrder,int poNO)
        {
            List<MySqlParameter> list = new List<MySqlParameter>();

            string sql = @"INSERT INTO `purchase_order` (po_no,item_id,supplier_id,quantity_order) 
                            VALUES (@pono,@id,@sid,@qty)";

            list.Add(new MySqlParameter("@pono", poNO));
            list.Add(new MySqlParameter("@id", itemid));
            list.Add(new MySqlParameter("@sid", supplierid));
            list.Add(new MySqlParameter("@qty", qtyOrder));

            await crud.ExecuteAsync(sql, list);
        }

        public int getLastPoNo()
        {
            int lastPo = 0;
            string sql = @"SELECT * FROM `purchase_order` ORDER by po_no DESC";
            MySqlDataReader reader = null;
            crud.RetrieveRecords(sql,ref reader,null);

            if (reader.Read())
            {
                lastPo = reader["po_no"].ToString() == "" ? 0 : int.Parse(reader["po_no"].ToString());
            }
            crud.CloseConnection();

            return lastPo;
        }
    }
}
