using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuardianRun
{
    public class Run
    {
        ServiceController serviceController;
        string[] targetServices = { "TDGatewayService" }; // Nombres de los servicios objetivo
        Timer timer;
        public Run()
        {
            LogClass.setLogClass("GuardianDogService", "Log/TransferDirecto", 4);

        }

        private void CheckTargetServices(object state)
        {
            foreach (string serviceName in targetServices)
            {
                ServiceController service = new ServiceController(serviceName);
                LogClass.LogInfo("System", "GuardianDogService:Revisando", $"{serviceName} Estatus {service.Status}");
                if (service.Status != ServiceControllerStatus.Running)
                {
                    // Si el servicio objetivo no está en ejecución, intenta reiniciarlo
                    try
                    {
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                        Console.WriteLine("El servicio {0} se ha reiniciado.", serviceName);
                        LogClass.LogInfo("System", "GuardianDogService:Iniciando", $"El servicio se ha reiniciado {serviceName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al reiniciar el servicio {0}: {1}", serviceName, ex.Message);
                        LogClass.LogInfo("System", "GuardianDogService:Error", $"Error al reiniciar el servicio {ex.Message}");
                    }
                }
            }
        }
        public void Start()
        {
            timer = new Timer(CheckTargetServices, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }
        public void Stop()
        {
            timer?.Dispose();
        }
    }
}
