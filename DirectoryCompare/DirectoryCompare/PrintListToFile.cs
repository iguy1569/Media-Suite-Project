using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DirectoryCompare
{
    public class PrintListToFile
    {
        public static void PrintFile(List<AllFileInfo> lafi, string sPath, bool bReverseTransfer)
        {
            File.WriteAllText(sPath, "");
            using (FileStream fs = new FileStream(sPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs);

                    sw.WriteLine("************************************************************************************************");

                if (!bReverseTransfer)
                    sw.WriteLine("|                            Source Contents Missing from Destination                          |");
                else
                    sw.WriteLine("|                            Destination Contents Missing from Source                          |");

                    sw.WriteLine("************************************************************************************************");
                    sw.WriteLine(string.Format("|{0, -68} | {1, 15}        |", "", ""));

                foreach (AllFileInfo afi in lafi)
                    sw.WriteLine(string.Format("|{0, -68} | {1, 15} MBytes |", afi.fiFilesFound.Name, (afi.lFileByteLength / 1048576)));

                    sw.WriteLine("************************************************************************************************");

                sw.Close();
                fs.Close();
            }
        }
    }
}
