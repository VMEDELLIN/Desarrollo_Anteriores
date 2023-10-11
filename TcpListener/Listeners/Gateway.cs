using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Listeners
{
    public class Gateway
    {        
        private CancellationTokenSource cts;

        public async Task StartAsync()
        {
            // Crea un CancellationTokenSource para permitir la cancelación del servidor
            cts = new CancellationTokenSource();

            // Crea un TcpListener y comienza a escuchar en una dirección IP y un puerto específicos
            
            IPAddress gatewayIpAddress = IPAddress.Parse("127.0.0.1");
            int gatewayPort = 7050;

            TcpListener gatewayListener = new TcpListener(gatewayIpAddress, gatewayPort);
            gatewayListener.Start();
            Console.WriteLine("Gateway escuchando en {0}:{1}...", gatewayIpAddress, gatewayPort);


            try
            {
                // Espera conexiones entrantes de forma asíncrona y las maneja en subprocesos separados
                while (!cts.Token.IsCancellationRequested)
                {

                    TcpClient client = await gatewayListener.AcceptTcpClientAsync();
                    Console.WriteLine("TDService conectado: {0}", client.Client.RemoteEndPoint);
                    
                    // Procesa cada conexión en un subproceso separado
                    _ = HandleClientAsync(client);
                }
            }
            catch (OperationCanceledException)
            {
                // El servidor se detuvo mediante una solicitud de cancelación
                Console.WriteLine("Servidor TCP detenido.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                // Cierra el gatewayListener cuando se detiene el servidor
                gatewayListener.Stop();
            }
        }

        public void Stop()
        {
            // Solicita la cancelación del servidor
            cts?.Cancel();
        }

        // Declaración del delegado
        public delegate void MiEventoGatewayDelegado(TcpClient tdServiceClient);
        public event MiEventoGatewayDelegado MiEventoGateway;
        private async Task HandleClientAsync(TcpClient tdServiceClient)
        {
            try
            {
                // Verificar si hay suscriptores al evento antes de invocarlo
                if (MiEventoGateway != null)
                {
                    // Invocar el evento
                    MiEventoGateway(tdServiceClient);
                }

                /*
                Console.WriteLine("HandleClientAsync:TDService conectado: {0}", tdServiceClient.Client.RemoteEndPoint);
                byte[] buffer = new byte[1024];
                int bytesRead = await tdServiceClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

                string requestData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Datos recibidos del TDService: {0}", requestData);

                


                // Aquí puedes manejar la lógica de comunicación con el cliente
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


                string responseData = "Respuesta del gateway";
                byte[] responseBytes = Encoding.ASCII.GetBytes(responseData);
                await tdServiceClient.GetStream().WriteAsync(responseBytes, 0, responseBytes.Length);



                tdServiceClient.Close();
                Console.WriteLine("Cliente desconectado: {0}", tdServiceClient.Client.RemoteEndPoint);

                */
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexión con el cliente: {0}", ex.Message);
            }
        }
    }
}
