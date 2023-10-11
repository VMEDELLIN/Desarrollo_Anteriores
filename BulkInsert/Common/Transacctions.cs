using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Linq;
using System.Text;
using Common.Models;
using Common.Class;

namespace Common
{
    public class FindCodeAndMessage
    {
        private string Id { get; set; }
        private string IssuerId { get; set; }
        private string ShippingKey { get; set; }
        public FindCodeAndMessage(string id, string issuerId, string shippingKey)
        {
            Id = id;
            IssuerId = issuerId;
            ShippingKey = shippingKey;
        }

        public string FindResponseIssuerById(MySqlDatabase Database)
        {
            string result = string.Empty;

            //MySqlConnection conn = new MySqlConnection(ConnectionString);
            MySqlConnection conn = Database.GetConnection();
            LogClass.LogInfo("Busqueda Mejorada", "Buscar mensaje de emisor", $"{ conn.ConnectionString } - { Id } - { IssuerId } - { ShippingKey }");
            MySqlCommand dbCmd;
            MySqlDataReader reader;

            string storedProcedure = $"CALL td_administracion.connector_FindResponseIssuerById( @Id, @IdIssuer, @ShippingKey );";
            try
            {
                using (conn)
                {
                    conn.Open();

                    dbCmd = new MySqlCommand(storedProcedure, conn);
                    dbCmd.Parameters.AddWithValue("@Id", Id);
                    dbCmd.Parameters.AddWithValue("@IdIssuer", IssuerId);
                    dbCmd.Parameters.AddWithValue("@ShippingKey", ShippingKey);

                    IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);

                    reader = dbCmd.EndExecuteReader(DBResult);
                    while (reader.Read())
                    {
                        result = $"{ reader.GetString("Codigo") }|{ reader.GetString("Mensaje") }|{ reader.GetString("IdEmisor") }";
                    }
                    reader.Close();
                    reader.Dispose();
                    reader.Close();
                }
            }
            catch (Exception err)
            {
                LogClass.LogInfo("Busqueda Mejorada", "Buscar mensaje de emisor", $"Error td_administracion.connector_AddResponseIssuer { err.Message } ");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                GC.Collect();
            }
            return result;
        }
    }
    public class Connector
    {
        private string Id { get; set; }
        private int IssuerId { get; set; }
        private int IdEntity { get; set; }
        private string ShippingKey { get; set; }
        private string Code { get; set; }
        private string Message { get; set; }
        private byte MessageIssuer { get; set; }
        private string Error { get; set; }

        public Connector(string id, int issuerId, int idEntity, string shippingKey, string code, string message, byte messageIssuer, string error)
        {
            Id = id;
            IssuerId = issuerId;
            IdEntity = idEntity;
            ShippingKey = shippingKey;
            Code = code;
            Message = message;
            MessageIssuer = messageIssuer;
            Error = error;
        }

        //public int SaveResult( IEnumerable< Transactions > dataToInsert )
        public void SaveResult(MySqlDatabase Database)
        {
            string result = string.Empty;
            MySqlConnection conn = Database.GetConnection();
            LogClass.LogInfo("Busqueda Mejorada", "Datos a insertar", $" { conn.ConnectionString }  { Id } - { IssuerId } - { ShippingKey } - { Code } - { Message } - { MessageIssuer }");
            // MySqlConnection conn = new MySqlConnection( ConnectionString );
            MySqlCommand dbCmd;
            MySqlDataReader reader;

            string storedProcedure = $"CALL td_administracion.connector_AddResponseIssuer( @Id, @IdIssuer, @IdEntity, @ShippingKey, @Code_, @Message, @MessageIssuer );";

            try
            {
                using (conn)
                {
                    conn.Open();

                    dbCmd = new MySqlCommand(storedProcedure, conn);
                    dbCmd.Parameters.AddWithValue("@Id", Id);
                    dbCmd.Parameters.AddWithValue("@IdIssuer", IssuerId);
                    dbCmd.Parameters.AddWithValue("@IdEntity", IdEntity);
                    dbCmd.Parameters.AddWithValue("@ShippingKey", ShippingKey);
                    dbCmd.Parameters.AddWithValue("@Code_", Code);
                    dbCmd.Parameters.AddWithValue("@Message", Message);
                    dbCmd.Parameters.AddWithValue("@MessageIssuer", MessageIssuer);

                    IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);

                    reader = dbCmd.EndExecuteReader(DBResult);
                    // reader.Read();
                    if (reader.Read())
                    {
                        result = reader.GetString("Result");
                        LogClass.LogInfo("Busqueda Mejorada", "Datos a insertar", $"Se inserto { result } ");
                    }
                    reader.Dispose();
                    reader.Close();
                }
            }
            catch (Exception err)
            {
                LogClass.LogInfo("Busqueda Mejorada", "Datos a insertar", $"Error td_administracion.connector_AddResponseIssuer { err.Message } ");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                GC.Collect();
            }
        }

        public void SaveCode(MySqlDatabase Database)
        {
            string result = string.Empty;
            MySqlConnection conn = Database.GetConnection();
            LogClass.LogInfo("Busqueda Mejorada", "Codigo a insertar", $" { conn.ConnectionString }  { IssuerId } - { ShippingKey } - { Code } - { Message } - { MessageIssuer }");
            // MySqlConnection conn = new MySqlConnection( ConnectionString );
            MySqlCommand dbCmd;
            MySqlDataReader reader;

            string storedProcedure = $"CALL td_administracion.connector_AddCodeErrorByIssuer( @IdIssuer, @IdEntity, @Code_, @Message, @MessageIssuer );";
            try
            {
                using (conn)
                {
                    conn.Open();

                    dbCmd = new MySqlCommand(storedProcedure, conn);
                    dbCmd.Parameters.AddWithValue("@IdIssuer", IssuerId);
                    dbCmd.Parameters.AddWithValue("@IdEntity", IdEntity);
                    dbCmd.Parameters.AddWithValue("@Code_", Code);
                    dbCmd.Parameters.AddWithValue("@Message", Message);
                    dbCmd.Parameters.AddWithValue("@MessageIssuer", MessageIssuer);

                    IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);

                    reader = dbCmd.EndExecuteReader(DBResult);
                    // reader.Read();
                    if (reader.Read())
                    {
                        result = reader.GetString("Result");
                        LogClass.LogInfo("Busqueda Mejorada", "Datos a insertar", $"Se inserto { result } ");
                    }
                    reader.Dispose();
                    reader.Close();
                }
            }
            catch (Exception err)
            {
                LogClass.LogInfo("Busqueda Mejorada", "Datos a insertar", $"Error td_administracion.connector_AddCodeErrorByIssuer { err.Message } ");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                GC.Collect();
            }
        }
    }

}
