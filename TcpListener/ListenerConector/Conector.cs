using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ListenerConector
{
    public class Conector
    {
        private CancellationTokenSource cts;

        public async Task StartAsync()
        {
            // Crea un CancellationTokenSource para permitir la cancelación del servidor
            cts = new CancellationTokenSource();

            // Crea un TcpListener y comienza a escuchar en una dirección IP y un puerto específicos
            IPAddress connectorIpAddress = IPAddress.Parse("127.0.0.1");
            int connectorPort = 7051;

            TcpListener connectorListener = new TcpListener(connectorIpAddress, connectorPort);
            connectorListener.Start();
            Console.WriteLine("Conector escuchando en {0}:{1}...", connectorIpAddress, connectorPort);

            try
            {
                // Espera conexiones entrantes de forma asíncrona y las maneja en subprocesos separados
                while (!cts.Token.IsCancellationRequested)
                {
                    TcpClient gatewayClient = await connectorListener.AcceptTcpClientAsync();
                    Console.WriteLine("Gateway conectado: {0}", gatewayClient.Client.RemoteEndPoint);

                    _ = HandleGatewayAsync(gatewayClient);
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
                connectorListener.Stop();
            }
        }
        public void Stop()
        {
            // Solicita la cancelación del servidor
            cts?.Cancel();
        }

        // Declaración del delegado
        public delegate void MiEventoConectorDelegado(TcpClient tdGatewayClient);
        public event MiEventoConectorDelegado MiEventoConector;
        private async Task HandleGatewayAsync(TcpClient tdGatewayClient)
        {
            try
            {
                // Verificar si hay suscriptores al evento antes de invocarlo
                if (MiEventoConector != null)
                {
                    // Invocar el evento
                    MiEventoConector(tdGatewayClient);
                }
                /*
                byte[] buffer = new byte[1024];
                int bytesRead = await gatewayClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

                string requestData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Datos recibidos del Gateway: {0}", requestData);

                string responseData = "Respuestadel conector";
                byte[] responseBytes = Encoding.ASCII.GetBytes(responseData);
                await gatewayClient.GetStream().WriteAsync(responseBytes, 0, responseBytes.Length);

                gatewayClient.Close();
                Console.WriteLine("Gateway desconectado.");
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }
    }
}
