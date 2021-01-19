using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Utilities
{
    class CreateDirectory
    {
        public static string CreateDir(string dirName)
        {
            //CreateFileServer
            string filePathServer = ReadFileServerPath.FetchServerLocation();
      
            string newDir = String.Format(@"{0}{1}\", filePathServer,dirName);
            bool exists = Directory.Exists(newDir);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(newDir);
            }
            return newDir;
        }
    }
}
