using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class TransferLogsController
    {
        dbcrud crud = new dbcrud();

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
                        qty_transfer,from_to,places_transfer.places_transfer_name,
                        date_transfer,CONCAT(useraccounts.firstname,' ',useraccounts.lastname) AS 'transferBy',
                        (SELECT CONCAT(useraccounts.firstname,' ',useraccounts.lastname) 
                        FROM useraccounts WHERE transferothers_logs.edit_by_id = useraccounts.u_id) AS 'Edit By',
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
    }
}
