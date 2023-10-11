using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMonitorTD.Model
{
    public class InfoOperation
    {
        public string Referencia { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public int IdEstatus { get; set; }
    }
}
