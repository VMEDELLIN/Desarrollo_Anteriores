using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorService
{
    class Program
    {
        static TcpClient client;
        static void Main(string[] args)
        {
            string serverIpAddress = "127.0.0.1";
            int serverPort = 7050;

            try
            {
                // Crea un TcpClient y se conecta al TcpListener
                client = new TcpClient(serverIpAddress, serverPort);
                Console.WriteLine("Conectado al servidor {0}:{1}", serverIpAddress, serverPort);

                // Obtiene la referencia al flujo de escritura de la conexión
                NetworkStream stream = client.GetStream();

                // Codifica el mensaje en bytes
                string message = "Hola, servidor!";
                byte[] data = Encoding.ASCII.GetBytes(message);

                // Envía los datos al TcpListener
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Mensaje enviado: {0}", message);

                byte[] connectorBuffer = new byte[1024];
                int connectorBytesRead = stream.Read(connectorBuffer, 0, connectorBuffer.Length);
                string response = Encoding.ASCII.GetString(connectorBuffer, 0, connectorBytesRead);
                Console.WriteLine("Respuesta recibida: {0}", response);




                Console.ReadLine();
                //// Cierra la conexión
                //client.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el mensaje: {0}", ex.Message);
            }
            finally
            {
                // Cierra el TcpClient de manera segura
                client?.Close();
            }
        }
    }
}
