using MySql.Data.MySqlClient;
using RMC.Lab;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PatientMedcertController
    {
        dbcrud crud = new dbcrud();

        public async Task save(params string [] data)
        {
            string sql = @"INSERT INTO patient_medcert (customer_id,path) VALUES(@id,@path)";



            int id = int.Parse(data[0]);
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@id", id));
            mySqlParameters.Add(new MySqlParameter("@path", data[1]));


            await crud.ExecuteAsync(sql, mySqlParameters);
        }


        public async Task<bool> isDoneMedCert(int id)
        {
            bool isDone = false;
            string sql = @"SELECT * FROM `patient_medcert` 
                        INNER JOIN customer_request_details ON patient_medcert.customer_id 
                                    = customer_request_details.customer_id
                        WHERE patient_medcert.customer_id = @id";


            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", id)) };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            if (reader.HasRows)
            {
                isDone =  true;
            }

            crud.CloseConnection();

            return isDone;
        }
    }
}
