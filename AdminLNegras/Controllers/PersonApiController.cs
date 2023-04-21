using AdminLNegras.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLNegras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonApiController : ControllerBase
    {
        private IHubContext<PersonHub> _hubcontext;

        public PersonApiController(IHubContext<PersonHub> hubContext)
        {
            _hubcontext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> AddPerson(Person person) {
            await _hubcontext.Clients.All.SendAsync("Receive", person.Clave, person.Name,person.Date); ; 
            return Ok();
        }
    }
}
