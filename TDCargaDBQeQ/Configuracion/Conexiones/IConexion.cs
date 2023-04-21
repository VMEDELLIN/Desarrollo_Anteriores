using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDCargaDBQeQ.Configuracion.Conexiones
{
    public interface IConexionRegedit
    {
        bool OptieneParametrosCnn(string registryPath);
    }
}
