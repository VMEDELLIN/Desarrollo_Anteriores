using GatewayTransaccional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTcpListener;

namespace TDGatewayTransaccional
{
    class Program
    {
        static void Main(string[] args)
        {
            GatewayListening server = new GatewayListening();
            server.Start();

            Console.WriteLine("Presiona cualquier tecla para detener el servidor.");
            Console.ReadKey();

            server.Stop();
        }
    }
}
