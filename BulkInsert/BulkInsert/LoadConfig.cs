using Common;
using Common.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert
{
    public class LoadConfig
    {

        public LoadConfig(bool EnableEventLog, int DebugLevel, string LogPath, string LogName)
        {
            LogClass.EnableEventLog = EnableEventLog;
            LogClass.DebugLevel = DebugLevel;
            LogClass.setLogClass(LogName, LogPath);

            LogClass.LogInfo("ConfigLog", "IniciaLog", "Se inicia el log para la descarga de transacciones", 5);
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
