using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
namespace MonitorearService
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceName = "NombreDelServicio";
            ServiceController serviceController = new ServiceController(serviceName);
            ServiceControllerStatus status = serviceController.Status;            
            Console.WriteLine("Estado del servicio: " + status.ToString());
        }
    }
}
