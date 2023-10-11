using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert
{
    public class Message
    {
        public TypeMessage Type { get; set; }
        public int Code { get; set; }
        public DateTime DateMessage { get; set; }
        public string TextMessage { get; set; }
        public Message() : this(TypeMessage.INFORMATION, "")
        {
        }
        public Message(Exception exception) : this(TypeMessage.ERROR, exception.Message)
        {
        }
        public Message(string textMessage) : this(TypeMessage.INFORMATION, textMessage)
        {
        }
        public Message(TypeMessage type, string textMessage) : this(type, 0, textMessage)
        {
        }
        public Message(TypeMessage type, int code, string textMessage)
        {
            this.Type = type;
            this.Code = code;
            this.TextMessage = textMessage;
            this.DateMessage = DateTime.Now;
        }
        public override string ToString()
        {
            return string.Concat(new object[] { "Type: ", this.Type.ToString(), " , Code: ", this.Code, " , Message: ", this.TextMessage });
        }
    }
    public enum TypeMessage
    {
        INFORMATION = 0,
        CONFIRMATION = 1,
        WARNING = 2,
        ERROR = 3
    }
}
