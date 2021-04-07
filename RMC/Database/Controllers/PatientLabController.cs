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
    class PatientLabController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int pid,int lid,string filename,string path)
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

        public async Task<string> getFullPath(int id)
        {
            string FullPath = "";
            string sql = @"SELECT CONCAT(path,filename) AS 'FullPath' 
                            FROM `patient_lab` WHERE patient_lab_id  = @id";
            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);

            while(await reader.ReadAsync())
            {
                FullPath = reader["FullPath"].ToString();
            }

            crud.CloseConnection();

            return FullPath;
        }


        public async Task<string> getFullPath(int patientid,int labid)
        {
            string FullPath = "";
            string sql = @"SELECT CONCAT(path,filename) AS 'FullPath' 
                            FROM `patient_lab` WHERE patient_id = @id AND laboratory_id = @labid AND DATE(date_patient_lab) = CURDATE()";
            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@id", patientid));

            listParams.Add(new MySqlParameter("@labid", labid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);

            while (await reader.ReadAsync())
            {
                FullPath = reader["FullPath"].ToString();
            }

            crud.CloseConnection();

            return FullPath;
        }

        public async Task<List<patientLabModel>> getPatientLabModel(int id)
        {
            List<patientLabModel> listPatientLabModel = new List<patientLabModel>();
            string sql = @"SELECT patient_lab.patient_lab_id AS 'ID',laboratorylist.labname,filename,
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
                patientLabModel.filename = reader["filename"].ToString();
                listPatientLabModel.Add(patientLabModel);
            }

            crud.CloseConnection();

            return listPatientLabModel;
        }


        public async Task<List<patientLabModel>> getPatientLabModel(int id,string date)
        {
            List<patientLabModel> listPatientLabModel = new List<patientLabModel>();
            string sql = @"SELECT patient_lab.patient_lab_id AS 'ID',laboratorylist.labname,filename,
                            labtype.labtype_name,patient_lab.date_patient_lab AS 'DateTaken' FROM `patient_lab` 
                            INNER JOIN laboratorylist ON patient_lab.laboratory_id = laboratorylist.laboratory_id
                            INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id 
                            WHERE patient_lab.patient_id = @id AND date_patient_lab = @date";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
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
