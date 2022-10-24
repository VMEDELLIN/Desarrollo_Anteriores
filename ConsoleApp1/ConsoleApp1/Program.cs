using Decompress;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using SharpCompress.Common.Rar;
using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string zipPath = @"C:\WORK\Utilerias\ZIP-RAR\ZIP\conciliaciones.zip";
            string rarPath = @"C:\WORK\Utilerias\ZIP-RAR\RAR\conciliaciones.rar";

            string extractZipPath = @"C:\WORK\Utilerias\ZIP-RAR\ZIP";
            string extractRarPath = @"C:\WORK\Utilerias\ZIP-RAR\RAR";


            string tempZipPath = Path.Combine(Directory.GetCurrentDirectory(), "TempUnzip");
            //ZipFile.ExtractToDirectory(zipPath, tempZipPath);

            // dirTemp = new DirectoryInfo(tempZipPath);
            //FileSystemInfo[] infos= dirTemp.GetFileSystemInfos();

            //FileInfo[] files= dirTemp.GetFiles();
            //DirectoryInfo[] dirs =dirTemp.GetDirectories();

            //foreach (FileSystemInfo item in infos)
            //{
            //    if (item.Attributes == FileAttributes.Archive)
            //    {
            //        FileInfo f = new FileInfo(item.FullName);
            //        f.CopyTo(extractZipPath, true);
            //    }
            //    else if (item.Attributes == FileAttributes.Directory)
            //    {
            //        DirectoryInfo d = new DirectoryInfo(item.FullName);

            //        foreach (var id in d.GetDirectories())
            //        {
            //            d.MoveTo(extractZipPath);
            //            d.Create();
            //        }
            //        foreach (var df in d.GetFiles())
            //            df.MoveTo(extractZipPath);
            //    }
            //}


            //DecompressZIP oDecZip = new DecompressZIP(zipPath, extractZipPath, true);
            //oDecZip.ExtractRoot = true;
            //ManagementDecompress oManDec = new ManagementDecompress(oDecZip);
            //oManDec.InitDecompress();


            //if (1 ==(int) packetType.ZIP)
            //{
            //    ContextDecompress2 oDecompress = new ContextDecompress2(new DecompressZIP());
            //    oDecompress.Decompress(zipPath, extractZipPath, true);
            //}

            //if (2 == (int)packetType.RAR)
            //{
            //    ContextDecompress2 oDecompress = new ContextDecompress2(new DecompressRAR());
            //    oDecompress.Decompress(zipPath, extractZipPath, true);
            //}


            ////if (1 == (int)packetType.ZIP)
            ////{
            ////    ManagementDecompress oDecompress = new ManagementDecompress(new DecompressZIP());
            ////    oDecompress.Decompress(zipPath, extractZipPath, true);
            ////}

            ////if (2 == (int)packetType.RAR)
            ////{
            ////    ManagementDecompress oDecompress = new ManagementDecompress(new DecompressRAR());
            ////    oDecompress.Decompress(zipPath, extractZipPath, true);
            ////}

            //using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Read))
            //{
            //    foreach (ZipArchiveEntry entry in zip.Entries)
            //    {
            //        if (entry.Name == string.Empty && entry.Length == 0)
            //            if (!Directory.Exists(Path.Combine(extractZipPath, entry.FullName)))
            //                Directory.CreateDirectory(Path.Combine(extractZipPath, entry.FullName));

            //        entry.ExtractToFile(Path.Combine(extractZipPath,entry.FullName, entry.Name), true);
            //    }
            //}


            //ZipArchive zip= ZipFile.Open(zipPath, ZipArchiveMode.Read);
            //zip.ExtractToDirectory(extractZipPath);

            // ZipFile.ExtractToDirectory(zipPath,extractZipPath);

            // ... Get the ZipArchive from ZipFile.Open.
            //using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Read))
            //{
            //    // ... Loop over entries.
            //    foreach (ZipArchiveEntry entry in archive.Entries)
            //    {
            //        if (entry.Name != string.Empty)
            //        {
            //            string newPath = System.IO.Path.Combine(extractZipPath, entry.Name);
            //            entry.ExtractToFile(newPath, true);
            //        }
            //    }
            //}



            //RarArchive rar = RarArchive.Open(rarPath);
            //IReader s = rar.ExtractAllEntries();
            //ExtractionOptions x = new ExtractionOptions();
            //x.Overwrite = true;
            ////WriteAllToDirectory(s, extractRarPath, x);
            //while (s.MoveToNextEntry())
            //{
            //    FileMode fm = FileMode.Create;
            //    s.WriteEntryToFile(extractRarPath + "\\nuevo.txt", x);
            //    //s.WriteEntryToDirectory(extractRarPath, x);
            //}

            //Console.ReadLine();
            //using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            //{
            //    archive.CreateEntryFromFile(newFile, "NewEntry.txt");                
            //    archive.ExtractToDirectory(extractPath);
            //}
        }
        //static void WriteAllToDirectory(this IReader reader, string destinationDirectory,
        //                                 ExtractionOptions options)
        //{
        //    while (reader.MoveToNextEntry())
        //    {
        //        reader.WriteEntryToDirectory(destinationDirectory, options);
        //    }
        //}



    }
        //internal static IEnumerable<RarArchiveEntry> GetEntries(RarArchive archive,
        //                                                IEnumerable<RarVolume> rarParts)
        //{
        //    foreach (var groupedParts in GetMatchedFileParts(rarParts))
        //    {
        //        yield return new RarArchiveEntry(archive, groupedParts);
        //    }
        //}
    
}

