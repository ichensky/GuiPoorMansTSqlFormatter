using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiPoorMansTSqlFormatter
{
    static class FileHandler
    {
        public static string ReadFile(this string fileFullName)
        {
            var str = "";

            using (var file = new StreamReader(fileFullName))
            {
                str = file.ReadToEnd();
            }

            return str;
        }

        public static void WriteToFile(this string str, string fileFullName)
        {
            using (var file = new System.IO.StreamWriter(fileFullName, false))
            {
                file.WriteLine(str);
            }
        }

        public static void SetRWAtt(this string fileFullName)
        {
            FileAttributes attributes = File.GetAttributes(fileFullName);

            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                // Make the file RW
                File.SetAttributes(fileFullName, attributes & ~FileAttributes.ReadOnly);
            }
        }
    }
}
