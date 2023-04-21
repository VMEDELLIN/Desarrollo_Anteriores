using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Web;

namespace WALLET
{
    public static class LogClass
    {
        // Configuracion Log de Windows
        private static EventLog eventLog = new EventLog();

        public static string eventLogName = "RedEfectiva";  //  Raiz de log RedEfectiva
        public static string eventSource = "Default App";   //  Nombre de la Aplicacion

        private static DirectoryInfo directoryInfo;
        private static FileStream fileStream;
        private static StreamWriter streamWriter;
        private static StackTrace stackTrace;
        private static MethodBase methodBase;

        private static object logLock = new object();

        private static bool DIRECTORY = false;
        private static bool m_EnableEventLog = false;

        private static int m_DebugLevel = 0;

        private static string driveLetter = "D";
        private static string folderName = "Log";
        private static string fileName;
        private static string fName = "RedEfectiva_Log";


        public static bool EnableEventLog
        {
            set { m_EnableEventLog = value; }
            get { return m_EnableEventLog; }
        }

        public static int DebugLevel
        {
            set { m_DebugLevel = value; }
            get { return m_DebugLevel; }
        }

        public static void setSource(string appName)
        {
            eventSource = appName;
            checkEventLog();
        }

        public static void setLogClass(string nameFile, string pathFile)
        {
            folderName = pathFile;
            fName = nameFile;
        }

        public static void setLogClass(string nameFile, string pathFile, int debug)
        {
            folderName = pathFile;
            fName = nameFile;
            DebugLevel = debug;
        }

        public static void setLogClass(string nameFile, string pathFile, int debug, string logName)
        {
            folderName = pathFile;
            fName = nameFile;
            DebugLevel = debug;
            eventSource = logName;
        }

        public static void setNameFile(string nameFile)
        {
            folderName = "Log";
            fName = (nameFile.Equals("")) ? ("Default.txt") : (nameFile);
        }

        public static void setLogDrive(string drive)
        {
            driveLetter = drive;
        }

        private static void checkEventLog()
        {
            //eventLog.Log = "Application";
            if (EnableEventLog)
            {
                if (!System.Diagnostics.EventLog.SourceExists(eventSource))
                {
                    System.Diagnostics.EventLog.CreateEventSource(eventSource, eventLogName);
                }
                eventLog.Log = eventLogName;
                eventLog.Source = eventSource;
            }
            //eventLog.Log = eventLogName;
        }

        private static void checkDir()
        {
            checkDrive();
            fileName = string.Format("{0:ddMMyy}", DateTime.Now) + "_" + fName + ".txt";



            directoryInfo = new DirectoryInfo(driveLetter + ":\\" + folderName + "\\" + string.Format("{0:yyMM}", DateTime.Now));

            try
            {
                if (!directoryInfo.Exists)
                {
                    directoryInfo = Directory.CreateDirectory(directoryInfo.FullName);
                }

                DIRECTORY = true;
            }
            catch (Exception ex)
            {
                DIRECTORY = false;
                LogInfo(ex);
            }

            fileName = (DIRECTORY) ? (driveLetter + ":\\" + folderName + "\\" + string.Format("{0:yyMM}", DateTime.Now) + "\\" + fileName) : fileName;
        }

        private static void checkDrive()
        {
            if (!Directory.Exists(@driveLetter + ":\\"))
            {
                driveLetter = "C";
            }
        }

        async private static void Info(string[] lines)
        {
            //fileName = string.Format("{0:yyMMdd}", DateTime.Now) + "_" + fName;

            checkDir();

            string user = string.Empty;

            //Check for existence of logger file
            if (File.Exists(fileName))
            {
                try
                {
                    fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write);

                    streamWriter = new StreamWriter(fileStream);

                    //string val = DateTime.Now.ToString() + " " + info.ToString();

                    //streamWriter.WriteLine(val);

                    //string[] lines = null;
                    foreach (string line in lines)
                    {
                        streamWriter.WriteLine(string.Format("{0:HH:mm:ss.fff}", DateTime.Now) + " " + line);
                    }


                    //System.IO.File.WriteAllLines(fileName,char[

                }
                catch (DirectoryNotFoundException ex)
                {
                    LogInfo(ex);
                }
                catch (FileNotFoundException ex)
                {
                    LogInfo(ex);
                }
                catch (PathTooLongException ex)
                {
                    LogInfo(ex);
                }
                catch (ArgumentException ex)
                {
                    LogInfo(ex);
                }
                catch (SecurityException ex)
                {
                    LogInfo(ex);
                }
                catch (Exception Ex)
                {
                    LogInfo(Ex);
                }
                finally
                {
                    Dispose();
                }
            }
            else
            {

                //If file doesn't exist create one
                try
                {

                    //directoryInfo = Directory.CreateDirectory(directoryInfo.FullName);

                    fileStream = File.Create(fileName);

                    streamWriter = new StreamWriter(fileStream);

                    foreach (string line in lines)
                    {
                        streamWriter.WriteLine(string.Format("{0:HH:mm:ss.fff}", DateTime.Now) + " " + line);
                    }

                    streamWriter.Close();

                    fileStream.Close();

                }
                catch (FileNotFoundException fileEx)
                {
                    LogInfo(fileEx);
                }
                catch (DirectoryNotFoundException dirEx)
                {
                    LogInfo(dirEx);
                }
                catch (Exception ex)
                {
                    LogInfo(ex);
                }
                finally
                {
                    Dispose();
                }

            }
        }

        async private static void Info(Object info)
        {

            //fileName = string.Format("{0:yyMMdd}", DateTime.Now) + "_" + fName;

            checkDir();

            string user = string.Empty;

            //Check for existence of logger file
            if (File.Exists(fileName))
            {
                try
                {
                    fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write);

                    streamWriter = new StreamWriter(fileStream);

                    string val = string.Format("{0:HH:mm:ss.fff}", DateTime.Now) + " " + info.ToString();

                    streamWriter.WriteLine(val);


                }
                catch (DirectoryNotFoundException ex)
                {
                    LogInfo(ex);
                }
                catch (FileNotFoundException ex)
                {
                    LogInfo(ex);
                }
                catch (PathTooLongException ex)
                {
                    LogInfo(ex);
                }
                catch (ArgumentException ex)
                {
                    LogInfo(ex);
                }
                catch (SecurityException ex)
                {
                    LogInfo(ex);
                }
                catch (Exception Ex)
                {
                    LogInfo(Ex);
                }
                finally
                {
                    Dispose();
                }
            }
            else
            {

                //If file doesn't exist create one
                try
                {

                    //directoryInfo = Directory.CreateDirectory(directoryInfo.FullName);
                    //fileName = "C:\\Log\\test.txt";
                    //fileName = "C:\\Red Efectiva\\\\Interface\\0627\\110627_int_activacion.txt";
                    fileStream = File.Create(fileName);

                    streamWriter = new StreamWriter(fileStream);

                    String val1 = string.Format("{0:HH:mm:ss.fff}", DateTime.Now) + " " + info.ToString();

                    streamWriter.WriteLine(val1);

                    streamWriter.Close();

                    fileStream.Close();

                }
                catch (FileNotFoundException fileEx)
                {
                    LogInfo(fileEx);
                }
                catch (DirectoryNotFoundException dirEx)
                {
                    LogInfo(dirEx);
                }
                catch (Exception ex)
                {
                    LogInfo(ex);
                }
                finally
                {
                    Dispose();
                }

            }
        }



        public static void LogInfo(Exception ex)
        {

            //fileName = string.Format("{0:yyMMdd}", DateTime.Now) + "_" + fName;

            checkDir();

            try
            {

                //Writes error information to the log file including name of the file, line number & error message description

                stackTrace = new StackTrace(ex, true);

                string fileNames = stackTrace.GetFrame((stackTrace.FrameCount - 1)).GetFileName();

                //fileNames = fileNames.Substring(fileNames.LastIndexOf(Application.ProductName));

                Int32 lineNumber = stackTrace.GetFrame((stackTrace.FrameCount - 1)).GetFileLineNumber();

                methodBase = stackTrace.GetFrame((stackTrace.FrameCount - 1)).GetMethod();    //These two lines are respnsible to find out name of the method

                String methodName = methodBase.Name;

                Info("Error in , Method name is " + methodName + ", at line number " + lineNumber.ToString() + " ,Error Message," + ex.Message);

            }
            catch (Exception genEx)
            {
                Info(ex.Message);
                LogClass.LogInfo(genEx);
            }
            finally
            {
                Dispose();
            }
        }

        public static void LogInfo(string[] messages)
        {
            try
            {
                //Write general message to the log file
                Info(messages);
            }
            catch (Exception genEx)
            {
                string[] lines = { genEx.Message };
                Info(lines);
            }

        }

        public static void LogInfo(string messages)
        {
            lock (logLock)
            {
                try
                {
                    //Write general message to the log file
                    Info(messages);
                }
                catch (Exception genEx)
                {
                    Info(genEx.Message);
                }
            }

        }

        public static void LogInfo(string id, string messages)
        {
            lock (logLock)
            {
                try
                {
                    //Write general message to the log file
                    Info(string.Concat(id, " ", messages));
                }
                catch (Exception genEx)
                {
                    Info(genEx.Message);
                }
            }

        }

        public static void LogInfo(string messages, int debug)
        {
            if (debug <= DebugLevel)
            {
                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(messages);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        public static void LogInfo(string id, string messages, int debug)
        {
            if (debug <= DebugLevel)
            {
                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(string.Concat(id, " ", messages));
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        async public static void LogInfo(string id, string location, string info)
        {
            int debug = 0;

            checkEventLog();

            string eventMesage = id + " -> " + location + "\n\n" + info;
            EventLog(eventMesage, EventLogEntryType.Information, 2);

            if (debug <= DebugLevel)
            {
                string Mesage = id + " @ " + location + " -> " + info;

                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(Mesage);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        public static void LogInfo(int id, string location, string info)
        {
            int debug = 0;

            checkEventLog();

            string eventMesage = id + " -> " + location + "\n\n" + info;
            EventLog(eventMesage, EventLogEntryType.Information, 2);

            if (debug <= DebugLevel)
            {
                string Mesage = id + " @ " + location + " -> " + info;

                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(Mesage);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        public static void LogInfo(string id, string location, string info, int debug)
        {
            checkEventLog();

            if (debug == 0) { EventLog(info, EventLogEntryType.Information, 2); }
            if (debug <= DebugLevel)
            {
                string Mesage = id + " @ " + location + " -> " + info;

                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(Mesage);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        public static void LogInfo(int id, string location, string info, int debug)
        {
            checkEventLog();

            if (debug == 0) { EventLog(info, EventLogEntryType.Information, 2); }
            if (debug <= DebugLevel)
            {
                string Mesage = string.Concat(id, " @ ", location, " -> ", info);

                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(Mesage);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        public static void LogError(string info)
        {
            int debug = 0;

            checkEventLog();

            string eventMesage = info;

            EventLog(eventMesage, EventLogEntryType.Error, 2);

            if (debug <= DebugLevel)
            {
                string Mesage = info;

                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(Mesage);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        public static void LogError(string id, string location, string info)
        {
            int debug = 0;

            checkEventLog();

            string eventMesage = id + " -> " + location + "\n\n" + info;

            EventLog(eventMesage, EventLogEntryType.Error, 2);

            if (debug <= DebugLevel)
            {
                string Mesage = id + " Error @ " + location + " -> " + info;

                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(Mesage);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        public static void LogError(int id, string location, string info)
        {
            int debug = 0;

            checkEventLog();

            string eventMesage = id + " -> " + location + "\n\n" + info;

            EventLog(eventMesage, EventLogEntryType.Error, 2);

            if (debug <= DebugLevel)
            {
                string Mesage = id + " Error @ " + location + " -> " + info;

                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(Mesage);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        public static void LogError(string id, string location, int idMsg, string msgDesc, string info)
        {
            int debug = 0;

            checkEventLog();

            string eventMesage = id + " -> " + location + "\n\n" + "Error: " + idMsg + " -> " + msgDesc + "\n" + info;

            EventLog(eventMesage, EventLogEntryType.Error, 2);
            if (debug <= DebugLevel)
            {
                string Mesage = id + " Error @ " + location + " -> " + "Error: " + idMsg + " -> " + msgDesc + "\n" + info;

                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(Mesage);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }

        public static void LogError(string id, Exception e)
        {
            int debug = 0;

            checkEventLog();

            string eventMesage = id + "->  " + e.Message + "\n\n" + e.StackTrace;

            EventLog(eventMesage, EventLogEntryType.Error, 2);

            if (debug <= DebugLevel)
            {
                string Mesage = id + " Error @ " + e.Message + "\n\n" + e.StackTrace + "\n\n" + e.InnerException;

                lock (logLock)
                {
                    try
                    {
                        //Write general message to the log file
                        Info(Mesage);
                    }
                    catch (Exception genEx)
                    {
                        Info(genEx.Message);
                    }
                }
            }
        }


        private static void EventLog(string eventMesage, EventLogEntryType Type, int eventID)
        {
            if (EnableEventLog)
            {
                eventLog.WriteEntry(eventMesage, Type, eventID);
            }
        }

        private static void Dispose()
        {
            if (directoryInfo != null)
                directoryInfo = null;

            if (streamWriter != null)
            {
                streamWriter.Close();
                streamWriter.Dispose();
                streamWriter = null;
            }
            if (fileStream != null)
            {
                fileStream.Dispose();
                fileStream = null;
            }
            if (stackTrace != null)
                stackTrace = null;
            if (methodBase != null)
                methodBase = null;
        }
    }
}