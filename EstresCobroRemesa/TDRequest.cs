using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstresCobroRemesa
{
    public class TDRequest
    {
        public Header Encabezado = new Header();

        public string RequestData = string.Empty;

        public string GetString()
        {
            return string.Concat(Encabezado.GetString(), "/r/n", RequestData);
        }
    }
}
