using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstresCobroRemesa
{
    public class TDResponse
    {
        public Header Encabezado = new Header();

        public int Folio = 0;
        public int ErrorCode = 1;
        public int ComisionAgente = 0;
        public string ErrorMsg = string.Empty;
        public string Autorizacion = string.Empty;
        public string ResponseData = string.Empty;
        public DataType ResponseDataType = DataType.None;
        public string[] ResponseMessage = null;

        public enum DataType
        {
            None,
            RemesaInfo,
            UsuarioInfo
        };
    }
}
