using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLNegras.Models
{
    public class Operation
    {
        public int IdOperacion { get; set; }
        public string Referencia { get; set; }
        public string AutorizacionCobro { get; set; }
        public decimal Monto { get; set; }
        public decimal ComisionAgente { get; set; }
        public decimal ComisionUsuario { get; set; }
        public decimal ComisionNeta { get; set; }
        public int IdEmisor { get; set; }
        public string Emisor { get; set; }
        public int IdAgente { get; set; }
        public string Agente { get; set; }
        public int IdAgencia { get; set; }
        public string Agencia { get; set; }
        public string Ticket { get; set; }
        public DateTime FechaCobro { get; set; }
        public int IdEstatus { get; set; }
        public string Estatus { get; set; }
        public int IdOperador { get; set; }
        public string Operador { get; set; }

        public string AutorizacionCenlacion { get; set; }
        public DateTime FechaCancela { get; set; }

        public string Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

    }
    public class InfoOperation {
        public string Referencia { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public int IdEstatus { get; set; }
    }
}
