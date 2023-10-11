using Listeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpListener
{
    class Program
    {
        private static TcpGatewayServerService service;

        static void Main(string[] args)
        {
            // Aquí puedes incrustar el servidor en un servicio de Windows o un demonio según tus necesidades

            service = new TcpGatewayServerService();            
            service.Start();

            Console.WriteLine("Presiona cualquier tecla para detener el servidor.");
            Console.ReadKey();

            service.Stop();
        }
    }
}
