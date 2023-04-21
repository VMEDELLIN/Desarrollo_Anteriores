using CliWrap;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService1
{
    public class Admin
    {
        const string ServiceName = "WorkerServiceV2";
        private string[] args = null;
        public Admin(string[] args)
        {

            this.args = args;
        }
        public async Task Iniciar()
        {
            if (args is { Length: 1 })
            {
                string executablePath =
                    Path.Combine(AppContext.BaseDirectory, "WorkerService1.exe");

                //EventLog eventLog1 = new EventLog(); ;
                if (args[0] is "/Install")
                {
                    //if (!System.Diagnostics.EventLog.SourceExists("TransferDirecto WorkerServiceV2"))
                    //{
                    //    System.Diagnostics.EventLog.CreateEventSource("TransferDirecto WorkerServiceV2", "TransferDirecto");
                    //}
                    //eventLog1.Source = "TransferDirecto WorkerServiceV2";
                    //eventLog1.Log = "TransferDirecto";
                    await Cli.Wrap("sc")
                        .WithArguments(new[] { "create", ServiceName, $"binPath={executablePath}", "start=auto" })
                        .ExecuteAsync();
                }
                else if (args[0] is "/Uninstall")
                {
                    await Cli.Wrap("sc")
                        .WithArguments(new[] { "stop", ServiceName })
                        .ExecuteAsync();

                    await Cli.Wrap("sc")
                        .WithArguments(new[] { "delete", ServiceName })
                        .ExecuteAsync();
                }
                return;
            }
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>()
                .AddSingleton<IFileData, FileData>();
            });
    }
}
