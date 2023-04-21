using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WALLET
{
    public class RemitenteEntity
    {
        [InfoAttribute(DataName = "nIdRemitente", Caption = "nIdRemitente")]
        public int nIdRemitente { get; set; }
        [InfoAttribute(DataName = "nNumCuenta", Caption = "nNumCuenta")]
        public int nNumCuenta { get; set; }
    }
}