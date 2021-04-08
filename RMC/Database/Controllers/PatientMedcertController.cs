using MySql.Data.MySqlClient;
using RMC.Database.Models;
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
            string sql = @"INSERT INTO patient_medcert (customer_id,path,med_cert_type) VALUES(@id,@path,@type)";



            int id = int.Parse(data[0]);
            int medtype = int.Parse(data[2]);
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>();
            mySqlParameters.Add(new MySqlParameter("@id", id));
            mySqlParameters.Add(new MySqlParameter("@path", data[1]));
            mySqlParameters.Add(new MySqlParameter("@type", medtype));


            await crud.ExecuteAsync(sql, mySqlParameters);
        }

        public async Task<MedCertModel> getMedcert(int id)
        {
            MedCertModel medCertModel = new MedCertModel();
            string sql = @"SELECT * FROM patient_medcert WHERE patient_medcert_id = @id";

            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", id)) };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);
            if (await reader.ReadAsync())
            {
                medCertModel.id = int.Parse(reader["patient_medcert_id"].ToString());
                medCertModel.customerid = int.Parse(reader["customer_id"].ToString());
                medCertModel.path = reader["path"].ToString();
                medCertModel.type = int.Parse(reader["med_cert_type"].ToString());
                medCertModel.date = DateTime.Parse(reader["medcert_date"].ToString());
            }
            crud.CloseConnection();

            return medCertModel;
        }

        public async Task<string> getPath(int patmedid)
        {
            string path = "";
            string sql = @"SELECT * FROM patient_medcert WHERE patient_medcert_id = @id";

              List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", patmedid)) };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);
            if(await reader.ReadAsync())
            {
                path = reader["path"].ToString();
            }
            crud.CloseConnection();

            return path;
        }


        public async Task<List<MedCertModel>> listMedCert(int patid )
        {
            List<MedCertModel> medCertModels = new List<MedCertModel>();

            string sql = @"SELECT * FROM patient_medcert WHERE customer_id in(SELECT customer_id 
                            FROM customer_request_details WHERE patient_id = @id)";
            List<MySqlParameter> mySqlParameters = new List<MySqlParameter>() { (new MySqlParameter("@id", patid)) };

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, mySqlParameters);

            while(await reader.ReadAsync())
            {
                MedCertModel medCertModel = new MedCertModel();

                medCertModel.id = int.Parse(reader["patient_medcert_id"].ToString());
                medCertModel.date = DateTime.Parse(reader["medcert_date"].ToString());
                medCertModel.type = int.Parse(reader["med_cert_type"].ToString());
                medCertModels.Add(medCertModel);
            }
            crud.CloseConnection();

            return medCertModels;
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
