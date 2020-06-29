using System.IO;


namespace RMC.Database.DbSettings
{
    class dbConfigFile
    {
        public static string[] FetchDatabaseLocation()
        {
            string[] returnData = new string[4];

            try
            {
                StreamReader file = new StreamReader(Directory.GetCurrentDirectory() + @"\databaseconfig.txt");

                returnData = file.ReadLine().Split('#');
                file.Close();
            }
            catch { }
            return returnData;
        }
    }
}
