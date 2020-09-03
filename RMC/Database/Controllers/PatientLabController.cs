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
    class PatientLabController
    {
        dbcrud crud = new dbcrud();

        public async void save(int pid,int lid,string filename,string path)
        {
            string sql = @"INSERT INTO patient_lab (patient_id,laboratory_id,filename,path) 
                          VALUES (@id,@lid,@fname,@path)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", pid));
            listparams.Add(new MySqlParameter("@lid", lid));
            listparams.Add(new MySqlParameter("@fname", filename));
            listparams.Add(new MySqlParameter("@path", path));


            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<List<patientLabModel>> getPatientLabModel(int id)
        {
            List<patientLabModel> listPatientLabModel = new List<patientLabModel>();
            string sql = @"SELECT patient_lab.patient_lab_id AS 'ID',laboratorylist.labname,
                            labtype.labtype_name,patient_lab.date_patient_lab AS 'DateTaken' FROM `patient_lab` 
                            INNER JOIN laboratorylist ON patient_lab.laboratory_id = laboratorylist.laboratory_id
                            INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                            WHERE patient_lab.patient_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                patientLabModel patientLabModel = new patientLabModel();
                patientLabModel.id = int.Parse(reader["ID"].ToString());
                patientLabModel.name = reader["labname"].ToString();
                patientLabModel.type = reader["labtype_name"].ToString();
                patientLabModel.date = DateTime.Parse(reader["DateTaken"].ToString());

                listPatientLabModel.Add(patientLabModel);
            }

            crud.CloseConnection();

            return listPatientLabModel;
        }
    }
}
