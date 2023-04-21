using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Logging;
using System.IO;
using CliWrap;

namespace WorkerService1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //const string ServiceName = "WorkerService";

            //if (args is { Length: 1 })
            //{
            //    string executablePath =
            //        Path.Combine(AppContext.BaseDirectory, "WorkerService1.exe");

            //    if (args[0] is "/Install")
            //    {
            //        Cli.Wrap("sc")
            //            .WithArguments(new[] { "create", ServiceName, $"binPath={executablePath}", "start=auto" })
            //            .ExecuteAsync();
            //    }
            //    else if (args[0] is "/Uninstall")
            //    {
            //        Cli.Wrap("sc")
            //            .WithArguments(new[] { "stop", ServiceName })
            //            .ExecuteAsync();

            //        Cli.Wrap("sc")
            //            .WithArguments(new[] { "delete", ServiceName })
            //            .ExecuteAsync();
            //    }
            //    return;
            //}

            Admin a = new Admin(args);
            a.Iniciar();
            //CreateHostBuilder(args).Build().Run();


            //IHost host = Host.CreateDefaultBuilder(args)
            //    .ConfigureLogging(options=>{
            //        if (OperatingSystem.IsWindows())
            //        {
            //            options.AddFilter<EventLogLoggerProvider>("Microsoft", LogLevel.Warning);
            //        }
            //    })
            //    .ConfigureServices(services=>
            //    {
            //        services.AddHostedService<Worker>();
            //        if (OperatingSystem.IsWindows()) {
            //            services.Configure<EventLogSettings>(config=>{
            //                config.LogName = "Sample Service";
            //                config.SourceName = "Sample Service Source";
            //            });
            //        }
            //    }).UseWindowsService()
            //    .Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>()
                .AddSingleton<IFileData, FileData>();
            });


        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //    .ConfigureLogging(options => {
        //        if (OperatingSystem.IsWindows())
        //        {
        //            options.AddFilter<EventLogLoggerProvider>("Microsoft", LogLevel.Warning);
        //        }
        //    })
        //    .ConfigureServices((hostContext, services) =>
        //    {
        //        services.AddHostedService<Worker>()
        //        .AddSingleton<IFileData, FileData>();
        //        if (OperatingSystem.IsWindows())
        //        {
        //            services.Configure<EventLogSettings>(config =>
        //            {
        //                config.LogName = "Sample Service";
        //                config.SourceName = "Sample Service Source";
        //            });
        //        }
        //    });
    }
}

