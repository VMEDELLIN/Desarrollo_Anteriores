using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkServiceRun
{
    public interface IFileData
    {
        Task Create(string path);
    }
}
