using EjemploApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EjemploApi.Controllers
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
        [HttpPost]
        public void Post(InfoOperation oInfoOperation)
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
