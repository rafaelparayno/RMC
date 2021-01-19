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
    class PatientVController
    {
        dbcrud crud = new dbcrud();

        public async Task<patientVModel> getDetailsID(int id)
        {
            patientVModel pv = new patientVModel();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM `patientvital` WHERE patient_vital_id = @id";
            listparams.Add(new MySqlParameter("@id", id));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if(await reader.ReadAsync())
            {
                pv.id = int.Parse(reader["patient_vital_id"].ToString());
                pv.date_vital = reader["date_vital"].ToString();
                pv.bp = reader["BP"].ToString();
                pv.temp = reader["TEMP"].ToString();
                pv.wt = reader["WT"].ToString();
                pv.lmp = reader["LMP"].ToString();
                pv.ua = reader["UA"].ToString();
                pv.pus = reader["PUS"].ToString();
                pv.rbc = reader["rbc"].ToString();
            }
            crud.CloseConnection();

            return pv;
            
        }

        public async Task<patientVModel> getDetailsidDate(int id,string date)
        {
            patientVModel pv = new patientVModel();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM `patientvital` WHERE patient_id = @id 
                        AND date_vital = @date";
            listparams.Add(new MySqlParameter("@id", id));
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            if (await reader.ReadAsync())
            {
                pv.id = int.Parse(reader["patient_vital_id"].ToString());
                pv.date_vital = reader["date_vital"].ToString();
                pv.bp = reader["BP"].ToString();
                pv.temp = reader["TEMP"].ToString();
                pv.wt = reader["WT"].ToString();
                pv.lmp = reader["LMP"].ToString();
                pv.ua = reader["UA"].ToString();
                pv.pus = reader["PUS"].ToString();
                pv.rbc = reader["rbc"].ToString();
            }
            crud.CloseConnection();

            return pv;

        }

        public async Task<List<patientVModel>> getPatientV(int id)
        {
            List<patientVModel> listpatientv = new List<patientVModel>();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM `patientvital` WHERE patient_id = @patid";
            listparams.Add(new MySqlParameter("@patid",id));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while(await reader.ReadAsync())
            {
                patientVModel patientV = new patientVModel();

                patientV.id = int.Parse(reader["patient_vital_id"].ToString());
                patientV.date_vital = reader["date_vital"].ToString();
                patientV.bp = reader["BP"].ToString();
                patientV.temp = reader["TEMP"].ToString();
                patientV.wt = reader["WT"].ToString();
                patientV.lmp = reader["LMP"].ToString();
                patientV.ua = reader["UA"].ToString();
                patientV.pus = reader["PUS"].ToString();
                patientV.rbc = reader["rbc"].ToString();
                listpatientv.Add(patientV);
            }

            crud.CloseConnection();

            return listpatientv;
        }

        public async Task<List<patientVModel>> getPatientV(int id,string date)
        {
            List<patientVModel> listpatientv = new List<patientVModel>();
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM `patientvital` 
                            WHERE patient_id = @patid AND date_vital = @date";
            listparams.Add(new MySqlParameter("@patid", id));
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(date)));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            while (await reader.ReadAsync())
            {
                patientVModel patientV = new patientVModel();

                patientV.id = int.Parse(reader["patient_vital_id"].ToString());
                patientV.date_vital = reader["date_vital"].ToString();
                patientV.bp = reader["BP"].ToString();
                patientV.temp = reader["TEMP"].ToString();
                patientV.wt = reader["WT"].ToString();
                patientV.lmp = reader["LMP"].ToString();
                patientV.ua = reader["UA"].ToString();
                patientV.pus = reader["PUS"].ToString();
                patientV.rbc = reader["rbc"].ToString();
                listpatientv.Add(patientV);
            }

            crud.CloseConnection();

            return listpatientv;
        }

        public async void save(params string[] data)
        {
            string sql = @"INSERT INTO patientvital 
                        (patient_id,date_vital,BP,TEMP,WT,LMP,UA,PUS,rbc) 
                        VALUES (@patid,@date,@bp,@temp,@wt,@lmp,@ua,@pus,@rbc)";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@patid", int.Parse(data[0])));
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(data[1])));
            listparams.Add(new MySqlParameter("@bp", data[2]));
            listparams.Add(new MySqlParameter("@temp", data[3]));
            listparams.Add(new MySqlParameter("@wt", data[4]));
            listparams.Add(new MySqlParameter("@lmp", data[5]));
            listparams.Add(new MySqlParameter("@ua", data[6]));
            listparams.Add(new MySqlParameter("@pus", data[7]));
            listparams.Add(new MySqlParameter("@rbc", data[8]));


          await crud.ExecuteAsync(sql, listparams);
        }


        public async void update(params string[] data)
        {
            string sql = @"UPDATE patientvital 
                        SET date_vital = @date, BP = @bp, TEMP = @temp, WT = @wt,
                        LMP = @lmp,UA = @ua,PUS = @pus,rbc = @rbc 
                        WHERE patient_vital_id = @id ";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", int.Parse(data[0])));
            listparams.Add(new MySqlParameter("@date", DateTime.Parse(data[1])));
            listparams.Add(new MySqlParameter("@bp", data[2]));
            listparams.Add(new MySqlParameter("@temp", data[3]));
            listparams.Add(new MySqlParameter("@wt", data[4]));
            listparams.Add(new MySqlParameter("@lmp", data[5]));
            listparams.Add(new MySqlParameter("@ua", data[6]));
            listparams.Add(new MySqlParameter("@pus", data[7]));
            listparams.Add(new MySqlParameter("@rbc", data[8]));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async void delete(int id)
        {
            string sql = @"DELETE FROM patientvital 
                        WHERE patient_vital_id = @id ";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", id));

            await crud.ExecuteAsync(sql, listparams);
        }
    }
}
