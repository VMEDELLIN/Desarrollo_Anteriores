using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTcpListener;

namespace ClienteSimulador
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();

            
        }
        private static async Task MainAsync()
        {
            Console.WriteLine($"Inicio");
            TDListenerClient client = new TDListenerClient(new TDListenerConfig() { IpAddress = "127.0.0.1", Port = 7000 });
            client.ReceiveMessageAsync += Client_ReceiveMessageAsync;
            await client.Connect();
            await client.SendMessage("Hola comoh hada iaskdflnf");
            Console.WriteLine($"Fin");

        }
        private static void Client_ReceiveMessageAsync(System.Net.Sockets.TcpClient oTcpClient, string response)
        {
            Console.WriteLine($"Mensaje ricibido {response}");

            //oTcpClient.Close();
        }
    }
}
