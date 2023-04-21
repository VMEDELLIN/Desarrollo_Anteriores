using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WALLET
{
    public class WebApiApplication : System.Web.HttpApplication
    {
      
        public WebApiApplication() {
            
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateHostBuilder().Build().RunAsync();

        }
        public IHostBuilder CreateHostBuilder() =>
           Host.CreateDefaultBuilder()            
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<Worker>()
                   .AddSingleton<IFileData, FileData>();
               });
    }
}
