using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PlacesTransferController
    {
        dbcrud crud = new dbcrud();


        public async Task<DataSet> getDataset()
        {
            string sql = @"SELECT * FROM `places_transfer`";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task save(string name)
        {
            string sql = @"INSERT INTO `places_transfer`(`places_transfer_name`) VALUES (@name)";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@name", name)) };

            await crud.ExecuteAsync(sql, mySqlParameters);
        }

        public async Task edit(string name,int id)
        {
            string sql = @"UPDATE `places_transfer` 
                        SET `places_transfer_name`= @name
                        WHERE places_transfer_id = @Id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() 
            { 
                (new MySqlParameter("@name", name)),
                (new MySqlParameter("@Id", id))
            };

            await crud.ExecuteAsync(sql, mySqlParameters);
        }

        public async Task<bool> isFound(string name)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM places_transfer WHERE places_transfer_name = @name";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@name", name)) };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);


            if (reader.HasRows)
            {
                isFound = true;
            }

            crud.CloseConnection();
            return isFound;
        }

        public async Task delete(int id)
        {
            string sql = @"DELETE FROM places_transfer WHERE places_transfer_id = @id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>()
            {
               
                (new MySqlParameter("@Id", id))
            };

            await crud.ExecuteAsync(sql, mySqlParameters);
        }
    }
}
