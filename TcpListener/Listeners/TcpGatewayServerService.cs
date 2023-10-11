using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Listeners
{
    public class TcpGatewayServerService
    {
        private Gateway serverGateway;
        private Task serverTask;

        public void Start()
        {
            serverGateway = new Gateway();
            serverGateway.MiEventoGateway += Server_MiEventoGateway;
            serverTask = serverGateway.StartAsync();
        }
        public void Stop()
        {
            serverGateway.Stop();
            serverTask.Wait();
        }
        private async void Server_MiEventoGateway(System.Net.Sockets.TcpClient tdServiceClient)
        {
            Console.WriteLine("HandleClientAsync:TDService conectado: {0}", tdServiceClient.Client.RemoteEndPoint);

            NetworkStream stream = tdServiceClient.GetStream();            
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string requestData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Datos recibidos del TDService: {0}", requestData);

            // Aquí puedes realizar el procesamiento de los datos recibidos y generar una respuesta


            
            // Puedes leer y escribir datos a través del cliente.GetStream()
            string selectedConnectorIpAddress = "127.0.0.1";
            int selectedConnectorPort = 7051;

            using (TcpClient connectorClient = new TcpClient())
            {
                await connectorClient.ConnectAsync(selectedConnectorIpAddress, selectedConnectorPort);

                await connectorClient.GetStream().WriteAsync(buffer, 0, bytesRead);

                byte[] connectorBuffer = new byte[1024];
                int connectorBytesRead = await connectorClient.GetStream().ReadAsync(connectorBuffer, 0, connectorBuffer.Length);

                await tdServiceClient.GetStream().WriteAsync(connectorBuffer, 0, connectorBytesRead);
            }

            Console.WriteLine("Responder al TDService");
            string responseData = "Respuesta del gateway";
            byte[] responseBytes = Encoding.ASCII.GetBytes(responseData);
            await tdServiceClient.GetStream().WriteAsync(responseBytes, 0, responseBytes.Length);



            //tdServiceClient.Close();
            Console.WriteLine("Cliente desconectado: {0}", tdServiceClient.Client.RemoteEndPoint);
        }        
    }
}
