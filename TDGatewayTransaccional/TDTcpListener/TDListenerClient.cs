using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TDTcpListener
{
    public class TDListenerClient
    {
        private TDListenerConfig otdListenerConfig;
        private TcpClient oClient = null;
        NetworkStream stream = null;
        public TDListenerClient(TDListenerConfig oTDListenerConfig) {
            this.otdListenerConfig = oTDListenerConfig;
        }
        public async Task<bool> Connect() {
            try
            {
                oClient = new TcpClient();
                await oClient.ConnectAsync(this.otdListenerConfig.IpAddress, this.otdListenerConfig.Port);
                Console.WriteLine($"Conectado al servidor { this.otdListenerConfig.IpAddress} { this.otdListenerConfig.Port}");
                stream = oClient.GetStream();
            }
            catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }
        public async Task SendMessage(string JsonRequest) {

            // Envía datos al TcpListener
            JsonRequest = "Hola, servidor!";
            byte[] data = Encoding.ASCII.GetBytes(JsonRequest);
            await stream.WriteAsync(data, 0, data.Length);
            Console.WriteLine($"Mensaje enviado: {JsonRequest}");


            // Lee la respuesta del TcpListener
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Respuesta recibida: {0}", response);

            //validar  si hay respuesta
            oClient?.Close();
            if (ReceiveMessageAsync != null)
                ReceiveMessageAsync(oClient, response);
        }
        public async Task<string> SendMessageAsync(string JsonRequest)
        {

            // Envía datos al TcpListener
            JsonRequest = "Hola, servidor!";
            byte[] data = Encoding.ASCII.GetBytes(JsonRequest);
            await stream.WriteAsync(data, 0, data.Length);
            Console.WriteLine($"Mensaje enviado: {JsonRequest}");


            // Lee la respuesta del TcpListener
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Respuesta recibida: {0}", response);

            oClient?.Close();
            return response;
        }
        public delegate void ReceiveMessageAsyncDelegado(TcpClient oTcpClient, string response);
        public event ReceiveMessageAsyncDelegado ReceiveMessageAsync;
    }
}
