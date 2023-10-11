using ListenerConector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpListenerConector
{
    class Program
    {
        private static TcpConectorServerService service;
        static void Main(string[] args)
        {
            service = new TcpConectorServerService();
            service.Start();

            Console.WriteLine("Presiona cualquier tecla para detener el servidor.");
            Console.ReadKey();

            service.Stop();
        }
    }
}
