using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TDGatewayGuardian
{
    class Program
    {
        static void Main(string[] args)
        {

            HostFactory.Run(x =>
            {
                x.Service<ValidaEstatus>(serv =>
                {
                    serv.ConstructUsing(name => new ValidaEstatus());
                    serv.WhenStarted(tc => tc.Start());
                    serv.WhenStopped(tc => tc.Stop());


                });

                x.RunAsLocalSystem();

                x.SetServiceName("TDGatewayGuardian");
                x.SetDisplayName("TDGatewayGuardian");
                x.SetDescription("Revisa el estatus del Gateway de TD.");
                // Configuración de reinicio automático
                x.EnableServiceRecovery(recovery =>
                {
                    recovery.RestartService(3); // Intenta reiniciar el servicio hasta 3 veces
                    recovery.OnCrashOnly(); // Solo reiniciar el servicio en caso de fallos                    
                });

                x.StartAutomaticallyDelayed();
            });
        }
    }
}
