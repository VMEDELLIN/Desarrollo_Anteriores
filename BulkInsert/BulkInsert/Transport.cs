using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert
{
    public class Transport
    {
        public List<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }
        private List<Message> messages;
        public IList Model
        {
            get { return this.model; }
            set { this.model = value; }
        }
        private IList model;
        public bool Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        private bool status;
        public int ErrorCode
        {
            get { return this.errorCode; }
            set { this.errorCode = value; }
        }
        private int errorCode;
        public string ErrorMessage
        {
            get { return this.errorMessage; }
            set { this.errorMessage = value; }
        }
        private string errorMessage;
        public Transport()
            : this(false, new List<object>())
        {
        }

        public Transport(bool status, Message onlyMessage)
            : this(status, new List<object>(), onlyMessage)
        {
        }

        public Transport(bool status, IList model)
            : this(status, model, new List<Message>())
        {
        }

        public Transport(bool status, string messageDescription)
            : this(status, new List<object>(), new Message(messageDescription))
        {
        }

        public Transport(bool status, IList model, Message message)
            : this(status, model, new List<Message>())
        {
            this.Messages.Add(message);
        }

        public Transport(bool status, IList model, List<Message> messages)
        {
            this.Status = status;
            this.Model = model;
            this.Messages = messages;
        }

        //public Transport(bool status, IList model, string messageDescription)
        //    : this(status, model, new Message(messageDescription))
        //{
        //}

        public string MessagesToString()
        {
            string str = "";
            if (this.Messages == null)
            {
                return "null";
            }
            for (int i = 0; i < this.Messages.Count; i++)
            {
                str = str + this.Messages[i].ToString();
            }
            return str;
        }

        public void SetMessage(Message message)
        {
            this.Messages.Add(message);
        }

        public void SetMessage(string messageDescription, TypeMessage type)
        {
            this.Messages.Add(new Message(type, messageDescription));
        }

        public void SetOneModel(object obj)
        {
            this.Model.Add(obj);
        }

    }
    public class ErrorApi
    {
        public bool Success { get; set; }
        public string Message { get; set; }

    }
}
