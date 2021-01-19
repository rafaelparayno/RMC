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
    class PatientPrescriptionController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int item_id,string instruction)
        {
            string sql = @"INSERT INTO patient_prescription (item_id,instruction,doctor_results_id) 
                            VALUES (@itemid,@instruction, 
                            (SELECT doctor_results_id FROM doctor_results ORDER BY doctor_results_id DESC LIMIT 1)) ";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", item_id));
            listparams.Add(new MySqlParameter("@instruction", instruction));

            await crud.ExecuteAsync(sql, listparams);
        }


        public async Task<List<PatientPrescriptionModel>> getPrescriptionModelDate(string date)
        {
            List<PatientPrescriptionModel> listpatientP = new List<PatientPrescriptionModel>();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM `patient_prescription` 
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id
                        WHERE date_prescription = @dsid";
            listparams.Add(new MySqlParameter("@dsid", date));


            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while (await reader.ReadAsync())
            {
                PatientPrescriptionModel ppModel = new PatientPrescriptionModel();

                ppModel.id = int.Parse(reader["patient_prescription_id"].ToString());

                ppModel.instruction = reader["instruction"].ToString();
                ppModel.medName = reader["item_name"].ToString();
                ppModel.date = DateTime.Parse(reader["date_prescription"].ToString());

                listpatientP.Add(ppModel);
            }

            crud.CloseConnection();

            return listpatientP;
        }

        public async Task<List<PatientPrescriptionModel>> getPrescriptionModel( )
        {
            List<PatientPrescriptionModel> listpatientP = new List<PatientPrescriptionModel>();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM `patient_prescription` 
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id";
    

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, null);


            while (await reader.ReadAsync())
            {
                PatientPrescriptionModel ppModel = new PatientPrescriptionModel();

                ppModel.id = int.Parse(reader["patient_prescription_id"].ToString());

                ppModel.instruction = reader["instruction"].ToString();
                ppModel.medName = reader["item_name"].ToString();
                ppModel.date = DateTime.Parse(reader["date_prescription"].ToString());

                listpatientP.Add(ppModel);
            }

            crud.CloseConnection();

            return listpatientP;
        }

        public async Task<List<PatientPrescriptionModel>> getPrescriptionModel(int doctorRes)
        {
            List<PatientPrescriptionModel> listpatientP = new List<PatientPrescriptionModel>();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM `patient_prescription` 
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id
                        WHERE doctor_results_id = @dsid";
            listparams.Add(new MySqlParameter("@dsid", doctorRes));
       

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while (await reader.ReadAsync())
            {
                PatientPrescriptionModel ppModel = new PatientPrescriptionModel();

                ppModel.id = int.Parse(reader["patient_prescription_id"].ToString());
         
                ppModel.instruction = reader["instruction"].ToString();
                ppModel.medName = reader["item_name"].ToString();
                ppModel.date = DateTime.Parse(reader["date_prescription"].ToString());

                listpatientP.Add(ppModel);
            }

            crud.CloseConnection();

            return listpatientP;
        }

    }
}
