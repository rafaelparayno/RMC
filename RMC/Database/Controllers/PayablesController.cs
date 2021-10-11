using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PayablesController
    {
        dbcrud crud = new dbcrud();

        public async Task Save(float amount,string in_no,string payable_due,int s_id)
        {
            string sql;
            List<MySqlParameter> list = new List<MySqlParameter>();

            sql = @"INSERT INTO `payables`(`payables_amount`, `invoice_no`, `payable_due`, `supplier_id`) VALUES
                            (@amount,@inno,@payable_due,@s_id)";

            list.Add(new MySqlParameter("@amount", amount));
            list.Add(new MySqlParameter("@inno", in_no));
            list.Add(new MySqlParameter("@payable_due", DateTime.Parse(payable_due)));
            list.Add(new MySqlParameter("@s_id", s_id));


            await crud.ExecuteAsync(sql, list);
        }
    }
}
