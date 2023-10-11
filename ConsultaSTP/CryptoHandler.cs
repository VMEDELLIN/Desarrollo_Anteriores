using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaSTP
{
    public class CryptoHandler
    {
        //private const Int32 CRYPT_ACQUIRE_USE_PROV_INFO_FLAG = 0x00000002;
        //private const Int32 CRYPT_ACQUIRE_COMPARE_KEY_FLAG = 0x00000004;

        public string filename = string.Empty; //ConfigurationManager.AppSettings["rutaCert"];
        public string password = string.Empty;//ConfigurationManager.AppSettings["passwordCert"];
        public bool debug = true;
        public CryptoHandler()
        {
            ////Transfer nueva
            string ruta = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "TransferNueva\\prueba.p12");
            filename = ruta;
            password = "TransferStp#4";

            ////Transfer anterior
            //ruta = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "TransferAnterior\\prueba.p12");
            //filename = ruta;
            //password = "12345678";



            ////Prod direc2global
            //ruta = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Direct2Prod\\llavePrivada.p12");
            //filename = ruta;
            //password = "D1r3ct2GlobalTp#4";
        }
        public string sign(string cadenaOriginal)
        {
           
            if (debug)
            {
                Console.WriteLine("Cadena original: " + cadenaOriginal);
            }
            //UnicodeEncoding ByteConverter = new UnicodeEncoding();

            X509Certificate2 certificate = new X509Certificate2(filename, password,X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
            var privateKey = certificate.PrivateKey as RSACryptoServiceProvider;
            //var privateKey = certificate.PrivateKey; .net core
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.FromXmlString(privateKey.ToXmlString(true));
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsaProvider);
            rsaFormatter.SetHashAlgorithm("SHA256");
            var sha256 = new SHA256Managed();
            var hashSignatureBytes= rsaFormatter.CreateSignature(sha256.ComputeHash(Encoding.UTF8.GetBytes(cadenaOriginal)));
            //string hashSignatureBase64 = Convert.ToBase64String(hashSignatureBytes);
            string signature = Convert.ToBase64String(hashSignatureBytes);
            //return hashSignatureBase64;

            //FormaClasica
            //X509Certificate2 x509 = new X509Certificate2(filename, password);
            //RSACryptoServiceProvider rsaProvider = (RSACryptoServiceProvider)x509.PrivateKey;
            //SHA256 hasher = SHA256CryptoServiceProvider.Create();
            ////byte[] hashValue = rsaProvider.SignData(System.Text.Encoding.UTF8.GetBytes(cadenaOriginal), HashAlgorithmName. CryptoConfig.MapNameToOID("SHA256"), RSASignaturePadding);
            //byte[] hashValue = rsaProvider.SignData(System.Text.Encoding.UTF8.GetBytes(cadenaOriginal), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            //string signature = System.Convert.ToBase64String(hashValue);
            if (debug)
            {
                Console.WriteLine("Firma: " + signature);
            }
            return signature;
        }
        public String cadenaOriginal(string empresa, string cuenta, string fecha)
        {
            StringBuilder sB = new StringBuilder();
            sB.Append("||");
            sB.Append(empresa == null ? "" : empresa.Trim());
            sB.Append("|");
            sB.Append(cuenta == null ? "" : cuenta.Trim());
            sB.Append("|");
            sB.Append(fecha == null ? "" : fecha.Trim());
            sB.Append("||");
            String cadena = sB.ToString();
            return new CryptoHandler().sign(cadena);
        }
    }
}
