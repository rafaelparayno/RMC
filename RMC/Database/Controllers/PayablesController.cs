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
    class PayablesController
    {
        dbcrud crud = new dbcrud();


        public async Task updatePaid(string invoice_no, bool ispaid)
        {
            int paid = ispaid ? 1 : 0;
            List<MySqlParameter> list = new List<MySqlParameter>();
            string sql = @"UPDATE payables SET is_paid = @paid WHERE invoice_no = @invo";

            list.Add(new MySqlParameter("@paid", paid));
            list.Add(new MySqlParameter("@invo", invoice_no));

            await crud.ExecuteAsync(sql, list);
        }

        public async Task<List<PayableModel>> listModel()
        {
            List<PayableModel> payableModels = new List<PayableModel>();

            string sql = @"SELECT * FROM `payables` ORDER BY payable_due ASC";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                PayableModel p = new PayableModel();
                p.id = int.Parse(reader["payables_id"].ToString());
                p.invoice_no = reader["invoice_no"].ToString();
                p.amount = float.Parse(reader["payables_amount"].ToString());
              
                p.isPaid = int.Parse(reader["is_paid"].ToString()) == 0 ? false : true;
                p.payableDue = reader["payable_due"].ToString();


                payableModels.Add(p);
            }

            crud.CloseConnection();

            return payableModels;
        }

        public async Task<PayableModel> getModel(string invoice)
        {
            PayableModel p = new PayableModel();

            string sql = @"SELECT payables.payables_id,invoice_no,payables_amount,payable_due,is_paid,suppliers.supplier_name FROM `payables` 
                                    INNER JOIN suppliers ON payables.supplier_id = suppliers.supplier_id 
                                    WHERE invoice_no = @ino";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@ino",invoice),
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (await reader.ReadAsync())
            {
                
                p.id = int.Parse(reader["payables_id"].ToString());
                p.invoice_no = reader["invoice_no"].ToString();
                p.amount = float.Parse(reader["payables_amount"].ToString());

                p.isPaid = int.Parse(reader["is_paid"].ToString()) == 0 ? false : true;
                p.payableDue = reader["payable_due"].ToString();
                p.supplierName = reader["supplier_name"].ToString();
            }

            crud.CloseConnection();

            return p;
        }


        public async Task<List<PayableModel>> listModel(int id)
        {
            List<PayableModel> payableModels = new List<PayableModel>();

            string sql = @"SELECT * FROM `payables` WHERE supplier_id = @id ORDER BY payable_due ASC";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",id),
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while (await reader.ReadAsync())
            {
                PayableModel p = new PayableModel();
                p.id = int.Parse(reader["payables_id"].ToString());
                p.invoice_no = reader["invoice_no"].ToString();
                p.amount = float.Parse(reader["payables_amount"].ToString());
             
                p.isPaid = int.Parse(reader["is_paid"].ToString()) == 0 ? false : true;
                p.payableDue = reader["payable_due"].ToString();


                payableModels.Add(p);
            }

            crud.CloseConnection();

            return payableModels;
        }


        public async Task<List<PayableModel>> listModel(int id,int m,int y )
        {
            List<PayableModel> payableModels = new List<PayableModel>();

            string sql = @"SELECT * FROM `payables` WHERE supplier_id = @id 
                                AND month(payable_due) = @m AND year(payable_due) = @y 
                            ORDER BY payable_due ASC";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@id",id),                
                new MySqlParameter("@y",y),
                new MySqlParameter("@m",m),
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while (await reader.ReadAsync())
            {
                PayableModel p = new PayableModel();
                p.id = int.Parse(reader["payables_id"].ToString());
                p.invoice_no = reader["invoice_no"].ToString();
                p.amount = float.Parse(reader["payables_amount"].ToString());

                p.isPaid = int.Parse(reader["is_paid"].ToString()) == 0 ? false : true;
                p.payableDue = reader["payable_due"].ToString();


                payableModels.Add(p);
            }

            crud.CloseConnection();

            return payableModels;
        }



        public async Task Save(float amount,string in_no,string payable_due,
            int s_id)
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
