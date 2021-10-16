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

        public async Task save(int pid, int xid, string filename, string path,int cid)
        {
            string sql = @"INSERT INTO patient_xray (patient_id,xray_id,filename,path,customer_id) 
                          VALUES (@id,@xid,@fname,@path,@cid)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", pid));
            listparams.Add(new MySqlParameter("@xid", xid));
            listparams.Add(new MySqlParameter("@fname", filename));
            listparams.Add(new MySqlParameter("@path", path));
            listparams.Add(new MySqlParameter("@cid", cid));


            await crud.ExecuteAsync(sql, listparams);

        }

        public async Task<List<patientXrayModel>> getPatientXray(int id)
        {
            List<patientXrayModel> listPatientXrayMod = new List<patientXrayModel>();
            string sql = @"SELECT patient_xray.patient_xray_id AS 'ID',
                        xraylist.xray_name,xraylist.xray_type,patient_xray.date_patient_xray as 'dateXray',filename FROM `patient_xray` 
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
                patientXrayModel.fileName = reader["filename"].ToString();
                listPatientXrayMod.Add(patientXrayModel);
            }

            crud.CloseConnection();
            return listPatientXrayMod;
        }

        public async Task<patientXrayModel> getPatientXray(int id,int xrayid)
        {
            patientXrayModel patientXrayModel = new patientXrayModel();
            string sql = @"SELECT patient_xray.patient_xray_id AS 'ID',
                        xraylist.xray_name,xraylist.xray_type,
                        patient_xray.date_patient_xray as 'dateXray',filename FROM `patient_xray` 
                        INNER JOIN xraylist ON patient_xray.xray_id = xraylist.xray_id 
                        WHERE patient_id = @id AND patient_xray.xray_id = @xid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@xid", xrayid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if(await reader.ReadAsync())
            {
              
                patientXrayModel.id = int.Parse(reader["ID"].ToString());
                patientXrayModel.name = reader["xray_name"].ToString();
                patientXrayModel.type = int.Parse(reader["xray_type"].ToString());
                patientXrayModel.date = DateTime.Parse(reader["dateXray"].ToString());
                patientXrayModel.fileName = reader["filename"].ToString();
                
            }

            crud.CloseConnection();
            return patientXrayModel;
        }


        public async Task<List<patientXrayModel>> getPatientXray(int id,string date)
        {
            List<patientXrayModel> listPatientXrayMod = new List<patientXrayModel>();
            string sql = @"SELECT patient_xray.patient_xray_id AS 'ID',
                        xraylist.xray_name,xraylist.xray_type,filename,patient_xray.date_patient_xray as 'dateXray' FROM `patient_xray` 
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
                patientXrayModel.fileName = reader["filename"].ToString();
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


        public async Task<string> getFullPath(int patientid, int xrayid,int cid)
        {
            string FullPath = "";
            string sql = @"SELECT CONCAT(path,filename) AS 'FullPath' 
                            FROM `patient_xray` WHERE patient_id = @id AND xray_id = @xray AND customer_id = @cid";
            List<MySqlParameter> listParams = new List<MySqlParameter>();
            listParams.Add(new MySqlParameter("@id", patientid));

            listParams.Add(new MySqlParameter("@xray", xrayid));
            listParams.Add(new MySqlParameter("@cid", cid));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listParams);

            while (await reader.ReadAsync())
            {
                FullPath = reader["FullPath"].ToString();
            }

            crud.CloseConnection();

            return FullPath;
        }


    }
}
