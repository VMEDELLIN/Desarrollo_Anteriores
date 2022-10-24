using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronEstrategy
{
    public class ProcesarPayCashBanamex : PayCash
    {
        public ProcesarPayCashBanamex() :
            base(new LeerArchivo("")
                , new EnviarDataContable()
                , new BuscarDataContable()
                , new PasarMovPayCash())
        {
            
        }
        public void Run() {
            if (base.Leer()) { 

            }
        }
    }
    public class LeerArchivo : IEstrategiaLeerArchivo
    {
        private string RutaDirectorio;
        public LeerArchivo(string rutaDirectorio) {
            RutaDirectorio = rutaDirectorio;
        }
        public bool Leer()
        {
            return false;
        }

    }
    public class EnviarDataContable : IEstrategiaEnviarDataContable
    {
        public void Enviar()
        {
            throw new NotImplementedException();
        }
    }
    public class BuscarDataContable : IEstrategiaBuscarDataContable
    {
        public void Buscar()
        {
            throw new NotImplementedException();
        }
    }
    public class PasarMovPayCash : IEstrategiaPasarMovPayCash
    {
        public void PasarPayCash()
        {
            throw new NotImplementedException();
        }
    }
}
