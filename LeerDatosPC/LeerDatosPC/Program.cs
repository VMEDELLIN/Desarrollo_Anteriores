using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LeerDatosPC
{
    class Program
    {
        private static string m_CertificateRoot = @"C:/Server/Certificate/WesternUnion.cer";
        private static string m_CertificateFile = @"C:/Server/Certificate/pi-client.pfx";
        private static string m_CertificatePassword = @"BAS$!@S87";
        private static string m_PathTemplate = @"Template.HTML";
        private static string m_EmailMonitor = @"";
        private static string m_ChannelName = "ESP";
        private static string m_ChannelVersion = "9500";
        static void Main(string[] args)
        {

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("vmedellin@redefectiva.com", "Valentin", System.Text.Encoding.UTF8);//Correo de salida          

            correo.To.Add("vmedellin@redefectiva.com"); //Correo destino?
            correo.Subject = "Prueba"; //Asunto
            correo.Body = "Prueba de envio de correo"; //Mensaje del correo
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            Console.WriteLine("Host");
            string host=Console.ReadLine();
            smtp.Host = host.Trim();// "relay.dnsexit.com"; //Host del servidor de correo
            Console.WriteLine("Puerto");
            string pr= Console.ReadLine();
            smtp.Port = Convert.ToInt32(pr.Trim()); //25; //Puerto de salida
            Console.WriteLine("Usuario");
            string us= Console.ReadLine();
            Console.WriteLine("Pass");
            string ps = Console.ReadLine();
            smtp.Credentials = new System.Net.NetworkCredential(us.Trim(), ps.Trim());
            //smtp.Credentials = new System.Net.NetworkCredential("frankrochin", "Estrategia2008");//Cuenta de correo
            //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            smtp.Send(correo);


            ////NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            ////String sMacAddress = string.Empty;
            ////foreach (NetworkInterface adapter in nics)
            ////{
            ////    if (sMacAddress == String.Empty)// only return MAC Address from first card
            ////    {
            ////        IPInterfaceProperties properties = adapter.GetIPProperties();
            ////        sMacAddress = adapter.GetPhysicalAddress().ToString();
            ////    }
            ////}
            ////Console.WriteLine(sMacAddress);
            ////Console.ReadLine();

            //try
            //{
            //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //    X509Certificate2 Cert = new X509Certificate2(m_CertificateFile, m_CertificatePassword);
            //    Console.WriteLine("Paso 1");
            //    WUService.WesternUnion_Service_H2H Server = new WUService.WesternUnion_Service_H2H();
            //    Server.ClientCertificates.Add(Cert);
            //    Console.WriteLine("Paso 2");
            //    WUService.feeinquiryrequest Request = new WUService.feeinquiryrequest();
            //    WUService.feeinquiryreply Reply = new WUService.feeinquiryreply();
            //    Console.WriteLine("Paso 3");
            //    Request.channel = new WUService.channel();
            //    Request.channel.type = WUService.channel_type.H2H;
            //    Request.channel.typeSpecified = true;
            //    Request.channel.name = m_ChannelName;
            //    Request.channel.version = m_ChannelVersion;
            //    Console.WriteLine("Paso 4");
            //    Reply = Server.FeeInquiry(Request);
            //    Console.WriteLine("Paso 5");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message+" => "+ (ex.InnerException!=null?ex.InnerException.ToString():""));
            //    Console.ReadLine();

            //}

        }
    }
}
