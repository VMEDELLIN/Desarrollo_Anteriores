using Common;
using Common.Models;
using Microsoft.Win32;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert
{
    class Program
    {

        private static string registryPatDataBaseTD = "SOFTWARE\\Transfer Directo\\MaxiTransferDatabase";
        private static LoadConfig oLoadConfig = null;
        private static ConfigDB oConfigDB = null;
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al sistema de prueba MySqlBulkCopy");
            oLoadConfig = new LoadConfig(false, 5, @"Log\TransferDirecto\WorkServiceEjercicio", "TDWorkerService_MaxiTransferNotificaciones_Ejercicio");


            Console.WriteLine("Cargando configuraciones...");
            oConfigDB = oLoadConfig.LoadConfigDataBaseTD(registryPatDataBaseTD);


            SaveBulkTransactionAction oSaveBulkCopyOfTransactionAction = new SaveBulkTransactionAction(oConfigDB);
            DataTable dataTableTransactionAction = oSaveBulkCopyOfTransactionAction.GetTable();

            dataTableTransactionAction.Rows.Add(2610000002, 1,null,null,null,null,null,false,1, DateTime.Now, 1,DateTime.Now);
            //dataTableTransactionAction.Rows.Add(2610000002, 99, null, null, null, null, null, false, 1, DateTime.Now, 1, DateTime.Now);
            Transport oTransport = BulkUpdate(1, dataTableTransactionAction);
        }

        public static Transport BulkUpdate(int Id, DataTable dataTableTransactionAction)
        {
            Console.WriteLine("Iniciando BulkInsert...");
            MySqlConnection connection = null;
            MySqlTransaction transaction = null;
            MySqlCommand dbCmd = null;
            Transport oTransport = null;
            SaveBulkTransactionAction oSaveBulkCopyOfTransactionAction = null;
            try
            {
                oTransport = new Transport();
                LogClass.LogInfo(Id, "UpdateBulkLoaderAction", "Inicia proceso de actualización de acciones de las transacciones por lotes", 5);
                oSaveBulkCopyOfTransactionAction = new SaveBulkTransactionAction(oConfigDB);
                string connectionString = oConfigDB.Database.ConnectionString;
                connectionString = connectionString + ";AllowLoadLocalInfile=true";


                string tbl = $"CREATE TEMPORARY TABLE td_administracion.inf_transaction_action_mt_temp(";

                tbl += "nIdReference BIGINT(20)  NOT NULL,";
                tbl += "nIdActionCode INT NOT NULL     ,";
                tbl += "dFecDateOfPayment DATETIME  NULL DEFAULT CURRENT_TIMESTAMP,";
                tbl += "nBranchCode INT,";
                tbl += "sBeneficiaryId VARCHAR(15),";
                tbl += "nBeneficiaryIdType INT,";
                tbl += "sNotes VARCHAR(150),";
                tbl += "bNotifiedToMaxi tinyint(1)  NOT NULL DEFAULT 1,";
                tbl += "nIdUsuarioAlta INT  NOT NULL,";
                tbl += "dFecAlta DATETIME NOT NULL ,";
                tbl += "nIdUsuarioNotified int(11) DEFAULT NULL,";
                tbl += "dFecNotified  DATETIME DEFAULT NULL";
                tbl += ") ENGINE = INNODB;";


                if (dataTableTransactionAction.Rows.Count > 0)
                {
                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    dbCmd = new MySqlCommand(connection, transaction);
                    dbCmd.CommandType = CommandType.Text;
                    dbCmd.CommandText = tbl;
                    dbCmd.ExecuteNonQuery();

                    MySqlBulkCopy bulkCopyTransactionAction = new MySqlBulkCopy(connection, transaction);
                    bulkCopyTransactionAction.DestinationTableName = "td_administracion.inf_transaction_action_mt_temp";
                    foreach (DataColumn cl in dataTableTransactionAction.Columns)
                        bulkCopyTransactionAction.ColumnMappings.Add(new MySqlBulkCopyColumnMapping(cl.Ordinal, cl.ColumnName));

                    LogClass.LogInfo(Id, "SaveBulkCopyAction", $"Inicia BulkCopy a td_administracion.inf_transaction_action_mt", 5);
                    Console.WriteLine("Ejecutando BulkInsert...");
                    MySqlBulkCopyResult resTransactionAction = bulkCopyTransactionAction.WriteToServer(dataTableTransactionAction);
                    LogClass.LogInfo(Id, "SaveBulkCopyAction", $"Finaliza BulkCopy a td_administracion.inf_transaction_action_mt", 5);
                    Console.WriteLine("Ejecución correcta.");

                    if (resTransactionAction.RowsInserted == dataTableTransactionAction.Rows.Count)
                    {
                        Console.WriteLine("Iniciando actualización masiva...");
                        string uppdate = string.Empty;
                        uppdate = $" UPDATE td_administracion.inf_transaction_action_mt T ";
                        uppdate += " INNER JOIN td_administracion.inf_transaction_action_mt_temp S ON S.nIdReference = T.nIdReference ";
                        uppdate += " AND S.nIdActionCode = T.nIdActionCode ";
                        uppdate += " AND S.bNotifiedToMaxi = T.bNotifiedToMaxi ";
                        uppdate += " AND S.nIdUsuarioAlta = T.nIdUsuarioAlta ";
                        uppdate += " SET T.bNotifiedToMaxi = 1, ";
                        uppdate += " T.nIdUsuarioNotified = 1, ";
                        uppdate += " T.dFecNotified = NOW() ";
                        uppdate += " WHERE T.bNotifiedToMaxi = 0; DROP TABLE td_administracion.inf_transaction_action_mt_temp; ";

                        dbCmd.CommandText = uppdate;
                        Console.WriteLine("Ejecutando actualización masiva...");
                        int rows = dbCmd.ExecuteNonQuery();
                        Console.WriteLine("Ejecución correcta.");
                        Console.WriteLine($"{rows} afectados");
                        if (rows > 0)
                        {

                            Console.WriteLine("Aplicando commit.");
                            oTransport.Status = true;
                            transaction.Commit();
                            connection.Close();
                            LogClass.LogError(Id, "UpdateBulkLoaderAction", $"BulkLoader a td_administracion.dat_transaction_mt y td_administracion.inf_transaction_action_mt ejecutado correctamente");
                            Console.WriteLine("Fin.");
                        }
                        else
                        {
                            if (transaction != null)
                                transaction.Rollback();
                            LogClass.LogError(Id, "UpdateBulkLoaderAction", $"No se pudo almacenar la información de las transacciones en la base de datos");                            
                        }
                    }
                    else
                    {
                        if (transaction != null)
                            transaction.Rollback();
                        LogClass.LogError(Id, "UpdateBulkLoaderAction", $"No se pudo almacenar la información de las transacciones en la base de datos");
                    }
                }
            }
            catch (MySqlException me)
            {
                LogClass.LogError(Id, "SaveBulkCopyTransaction", $"MySqlException {me.Message}");
                Console.WriteLine($"MySqlException {me.Message}");
                Console.WriteLine($"MySqlException {me.StackTrace}");
                if (transaction != null)
                    transaction.Rollback();
            }
            catch (MySqlConversionException mec)
            {
                LogClass.LogError(Id, "SaveBulkCopyTransaction", $"MySqlConversionException {mec.Message}");
                Console.WriteLine($"MySqlConversionException {mec.Message}");
                Console.WriteLine($"MySqlConversionException {mec.StackTrace}");
                if (transaction != null)
                    transaction.Rollback();
            }
            catch (Exception ex)
            {
                LogClass.LogError(Id, "SaveBulkCopyTransaction", $"exception {ex.Message}");
                Console.WriteLine($"exception {ex.Message}");
                Console.WriteLine($"exception {ex.StackTrace}");
                if (transaction != null)
                    transaction.Rollback();
            }
            finally
            {
                if ((connection != null) && connection.State == ConnectionState.Open)
                    connection.Close();
            }
            Console.WriteLine($"Proceso finalizado.");

            Console.WriteLine($"Presione una tecla para salir");
            Console.ReadLine();

            return oTransport;
        }

        public ConfigDB LoadConfigDataBaseTD(string RegistryPath)
        {
            ConfigDB oConfigDB = null;

            int l_MaxConnections = 300;
            int l_TimeoutConnection = 20;
            if (string.IsNullOrEmpty(RegistryPath))
            {
                LogClass.LogError("Config", "Config:LoadFromRegistry->DataBaseTD", "Empty or Null RegistryPath");
            }
            else
            {

                RegistryKey Registro = Registry.LocalMachine;
                RegistryKey TDRegistry = null;

                try
                {
                    TDRegistry = Registro.OpenSubKey(RegistryPath);
                    if (TDRegistry == null)
                    {
                        LogClass.LogError("Config", "LoadFromRegistry->DataBaseTD", "Invalid Registry " + RegistryPath);
                    }
                    else
                    {
                        string _Host = Library.getRegistryValue(TDRegistry, "dbHost");
                        string _UserName = Library.getRegistryValue(TDRegistry, "dbUser");
                        string _Password = Library.getRegistryValue(TDRegistry, "dbPass");
                        string _DefaultSchema = Library.getRegistryValue(TDRegistry, "dbName");
                        int _Port = 0;
                        int _MaxConnections = 300;
                        int _TimeoutConnection = 20;
                        int.TryParse((Library.getRegistryValue(TDRegistry, "dbPort")), out _Port);
                        int.TryParse((Library.getRegistryValue(TDRegistry, "dbMaxCon")), out _MaxConnections);
                        int.TryParse((Library.getRegistryValue(TDRegistry, "dbTimeout")), out _TimeoutConnection);

                        _MaxConnections = (l_MaxConnections.Equals(0)) ? (300) : (l_MaxConnections);
                        _TimeoutConnection = (l_TimeoutConnection.Equals(0)) ? (60) : (l_TimeoutConnection);

                        oConfigDB = new ConfigDB();
                        oConfigDB.Database.Set(_Host, _Port, _UserName, _Password, _DefaultSchema, _MaxConnections, _TimeoutConnection);
                        oConfigDB.ConnectioCreate = true;
                    }
                }
                catch (Exception e)
                {
                    LogClass.LogError("Config", "LoadFromRegistry->DataBaseTD", e.Message);
                    LogClass.LogError("Config", "LoadFromRegistry->DataBaseTD", e.StackTrace);
                    oConfigDB = null;
                }
            }
            return oConfigDB;
        }
    }
}
