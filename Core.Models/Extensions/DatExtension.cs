using System;
using System.IO;
namespace Core.Models.Extensions
{
    public class DatExtension
    {
        public string? ReadDatFile(string ftpLocation, string fileName, string fileContent)
        {
            var file = ftpLocation + fileName + ".dat";;
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                BinaryReader r = new BinaryReader(fs);
                var lines = r.ReadString();
                r.Close();
                fs.Close();
                return lines;
            }
            return string.Empty;
        }

        public string? WriteDatFile(string ftpLocation, string fileName, string fileContent)
        {
             var file = ftpLocation + fileName + ".dat";
             using (FileStream fs = new FileStream(file, FileMode.CreateNew))
             {
                 BinaryWriter w = new BinaryWriter(fs);
                 w.Write(fileContent);
                 w.Close();
                 fs.Close();
             }
            return string.Empty;
        }
    }
}
