using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerConector
{    
    public class TcpConectorServerService
    {
        private Conector serverConector;
        private Task serverTask;
        public void Start()
        {
            serverConector = new Conector();
            serverConector.MiEventoConector += ServerConector_MiEventoConector;
            serverTask = serverConector.StartAsync();
        }
        public void Stop()
        {
            serverConector.Stop();
            serverTask.Wait();
        }
        private async void ServerConector_MiEventoConector(System.Net.Sockets.TcpClient tdGatewayClient)
        {
            Console.WriteLine("MiEventoConector:Gateway conectado: {0}", tdGatewayClient.Client.RemoteEndPoint);
            byte[] buffer = new byte[1024];
            int bytesRead = await tdGatewayClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

            string requestData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Datos recibidos del Gateway: {0}", requestData);

            string responseData = "Respuestadel conector";
            byte[] responseBytes = Encoding.ASCII.GetBytes(responseData);
            await tdGatewayClient.GetStream().WriteAsync(responseBytes, 0, responseBytes.Length);

            tdGatewayClient.Close();
            Console.WriteLine("Gateway desconectado: {0}", tdGatewayClient.Client.RemoteEndPoint);                
        }
    }
}
