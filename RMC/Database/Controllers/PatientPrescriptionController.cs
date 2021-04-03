using MySql.Data.MySqlClient;
using RMC.Database.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    class PatientPrescriptionController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int item_id,string instruction,string sInstruction,string disNo)
        {
            string sql = @"INSERT INTO patient_prescription (item_id,instruction,dispense_no,sInstruction,doctor_results_id) 
                            VALUES (@itemid,@instruction,@dis,@sIns, 
                            (SELECT doctor_results_id FROM doctor_results ORDER BY doctor_results_id DESC LIMIT 1)) ";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", item_id));
            listparams.Add(new MySqlParameter("@instruction", instruction));
            listparams.Add(new MySqlParameter("@dis", disNo));
            listparams.Add(new MySqlParameter("@sIns", sInstruction));

            await crud.ExecuteAsync(sql, listparams);
        }

        public async Task remove(int item_id,int resid)
        {
            string sql = @"DELETE FROM patient_prescription WHERE doctor_results_id = @resid AND item_id = @itemid";

            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@itemid", item_id));
            listparams.Add(new MySqlParameter("@resid", resid));


            await crud.ExecuteAsync(sql, listparams);
        }


        public async Task<DataSet> GetDataSetInfo()
        {
            string sql = @"SELECT DISTINCT patient_prescription.doctor_results_id,date_prescription,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname'
                    FROM `patient_prescription` 
                    INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                    INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id";


            DataSet ds = await crud.GetDataSetAsync(sql, null);

            return ds;
        }

        public async Task<DataSet> GetDataSetInfoMeds()
        {
            string sql = @"SELECT DISTINCT patient_prescription.doctor_results_id,date_prescription,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname'
                    FROM `patient_prescription` 
                    INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                    INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id";


            DataSet ds = await crud.GetDataSetAsync(sql, null);

            return ds;
        }

        public async Task<DataSet> GetDataSetInfo(int id)
        {
            string sql = @"SELECT DISTINCT patient_prescription.doctor_results_id,date_prescription,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname'
                    FROM `patient_prescription` 
                    INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                    INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id    
                    WHERE doctor_results.patient_id = @id";

            List<MySqlParameter> listparam = new List<MySqlParameter>();

            listparam.Add(new MySqlParameter("@id", id));

            DataSet ds = await crud.GetDataSetAsync(sql, listparam);

            return ds;
        }



        public async Task<DataSet> GetDataSetInfo(string searchkey)
        {
            string sql = @"SELECT DISTINCT patient_prescription.doctor_results_id,date_prescription,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname'
                    FROM `patient_prescription` 
                    INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                    INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id    
                      WHERE Concat(patientdetails.firstname,' ',patientdetails.lastname) LIKE @key";

            List<MySqlParameter> listparam = new List<MySqlParameter>();
            string searches = "%" + searchkey + "%";
            listparam.Add(new MySqlParameter("@key", searches));


            DataSet ds = await crud.GetDataSetAsync(sql, listparam);

            return ds;
        }

        public async Task<DataSet> GetDataSetInfoDate(string date)
        {
            string sql = @"SELECT DISTINCT patient_prescription.doctor_results_id,date_prescription,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname'
                    FROM `patient_prescription` 
                    INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                    INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id    
                     WHERE date_prescription = @date";

            List<MySqlParameter> listparam = new List<MySqlParameter>();

            listparam.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            DataSet ds = await crud.GetDataSetAsync(sql, listparam);

            return ds;
        }

       

        public async Task<DataSet> getPrescriptionByResID(int resid)
        {
            string sql = @"SELECT patient_prescription.patient_prescription_id,itemlist.item_name AS 'Medicine_Prescribe',patient_prescription.instruction,
                        patient_prescription.dispense_no,patient_prescription.sInstruction,itemlist.Description,itemlist.isBranded FROM `patient_prescription`
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id
                        WHERE doctor_results_id = @resid";


            List<MySqlParameter> listparam = new List<MySqlParameter>();

            listparam.Add(new MySqlParameter("@resid", resid));

            DataSet ds = await crud.GetDataSetAsync(sql, listparam);

            return ds;
        
        }

        public async Task<DataSet> getDataset(int id)
        {
            string sql = @"SELECT patient_prescription.patient_prescription_id,itemlist.item_name AS 'Medicine_Prescribe',patient_prescription.instruction,
                        patient_prescription.dispense_no,patient_prescription.sInstruction,date_prescription ,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname' FROM `patient_prescription`
                        INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id
                        INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id
                        WHERE doctor_results.patient_id = @id";

            List<MySqlParameter> listparam = new List<MySqlParameter>();

            listparam.Add(new MySqlParameter("@id", id));

            DataSet ds = await crud.GetDataSetAsync(sql, listparam);

            return ds;
        }

        public async Task<DataSet> getDataset(int id, string date)
        {
            string sql = @"SELECT patient_prescription.patient_prescription_id,itemlist.item_name AS 'Medicine_Prescribe',patient_prescription.instruction,
                        patient_prescription.dispense_no,patient_prescription.sInstruction,date_prescription ,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname' FROM `patient_prescription`
                        INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id
                        INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id
                        WHERE doctor_results.patient_id = @id AND date_prescription = @date";

            List<MySqlParameter> listparam = new List<MySqlParameter>();

            listparam.Add(new MySqlParameter("@id", id));
            listparam.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            DataSet ds = await crud.GetDataSetAsync(sql, listparam);

            return ds;
        }


        public async Task<DataSet> getDatasetName(string keySearch)
        {
            string sql = @"SELECT patient_prescription.patient_prescription_id,itemlist.item_name AS 'Medicine_Prescribe',patient_prescription.instruction,
                        patient_prescription.dispense_no,patient_prescription.sInstruction,date_prescription ,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname' FROM `patient_prescription`
                        INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id
                        INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id
                        WHERE Concat(patientdetails.firstname,' ',patientdetails.lastname) LIKE @key";

            List<MySqlParameter> listparam = new List<MySqlParameter>();
            string searches = "%" + keySearch + "%";
            listparam.Add(new MySqlParameter("@key", searches));


            DataSet ds = await crud.GetDataSetAsync(sql, listparam);

            return ds;
        }

        public async Task<DataSet> getDataset(string date)
        {
            string sql = @"SELECT patient_prescription.patient_prescription_id,itemlist.item_name AS 'Medicine_Prescribe',patient_prescription.instruction,
                        patient_prescription.dispense_no,patient_prescription.sInstruction,date_prescription ,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname' FROM `patient_prescription`
                        INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id
                        INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id
                        WHERE date_prescription = @date";

            List<MySqlParameter> listparam = new List<MySqlParameter>();

            listparam.Add(new MySqlParameter("@date", DateTime.Parse(date)));

            DataSet ds = await crud.GetDataSetAsync(sql, listparam);

            return ds;
        }

        public async Task<DataSet> getDataset()
        {
            string sql = @"SELECT patient_prescription.patient_prescription_id,itemlist.item_name AS 'Medicine_Prescribe',patient_prescription.instruction,
                        patient_prescription.dispense_no,patient_prescription.sInstruction,date_prescription ,CONCAT(patientdetails.firstname,' ',patientdetails.lastname) As 'patientname' FROM `patient_prescription`
                        INNER JOIN doctor_results ON patient_prescription.doctor_results_id = doctor_results.doctor_results_id
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id
                        INNER JOIN patientdetails ON doctor_results.patient_id = patientdetails.patient_id ";



            DataSet ds = await crud.GetDataSetAsync(sql, null);

            return ds;
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


        public async Task<string> getPrescriptionSKU(int id)
        {
            string sku = "";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            string sql = @"SELECT * FROM `patient_prescription` 
                        INNER JOIN itemlist ON patient_prescription.item_id = itemlist.item_id 
                        WHERE patient_prescription_id = @id";

            listparams.Add(new MySqlParameter("@id", id));
            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);


            if (await reader.ReadAsync())
            {
                sku = reader["SKU"].ToString();
            }

            crud.CloseConnection();

            return sku;
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
                ppModel.itemid = int.Parse(reader["item_id"].ToString());
                ppModel.dispenseno = reader["dispense_no"].ToString();
                ppModel.sinstruction = reader["sInstruction"].ToString();
                ppModel.date = DateTime.Parse(reader["date_prescription"].ToString());

                listpatientP.Add(ppModel);
            }

            crud.CloseConnection();

            return listpatientP;
        }

        public async Task<bool> isFound(int resid, int itemid)
        {
            bool isFound = false;
            string sql = @"SELECT * FROM `patient_prescription` WHERE doctor_results_id = @id AND item_id = @itemid";
            List<MySqlParameter> listparams = new List<MySqlParameter>();
            listparams.Add(new MySqlParameter("@id", resid));
            listparams.Add(new MySqlParameter("@itemid", itemid));

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
