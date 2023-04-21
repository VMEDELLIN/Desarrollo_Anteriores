using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
namespace STP
{
    public class CryptoHandler_
    {
        //public string sign(string empresa,string cuenta)
        //{

        //    bool debug = true;
        //    string cadenaOriginal = originaString(empresa, cuenta);
        //    string path=System.IO.Directory.GetCurrentDirectory();
        //    DirectoryInfo di = new DirectoryInfo("../../");
        //    string ruta=Path.Combine(di.FullName, "certificados\\prueba.p12");
        //    return sign_(ruta, "TransferStp#4", cadenaOriginal);

        //    if (debug)
        //    {
        //        Console.WriteLine("Cadena original: " + cadenaOriginal);
        //    }
        //    X509Certificate2 x509 = new X509Certificate2(ruta, "TransferStp#4", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
        //    RSACryptoServiceProvider rsaProvider = (RSACryptoServiceProvider)x509.PrivateKey;
        //    SHA256 hasher = SHA256CryptoServiceProvider.Create();
        //    byte[] hashValue = rsaProvider.SignData(System.Text.Encoding.UTF8.GetBytes(cadenaOriginal), hasher);
        //    string signature = System.Convert.ToBase64String(hashValue);
        //    if (debug)
        //    {
        //        Console.WriteLine("Firma: " + signature);
        //    }
        //    return signature;
        //}
        //public string sign_(string fileName,string password, string cadenaOriginal)
        //{

        //    try
        //    {

        //        X509Certificate2 x509 = new X509Certificate2(fileName, password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
        //        RSACryptoServiceProvider rsaProvider = (RSACryptoServiceProvider)x509.PrivateKey;
        //        SHA256 hasher = SHA256CryptoServiceProvider.Create();
        //        byte[] hashValue = rsaProvider.SignData(System.Text.Encoding.UTF8.GetBytes(cadenaOriginal), "SHA1");
        //        string signature = System.Convert.ToBase64String(hashValue);

        //        return signature;
        //    }
        //    catch (CryptographicException ce)
        //    {

        //        throw new Exception($"Exception: {ce.Message}, \n StackTrace: {ce.StackTrace}, \n Data: {ce.Data}, \n FileName: {fileName}, \n Password: {password}. \n Target: {ce.TargetSite}");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception($"Exception: {e.Message}, \n StackTrace: {e.StackTrace}, \n Data: {e.Data}, \n FileName: {fileName}, \n Password: {password}");
        //    }


        //}
        //public string originaString(string empresa, string cuenta) {
        //    string cadenaoriginal = string.Empty;            
        //    cadenaoriginal = "|| "+ empresa + " | "+ cuenta + " |||";
        //    return cadenaoriginal;
        //}
        //}

        public string filename = string.Empty;
        public string password = "TransferStp#4";
        public bool debug = true;
        public CryptoHandler_()
        {
            DirectoryInfo di = new DirectoryInfo("../../");
            //string ruta = Path.Combine(di.FullName, "certificados\\prueba.p12");
            
            string ruta = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "certificados\\prueba.p12");
            filename = ruta;
        }

        public string FirmaConsultaSaldoCuenta(string empresa, string cuenta,string fecha)
        {
            string cadenaOriginal = CadenaOriginalConsultaSaldoCuenta(empresa,cuenta,fecha);
            if (debug)
            {
                Console.WriteLine("Cadena original: " + cadenaOriginal);
            }
            Console.WriteLine("X509Certificate2");


            //        var certificate2 = new X509Certificate2(filename, password,
            //X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            //        byte[] cipherbytes = ASCIIEncoding.ASCII.GetBytes(cadenaOriginal);
            //        RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate2.PublicKey.Key;
            //        byte[] cipher = rsa.Encrypt(cipherbytes, false);
            //        string da=Convert.ToBase64String(cipher);




            //byte[] originalData = System.Text.Encoding.UTF8.GetBytes(cadenaOriginal);
            //X509Certificate2 x509_ = new X509Certificate2(filename, password);
            ////RSACryptoServiceProvider RSAalg = (RSACryptoServiceProvider)x509_.PrivateKey;
            //RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            //RSAParameters Key = RSAalg.ExportParameters(true);
            //byte[] signedData = HashAndSignBytes(originalData, Key);
            //string signature = System.Convert.ToBase64String(signedData);
            //if (VerifySignedHash(originalData, signedData, Key))
            //{
            //    Console.WriteLine("The data was verified.");
            //}
            //else
            //{
            //    Console.WriteLine("The data does not match the signature.");
            //}


            X509Certificate2 x509 = new X509Certificate2(filename, password);
            RSACryptoServiceProvider rsaProvider = (RSACryptoServiceProvider)x509.PrivateKey;
            SHA256 hasher = SHA256CryptoServiceProvider.Create();
            byte[] hashValue = rsaProvider.SignData(System.Text.Encoding.UTF8.GetBytes(cadenaOriginal), hasher);
            string signature = System.Convert.ToBase64String(hashValue);


            //X509Certificate2 x509 = new X509Certificate2(filename, password);
            //Console.WriteLine(password);
            //RSACryptoServiceProvider rsaProvider = (RSACryptoServiceProvider)x509.PrivateKey;
            //Console.WriteLine("PrivateKey");
            //SHA256 hasher = SHA256CryptoServiceProvider.Create();
            //Console.WriteLine("Create");
            //byte[] hashValue = rsaProvider.SignData(System.Text.Encoding.UTF8.GetBytes(cadenaOriginal), SHA256.Create());
            //Console.WriteLine("SHA1");
            //string signature = System.Convert.ToBase64String(hashValue);
            //Console.WriteLine("ToBase64String");
            if (debug)
            {
                Console.WriteLine("Firma: " + signature);
            }
            Console.WriteLine("signature: "+ signature);
            return signature;
        }
        public static byte[] HashAndSignBytes(byte[] DataToSign, RSAParameters Key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the
                // key from RSAParameters.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                RSAalg.ImportParameters(Key);

                // Hash and sign the data. Pass a new instance of SHA256
                // to specify the hashing algorithm.
                return RSAalg.SignData(DataToSign, SHA256.Create());
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
        public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, RSAParameters Key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the
                // key from RSAParameters.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                RSAalg.ImportParameters(Key);

                // Verify the data using the signature.  Pass a new instance of SHA256
                // to specify the hashing algorithm.
                return RSAalg.VerifyData(DataToVerify, SHA256.Create(), SignedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }
        private String CadenaOriginalConsultaSaldoCuenta(string empresa, string cuenta, string fecha)
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
            Console.WriteLine("Cadena original: " + cadena);
            return cadena;
        }
    }
}