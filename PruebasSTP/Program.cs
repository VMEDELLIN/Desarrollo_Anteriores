using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PruebasSTP
{
    class Program
    {
        static void Main(string[] args)
        {

            SolicitarRetiro();
        }

        static async Task SolicitarRetiro()
        {
            DatosRetornarOrden DatosRetornar = new DatosRetornarOrden();
            DatosRetornar.fechaOperacion = "20221012";
            DatosRetornar.claveRastreo = "RAS";
            DatosRetornar.institucionOperante = "345";// (data["nInstitucionOrdenante"]).ToString();
            DatosRetornar.empresa = "Test";// (data["sEmpresa"]).ToString();
            DatosRetornar.monto = "10";// Convert.ToString(request.Monto);
            DatosRetornar.claveRastreoDevolucion = "123";// (data["sClaveRastreo"]).ToString();
            DatosRetornar.digitoIdentificadorBeneficiario = "2";
            DatosRetornar.medioEntrega = "3";

            CryptoHandler cryptoHandler = new CryptoHandler();
            DatosRetornar.firma = cryptoHandler.cadenaOriginal(DatosRetornar);

            string url = ConfigurationManager.AppSettings["servcioSTP"];
            var client = new HttpClient();
            CryptoHandler dssv = new CryptoHandler();

            var dataS = JsonConvert.SerializeObject(DatosRetornar);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                HttpContent content = new StringContent(dataS, System.Text.Encoding.UTF8, "application/json");
                var httpResponse = await client.PutAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {

                }
            }
            catch (Exception ex)
            {
                string es = ex.Message;
            }
            
            //    var result = await httpResponse.Content.ReadAsStringAsync();
            //    LogClass.LogError("APITransfer", "SolicitarRetiro()", "Recibe: " + result);
            //    ResponseRetornarOrden postResult = JsonConvert.DeserializeObject<ResponseRetornarOrden>(result);
            //    // if (postResult.resultado.id < 1)
            //    if (postResult.resultado.id > 0)
            //    {

            //        if (MontoTotal > Convert.ToDecimal(data["MontoWa"]))
            //        {
            //            MontoEnviar = Convert.ToDecimal(data["MontoWa"]);
            //        }
            //        else
            //        {
            //            MontoEnviar = MontoTotal;

            //        }
            //        MontoTotal = MontoTotal - Convert.ToDecimal(data["MontoWa"]);

            //        this.MontoEnvio = MontoEnviar;
            //        this.Remitente = Remitente;
            //        this.Usuario = Usuario;
            //        this.nIdhstTransaccion = idSolcitud;
            //        this.IdWallet = Convert.ToInt32(data["nIdMontosWallet"]);
            //        Retiro();
            //        if (!Exito)
            //        {
            //            LogClass.LogError("APITransfer", "RetiroBLL SolicitarRetiro", "Error en solicitar retiro, nIdhstTransaccion: " + Convert.ToString(idSolcitud) +
            //                         " ,IdWallet: " + Convert.ToString(data["nIdMontosWallet"])
            //                         + " ,MontoEnvio: " + Convert.ToString(MontoEnviar));
            //        }


            //        //     bool rev = RevertirRetorno((data["nId_solicitudretiro"]).ToString(), idSolcitud, ref ResponseTrama, cargo);
            //    }



            //}
            //else
            //{
            //    //     bool rev = RevertirRetorno((data["nId_solicitudretiro"]).ToString(), idSolcitud, ref ResponseTrama, cargo);
            //}
        }
    }
}
