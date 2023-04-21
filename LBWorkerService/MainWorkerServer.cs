using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LBWorkerService
{
    public class MainWorkerServer
    {
        private string[] args = null;
        IHost host = null;
        public MainWorkerServer(string[] args) {
            this.args = args;
        }
        public async Task Iniciar() {
            
            
            host = CreateHostBuilder(args).Build();
            host.RunAsync();
        }
        public async Task Detener(CancellationToken cancellationToken)
        {
            host.StopAsync(cancellationToken);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>()
                .AddSingleton<IFileData,FileData>();
            });
    }
}
