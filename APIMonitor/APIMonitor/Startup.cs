using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(APIMonitor.Startup))]

namespace APIMonitor
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/monitorHub", map=> {
                map.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration { };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
