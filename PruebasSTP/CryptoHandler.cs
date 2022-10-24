using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PruebasSTP
{
    public class CryptoHandler
    {
        public string filename = ConfigurationManager.AppSettings["rutaCert"];
        public string password = ConfigurationManager.AppSettings["passwordCert"];
        public bool debug = true;
        /*
                public String firma(String cadena)
                {
                    Chilkat.Rsa rsa = new Chilkat.Rsa();
                    //Creación del objeto .pfx/.p12
                    Chilkat.Pfx pfx = new Chilkat.Pfx();
                    bool success = pfx.LoadPfxFile(filename, password);
                    if (success != true)
                    {
                        Debug.WriteLine(pfx.LastErrorText);
                        return "";
                    }
                    //Se parametriza en Default la llave privada.
                    Chilkat.PrivateKey privKey = pfx.GetPrivateKey(0);
                    if (pfx.LastMethodSuccess != true)
                    {
                        Debug.WriteLine(pfx.LastErrorText);
                        return "";
                    }
                    //Se importa la llave privada en el componente RSA:
                    success = rsa.ImportPrivateKeyObj(privKey);
                    if (success != true)
                    {
                        Debug.WriteLine(rsa.LastErrorText);
                        return "";
                    }
                    rsa.EncodingMode = "base64";
                    rsa.LittleEndian = false;
                    string strData = cadena;
                    string base64Sig = rsa.SignStringENC(strData, "sha-256");
                    Debug.WriteLine(base64Sig);
                    Debug.WriteLine("Success!");
                    return base64Sig;
                }
                */
        public string sign(string ordenPago)
        {
            string cadenaOriginal = ordenPago;
            if (debug)
            {
                Console.WriteLine("Cadena original: " + cadenaOriginal);
            }
            X509Certificate2 x509 = new X509Certificate2(filename, password);
            RSACryptoServiceProvider rsaProvider = (RSACryptoServiceProvider)x509.PrivateKey;
            SHA256 hasher = SHA256CryptoServiceProvider.Create();
            byte[] hashValue = rsaProvider.SignData(System.Text.Encoding.UTF8.GetBytes(cadenaOriginal), hasher);
            string signature = System.Convert.ToBase64String(hashValue);
            if (debug)
            {
                Console.WriteLine("Firma: " + signature);
            }
            return signature;
        }

        public String cadenaOriginal(DatosRetornarOrden oPW)
        {
            StringBuilder sB = new StringBuilder();
            sB.Append("||");
            sB.Append(oPW.fechaOperacion == null ? "" : oPW.fechaOperacion.Trim()).Append("|");
            sB.Append(oPW.institucionOperante == null ? "" : oPW.institucionOperante.Trim()).Append("|");
            sB.Append(oPW.claveRastreo == null ? "" : oPW.claveRastreo.Trim()).Append("|");
            sB.Append(oPW.monto == null ? "" : oPW.monto.Trim()).Append("|");
            sB.Append(oPW.digitoIdentificadorBeneficiario == null ? "" : oPW.digitoIdentificadorBeneficiario.Trim()).Append("|");
            sB.Append(oPW.claveRastreoDevolucion == null ? "" : oPW.claveRastreoDevolucion.Trim()).Append("|");
            sB.Append(oPW.medioEntrega == null ? "" : oPW.medioEntrega.Trim()).Append("||");
            String cadena = sB.ToString();
            Console.WriteLine("Cadena original: " + cadena);
            return new CryptoHandler().sign(cadena);
        }

    }
}
