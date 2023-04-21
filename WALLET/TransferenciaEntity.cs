using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WALLET
{
    public class TransferenciaEntity
    {
        public int FolioWallet { get; set; }
        public int nidRemitente { get; set; }
        //public int nFolio { get; set; }
        public int nResultado { get; set; }
        public int nTipo { get; set; }
        public int nEmisor { get; set; }
        public int nSecuencia { get; set; }
        public decimal dMonto { get; set; }
        public string sFecha { get; set; }
        public string sHora { get; set; }
        public string sAutorizacion { get; set; }
        public string sReferencia { get; set; }
        public string sValue { get; set; }
        public string dFechaCreacion { get; set; }
        public string dFechaConfirmation { get; set; }
        public string dFechaVencimiento { get; set; }
        public int nId { get; set; }
        public int nFechaOperacion { get; set; }
        public int nInstitucionOrdenante { get; set; }
        public int nInstitucionBeneficiaria { get; set; }
        public string sClaveRastreo { get; set; }
        public string sNombreOrdenante { get; set; }
        public int nTipoCuentaOrdenante { get; set; }
        public string sCuentaOrdenante { get; set; }
        public string sRfcCurpOrdenante { get; set; }
        public string sNombreBeneficiario { get; set; }
        public int nTipoCuentaBeneficiario { get; set; }
        public string sCuentaBeneficiario { get; set; }
        public string sNombreBeneficiario2 { get; set; }
        public string sTipoCuentaBeneficiario2 { get; set; }
        public string sCuentaBeneficiario2 { get; set; }
        public string sRfcCurpBeneficiario { get; set; }
        public string sConceptoPago { get; set; }
        public int nReferenciaNumerica { get; set; }
        public string sEmpresa { get; set; }
        public int nTipoPago { get; set; }
        public string sTsLiquidacion { get; set; }
        public string sFolioCodi { get; set; }
        public string key { get; set; }
        public decimal dMontoWallet { get; set; }
    }
}