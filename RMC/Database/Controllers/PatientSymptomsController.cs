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
    class PatientSymptomsController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int sid)
        {
            string sql = @"INSERT INTO patient_symptoms (symptoms_id,doctor_results_id) 
                          VALUES (@sid,(SELECT doctor_results_id FROM doctor_results ORDER BY doctor_results_id DESC LIMIT 1))";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@sid", sid));

            await crud.ExecuteAsync(sql, listparams);

        }


        public async Task remove(int sid,int resid)
        {
            string sql = @"DELETE FROM patient_symptoms WHERE doctor_results_id = @id AND  symptoms_id = @sid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@sid", sid));
            listparams.Add(new MySqlParameter("@id", resid));

            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<List<PatientSymptomsModel>> getPatientSymptomsMod(int doctorResults)
        {
            List<PatientSymptomsModel> listPatientSymp = new List<PatientSymptomsModel>();

            string sql = @"SELECT * FROM `patient_symptoms`
                        INNER JOIN symptoms ON patient_symptoms.symptoms_id = symptoms.symptoms_id
                        where doctor_results_id = @dsid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@dsid", doctorResults));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                PatientSymptomsModel patientSymp = new PatientSymptomsModel();
                patientSymp.id = int.Parse(reader["patient_symptoms_id"].ToString());
                patientSymp.symptoms = reader["symptoms_name"].ToString();
                patientSymp.s_id = int.Parse(reader["symptoms_id"].ToString());

                listPatientSymp.Add(patientSymp);

            }



            crud.CloseConnection();

            return listPatientSymp;
        }

        public async Task<bool> isFound(int resid, int sid)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM `patient_symptoms` WHERE doctor_results_id = @id AND symptoms_id = @sid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", resid));
            listparams.Add(new MySqlParameter("@sid", sid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                isFound = true;
            }

            crud.CloseConnection();

            return isFound;
        }
    }
}
