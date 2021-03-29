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
    class RadioQueueController
    {
        dbcrud crud = new dbcrud();

        public async Task save(int customerid, int radioid)
        {
            string sql = @"INSERT INTO radio_queue (xray_id,customer_id) VALUES(@id,@cid)";


            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", radioid));
            listparams.Add(new MySqlParameter("@cid", customerid));

            await crud.ExecuteAsync(sql, listparams);
        }


        public async Task<List<xraymodel>> getReqLabByPatientID(int id)
        {
            List<xraymodel> listxraymodel = new List<xraymodel>();

            string sql = @"SELECT  radio_queue.xray_id,radio_queue.is_done_x,xraylist.xray_name,xraylist.xray_type FROM `radio_queue` 
                        INNER JOIN xraylist ON radio_queue.xray_id = xraylist.xray_id 
                        WHERE customer_id IN (SELECT customer_id  FROM customer_request_details WHERE customer_request_details.patient_id = @id
                                                    AND  DATE(date_req) = CURDATE())";

            List<MySqlParameter> listparams = new List<MySqlParameter>();

            listparams.Add(new MySqlParameter("@id", id));

            DbDataReader reader = await crud.RetrieveRecordsAsync(sql, listparams);

            while (await reader.ReadAsync())
            {
                xraymodel l = new xraymodel();

                l.id = int.Parse(reader["xray_id"].ToString());
                l.name = reader["xray_name"].ToString();
                l.type = int.Parse(reader["xray_type"].ToString());
                l.is_done = int.Parse(reader["is_done_x"].ToString());
                listxraymodel.Add(l);
            }



            crud.CloseConnection();
            return listxraymodel;
                ;
        }
    }
}
