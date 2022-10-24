using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Envio_Correo_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //try
            //{
            //    MailMessage correo = new MailMessage();
            //    correo.From = new MailAddress("admin@paynau.com", "Valentin", System.Text.Encoding.UTF8);//Correo de salida          

            //    correo.To.Add("vmedellin@redefectiva.com"); //Correo destino?
            //    correo.Subject = "Prueba"; //Asunto
            //    correo.Body = "Prueba de envio de correo"; //Mensaje del correo
            //    correo.IsBodyHtml = true;
            //    correo.Priority = MailPriority.Normal;
            //    SmtpClient smtp = new SmtpClient();
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Host = "smtp.office365.com"; //Host del servidor de correo
            //    smtp.Port = 587; //Puerto de salida                
            //    smtp.Credentials = new System.Net.NetworkCredential("admin@paynau.com", "Septiembre2020");
            //    //smtp.Credentials = new System.Net.NetworkCredential("frankrochin", "Estrategia2008");//Cuenta de correo
            //    //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            //    smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            //    smtp.Send(correo);
            //    MessageBox.Show("Enviado", "AVISO");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "AVISO");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    MailMessage correo = new MailMessage();
            //    correo.From = new MailAddress(txtde.Text.Trim(), "Valentin", System.Text.Encoding.UTF8);//Correo de salida          

            //    correo.To.Add(txtpara.Text.Trim()); //Correo destino?
            //    correo.Subject = "Prueba"; //Asunto
            //    correo.Body = "Prueba de envio de correo"; //Mensaje del correo
            //    correo.IsBodyHtml = true;
            //    correo.Priority = MailPriority.Normal;
            //    SmtpClient smtp = new SmtpClient();
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Host =txthost.Text.Trim();// "relay.dnsexit.com"; //Host del servidor de correo
            //    smtp.Port = Convert.ToInt32(txtpuerto.Text.Trim()); //25; //Puerto de salida                
            //    smtp.Credentials = new System.Net.NetworkCredential(txtuser.Text.Trim(), txtpass.Text.Trim());
            //    //smtp.Credentials = new System.Net.NetworkCredential("frankrochin", "Estrategia2008");//Cuenta de correo
            //    //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            //    smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            //    smtp.Send(correo);
            //    MessageBox.Show("Enviado", "AVISO");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "AVISO");
            //}

            string path = Directory.GetCurrentDirectory() + "\\Log";
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                StreamWriter sw = new StreamWriter(path + "\\Log.txt");
                sw.WriteLine("Inicia");

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(txtde.Text.Trim(), "Valentin", System.Text.Encoding.UTF8);//Correo de salida          

                correo.To.Add(txtpara.Text.Trim()); //Correo destino?
                correo.Subject = "Prueba"; //Asunto
                correo.Body = "Prueba de envio de correo"; //Mensaje del correo
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = txthost.Text.Trim();// "relay.dnsexit.com"; //Host del servidor de correo
                smtp.Port = Convert.ToInt32(txtpuerto.Text.Trim()); //25; //Puerto de salida                
                smtp.Credentials = new System.Net.NetworkCredential(txtuser.Text.Trim(), txtpass.Text.Trim());
                //smtp.Credentials = new System.Net.NetworkCredential("frankrochin", "Estrategia2008");//Cuenta de correo
                //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                smtp.Send(correo);
                MessageBox.Show("Enviado", "AVISO");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AVISO");
            }
        }
    }
}
