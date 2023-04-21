using LBWorkerService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkServiceRun
{
    class Program
    {
        static void Main(string[] args)
        {
            //MainWorkerServer ws = new MainWorkerServer(null);
            //ws.Iniciar();


            try
            {

                Policy.Handle<Exception>().Retry(3, (exception, retry) =>
                {
                    Console.WriteLine($"Se ejecuta {retry}");
                }).Execute(() => {
                    throw new Exception();
                });

                //Policy.Handle<Exception>().Retry(3).Execute(()=> { 

                //});
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fallaron {ex}");
            }


            //Console.WriteLine("Detener 0");
            //int valir=Convert.ToInt32(Console.ReadLine());

            //if (valir == 0)
            //{
            //    CancellationTokenSource source = new CancellationTokenSource();
            //    CancellationToken token = source.Token;
            //    ws.Detener(token);
            //}

            

            //CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)            
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>()
                .AddSingleton<IFileData, FileData>();
            });
    }
}
