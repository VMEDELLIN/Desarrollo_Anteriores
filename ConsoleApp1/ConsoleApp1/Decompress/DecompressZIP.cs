using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompress
{
    public class DecompressZIP : IEstrategiaDecompress
    {

        //public void Decompress(string pathSource, string pathTarget)
        //{
        //    Decompress(pathSource, pathTarget, false);
        //}

        //public void Decompress(string pathSource, string pathTarget, bool overWrite)
        //{
        //    Decompress(pathSource, pathTarget, overWrite,false);
        //}

        public void Decompress(string pathSource, string pathTarget, bool overWrite, bool extractRoot)
        {
            if (!overWrite && !extractRoot)
                ZipFile.ExtractToDirectory(pathSource, pathTarget);
            else if (!overWrite && extractRoot) {
                using (ZipArchive archive = ZipFile.Open(pathSource, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name != string.Empty)
                        {
                            string newPath = System.IO.Path.Combine(pathTarget, entry.Name);
                            entry.ExtractToFile(newPath,false);
                        }
                    }
                }
            }
            else if (overWrite && !extractRoot)
            {
                if (Directory.Exists(pathSource))
                    Directory.Delete(pathSource);

                ZipFile.ExtractToDirectory(pathSource, pathTarget);
            }
            else if (overWrite && extractRoot) {
                using (ZipArchive archive = ZipFile.Open(pathSource, ZipArchiveMode.Read))
                {                    
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name != string.Empty)
                        {
                            string newPath = System.IO.Path.Combine(pathTarget, entry.Name);
                            entry.ExtractToFile(newPath, true);
                        }
                    }
                }
            }
        }
    }
}


