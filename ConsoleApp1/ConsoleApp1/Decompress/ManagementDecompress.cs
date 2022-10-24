using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompress
{
    public class ManagementDecompress: ContextDecompress
    {
        public ManagementDecompress(IEstrategiaDecompress iDecompress):base(iDecompress) { }
    }
}
