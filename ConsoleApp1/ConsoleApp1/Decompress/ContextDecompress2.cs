using Decompress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompress
{
    public class ContextDecompress2
    {
        private readonly IEstrategiaDecompress IDecompress;

        public ContextDecompress2(IEstrategiaDecompress iDecompress) {
            this.IDecompress = iDecompress;
        }
        public void Decompress(string pathSource, string pathTarget)
        {
            Decompress(pathSource, pathTarget);
        }

        public void Decompress(string pathSource, string pathTarget, bool overWrite)
        {
            Decompress(pathSource, pathTarget, overWrite);
        }

        public void Decompress(string pathSource, string pathTarget, bool overWrite, bool extractRoot)
        {
            Decompress(pathSource, pathTarget, overWrite, extractRoot);
        }
    }
    public enum packetType { 
        ZIP=1,
        RAR=2
    }
}
