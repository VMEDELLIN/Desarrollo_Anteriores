using GuardianRun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace DemonTest1
{
    public class Program
    {
        private static ServiceController serviceController;
        private static string[] targetServices = { "TDGatewayService" }; // Nombres de los servicios objetivo
        private static Timer timer;
        static void Main(string[] args)
        {
            //WindowsIdentity identity = WindowsIdentity.GetCurrent();
            //WindowsPrincipal principal = new WindowsPrincipal(identity);
            //principal.IsInRole(WindowsBuiltInRole.Administrator);

            Run r = new Run();
            r.Start();
            Console.ReadLine();
        }
    }
}