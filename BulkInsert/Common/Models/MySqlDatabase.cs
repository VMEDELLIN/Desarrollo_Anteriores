using System;
using Microsoft.Win32;
using System.Threading;
using MySql.Data.MySqlClient;

namespace Common.Models
{
    public class MySqlDatabase
    {
        #region Declaracion de Variables

        private string _connectionString = string.Empty;
        private string _Host = string.Empty;
        private string _UserName = string.Empty;
        private string _Password = string.Empty;
        private string _DefaultSchema = string.Empty;

        private int _Port = 3306;
        private int _MaxConnections = 300;
        private int _TimeoutConnection = 10;

        private int _Id = 0;
        private int _Status = 0;
        private int _LocationId = 0;
        private int _EnableWrite = 0;
        private int _Priority = 0;

        #endregion

        #region Funciones Privadas

        private void setString()
        {
            //_connectionString = "Data Source=" + _Host + ";Port=" + _Port + ";Database=" + _DefaultSchema + ";User ID=" + _UserName + ";Password=" + _Password + ";Max Pool Size=" + _MaxConnections + ";Connect Timeout=" + _TimeoutConnection;
            //_connectionString = "Data Source=" + _Host + ";Port=" + _Port + ";Database=" + _DefaultSchema + ";User ID=" + _UserName + ";Password=" + _Password + ";Connect Timeout=" + _TimeoutConnection + ";Min Pool Size=10" + ";Max Pool Size=" + _MaxConnections + ";ConnectionLifeTime=60";
            _connectionString = "Data Source=" + _Host + ";Port=" + _Port + ";Database=" + _DefaultSchema + ";User ID=" + _UserName + ";Password=" + _Password + ";Connect Timeout=" + _TimeoutConnection + ";Min Pool Size=10" + ";ConnectionLifeTime=60";
            //_connectionString = "Data Source=" + _Host + ";Port=" + _Port + ";Database=" + _DefaultSchema + ";User ID=" + _UserName + ";Password=" + _Password + ";Connect Timeout=" + _TimeoutConnection + ";Pooling=false";
        }

        #endregion

        #region Funciones Publicas

        //public MySqlDatabase(string Host, int port, string User, string Pass, string dbName, int MaxConnections, int TimeoutConnection)
        //{
        //    Set(Host, port, User, Pass, dbName, MaxConnections, TimeoutConnection);
        //}

        public bool Set(string Host, int port, string User, string Pass, string dbName, int MaxConnections, int TimeoutConnection)
        {
            if (MaxConnections > 0)
            {
                _MaxConnections = MaxConnections;
            }

            if (TimeoutConnection > 0)
            {
                _TimeoutConnection = TimeoutConnection;
            }

            if (String.IsNullOrWhiteSpace(Host.Trim()))
            {
                return false;
            }

            if (port == 0)
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(User.Trim()))
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(Pass.Trim()))
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(dbName.Trim()))
            {
                return false;
            }

            _Host = Host.Trim();
            _UserName = User.Trim();
            _Password = Pass.Trim();
            _DefaultSchema = dbName.Trim();
            _Port = port;

            setString();

            //return checkConnection();

            return true;
        }
        public MySqlConnection GetConnection()
        {
            setString();

            // Create a new SQL connection using the provider's connection string.
            MySqlConnection connection = new MySqlConnection(_connectionString);
            // Return the connection to the 
            return connection;
        }
        public bool checkConnection()
        {
            bool Result = false;

            setString();

            MySqlConnection conn = GetConnection();

            string sqlQuery = "SHOW DATABASES";

            using (conn)
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = conn.CreateCommand();

                    command.CommandText = "SHOW DATABASES";

                    IAsyncResult DBResult = command.BeginExecuteNonQuery();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);

                    command.EndExecuteNonQuery(DBResult);

                    Result = true;

                    LogClass.LogInfo("System", "Database:checkConnection", "Reader Closed", 3);
                }
                catch (Exception e)
                {
                    Result = false;

                    LogClass.LogError("System", "Database:checkConnection", "Exception: " + e.Message + "\n\nSql Query: " + sqlQuery);
                }
                finally
                {
                    conn.Close();
                    LogClass.LogInfo("System", "Database:checkConnection", "Conn Closed", 3);
                    conn.Dispose();
                    LogClass.LogInfo("System", "Database:checkConnection", "Conn Disposed", 3);
                }
            }
            return Result;
        }

        #endregion

        #region Metodos de Acceso

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        public string Host
        {
            get { return _Host; }
            set { _Host = value; }
        }
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string DefaultSchema
        {
            get { return _DefaultSchema; }
            set { _DefaultSchema = value; }
        }
        public int MaxConnections
        {
            get { return _MaxConnections; }
            set { _MaxConnections = value; }
        }
        public int TimeoutConnection
        {
            get { return _TimeoutConnection; }
            set { _TimeoutConnection = value; }
        }
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public int LocationId
        {
            get { return _LocationId; }
            set { _LocationId = value; }
        }
        public int EnableWrite
        {
            get { return _EnableWrite; }
            set { _EnableWrite = value; }
        }
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }
        public bool Enabled
        {
            get { return (_Status.Equals(0)) ? (true) : (false); }
            set { _Status = (value) ? (0) : (1); }
        }

        #endregion
    }
}
