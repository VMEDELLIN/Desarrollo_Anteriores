using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkServiceRun
{
    public class FileData : IFileData
    {
        public async Task Create(string path)
        {
            using (var sw=new StreamWriter(path))
            {
                await Task.Delay(1000);
                await sw.WriteAsync("Pato");
            }
        }
    }
}
