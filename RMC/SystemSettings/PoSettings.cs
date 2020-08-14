using System.IO;


namespace RMC.SystemSettings
{
    class PoSettings
    {
        public static string[] FetchPoSettings()
        {
            string[] returnData = new string[3];

            try
            {
                StreamReader file = new StreamReader(Directory.GetCurrentDirectory() + @"\poSettings.txt");

                returnData = file.ReadLine().Split('#');
                file.Close();
            }
            catch { }
            return returnData;
        }
    }
}
