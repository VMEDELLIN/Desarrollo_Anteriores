using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompress
{
    public abstract class ContextDecompress
    {
        private readonly IEstrategiaDecompress IDecompress;
        private protected ContextDecompress(IEstrategiaDecompress iDecompress) {
            IDecompress = iDecompress;
        }
        /// <summary>
        /// Extrae el contenido del paquete tal cual se encuentre y sobre escribe el contenido en el destino 
        /// </summary>
        /// <param name="pathSource">Origen del paquete</param>
        /// <param name="pathTarget">Destino del paquete</param>
        public void Decompress(string pathSource, string pathTarget)
        {
            Decompress(pathSource, pathTarget,false);
        }
        /// <summary>
        /// Extrae el contenido del paquete tal cual se encuentre indicando si debe o no sobre escribir el contenido en el destino.
        /// </summary>
        /// <param name="pathSource">Origen del paquete</param>
        /// <param name="pathTarget">Destino del paquete</param>
        /// <param name="overWrite">Sobre escribir contenido</param>
        public void Decompress(string pathSource, string pathTarget, bool overWrite)
        {
            Decompress(pathSource, pathTarget, overWrite,false);
        }
        /// <summary>
        /// Extrae el contenido del paquete y pone el contenido de las carpetas principales dentro de la raiz del destino.
        /// </summary>
        /// <param name="pathSource">Origen del paquete</param>
        /// <param name="pathTarget">Destino del paquete</param>
        /// <param name="overWrite">Sobre escribir archivos</param>
        /// <param name="extractRoot">Extraer a la raiz</param>
        public void Decompress(string pathSource, string pathTarget, bool overWrite, bool extractRoot)
        {
            IDecompress.Decompress(pathSource, pathTarget, overWrite, extractRoot);
        }
    }
}
