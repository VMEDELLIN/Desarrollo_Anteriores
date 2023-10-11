using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ObtenerTareas
{
    class Program
    {
        static string PathBase
        {
            get
            {
                string _PathBase = string.Empty;
                _PathBase = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
                return _PathBase;
            }
        }
        static void Main(string[] args)
        {
            
            Menu();
            Console.ReadKey();
            Environment.Exit(0);
        }
        static void Menu() {
            Console.WriteLine("Elige una opción");
            Console.WriteLine($"\n");
            Console.WriteLine("ET -Exportar Tareas");
            Console.WriteLine("IT -Importar Tareas");
            Console.WriteLine("ER -Exportar Regedit");
            //Console.WriteLine("IR -Importar Regedit");
            Console.WriteLine("INS -Instalar .msi");
            Console.WriteLine("CFG -Exportar Archivos .config");

            string comandp=Console.ReadLine();

            switch (comandp.ToUpper())
            {
                case "ET":
                    ExportTasksMenu(PathBase + "\\Tareas");
                    break;
                case "IT":
                    ImportTasksMenu(PathBase + "\\Tareas");
                    break;
                case "ER":
                    Console.WriteLine($"Exportando SOFTWARE\\Transfer Directo...");
                    

                    //Console.WriteLine($"Exportando SOFTWARE\\Transfer Directo...");
                    //ExportRegistryKey(RegistryHive.CurrentUser, @"SOFTWARE\Transfer Directo", PathBase+"\\Regedit");
                    //Console.WriteLine($"Exportando SOFTWARE\\TransferDirecto...");
                    //ExportRegistryKey(RegistryHive.CurrentUser, @"SOFTWARE\TransferDirecto", PathBase + "\\Regedit");

                    //Console.WriteLine($"Exportando SOFTWARE\\WOW6432Node\\Transfer Directo...");
                    //ExportRegistryKey(RegistryHive.CurrentUser, @"SOFTWARE\WOW6432Node\Transfer Directo", PathBase + "\\Regedit");
                    //Console.WriteLine($"Exportando SOFTWARE\\WOW6432Node\\TransferDirecto...");
                    //ExportRegistryKey(RegistryHive.CurrentUser, @"SOFTWARE\WOW6432Node\TransferDirecto", PathBase + "\\Regedit");
                    List<string> lst = new List<string>();

                    try
                    {
                        using (StreamReader reader = new StreamReader(PathBase + "\\Config\\Regedit.txt"))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                lst.Add(line);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }


                    //lst.Add(@"SOFTWARE\AquiMisPagos");
                    //lst.Add(@"SOFTWARE\Facturacion");
                    //lst.Add(@"SOFTWARE\MisPagos");
                    //lst.Add(@"SOFTWARE\MiSuerte");
                    //lst.Add(@"SOFTWARE\Nautilus");
                    //lst.Add(@"SOFTWARE\PayCash");
                    //lst.Add(@"SOFTWARE\PayCashOne");
                    //lst.Add(@"SOFTWARE\PayNau");
                    //lst.Add(@"SOFTWARE\Red Efectiva");
                    //lst.Add(@"SOFTWARE\Transfer Directo");
                    //lst.Add(@"SOFTWARE\TransferDirecto");
                    //lst.Add(@"SOFTWARE\ZeroPago");

                    //lst.Add(@"SOFTWARE\Wow6432Node\AquiMisPagos");
                    //lst.Add(@"SOFTWARE\Wow6432Node\Facturacion");                    
                    //lst.Add(@"SOFTWARE\Wow6432Node\PayCash");
                    //lst.Add(@"SOFTWARE\Wow6432Node\PayCashOne");                    
                    //lst.Add(@"SOFTWARE\Wow6432Node\Red Efectiva");
                    //lst.Add(@"SOFTWARE\Wow6432Node\RedEfectiva");
                    //lst.Add(@"SOFTWARE\Wow6432Node\Transfer Directo");
                    //lst.Add(@"SOFTWARE\Wow6432Node\TransferDirecto");
                    //lst.Add(@"SOFTWARE\Wow6432Node\ZeroPago");

                    foreach (var item in lst)
                    {
                        Console.Write($"**********Inicia Exportar {item}**********\n");
                        ExportRegistryKey(RegistryHive.CurrentUser, item, PathBase + "\\Regedit");
                        Console.Write($"**********Inicia Exportar {item}**********\n");
                        Console.Write($"\n\n");
                    }

                    Menu();
                    break;
                case "IR":
                    ImportRegistryFiles(PathBase + "\\Regedit");
                    break;
                case "INS":
                    InstallMsisInFolder(PathBase + "\\MSI");
                    break;
                case "CFG":                    
                    
                    string extension = ".config";
                    string[] carpetasProgramFiles = { @"C:\Program Files\Transfer Directo", @"C:\Program Files (x86)\Transfer Directo" };
                    foreach (string origen in carpetasProgramFiles)
                    {
                        string des = Path.Combine(PathBase, "ArchivosConfig") + $"{origen.Substring(2,origen.LastIndexOf('\\')-1)}";
                        CopiarArchivosConf(origen, des, extension);
                    }
                    Menu();
                    break;
                default:
                    Menu();
                    break;
            } 

            
        }

        #region TAREAS
        static void ExportTasksMenu(string exportPath) {
            
            
            Console.WriteLine("Iniciando proceso de exportación de tareas.....");
            Console.WriteLine("Desea proporcionar la ruta para depositar los XML? S=si, N=no");
            string er = Console.ReadLine();
            if (er.ToUpper() == "N")
            {
                Console.WriteLine($"Se depositaran en la ruta default {exportPath}");
                Console.WriteLine($"Desea continuar? S=si, N=no");
                string r = Console.ReadLine();
                if (r.ToUpper() == "S")
                {
                    ExportTasks(exportPath);
                }
                else
                {
                    Menu();
                }

            }
            else if (er.ToUpper() == "S")
            {
                Console.WriteLine($"Proporcione la ruta:");
                exportPath = Console.ReadLine();
                Console.WriteLine($"Desea continuar? S=si, N=no");
                string r = Console.ReadLine();
                if (r.ToUpper() == "S")
                {
                    ExportTasks(exportPath);
                }
                else
                {
                    Menu();
                }
            }
            else {
                Menu();
            }
        }
        static void ExportTasks(string exportPath)
        {
            using (TaskService taskService = new TaskService())
            {
                foreach (Task t in taskService.AllTasks)
                {
                    try
                    {
                        if (t.Enabled)
                        {
                            string taskFileName = Path.Combine(exportPath, $"{t.Name}.xml");
                            t.Export(taskFileName);
                            Console.WriteLine($"Trea {t.Name} Exportada");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Tarea {t.Name} no Exportada Error=> {ex.Message}");
                    }
                }
                //foreach (TaskFolder folder in taskService.RootFolder.SubFolders)
                //{
                //    ExportTasksFromFolder(folder, exportPath);
                //}
            }

            Console.WriteLine("Proceso de exportación de tareas finalizado");
            Console.WriteLine($"RUTA: { exportPath}");
            Console.WriteLine($"\n");
            Menu();
        }
        //static void ExportTasksFromFolder(TaskFolder folder, string exportPath)
        //{
        //    foreach (Task t in folder.Tasks)
        //    {
        //        try
        //        {
        //            if (t.Enabled)
        //            {
        //                if ((t.Definition.RegistrationInfo.Author == null) ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Software") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Google") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("McAffe") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("McAfee") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Office") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Proxy") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("User") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Microsoft") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Window") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Memory") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Drive") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Update") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Discovery") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Work") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Detalle") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Calibration") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("WiFi") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Usb") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Remote") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("System") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Synch") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Dell") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("Update") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("ManifestUpdate") ||
        //                    t.Definition.RegistrationInfo.Author.Contains("sql"))
        //                {
        //                    continue;
        //                }

        //                string taskFileName = Path.Combine(exportPath, folder.Name, $"{t.Name}.xml");
        //                Directory.CreateDirectory(Path.GetDirectoryName(taskFileName));
        //                t.Export(taskFileName);
        //                Console.WriteLine($"Trea {t.Name} Exportada");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Tarea {t.Name} no Exportada Error=> {ex.Message}");
        //        }                
        //    }

        //    foreach (TaskFolder subfolder in folder.SubFolders)
        //    {
        //        ExportTasksFromFolder(subfolder, exportPath);
        //    }
        //}
        static void ImportTasksMenu(string exportPath)
        {


            Console.WriteLine("Iniciando proceso de importación de tareas.....");
            Console.WriteLine("Desea proporcionar la ruta de los XML? S=si, N=no");
            string er = Console.ReadLine();
            if (er.ToUpper() == "N")
            {
                Console.WriteLine($"Se tomaran de la ruta default {exportPath}");
                Console.WriteLine($"Desea continuar? S=si, N=no");
                string r = Console.ReadLine();
                if (r.ToUpper() == "S")
                {
                    ImportTasks(exportPath);
                }
                else
                {
                    Menu();
                }

            }
            else if (er.ToUpper() == "S")
            {
                Console.WriteLine($"Proporcione la ruta:");
                exportPath = Console.ReadLine();
                Console.WriteLine($"Desea continuar? S=si, N=no");
                string r = Console.ReadLine();
                if (r.ToUpper() == "S")
                {
                    ImportTasks(exportPath);
                }
                else
                {
                    Menu();
                }
            }
            else
            {
                Menu();
            }
        }
        static void ImportTasks(string importPath)
        {
            string name = string.Empty;
            using (TaskService taskService = new TaskService())
            {
                foreach (string taskFilePath in System.IO.Directory.GetFiles(importPath, "*.xml"))
                {
                    try
                    {
                        TaskDefinition taskDefinition = taskService.NewTask();
                        taskDefinition.XmlText = System.IO.File.ReadAllText(taskFilePath);
                        name = taskDefinition.RegistrationInfo.URI.Substring(taskDefinition.RegistrationInfo.URI.LastIndexOf('\\')+1);
                        TaskFolder taskFolder = taskService.RootFolder; // You can change this to the appropriate folder

                        taskFolder.RegisterTaskDefinition(
                            System.IO.Path.GetFileNameWithoutExtension(taskFilePath),
                            taskDefinition,
                            TaskCreation.CreateOrUpdate,
                            null,
                            null,
                            TaskLogonType.InteractiveToken,
                            null);
                        
                        Console.WriteLine($"Tarea {name} Importada...");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Tarea {name} no Importada Error=> {ex.Message}");
                    }
                }
            }
        }
        #endregion TAREAS

        #region REGEDIT
        static void ExportRegistryKey(RegistryHive hive, string keyPath, string exportPath)
        {
            RegistryKey baseKey = Registry.LocalMachine;
            using (RegistryKey key = baseKey.OpenSubKey(keyPath))
            {
                try
                {
                    if (key != null)
                    {
                        string regContent = ExportSubkeysRecursively(key);
                        File.WriteAllText($"{exportPath}\\{keyPath.Replace('\\','_')}.reg", $"Windows Registry Editor Version 5.00\r\n\r\n{regContent}");                        
                        Console.WriteLine($"Regit {keyPath} Exportado");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Regit {keyPath} no Exportado Error=> {ex.Message}");
                    Menu();
                }
            }
        }
        static string ExportSubkeysRecursively(RegistryKey key, string indent = "")
        {
            
            string result = string.Empty;           
           

            result = $"{indent}[{key.Name}]\r\n";

                foreach (string valueName in key.GetValueNames())
                {
                    Console.WriteLine($"Key {key.Name} key.GetValueNames() {valueName}");
                    if (valueName == "")
                        continue;
                    string value = key.GetValue(valueName).ToString();
                    result += $"{indent}\"{valueName}\"=\"{value}\"\r\n";
                }

            foreach (string subKeyName in key.GetSubKeyNames())
            {
                Console.WriteLine($"key.GetSubKeyNames() {subKeyName}");
                using (RegistryKey subKey = key.OpenSubKey(subKeyName))
                {
                    if (subKey != null)                    
                    {
                        
                            Console.WriteLine($"subKey {subKey.Name}key.GetSubKeyNames() {subKeyName}");
                            result += $"\n";
                            result += ExportSubkeysRecursively(subKey, indent);
                       
                    }
                }
            }
            
            return result;
        }
        //static void ImportRegistryFiles(string importPath)
        //{
        //    //Process.Start("regedit.exe", $"/s \"{importPath}\\*.reg\"").WaitForExit();
        //    foreach (string taskFilePath in System.IO.Directory.GetFiles(importPath, "*.reg"))
        //    {
        //        try
        //        {
        //            Process.Start("regedit.exe", $"/s \"{taskFilePath}\"").WaitForExit();



        //            Console.WriteLine($"Regdit {taskFilePath} Importado...");
        //            Menu();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Regdit {taskFilePath} no Importada Error=> {ex.Message}");
        //            Menu();
        //        }
        //    }
        //}
        static void ImportRegistryFiles(string folderPath)
        {
            string[] regFiles = Directory.GetFiles(folderPath, "*.reg");

            foreach (string filePath in regFiles)
            {
                ImportRegistryFile(filePath);
            }
            Menu();
        }
        static void ImportRegistryFile(string filePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "regedit.exe",
                Arguments = $"/s \"{filePath}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process regeditProcess = new Process())
            {
                regeditProcess.StartInfo = startInfo;
                regeditProcess.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                regeditProcess.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

                regeditProcess.Start();
                regeditProcess.BeginOutputReadLine();
                regeditProcess.BeginErrorReadLine();
                regeditProcess.WaitForExit();
            }
            
        }
        #endregion REGEDIT

        #region MSI
        static void InstallMsisInFolder(string folderPath)
        {
            string[] msiFiles = Directory.GetFiles(folderPath, "*.msi");

            foreach (string msiFilePath in msiFiles)
            {
                //string productCode = GetProductCode(msiFilePath);

                //if (!string.IsNullOrEmpty(productCode))
                //{
                //    UninstallMsi(productCode);
                //}
                InstallMsi(msiFilePath);
            }
        }
        static string GetProductCode(string msiFilePath)
        {
            string productCode = null;

            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "SetupConector_BTS",
                    //Arguments = $"/i \"{msiFilePath}\" /L*v temp.txt",
                    Arguments = "/i \"" + msiFilePath + "\" /qn", // Argumentos para instalación silenciosa
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                int startIndex = output.IndexOf("Product Code:");
                if (startIndex >= 0)
                {
                    startIndex += "Product Code:".Length;
                    int endIndex = output.IndexOf(Environment.NewLine, startIndex);
                    if (endIndex > startIndex)
                    {
                        productCode = output.Substring(startIndex, endIndex - startIndex).Trim();
                    }
                }
            }

            return productCode;
        }
        static void UninstallMsi(string productCode)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "msiexec",
                Arguments = $"/x {productCode}",
                CreateNoWindow = true,
                UseShellExecute = false
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }
        }
        static void InstallMsi(string msiFilePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "msiexec",
                Arguments = "/i \"" + msiFilePath + "\" /qn", // Argumentos para instalación silenciosa
                CreateNoWindow = false,
                UseShellExecute = false
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }
        }
        #endregion MSI

        #region CONFIG
        static string GetRelativePath(string fromPath, string toPath)
        {
            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            return Uri.UnescapeDataString(relativeUri.ToString());
        }

        static void CopiarArchivosConf(string origen, string destino, string extension)
        {
            foreach (var origenArchivo in Directory.GetFiles(origen, "*" + extension, SearchOption.AllDirectories))
            {
                var relPath = GetRelativePath(origen, origenArchivo);
                var destinoArchivo = Path.Combine(destino, relPath);

                var destinoDirectorio = Path.GetDirectoryName(destinoArchivo);
                if (!Directory.Exists(destinoDirectorio))
                {
                    Directory.CreateDirectory(destinoDirectorio);
                }

                File.Copy(origenArchivo, destinoArchivo, true);
            }
        }
        #endregion

    }
}
