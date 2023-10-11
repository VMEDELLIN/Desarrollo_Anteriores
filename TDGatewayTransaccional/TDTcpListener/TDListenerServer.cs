using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TDTcpListener
{
    public class TDListenerServer
    {
        private TDListenerConfig otdListenerConfig;
        private TcpListener oListener = null;
        private CancellationTokenSource cts;
        /// <summary>
        /// Genera la instancia del objeto sin la configuración, la cual sera pasada al crear el TcpListener.
        /// </summary>
        public TDListenerServer():this(null)
        {
        }
        /// <summary>
        /// Genera la instancia del objeto con la configuración, se usara al crear el TcpListener.
        /// </summary>
        public TDListenerServer(TDListenerConfig oTDListenerConfig) {
            this.otdListenerConfig = oTDListenerConfig;
        }
        /// <summary>
        /// Crear el obejeto TcpListener usara la configuración que se paso al instanciar el objeto.
        /// En caso de no tener la configuración generara una Exception.
        /// </summary>
        /// <returns></returns>
        public bool Create() {
            return Create(otdListenerConfig);
        }

        /// <summary>
        /// Crear el obejeto TcpListener en caso de no tener la configuración.
        /// En caso de no tener la configuración generara una Exception.
        /// </summary>
        /// <param name="oTDListenerConfig">Configuracíon para el TcpListener</param>
        /// <returns></returns>
        public bool Create(TDListenerConfig oTDListenerConfig)
        {
            bool vResultado = false;
           

            try
            {
                if (oTDListenerConfig == null)
                    throw new Exception("Proporcione la configuración para crear el TcpListener");

                // Crea un TcpListener y comienza a escuchar en una dirección IP y un puerto específicos
                IPAddress oIpAddress = IPAddress.Parse(oTDListenerConfig.IpAddress);
                oListener = new TcpListener(oIpAddress, oTDListenerConfig.Port);
                vResultado = true;
            }
            catch (Exception ex)
            {
                vResultado = false;
            }

            return vResultado;
        }
        public async Task StartAsync()
        {
            // Crea un CancellationTokenSource para permitir la cancelación del servidor
            cts = new CancellationTokenSource();

            try
            {
                oListener.Start();
                Console.WriteLine("Gateway escuchando en {0}:{1}...", otdListenerConfig.IpAddress, otdListenerConfig.Port);

                int CountConexiones = 0;
                // Espera conexiones entrantes de forma asíncrona y las maneja en subprocesos separados
                while (!cts.Token.IsCancellationRequested)
                {

                    TcpClient client = await oListener.AcceptTcpClientAsync();
                    Console.WriteLine("TDService conectado: {0}", client.Client.RemoteEndPoint);
                    
                    CountConexiones++;
                    // Procesa cada conexión en un subproceso separado
                    _ = HandleListeningAsync(client, CountConexiones);
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
                oListener?.Stop();
            }
        }
        public void Stop()
        {
            // Solicita la cancelación del servidor
            cts?.Cancel();
        }
        // Declaración del delegado
        public delegate void ListeningAsyncDelegado(TcpClient oTcpClient, string requestData, int CountConexiones);
        public event ListeningAsyncDelegado ListeningAsync;
        private async Task HandleListeningAsync(TcpClient oTcpClient, int CountConexiones)
        {
            try
            {
                Console.WriteLine($"HandleClientAsync:Cliente conectado {oTcpClient.Client.RemoteEndPoint} Total: {CountConexiones}");

                NetworkStream stream = oTcpClient.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string requestData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Datos recibidos del Cliente: {requestData}");


                // Verificar si hay suscriptores al evento antes de invocarlo
                if (ListeningAsync != null)
                {
                    // Invocar el evento
                    ListeningAsync(oTcpClient, requestData, CountConexiones);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }
    }
    public class TDListenerConfig { 
        public string IpAddress { get; set; }
        public int Port { get; set; }

        public TDListenerConfig():this(string.Empty,0) {
        }
        public TDListenerConfig(string IpAddress,int Port)
        {
            this.IpAddress = IpAddress;
            this.Port = Port;
        }
    }
}
