using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Common.Models
{
    public class Email
    {
        private string m_host = "smtp-auth.no-ip.com";
        private int m_port = 3325;

        private bool m_defaultCredentials = false;
        private bool m_EnableSSL = false;

        private string m_user = "redefectiva.com@noip-smtp";
        private string m_pass = "Estrategia2008";

        private string m_fromAddresss = "sistemas@transferdirecto.com";
        private string m_fromName = "Transfer Directo";

        private int m_timeout = 30000;

        private SmtpClient Client;
        private NetworkCredential login;

        private MailMessage eMessage = new MailMessage();

        public void setup()
        {
            login = new NetworkCredential(m_user, m_pass);

            Client = new SmtpClient(m_host, m_port);

            Client.UseDefaultCredentials = m_defaultCredentials;
            Client.Credentials = login;
            Client.Timeout = m_timeout;
            Client.EnableSsl = m_EnableSSL;

            eMessage.From = new MailAddress(m_fromAddresss, m_fromName, Encoding.UTF8);

            //eMessage.Subject = m_;
            eMessage.SubjectEncoding = Encoding.UTF8;

            //eMessage.Body = "";
            eMessage.BodyEncoding = Encoding.UTF8;

        }

        public bool sendMail()
        {
            setup();

            try
            {
                Client.Send(eMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }

            return true;
        }

        public bool addRecipient(string email, string name)
        {
            eMessage.To.Add(new MailAddress(email, name, Encoding.UTF8));

            return true;
        }

        public bool addRecipient(string email)
        {
            eMessage.To.Add(new MailAddress(email, email, Encoding.UTF8));

            return true;
        }

        public bool addMultipleRecipient(string multiAddress)
        {
            eMessage.To.Add(multiAddress);

            return true;
        }

        public bool addBcc(string email, string name)
        {
            eMessage.Bcc.Add(new MailAddress(email, name, Encoding.UTF8));

            return true;
        }

        public bool addBcc(string email)
        {
            eMessage.Bcc.Add(new MailAddress(email, email, Encoding.UTF8));

            return true;
        }

        public bool addMultipleBcc(string multiAddress)
        {
            eMessage.Bcc.Add(multiAddress);

            return true;
        }

        public bool addCC(string email, string name)
        {
            eMessage.CC.Add(new MailAddress(email, name, Encoding.UTF8));

            return true;
        }

        public bool addCC(string email)
        {
            eMessage.CC.Add(new MailAddress(email, email, Encoding.UTF8));

            return true;
        }

        public bool addMultipleCC(string multiAddress)
        {
            eMessage.CC.Add(multiAddress);

            return true;
        }


        public void ReplyTo(string email, string name)
        {
            //eMessage.ReplyTo = new MailAddress(email, name, Encoding.UTF8);
            eMessage.ReplyToList.Add(new MailAddress(email, name, Encoding.UTF8));
        }

        public void ReplyTo(string email)
        {
            //eMessage.ReplyTo = new MailAddress(email, email, Encoding.UTF8);
            eMessage.ReplyToList.Add(new MailAddress(email, email, Encoding.UTF8));
        }


        public void setAttachment(Attachment data)
        {
            eMessage.Attachments.Add(data);
        }

        public void setHeader(string name, string value)
        {
            eMessage.Headers.Add(name, value);
        }

        public string FromAddress
        {
            get { return m_fromAddresss; }
            set { m_fromAddresss = value; }
        }

        public string FromName
        {
            get { return m_fromName; }
            set { m_fromName = value; }
        }

        public string User
        {
            //get { return m_user; }
            set { m_user = value; }
        }

        public string Password
        {
            //get { return m_pass; }
            set { m_pass = value; }
        }

        public string Host
        {
            get { return m_host; }
            set { m_host = value; }
        }

        public int Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        public int Timeout
        {
            get { return m_timeout; }
            set { m_timeout = value; }
        }


        public string Subject
        {
            get { return eMessage.Subject; }
            set { eMessage.Subject = value; }
        }

        public MailPriority MailPriority
        {
            get { return eMessage.Priority; }
            set { eMessage.Priority = value; }
        }

        public string Body
        {
            get { return eMessage.Body; }
            set { eMessage.Body = value; }
        }

        public Encoding BodyEncoding
        {
            get { return eMessage.BodyEncoding; }
            set { eMessage.BodyEncoding = value; }
        }

        public bool HtmlBody
        {
            get { return eMessage.IsBodyHtml; }
            set { eMessage.IsBodyHtml = value; }
        }

        public DeliveryNotificationOptions Notifications
        {
            get { return eMessage.DeliveryNotificationOptions; }
            set { eMessage.DeliveryNotificationOptions = value; }
        }

        public bool EnableSSL
        {
            get { return m_EnableSSL; }
            set { m_EnableSSL = value; }
        }
    }
}
