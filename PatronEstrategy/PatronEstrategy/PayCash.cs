using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronEstrategy
{
    public abstract class PayCash
    {
        private readonly IEstrategiaLeerArchivo ILeerArchivo;
        private readonly IEstrategiaEnviarDataContable IEnviarDataContable;
        private readonly IEstrategiaBuscarDataContable IBuscarDataContable;
        private readonly IEstrategiaPasarMovPayCash IPasarPayCash;

        private protected PayCash(IEstrategiaLeerArchivo iLeerArchivo, IEstrategiaEnviarDataContable iEnviarDataContable,IEstrategiaBuscarDataContable iBuscarDataContable,IEstrategiaPasarMovPayCash iPasarPayCash)
        {
            ILeerArchivo = iLeerArchivo;
            IEnviarDataContable = iEnviarDataContable;
            IBuscarDataContable = iBuscarDataContable;
            IPasarPayCash = iPasarPayCash;
        }
        public bool Leer() {
            return ILeerArchivo.Leer();
        }
        public void Enviar() {
            IEnviarDataContable.Enviar();
        }
        public void Buscar() {
            IBuscarDataContable.Buscar();
        }
        public void PasarPayCash() {
            IPasarPayCash.PasarPayCash();
        }
    }
}
