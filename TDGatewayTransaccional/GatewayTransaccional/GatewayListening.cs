using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TDTcpListener;

namespace GatewayTransaccional
{
    public class GatewayListening
    {

        private TDListenerServer Server = null;
        private Task serverTask;
        public GatewayListening()
        {

            Server = new TDListenerServer(new TDListenerConfig() { IpAddress = "127.0.0.1", Port = 7000 });
            Server.Create();
            Server.ListeningAsync += Server_ListeningAsync;
        }
        public void Start()
        {
            serverTask= Server.StartAsync();
        }
        public void Stop()
        {
            Server.Stop();
            serverTask.Wait();
        }
        private async void Server_ListeningAsync(System.Net.Sockets.TcpClient oTcpClient, string requestData, int CountConexiones)
        {
            Console.WriteLine($"Datos recibidos del Cliente: {requestData}");

            // Aquí puedes realizar el procesamiento de los datos recibidos y generar una respuesta


            
            string responseData = $"Hola Cliente {CountConexiones} Con Id {oTcpClient.Client.RemoteEndPoint} ¿Cómo estas?";
            Console.WriteLine($"Responder al Cliente {responseData}");
            
            
            byte[] responseBytes = Encoding.ASCII.GetBytes(responseData);
            await oTcpClient.GetStream().WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
