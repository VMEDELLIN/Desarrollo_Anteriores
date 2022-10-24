using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompress
{
    public interface IEstrategiaDecompress
    {
        //void Decompress(string pathSource, string pathTarget);
        //void Decompress(string pathSource, string pathTarget, bool overWrite);       
        void Decompress(string pathSource, string pathTarget, bool overWrite,bool extractRoot);
    }
}


