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
    class ReceivableTransferController
    {
        dbcrud crud = new dbcrud();


        public async Task<List<ReceivableTransferModel>> getModel()
        {

            List<ReceivableTransferModel> receivableTransferModels = new List<ReceivableTransferModel>();
            string sql = @"SELECT rdt_id,totalamount_rdt,invoice_no,date_transfer,isPaid,check_no_rdt,check_date,due_date,receivable_details_transfer.places_transfer_id,places_transfer.places_transfer_name FROM `receivable_details_transfer`
                            LEFT JOIN places_transfer ON receivable_details_transfer.places_transfer_id = places_transfer.places_transfer_id
                            WHERE receivable_details_transfer.places_transfer_id !=0 ORDER BY due_date DESC ";

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);

            while(await reader.ReadAsync())
            {
                ReceivableTransferModel r = new ReceivableTransferModel();
                r.id = int.Parse(reader["rdt_id"].ToString());
                r.amount = float.Parse(reader["totalamount_rdt"].ToString());
                r.invoice = reader["invoice_no"].ToString();
                r.dateTransfer = reader["date_transfer"].ToString();
                r.isPaid = int.Parse(reader["isPaid"].ToString());
                r.checkNo = reader["check_no_rdt"].ToString();
                r.checkDate = reader["check_date"].ToString();
                r.dueDate = reader["due_date"].ToString();
                r.pid = int.Parse(reader["places_transfer_id"].ToString());
                r.namep = reader["places_transfer_name"].ToString();

                receivableTransferModels.Add(r);
            }

            crud.CloseConnection();

            return receivableTransferModels;

        }

        public async Task<List<ReceivableTransferModel>> getModel(int id)
        {

            List<ReceivableTransferModel> receivableTransferModels = new List<ReceivableTransferModel>();
            string sql = @"SELECT rdt_id,totalamount_rdt,invoice_no,date_transfer,isPaid,check_no_rdt,
                            check_date,due_date,receivable_details_transfer.places_transfer_id,places_transfer.places_transfer_name 
                            FROM `receivable_details_transfer`
                            LEFT JOIN places_transfer ON receivable_details_transfer.places_transfer_id = places_transfer.places_transfer_id
                            WHERE receivable_details_transfer.places_transfer_id = @id 
                            ORDER BY due_date DESC";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while (await reader.ReadAsync())
            {
                ReceivableTransferModel r = new ReceivableTransferModel();
                r.id = int.Parse(reader["rdt_id"].ToString());
                r.amount = float.Parse(reader["totalamount_rdt"].ToString());
                r.invoice = reader["invoice_no"].ToString();
                r.dateTransfer = reader["date_transfer"].ToString();
                r.isPaid = int.Parse(reader["isPaid"].ToString());
                r.checkNo = reader["check_no_rdt"].ToString();
                r.checkDate = reader["check_date"].ToString();
                r.dueDate = reader["due_date"].ToString();
                r.pid = int.Parse(reader["places_transfer_id"].ToString());
                r.namep = reader["places_transfer_name"].ToString();

                receivableTransferModels.Add(r);
            }

            crud.CloseConnection();

            return receivableTransferModels;

        }


        public async Task<List<ReceivableTransferModel>> getModel(int id,int m,int y)
        {

            List<ReceivableTransferModel> receivableTransferModels = new List<ReceivableTransferModel>();
            string sql = @"SELECT rdt_id,totalamount_rdt,invoice_no,date_transfer,isPaid,check_no_rdt,check_date,due_date,receivable_details_transfer.places_transfer_id,places_transfer.places_transfer_name FROM `receivable_details_transfer`
                            LEFT JOIN places_transfer ON receivable_details_transfer.places_transfer_id = places_transfer.places_transfer_id
                            WHERE receivable_details_transfer.places_transfer_id = @id 
                            AND month(due_date) = @m AND year(due_date) = @y
                            ORDER BY due_date DESC ";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@id", id));
            mySqlParameters.Add(new MySqlParameter("@m", m));
            mySqlParameters.Add(new MySqlParameter("@y", y));


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while (await reader.ReadAsync())
            {
                ReceivableTransferModel r = new ReceivableTransferModel();
                r.id = int.Parse(reader["rdt_id"].ToString());
                r.amount = float.Parse(reader["totalamount_rdt"].ToString());
                r.invoice = reader["invoice_no"].ToString();
                r.dateTransfer = reader["date_transfer"].ToString();
                r.isPaid = int.Parse(reader["isPaid"].ToString());
                r.checkNo = reader["check_no_rdt"].ToString();
                r.checkDate = reader["check_date"].ToString();
                r.dueDate = reader["due_date"].ToString();
                r.pid = int.Parse(reader["places_transfer_id"].ToString());
                r.namep = reader["places_transfer_name"].ToString();

                receivableTransferModels.Add(r);
            }

            crud.CloseConnection();

            return receivableTransferModels;

        }

        public async Task<bool> foundInvoice(string invoiceno)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM receivable_details_transfer WHERE invoice_no = @invo";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
                new MySqlParameter("@invo",invoiceno)
            };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (reader.HasRows)
            {
                isFound = true;
            }

            crud.CloseConnection();

            return isFound;
        }

        public async Task saveData(float totalAmount, string invo,string dateT,
                                int isPaid, string checkNo,string checkDate,string dueDate,int tid)
        {
            string sql = @"INSERT INTO `receivable_details_transfer`(`totalamount_rdt`, 
                            `invoice_no`, `date_transfer`, `isPaid`, `check_no_rdt`, 
                                    `check_date`, `due_date`, `places_transfer_id`) 
                            VALUES (@total,@invo,@dateTransfer,@ispaid,@checkno,@cDate,@dDate,@tid)";



            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@total",totalAmount));
            mySqlParameters.Add(new MySqlParameter("@invo", invo));
            mySqlParameters.Add(new MySqlParameter("@dateTransfer", dateT));
            mySqlParameters.Add(new MySqlParameter("@ispaid", isPaid));
            mySqlParameters.Add(new MySqlParameter("@checkno", checkNo));
            mySqlParameters.Add(new MySqlParameter("@cDate", checkDate == "" ? null : checkDate));
            mySqlParameters.Add(new MySqlParameter("@dDate", dueDate == "" ? null : dueDate));
            mySqlParameters.Add(new MySqlParameter("@tid", tid));


            await crud.ExecuteAsync(sql, mySqlParameters);
        }
    }
}
