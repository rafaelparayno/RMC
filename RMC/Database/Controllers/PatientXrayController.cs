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
    class PatientXrayController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int pid, int xid, string filename, string path)
        {
            string sql = @"INSERT INTO patient_xray (patient_id,xray_id,filename,path) 
                          VALUES (@id,@xid,@fname,@path)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", pid));
            listparams.Add(new MySqlParameter("@xid", xid));
            listparams.Add(new MySqlParameter("@fname", filename));
            listparams.Add(new MySqlParameter("@path", path));


            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<List<patientXrayModel>> getPatientXray(int id)
        {
            List<patientXrayModel> listPatientXrayMod = new List<patientXrayModel>();
            string sql = @"SELECT patient_xray.patient_xray_id AS 'ID',
                        xraylist.xray_name,xraylist.xray_type,patient_xray.date_patient_xray as 'dateXray' FROM `patient_xray` 
                        INNER JOIN xraylist ON patient_xray.xray_id = xraylist.xray_id 
                        WHERE patient_id = @id";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while(await reader.ReadAsync())
            {
                patientXrayModel patientXrayModel = new patientXrayModel();
                patientXrayModel.id = int.Parse(reader["ID"].ToString());
                patientXrayModel.name = reader["xray_name"].ToString();
                patientXrayModel.type = int.Parse(reader["xray_type"].ToString());
                patientXrayModel.date = DateTime.Parse(reader["dateXray"].ToString());

                listPatientXrayMod.Add(patientXrayModel);
            }

            crud.CloseConnection();
            return listPatientXrayMod;
        }


        public async Task<List<patientXrayModel>> getPatientXray(int id,string date)
        {
            List<patientXrayModel> listPatientXrayMod = new List<patientXrayModel>();
            string sql = @"SELECT patient_xray.patient_xray_id AS 'ID',
                        xraylist.xray_name,xraylist.xray_type,patient_xray.date_patient_xray as 'dateXray' FROM `patient_xray` 
                        INNER JOIN xraylist ON patient_xray.xray_id = xraylist.xray_id 
                        WHERE patient_id = @id AND date_patient_xray = @date";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                patientXrayModel patientXrayModel = new patientXrayModel();
                patientXrayModel.id = int.Parse(reader["ID"].ToString());
                patientXrayModel.name = reader["xray_name"].ToString();
                patientXrayModel.type = int.Parse(reader["xray_type"].ToString());
                patientXrayModel.date = DateTime.Parse(reader["dateXray"].ToString());

                listPatientXrayMod.Add(patientXrayModel);
            }

            crud.CloseConnection();
            return listPatientXrayMod;
        }


        public async Task<string> getFullPath(int id)
        {
            string fullPath = "";
            string sql = @"SELECT CONCAT(path,filename) AS 'Path' FROM `patient_xray` WHERE patient_xray_id = @id";

            List<MySqlParameter> listParams = new List<MySqlParameter>();

            listParams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);


            while(await reader.ReadAsync())
            {
                fullPath = reader["Path"].ToString();
            }

            crud.CloseConnection();

            return fullPath;
        }

    }
}
