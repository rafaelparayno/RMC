using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    public class ItemList
    {
        public  long id;
        public  string name;
        public  int stocks;
        public  int subCategory;
        public  string Category;
        public  int isBrand;
        public string UnitName;
        public  string ExpirationDate;
        public  string DateAdded;
        public  float unitPrice;
        public  float markupPrice;
        public  float sellingPrice;
        public  string sku;
        public  string description;
     

        public ItemList(params string[] item)
        {
            this.id = long.Parse(item[0]);
            this.name = item[1];
            this.stocks = item[2] == "" ? 0 : int.Parse(item[2]);
            this.subCategory = int.Parse(item[3]);
            this.Category = item[4];
            this.isBrand = int.Parse(item[5]);
            this.UnitName = item[6];
          /*  this.ExpirationDate = item[7];
            this.DateAdded = item[8];*/
            this.unitPrice = float.Parse(item[7]);
            this.markupPrice = float.Parse(item[8]); 
            this.sellingPrice = float.Parse(item[9]); 
            this.sku = item[10]; 
            this.description = item[11];
        }




        // public 
    }
}
