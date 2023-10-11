using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TDGatewayGuardian
{
    public class ValidaEstatus
    {
        ServiceController serviceController;
        string[] targetServices = { "TDGatewayService", "TDGatewayTest" }; // Nombres de los servicios objetivo
        Timer timer;
        public ValidaEstatus()
        {
            LogClass.setLogClass("GuardianDogService", "Log/TransferDirecto/Guardian", 4);

        }

        private void CheckTargetServices(object state)
        {
            foreach (string serviceName in targetServices)
            {
                ServiceController service = new ServiceController(serviceName);
                //LogClass.LogInfo("System", "GuardianDogService:Revisando", $"{serviceName} Estatus {service.Status}");
                if (service.Status != ServiceControllerStatus.Running)
                {
                    LogClass.LogInfo("System", "GuardianDogService:Revisando", $"{serviceName} Estatus {service.Status}");
                    Console.WriteLine("El servicio {0} esta en estatus {0}.", serviceName, service.Status);
                    // Si el servicio objetivo no está en ejecución, intenta reiniciarlo
                    try
                    {
                        LogClass.LogInfo("System", "GuardianDogService:Iniciando", $"{serviceName}");
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(5));
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
            timer = new Timer(CheckTargetServices, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        }
        public void Stop()
        {
            timer?.Dispose();
        }
    }
}
