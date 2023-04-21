using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBWorkerService
{
    public interface IFileData
    {
        Task Create(string path);
    }
}
