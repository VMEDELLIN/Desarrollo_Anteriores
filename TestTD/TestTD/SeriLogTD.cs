using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTD
{
    public static class SeriLogTD
    {
        
        private static string driveLetter = "D";
        private static string logDirectory = "Log";        
        private static string logFileName = "TransferDirecto_Log";
        public static DateTime currentTime = DateTime.Now;
        public static Logger Logger;
        private static LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch();
        public static void setLogClass(string fileName, string pathFile, LogEventLevel level)
        {            
            logDirectory = pathFile;
            logFileName = fileName;
            levelSwitch.MinimumLevel = level;
            Write("Log", "INICIO", "*****************INICIANDO LOG*********************", LogEventLevel.Information);
            //ConfigureLogging();
        }
        public static void LogDispose()
        {
            Write("Log", "FIN", "*****************FINALIZANDO LOG*********************", LogEventLevel.Information);
            Logger.Dispose();
            Log.CloseAndFlush();            
        }
        public static void setLogDrive(string drive)
        {            
            driveLetter = drive;
            //ConfigureLogging();
        }
        public static void setLogEventLevel(LogEventLevel level)
        {            
            levelSwitch.MinimumLevel = level;
        }
        static void ConfigureLogging()
        {
            string logFilePath = GetLogFilePath(DateTime.Now);            
            
            if (logFilePath != null)
            {
                
                    if (!File.Exists(logFilePath) || Logger == null)
                    {
                        

                        Logger = new LoggerConfiguration()
                                   //.MinimumLevel.Is(logLevel)
                                   .MinimumLevel.ControlledBy(levelSwitch)
                                   .WriteTo.File(
                                       //new RenderedCompactJsonFormatter(new Serilog.Formatting.Json.JsonValueFormatter("{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")), // Utiliza el formato compacto para los registros                        
                                       logFilePath,
                                       //rollingInterval: RollingInterval.Day, // Genera un nuevo archivo cada día
                                       rollOnFileSizeLimit: true, // Cambiar de archivo cuando se alcance un tamaño límite
                                       retainedFileCountLimit: null, // Límite de archivos antiguos a retener
                                       outputTemplate: "{{\"TM\":{Timestamp:HH:mm:ss.fff}, \"Type\": {Level:u3}, \"Message\":{Message:lj}{Exception}}{NewLine}"// Plantilla de salida
                                                                                                                                                               //outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}"// Plantilla de salida
                                                                                                                                                               //,restrictedToMinimumLevel: logLevel
                                     )
                                   .CreateLogger();
                }
            }
        }
        static string GetLogFilePath(DateTime _currentTime)
        {
            //DateTime currentTime = DateTime.Now;
            
            string monthDirectoryName = currentTime.ToString("yyMM");
            string directory = string.Empty;

            if (!DriveExists($"{driveLetter}:\\"))
                driveLetter = "C";

            directory = $"{driveLetter}:\\{logDirectory}\\{monthDirectoryName}";
            
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            if (!directoryInfo.Exists)
                directoryInfo.Create();            

            string fileName = $"{string.Format("{0:ddMMyy}", currentTime)}_{logFileName}.txt";
            string logFilePath = Path.Combine(directory, fileName);

            return logFilePath;
        }

        static bool DriveExists(string rootPath)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                if (drive.Name.Equals(rootPath, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        
        public static string GetMessageFormat(int id, string location, string info)
        {
            return GetMessageFormat(id.ToString(), location, info);
        }
        public static string GetMessageFormat(string id, string location, string info)
        {
            return $"{{\"Id\": {id}, \"Location\": {location }, \"Info\": {info}}}";
        }

        #region Verbose
        public static void Verbose(int id, string location, string info)
        {
            Verbose(id.ToString(), location, info);
        }
        public static void Verbose(string id, string location, string info)
        {
            Verbose(GetMessageFormat(id, location, info));
        }
        private static void Verbose(string message)
        {
            WriteLog(message, LogEventLevel.Verbose);
        }
        #endregion Verbose

        #region Debug
        public static void Debug(int id, string location, string info)
        {
            Debug(id.ToString(), location, info);
        }
        public static void Debug(string id, string location, string info)
        {
            Debug(GetMessageFormat(id, location, info));
        }
        private static void Debug(string message)
        {
            WriteLog(message, LogEventLevel.Debug);
        }
        #endregion Debug

        #region Information
        public static void Information(int id, string location, string info)
        {
            Information(id.ToString(), location, info);
        }
        public static void Information(string id, string location, string info)
        {
            Information(GetMessageFormat(id, location, info));
        }
        private static void Information(string message)
        {            
            WriteLog(message, LogEventLevel.Information);
        }
        #endregion Information

        #region Warning
        public static void Warning(int id, string location, string info)
        {
            Warning(id.ToString(), location, info);
        }
        public static void Warning(string id, string location, string info)
        {
            Warning(GetMessageFormat(id, location, info));
        }
        private static void Warning(string message)
        {
            WriteLog(message, LogEventLevel.Warning);

        }
        #endregion Warning

        #region Error
        public static void Error(int id, string location, string info)
        {
            Error(id.ToString(), location, info);
        }
        public static void Error(string id, string location, string info)
        {
            Error(GetMessageFormat(id, location, info));
        }
        private static void Error(string message)
        {
            WriteLog(message, LogEventLevel.Error);

        }
        #endregion Error

        #region Fatal
        public static void Fatal(int id, string location, string info)
        {
            Fatal(id.ToString(), location, info);
        }
        public static void Fatal(string id, string location, string info)
        {
            Fatal(GetMessageFormat(id, location, info));
        }
        private static void Fatal(string message)
        {
            WriteLog(message, LogEventLevel.Fatal);
        }
        #endregion Fatal

        #region Write
        public static void Write(int id, string location, string info, LogEventLevel level)
        {
            Write(id.ToString(), location, info, level);
        }
        public static void Write(string id, string location, string info, LogEventLevel level)
        {
            Write(GetMessageFormat(id, location, info), level);
        }
        private static void Write(string message, LogEventLevel level)
        {
            WriteLog(message, LogEventLevel.Fatal);
        }
        #endregion Write
        private static void WriteLog( string Message, LogEventLevel level) {
           
            ConfigureLogging();
           
            switch (level)
            {
                case LogEventLevel.Verbose:
                    Logger.Verbose(Message);
                    break;
                case LogEventLevel.Debug:
                    Logger.Debug(Message);
                    break;
                case LogEventLevel.Information:
                    Logger.Information(Message);                    
                    break;
                case LogEventLevel.Warning:
                    Logger.Warning(Message);
                    break;
                case LogEventLevel.Error:
                    Logger.Error(Message);
                    break;
                case LogEventLevel.Fatal:
                    Logger.Fatal(Message);
                    break;
                default:
                    Logger.Write(level,Message);
                    break;
            }
        }
    }
}
