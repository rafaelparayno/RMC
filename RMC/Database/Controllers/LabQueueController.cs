﻿using MySql.Data.MySqlClient;
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
    class LabQueueController
    {
        dbcrud crud = new dbcrud();


        public async Task save(int labid,int customerid)
        {
            string sql = @"INSERT INTO lab_queue (laboratory_id,customer_id) VALUES(@labid,@cid)";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@labid", labid));

            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }


        public async Task<List<labModel>> getReqLabByPatientID(int id)
        {
            List<labModel> listLabModel = new List<labModel>();

            string sql = @"SELECT lab_queue.customer_id,lab_queue.laboratory_id,laboratorylist.labname,labtype.labtype_name,lab_queue.is_done_l FROM `lab_queue` 
                        INNER JOIN laboratorylist ON lab_queue.laboratory_id = laboratorylist.laboratory_id 
                        INNER JOIN labtype ON laboratorylist.labtype_id = labtype.labtype_id
                        WHERE customer_id IN (SELECT customer_id  FROM customer_request_details WHERE customer_request_details.patient_id = 5
                                                    AND  DATE(date_req) = CURDATE()) ";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql,listparams);

            while(await reader.ReadAsync())
            {
                labModel l = new labModel();

                l.labID = int.Parse(reader["laboratory_id"].ToString());
                l.name = reader["labname"].ToString();
                l.labtypename = reader["labtype_name"].ToString();
                l.is_done = int.Parse(reader["is_done_l"].ToString());
                listLabModel.Add(l);
            }

            

            crud.CloseConnection();
            return listLabModel;
        }


    }
}
