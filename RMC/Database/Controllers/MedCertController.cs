using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class MedCertController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int id)
        {

            string sql = @"INSERT INTO medcert_request (customer_id) VALUES (@id)";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", id)) };


            await crud.ExecuteAsync(sql, mySqlParameters);
        }

        public async Task<DataSet> getDataSetNotDone()
        {
            string sql = @"SELECT medcert_request_id as 'ID' ,patientdetails.patient_id,
                            CONCAT(firstname,' ',middlename,' ',lastname) as 'PatientName' FROM `medcert_request`
                            INNER JOIN customer_request_details ON medcert_request.customer_id = customer_request_details.customer_id 
                            INNER JOIN patientdetails ON customer_request_details.patient_id = patientdetails.patient_id
                            WHERE is_done_cert = 0 AND DATE(medcert_request.date_request) = CURDATE()";

            return await crud.GetDataSetAsync(sql, null);
        }

        public async Task setDone(int id)
        {
            string sql = @"UPDATE medcert_request SET is_done_cert = 1 WHERE medcert_request_id = @id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", id)) };

            await crud.ExecuteAsync(sql, mySqlParameters);
        }
    }
}
