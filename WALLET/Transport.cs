using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WALLET
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
    public class Maper
    {
        /// <summary>
        /// Convierte un DataReader a una Lista de entidades
        /// </summary>
        /// <typeparam name="T">entidad</typeparam>
        /// <param name="rowMapper">Clase con los atributos de la entidad</param>
        /// <param name="dataReader">DataReader a Mapear</param>
        /// <returns>Lista de la entidad</returns>
        public static List<T> DataReaderToList<T>(Mapeo rowMapper, IDataReader dataReader)
        {
            List<T> list = new List<T>();
            rowMapper.SetOrdinals(dataReader);
            int num = 0;
            while (dataReader.Read())
            {
                list.Add((T)rowMapper.MapRow(dataReader, num++));
            }
            return list;
        }
        /// <summary>
        /// Convierte un DataReader a una Lista de entidades
        /// </summary>
        /// <typeparam name="T">entidad</typeparam>
        /// <param name="rowMapper">Clase con los atributos de la entidad</param>
        /// <param name="dataReader">DataReader a Mapear</param>
        /// <returns>Una entidad</returns>
        public static T DataReaderToSingle<T>(Mapeo rowMapper, IDataReader dataReader)
        {
            List<T> list = new List<T>();
            rowMapper.SetOrdinals(dataReader);
            int num = 0;
            while (dataReader.Read())
            {
                list.Add((T)rowMapper.MapRow(dataReader, num++));
            }
            return list[0];
        }
        /// <summary>
        /// Función Crear un IDataReader a partir de una tabla
        /// </summary>
        public static Func<DataTable, IDataReader> CreaDataReader = (tabla) =>
        {
            return tabla.CreateDataReader();
        };
        /// <summary>
        /// Función crea una tabla a partir de DataRows
        /// </summary>
        public static Func<DataRow[], DataTable> CrearTabla = (rows) =>
        {
            return rows.AsEnumerable().CopyToDataTable();
        };
    }
    public class InfoAttribute : Attribute
    {
        private string dataName;
        public string DataName
        {
            get
            {
                return this.dataName;
            }
            set
            {
                if (!(this.dataName == value))
                {
                    this.dataName = value;
                }
            }
        }
        private string caption;
        public string Caption
        {
            get
            {
                return this.caption;
            }
            set
            {
                if (!(this.caption == value))
                {
                    this.caption = value;
                }
            }
        }
    }
    public class Mapeo
    {
        public Dictionary<string, string> PropertiesFields = null;
        public Dictionary<string, int> PropertiesOrdinals = null;
        private Type objectType;
        private List<string> columns = null;
        /// <summary>
        /// Obtiene la propiedades de la Entidad
        /// [InfoAttribute(DataName = "propiedad")]
        /// Ejemplo: int propiedad
        /// </summary>
        /// <param name="objectType">Entidad</param>
        public Mapeo(Type objectType)
        {
            this.objectType = objectType;
            this.PropertiesFields = GetPropertyField(objectType);
        }
        private static Dictionary<string, string> GetPropertyField(Type objType)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (PropertyInfo info in objType.GetProperties())
            {
                InfoAttribute attribute = FindFieldInfoAttribute(info);
                if (attribute != null)
                {
                    dictionary.Add(info.Name, attribute.DataName);
                }
                //dictionary.Add(info.Name, info.Name);
            }
            return dictionary;
        }
        private static InfoAttribute FindFieldInfoAttribute(PropertyInfo propertyInfo)
        {
            foreach (object obj2 in propertyInfo.GetCustomAttributes(true))
            {
                if (obj2 is InfoAttribute)
                {
                    return (InfoAttribute)obj2;
                }
            }
            return null;
        }
        public void SetOrdinals(IDataReader dataReader)
        {
            columns = new List<string>();
            for (int i = 0; i < dataReader.FieldCount; i++)
                columns.Add(dataReader.GetName(i));
            this.PropertiesOrdinals = new Dictionary<string, int>();
            foreach (KeyValuePair<string, string> pair in this.PropertiesFields)
            {
                if (pair.Value == null)
                {
                    throw new Exception("No se definio nombre de campo para la propiedad " + pair.Key + " en la clase" + this.objectType.ToString());
                }
                if (columns.Contains(pair.Value))
                    this.PropertiesOrdinals.Add(pair.Key, dataReader.GetOrdinal(pair.Value));
            }
        }

        public object MapRow(IDataReader dataReader, int rowNum)
        {
            object obj2 = null;
            if ((this.objectType == typeof(string)) || (this.objectType == typeof(string)))
            {
                obj2 = "";
            }
            else
            {
                obj2 = Activator.CreateInstance(this.objectType);
            }
            foreach (KeyValuePair<string, string> pair in this.PropertiesFields)
            {
                if (columns.Contains(pair.Value))
                {

                    int num;
                    this.PropertiesOrdinals.TryGetValue(pair.Key, out num);
                    object obj3 = dataReader.GetValue(num);
                    if (obj3.ToString() != "")
                    {
                        this.objectType.GetProperty(pair.Key).SetValue(obj2, obj3, null);
                    }
                    else
                    {
                        Type propertyType = this.objectType.GetProperty(pair.Key).PropertyType;
                        if (propertyType.Name == "Int32")
                        {
                            this.objectType.GetProperty(pair.Key).SetValue(obj2, -2147483648, null);
                        }
                        if (propertyType.Name == "Double")
                        {
                            this.objectType.GetProperty(pair.Key).SetValue(obj2, -1.7976931348623157E+308, null);
                        }
                        if (propertyType.Name == "String")
                        {
                            this.objectType.GetProperty(pair.Key).SetValue(obj2, string.Empty, null);
                        }
                        if (propertyType.Name == "Int16")
                        {
                            this.objectType.GetProperty(pair.Key).SetValue(obj2, (short)(-32768), null);
                        }
                    }
                }
            }
            return obj2;
        }
    }
}