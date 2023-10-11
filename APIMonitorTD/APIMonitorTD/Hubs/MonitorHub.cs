using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMonitorTD.Hubs
{
    public class MonitorHub: Hub
    {
        public async Task SendOperation(string oOperation, int oIntentos, int oPagados, int oCancelados)
        {
            await Clients.All.SendAsync("ReceiveOperation", oOperation, oIntentos, oPagados, oCancelados);
        }
    }
}
