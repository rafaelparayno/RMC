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
    class TransferLogsController
    {
        dbcrud crud = new dbcrud();


        public async Task<float> getTotalOut(int year, int month)
        {
            float totalOut = 0;
            string sql = @"SELECT SUM((UnitPrice * transferothers_logs.qty_transfer)) As 'totalOut' FROM itemlist 
            INNER JOIN transferothers_logs ON itemlist.item_id = transferothers_logs.item_id
            WHERE MONTH(transferothers_logs.date_transfer) = @m AND YEAR(transferothers_logs.date_transfer) = @y";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@y", year));
            mySqlParameters.Add(new MySqlParameter("@m", month));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);
            
            while(await reader.ReadAsync())
            {
                totalOut = string.IsNullOrEmpty(reader["totalOut"].ToString()) ? 
                    0 : float.Parse(reader["totalOut"].ToString());
            }

            crud.CloseConnection();
            return totalOut;
        }

        public async Task<DataSet> getDataset()
        {
            string sql = @"SELECT transferothers_logs.transferothers_logs_id AS 'ID',itemlist.item_name,
                         from_to,qty_transfer,places_transfer.places_transfer_name,
                        date_transfer,CONCAT(useraccounts.firstname,' ',useraccounts.lastname) AS 'transferBy',
                        (SELECT CONCAT(useraccounts.firstname,' ',useraccounts.lastname) FROM useraccounts WHERE transferothers_logs.edit_by_id = useraccounts.u_id) AS 'Edit By',
                        edit_save_date
                        FROM `transferothers_logs` 
                         INNER JOIN itemlist ON transferothers_logs.item_id = itemlist.item_id
                         INNER JOIN places_transfer ON transferothers_logs.places_transfer_id = places_transfer.places_transfer_id
                         INNER JOIN useraccounts ON transferothers_logs.save_by_id = useraccounts.u_id 
                            ORDER BY transferothers_logs_id ASC";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task<DataSet> getDataset(string Date)
        {
            string sql = @"SELECT transferothers_logs.transferothers_logs_id AS 'ID',itemlist.item_name,
                         from_to,qty_transfer,places_transfer.places_transfer_name,
                        date_transfer,CONCAT(useraccounts.firstname,' ',useraccounts.lastname) AS 'transferBy',
                        (SELECT CONCAT(useraccounts.firstname,' ',useraccounts.lastname) FROM useraccounts WHERE transferothers_logs.edit_by_id = useraccounts.u_id) AS 'Edit By',
                        edit_save_date
                        FROM `transferothers_logs` 
                         INNER JOIN itemlist ON transferothers_logs.item_id = itemlist.item_id
                         INNER JOIN places_transfer ON transferothers_logs.places_transfer_id = places_transfer.places_transfer_id
                         INNER JOIN useraccounts ON transferothers_logs.save_by_id = useraccounts.u_id 
                            WHERE Date(date_transfer) = @date 
                                ORDER BY transferothers_logs_id ASC";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@date",Date))};

            return await crud.GetDataSetAsync(sql, mySqlParameters);
        }


        public async Task<TransferLogsModel> getModel(int editId)
        {
            TransferLogsModel transferLogsModel = new TransferLogsModel();

            string sql = @"SELECT transferothers_logs.transferothers_logs_id,transferothers_logs.item_id,
                        transferothers_logs.from_to,qty_transfer,transferothers_logs.places_transfer_id,date_transfer,
                        save_by_id,itemlist.item_name,places_transfer.places_transfer_name FROM transferothers_logs
                        INNER JOIN itemlist ON transferothers_logs.item_id = itemlist.item_id
                        INNER JOIN places_transfer ON transferothers_logs.places_transfer_id = places_transfer.places_transfer_id
                         WHERE transferothers_logs_id = @id";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            { (new MySqlParameter("@id", editId)) };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);
           
            while(await reader.ReadAsync())
            {
                transferLogsModel.id = int.Parse(reader["transferothers_logs_id"].ToString());
                transferLogsModel.itemid = int.Parse(reader["item_id"].ToString());
                transferLogsModel.itemName = reader["item_name"].ToString();
                transferLogsModel.fromTo = int.Parse(reader["from_to"].ToString());
                transferLogsModel.qtyTransfer = int.Parse(reader["qty_transfer"].ToString());
           
                transferLogsModel.transferid = int.Parse(reader["places_transfer_id"].ToString());
                transferLogsModel.transferName = reader["places_transfer_name"].ToString();
                transferLogsModel.date_transfer = DateTime.Parse(reader["date_transfer"].ToString());
                transferLogsModel.transferBy = int.Parse(reader["save_by_id"].ToString());
                
            }

            crud.CloseConnection();

            return transferLogsModel;

        }

        public async Task save(params int [] data)
        {
            string sql = @"INSERT INTO `transferothers_logs`( 
                    `item_id`, `qty_transfer`, `places_transfer_id`, 
                    `save_by_id`,from_to)  VALUES 
                    (@itemid,@qty,@place,@userid,@from)";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@itemid", data[0]));
            mySqlParameters.Add(new MySqlParameter("@qty", data[1]));
            mySqlParameters.Add(new MySqlParameter("@place", data[2]));
            mySqlParameters.Add(new MySqlParameter("@userid", data[3]));
            mySqlParameters.Add(new MySqlParameter("@from", data[4]));


            await crud.ExecuteAsync(sql, mySqlParameters);
        }



        public async Task update(params int[] data)
        {
            string sql = @"UPDATE transferothers_logs SET qty_transfer = @qty, 
                            places_transfer_id = @place, edit_by_id = @uid, 
                            edit_save_date = @date WHERE transferothers_logs_id = @editid";

            DateTime dateToday = DateTime.Today;
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
         
            mySqlParameters.Add(new MySqlParameter("@qty", data[0]));
            mySqlParameters.Add(new MySqlParameter("@place", data[1]));
            mySqlParameters.Add(new MySqlParameter("@uid", data[2]));
            mySqlParameters.Add(new MySqlParameter("@date", dateToday));
            mySqlParameters.Add(new MySqlParameter("@editid", data[3]));
         

            await crud.ExecuteAsync(sql, mySqlParameters);
        }
    }
}
