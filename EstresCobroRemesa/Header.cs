using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstresCobroRemesa
{
    public class Header
    {
        private int _idAgente = 0;
        private int _idAgencia = 0;
        private string _Token = string.Empty;
        private string _Caja = string.Empty;
        private string _Ticket = string.Empty;
        private string _CveOperador = string.Empty;

        public int idAgente
        {
            get { return _idAgente; }
            set { _idAgente = value; }
        }
        public int idAgencia
        {
            get { return _idAgencia; }
            set { _idAgencia = value; }
        }
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        public string Caja
        {
            get { return _Caja; }
            set { _Caja = value; }
        }
        public string Ticket
        {
            get { return _Ticket; }
            set { _Ticket = value; }
        }
        public string CveOperador
        {
            get { return _CveOperador; }
            set { _CveOperador = value; }
        }

        public string GetString()
        {
            return string.Concat("|idAgente|", idAgente, "|idAgencia|", idAgencia, "|Token|", Token, "|Caja|", Caja, "|Ticket|", Ticket, "|CveOperador|", CveOperador);
        }
    }
}
