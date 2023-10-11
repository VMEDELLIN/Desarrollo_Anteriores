using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DDAdmin
{
    public class CambioDatos
    {
        DateTime FechaActual;
        public CambioDatos(bool oDefault, int oMes)
        {
            if (oDefault)
                FechaActual = DateTime.Now;
            else
                FechaActual = DateTime.Now.AddMonths(oMes);
        }
        public bool Gateway()
        {
            bool res= EditYaml("gateway.d", "conf.yaml");
            LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Ejecuto gateway {res}",4);
            return res;
        }
        public bool Conectores()
        {
            bool res = EditYaml("conectorestd.d", "conf.yaml");
            LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Ejecuto conectores {res}",4);
            return res;
        }
        public bool EditYaml(string confd, string file)
        {
            bool Estatus = false;

            try
            {
                string rutaProgramData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Datadog", "conf.d", confd);
                string nombreArchivoBuscado = file;

                string[] archivosEnProgramData = Directory.GetFiles(rutaProgramData, nombreArchivoBuscado, SearchOption.AllDirectories);

                if (archivosEnProgramData.Length > 0)
                {
                    // El archivo fue encontrado
                    string filePath = archivosEnProgramData[0];

                    var encoding = DetectEncoding(filePath);
                    // Leer el archivo YAML
                    var yamlText = File.ReadAllText(filePath, encoding);

                    //DateTime FechaActual = DateTime.Now.AddDays(-2);
                    
                    //Cambio nombre de carpeta
                    bool CarpExists = Regex.IsMatch(yamlText, @"{repcarp}");
                    string CarpetaActual = $"{FechaActual.ToString("yyMM")}";
                    string CarpetaAnterior = $"{FechaActual.AddMonths(-1).ToString("yyMM")}";
                    if (CarpExists)
                    {
                        yamlText = Regex.Replace(yamlText, @"{repcarp}", CarpetaActual);
                        Estatus = true;
                        LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Cambio nombre de carpeta de {{repcarp}} a {CarpetaActual}",4);
                    }

                    if (Regex.IsMatch(yamlText, CarpetaAnterior))
                    {
                        yamlText = Regex.Replace(yamlText, CarpetaAnterior, CarpetaActual);
                        Estatus = true;
                        LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Cambio nombre de carpeta de {CarpetaAnterior} a {CarpetaActual}");
                    }


                    // Cambio nombre de archivo
                    //bool NameExists = Regex.IsMatch(yamlText, @"{repname}");
                    //string NombreActual = $"{FechaActual.ToString("ddMMyy")}";
                    //string NombreAnterior = $"{FechaActual.AddDays(-1).ToString("ddMMyy")}";
                    //if (NameExists)
                    //{
                    //    yamlText = Regex.Replace(yamlText, @"{repname}", NombreActual);
                    //    Estatus = true;
                    //    LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Ejecuto nombre   CarpExists");
                    //}

                    //if (Regex.IsMatch(yamlText, NombreAnterior))
                    //{
                    //    yamlText = Regex.Replace(yamlText, NombreAnterior, NombreActual);
                    //    Estatus = true;
                    //    LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Ejecuto nombre   NombreAnterior {NombreAnterior}");
                    //}

                    // Guardar el contenido YAML actualizado en el archivo
                    File.WriteAllText(filePath, yamlText, encoding);                    
                }
            }
            catch (Exception ex)
            {
                Estatus = false;
            }
            LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Ejecuto estatus {Estatus}");
            return Estatus;
        }
        // Detecta la codificación utilizada por el archivo YAML
        public Encoding DetectEncoding(string filePath)
        {
            using (var reader = new StreamReader(filePath, Encoding.Default, true))
            {
                reader.Peek(); // Permite que el StreamReader detecte la codificación
                return reader.CurrentEncoding;
            }
        }
    }
}