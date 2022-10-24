using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObtenerTareas
{
    class Program
    {
        static string dir
        {
            get
            {
                string d = string.Empty;
                d = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
                return d;
            }
        }
        static void Main(string[] args)
        {
            string nameFile = string.Empty;
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                nameFile = Path.Combine(dir, "Tareas_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HH_mm")) + ".txt";
                if (File.Exists(nameFile))
                    File.Delete(nameFile);

                using (TaskService ts = new TaskService())
                {

                    using (StreamWriter w = new StreamWriter(nameFile))
                    {
                       
                        foreach (Microsoft.Win32.TaskScheduler.Task t in ts.AllTasks)
                        {
                            try
                            {
                                if (t.Enabled && t.IsActive)
                                {
                                    if ((t.Definition.RegistrationInfo.Author == null) ||
                                        t.Definition.RegistrationInfo.Author.Contains("Software") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Google") ||
                                        t.Definition.RegistrationInfo.Author.Contains("McAffe") ||
                                        t.Definition.RegistrationInfo.Author.Contains("McAfee") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Office") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Proxy") ||
                                        t.Definition.RegistrationInfo.Author.Contains("User") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Microsoft") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Window") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Memory") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Drive") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Update") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Discovery") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Work") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Detalle") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Calibration") ||
                                        t.Definition.RegistrationInfo.Author.Contains("WiFi") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Usb") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Remote") ||
                                        t.Definition.RegistrationInfo.Author.Contains("System") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Synch") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Dell") ||
                                        t.Definition.RegistrationInfo.Author.Contains("Update") ||
                                        t.Definition.RegistrationInfo.Author.Contains("ManifestUpdate") ||
                                        t.Definition.RegistrationInfo.Author.Contains("sql"))
                                    {
                                        continue;
                                    }
                                    string n = t.Name;
                                    w.WriteLine("[Nombre =" + t.Name + "] [Autor =" + t.Definition.RegistrationInfo.Author + "] [Proposito =" + t.Definition.RegistrationInfo.Description + "] [Detalle =" + t.Definition.Triggers.ToString() + "] [Ruta =" + t.Definition.Actions.ToString() + "]");
                                }
                            }
                            catch (Exception ex)
                            {
                                w.WriteLine("[Nombre =" + t.Name + "] Error=>"+ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                Console.WriteLine("Archivo creado : "+nameFile);
                Console.WriteLine("Presiona cualquier tecla para salir");
            }
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
