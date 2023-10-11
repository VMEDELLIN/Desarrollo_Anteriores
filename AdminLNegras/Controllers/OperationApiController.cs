using AdminLNegras.Hubs;
using AdminLNegras.Models;
using AdminLNegras.Singleton;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminLNegras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationApiController : ControllerBase
    {
        private IHubContext<OperationH> _hubcontext;

        public OperationApiController(IHubContext<OperationH> hubContext)
        {
            _hubcontext = hubContext;
        }


        [HttpPost]
        public async Task<IActionResult> AddOperation(InfoOperation oInfoOperation)
        {
            

            Operation oOperation = new Operation() { 
                Referencia= oInfoOperation.Referencia, 
                Code= oInfoOperation.Code, 
                Message= oInfoOperation.Message,
                Data= oInfoOperation.Data,
                IdEstatus=oInfoOperation.IdEstatus,
                IdAgencia =1, 
                IdAgente=1, 
                AutorizacionCobro="sdfsdff" 
            };
            //JsonSerializerOptions options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase                
            //};
            //JsonSerializerOptions options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //    IgnoreNullValues = false,
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //};

            //string jsonString = JsonSerializer.Serialize(oOperation, options);


            OperationSingleton.Instance.Add(oOperation);
            string jsonAll = OperationSingleton.Instance.GetAllToJson();
            int jsonTry = OperationSingleton.Instance.GetTryToJson();
            int jsonPay = OperationSingleton.Instance.GetPayToJson();
            int jsonCalcell = OperationSingleton.Instance.GetCalcellToJson();



            await _hubcontext.Clients.All.SendAsync("ReceiveOperation", jsonAll, jsonTry, jsonPay, jsonCalcell);
            return Ok();
        }
    }
}
