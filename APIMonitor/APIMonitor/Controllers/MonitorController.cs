using APIMonitor.Hubs;
using APIMonitor.Models;
using APIMonitor.Singleton;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIMonitor.Controllers
{
    public class MonitorController : ApiController
    {
        // GET: api/Monitor
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Monitor/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Monitor
        //public void Post([FromBody]string value)
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] InfoOperation oInfoOperation)
        {
            Operation oOperation = new Operation()
            {
                Referencia = oInfoOperation.Referencia,
                Code = oInfoOperation.Code,
                Message = oInfoOperation.Message,
                Data = oInfoOperation.Data,
                IdEstatus = oInfoOperation.IdEstatus,
                IdAgencia = 1,
                IdAgente = 1,
                AutorizacionCobro = "sdfsdff"
            };

            OperationSingleton.Instance.Add(oOperation);
            string jsonAll = OperationSingleton.Instance.GetAllToJson();
            int jsonTry = OperationSingleton.Instance.GetTryToJson();
            int jsonPay = OperationSingleton.Instance.GetPayToJson();
            int jsonCalcell = OperationSingleton.Instance.GetCalcellToJson();

            var respuesta = new
            {
                Code=0,
                Mensaje = "Exitoso"
                //Producto = oInfoOperation
            };
            //_hubContext.Clients.All.SendOperation(jsonAll, jsonTry, jsonPay, jsonCalcell);
            //_hubContext.Clients.All.SendAsync("ReceiveOperation", jsonAll, jsonTry, jsonPay, jsonCalcell);
            //return StatusCode(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.OK, respuesta, Configuration.Formatters.JsonFormatter);
        }

        // PUT: api/Monitor/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Monitor/5
        public void Delete(int id)
        {
        }
    }
}
