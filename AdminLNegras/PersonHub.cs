using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLNegras
{
    public class PersonHub:Hub
    {
        public async Task Send(string Clave, string Name, DateTime Date) {
            await Clients.All.SendAsync("Receive", Clave, Name, Date);
        }
    }
}
