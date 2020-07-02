using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Controllers
{
    public class SupplierController
    {
        dbcrud crud = new dbcrud();

        public async Task<DataSet> getdataSetActive()
        {
            string sql = @"SELECT supplier_id AS 'Id',supplier_name As 'Supplier Name', supplier_number AS 'Contact Number', 
                            Location FROM suppliers WHERE is_active = 1";
            DataSet dgSuppliers = new DataSet();
            return dgSuppliers = await crud.GetDataSetAsync(sql, null);
        }

        public async void save(string name,string number,string location)
        {
            string sql = @"INSERT INTO suppliers (supplier_name,supplier_number,Location,is_active) 
                        VALUES (@name,@number,@location,@isactive)";
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@name", name));
            list.Add(new MySqlParameter("@number", number));
            list.Add(new MySqlParameter("@location", location));
            list.Add(new MySqlParameter("@isactive", 1));

            await crud.ExecuteAsync(sql, list);
        }

        public async void Edit(string name,string number,string location,int id)
        {
            string sql = String.Format(@"UPDATE suppliers SET supplier_name=@supplier_name, 
                           supplier_number=@supplier_number, Location=@Location 
                            WHERE supplier_id= @supplier_id");

            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@supplier_name", name));
            list.Add(new MySqlParameter("@supplier_number", number));
            list.Add(new MySqlParameter("@Location", location));
            list.Add(new MySqlParameter("@supplier_id", id));

            await crud.ExecuteAsync(sql, list);
        }

        public async void deactivate(int id)
        {
            string sql = String.Format(@"UPDATE suppliers SET is_active=@isactive
                            WHERE supplier_id= @supplier_id");
            List<MySqlParameter> list = new List<MySqlParameter>();
            list.Add(new MySqlParameter("@isactive", 0));
            list.Add(new MySqlParameter("@supplier_id", id));


            await crud.ExecuteAsync(sql, list);
        }
    }
}
