using PatronEstrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejecuta
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProcesarPayCashBanamex banamex = new ProcesarPayCashBanamex();
            //banamex.Run();
        }
    }
    class CustomerDTO
    {
        [F("id")]
        public int? CustomerId;

        [Field("name")]
        public string CustomerName;
    }
}
