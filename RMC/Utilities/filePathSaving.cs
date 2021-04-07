using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Utilities
{
    static class filePathSaving
    {
        public static string saveLab(string pathFileFolder)
        {
            CreateDirectory.CreateDir(pathFileFolder);
            string newFilePath2 = CreateDirectory.CreateDir(pathFileFolder + "\\" + "LabFiles");


            return newFilePath2;
        }

        public static string saveXray(string pathFileFolder)
        {
            CreateDirectory.CreateDir(pathFileFolder);
            string newFilePath2 = CreateDirectory.CreateDir(pathFileFolder + "\\" + "XrayFiles");

            return newFilePath2;
        }

        public static string saveOthers(string pathFileFolder)
        {
            CreateDirectory.CreateDir(pathFileFolder);
            string newFilePath2 = CreateDirectory.CreateDir(pathFileFolder + "\\" + "otherFiles");

            return newFilePath2;
        }

        public static string medCert(string pathFileFolder)
        {
            CreateDirectory.CreateDir(pathFileFolder);
            string newFilePath2 = CreateDirectory.CreateDir(pathFileFolder + "\\" + "MedCerts");
            return newFilePath2;
        }


    }
}
