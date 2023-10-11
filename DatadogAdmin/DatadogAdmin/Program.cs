using DDAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace DatadogAdmin
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceDatadogCambioLog servCambio = new ServiceDatadogCambioLog();
            HostFactory.Run(x =>
            {
                x.Service<ServiceDatadogCambioLog>(serv =>
                {

                    serv.ConstructUsing(name => servCambio);
                    serv.WhenStarted(tc => tc.Start(servCambio.Period));
                    serv.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetServiceName("datadogagentcambiopaths");
                x.SetDisplayName("DataDog Cambio Path");
                x.SetDescription("Cambia el path de los logs.");
                // Configuración de reinicio automático
                x.EnableServiceRecovery(recovery =>
                {
                    recovery.RestartService(3); // Intenta reiniciar el servicio hasta 3 veces
                    recovery.OnCrashOnly(); // Solo reiniciar el servicio en caso de fallos                    
                });

                x.StartAutomaticallyDelayed();
            });

            //servCambio.Start(servCambio.Period);
        }
    }
}
