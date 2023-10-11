using APIMonitorTD.Hubs;
using APIMonitorTD.Model;
using APIMonitorTD.Singleton;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIMonitorTD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : ControllerBase
    {
        private IHubContext<MonitorHub> _hubcontext;
        public MonitorController(IHubContext<MonitorHub> hubContext)
        {
            _hubcontext = hubContext;
        }

        // GET: api/<MonitorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MonitorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MonitorController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InfoOperation oInfoOperation)
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
                Code = 0,
                Mensaje = "Exitoso",
                Producto = oInfoOperation
            };

            var jsonResult = new JsonResult(respuesta)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            _hubcontext.Clients.All.SendAsync("ReceiveOperation", jsonAll, jsonTry, jsonPay, jsonCalcell);
            return jsonResult;
        }

        // PUT api/<MonitorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MonitorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
