using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class ReceivableTransferController
    {
        dbcrud crud = new dbcrud();

        public async Task saveData(float totalAmount, string invo,string dateT,
                                int isPaid, string checkNo,string checkDate,string dueDate)
        {
            string sql = @"INSERT INTO `receivable_details_transfer`(`totalamount_rdt`, 
                            `invoice_no`, `date_transfer`, `isPaid`, `check_no_rdt`, `check_date`, `due_date`) 
                            VALUES (@total,@invo,@dateTransfer,@ispaid,@checkno,@cDate,@dDate)";


            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@total",totalAmount));
            mySqlParameters.Add(new MySqlParameter("@invo", invo));
            mySqlParameters.Add(new MySqlParameter("@dateTransfer", dateT));
            mySqlParameters.Add(new MySqlParameter("@ispaid", isPaid));
            mySqlParameters.Add(new MySqlParameter("@checkno", checkNo));
            mySqlParameters.Add(new MySqlParameter("@cDate", checkDate));
            mySqlParameters.Add(new MySqlParameter("@dDate", dueDate));


            await crud.ExecuteAsync(sql, mySqlParameters);
        }
    }
}
