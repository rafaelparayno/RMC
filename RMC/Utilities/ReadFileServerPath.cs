
using System.IO;

namespace RMC.Utilities
{
    class ReadFileServerPath
    {
        public static string FetchServerLocation()
        {
            string returnData = "";

            try
            {
                StreamReader file = new StreamReader(Directory.GetCurrentDirectory() + @"\fileserver.txt");

                returnData = file.ReadLine();
                file.Close();
            }
            catch { }
            return returnData;
        }
    }
}
