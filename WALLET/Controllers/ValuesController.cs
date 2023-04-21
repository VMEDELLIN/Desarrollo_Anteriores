using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WALLET.Controllers
{
    public class ValuesController : ApiController
    {
       
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [Route("api/Transferencia")]
        [HttpPost]
        public HttpResponseMessage Transferencia([FromBody] TransferenciaRequest requestData)
        {
            string Date = DateTime.Now.ToString("ddMMyyyy");
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            string key = Date + builder.ToString() + " - ";

            LogClass.setLogClass(ConfigurationManager.AppSettings["NAME_FILE_LOG"], ConfigurationManager.AppSettings["PATH_LOG"], 0);
            LogClass.LogInfo(key + "CrossBorderWeb", Environment.NewLine);
            LogClass.LogInfo(key + "CrossBorderWeb", "############################################################################################################");
            LogClass.LogInfo(key + "CrossBorderWeb", "api/Transferencia", "Begin Transferencia Function");
            string request = JsonConvert.SerializeObject(requestData);
            LogClass.LogInfo(key + "CrossBorderWeb", "Transferencia", "Request: " + request);
            HttpResponseMessage responseMessage = null; ;
            TransferenciaResponse json = new TransferenciaResponse();
            //WalletBLLV bll = new WalletBLLV();
            //WalletBLLV2 bll = new WalletBLLV2();
            //WalletBLLV3 bll = new WalletBLLV3();
            WalletBLLV4 bll = new WalletBLLV4();
            bool response = bll.Transferencia(key, requestData, ref json);
            

            if (response)
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.OK, json);
            }
            else
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, json);
            }
            LogClass.LogInfo(key + "CrossBorderWeb", "api/Transferencia", "End Transferencia Function");
            return responseMessage;

        }
    }
    public class TransferenciaRequest
    {
        public int id { get; set; }
        public int fechaOperacion { get; set; }
        public int institucionOrdenante { get; set; }
        public int institucionBeneficiaria { get; set; }
        public string claveRastreo { get; set; }
        public decimal monto { get; set; }
        public string nombreOrdenante { get; set; }
        public int tipoCuentaOrdenante { get; set; }
        public string cuentaOrdenante { get; set; }
        public string rfcCurpOrdenante { get; set; }
        public string nombreBeneficiario { get; set; }
        public int tipoCuentaBeneficiario { get; set; }
        public string cuentaBeneficiario { get; set; }
        public string nombreBeneficiario2 { get; set; }
        public string tipoCuentaBeneficiario2 { get; set; }
        public string cuentaBeneficiario2 { get; set; }
        public string rfcCurpBeneficiario { get; set; }
        public string conceptoPago { get; set; }
        public int referenciaNumerica { get; set; }
        public string empresa { get; set; }
        public int tipoPago { get; set; }
        public string tsLiquidacion { get; set; }
        public string folioCodi { get; set; }


    }

    public class TransferenciaResponse
    {
        public string mensaje { get; set; }
        public string id { get; set; }

    }
    public class Cargo
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int NumCuenta { get; set; }
        public int TipoRecurso { get; set; }
        public string Referencia { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }

    public class ResponseCargo
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public int Folio { get; set; }
        public int NumCuenta { get; set; }
        public decimal Saldo { get; set; }
    }
}
