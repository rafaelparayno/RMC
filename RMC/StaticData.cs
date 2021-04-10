
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RMC
{
    public class StaticData
    {
        public static Dictionary<string, int> DataItemType = new Dictionary<string, int>() { { "Medicine", 1 }, { "Products", 2 }, { "Equipment", 3 } };

        public static Dictionary<string, int> XrayTypes = new Dictionary<string, int>() { { "Xray", 1 }, { "ECG", 2 }, { "Ultrasound", 3 } };

        public static string[] months = {"January","February","March","April",
                                        "May","June","July","August","September","October","November",
                                        "December"};

        public static Dictionary<string, int> accessValues = new Dictionary<string, int>() 
        { 
            { "Admin", 1 },
            {"labAccess",2 },
            {"pharmaAccess",3 },
            {"receptionAcess",4 },
            {"doctorAccess",5 },
            {"inventoryAccess",6 },
            {"xrayAccess",7 },
            {"otherAccess",8 }

        };

       public static ImageList listImages()
        {
            ImageList ImageList1 = new ImageList();
            ImageList1.ImageSize = new Size(30, 30);


            ImageList1.Images.Add(Properties.Resources.check);
            ImageList1.Images.Add(Properties.Resources.x);
            ImageList1.Images.Add(Properties.Resources.wait);
            ImageList1.Images.Add(Properties.Resources.emp);

            return ImageList1;

        }

    }
}
