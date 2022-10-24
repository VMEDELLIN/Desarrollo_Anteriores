using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLecturaJson
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        private static bool asignarTipoDocumento()
        {
            string jsonConfig = ObtenerContenidoArchivo(ConfigurationManager.AppSettings["PATH_TIPODOCUMENTOS"]);
            List<TipoDocumento> tipoDocumentos = JsonConvert.DeserializeObject<List<TipoDocumento>>(jsonConfig);
            if (tipoDocumentos is null) tipoDocumentos = new List<TipoDocumento>();

            bool tipoEncontrado = false;
            foreach (TipoDocumento tipoDocumento in tipoDocumentos)
            {
                if (tipoDocumento.TD_ID == remesa.Beneficiario[0].Identificacion.TipoIdentificacion)
                {
                    pagoData.beneficiary.identificationType = tipoDocumento.V_Codigo;
                    pagoData.beneficiary.identificationDesc = tipoDocumento.V_Descripcion;
                    tipoEncontrado = true;
                    break;
                }
            }
            return tipoEncontrado;
        }
        private static string ObtenerContenidoArchivo(string pathArchivo)
        {
            DirectoryInfo directorydebug = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            DirectoryInfo directorybin = directorydebug.Parent;
            DirectoryInfo directoryCapa = directorybin.Parent;
            DirectoryInfo directoryRaiz = directoryCapa.Parent;
            string directorioBase = directoryRaiz.FullName;
            string jsonConfig;
            try
            {
                StreamReader reader = new StreamReader(directorioBase + pathArchivo);
                jsonConfig = reader.ReadToEnd();
            }
            catch (IOException)
            {
                jsonConfig = "";
            }
            return jsonConfig;
        }
    }
}
