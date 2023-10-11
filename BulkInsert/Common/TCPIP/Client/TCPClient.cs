using System;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Common.Models;


namespace Common.TCPIP.Client
{
    public class TCPClient
    {
        // Parametros de Conexion a Gateway o Interface
        private string RemoteHost = "127.0.0.0";         // Dirección IP del Gateway
        private int RemotePort = 5001;             // Puerto que el Gateway estara escuchando para la comunicación.
        private int RemoteTimeout = 60000;      // Timeout del socket cuando se inicia una nueva conexión

        public string Host
        {
            get { return RemoteHost; }
            set { RemoteHost = value; }
        }
        public int Port
        {
            get { return RemotePort; }
            set { RemotePort = value; }
        }
        public int Timeout
        {
            get { return RemoteTimeout; }
            set { RemoteTimeout = value; }
        }

        public string Send(string Data)
        {
            string Response = string.Empty;

            TSocket info = connect();

            string Base64Data = Library.Base64Encode(Data);
            var databuf = Encoding.UTF8.GetBytes(Base64Data);
            var lenbuf = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(databuf.Length + sizeof(int)));

            byte[] DataArray = new byte[databuf.Length + sizeof(int)];
            Array.Copy(lenbuf, 0, DataArray, 0, lenbuf.Length);
            Array.Copy(databuf, 0, DataArray, lenbuf.Length, databuf.Length);

            if (info == null)
            {
                int ErrorCode = 102;
                string ErrorMsg = "EndPoint no se encuentra disponible o no responde.";

                return SendResponse(info, ErrorCode, ErrorMsg);
            }

            try
            {
                info.socket.Send(DataArray, DataArray.Length, SocketFlags.None);
            }
            catch (SocketException se)
            {
                int ErrorCode = 102;
                string ErrorMsg = "EndPoint no response. SoEx.";

                if (se.SocketErrorCode== SocketError.TimedOut)
                {
                    ErrorMsg = $"SocketException {se.Message}";
                }
               

                return SendResponse(info, ErrorCode, ErrorMsg);
            }
            catch (Exception e)
            {
                int ErrorCode = 102;
                string ErrorMsg = "EndPoint no response. Ex.";

                return SendResponse(info, ErrorCode, ErrorMsg);
            }

            try
            {
                int loop = 1;
                int expectedLen = 0;
                int receivedLen = 0;
                ArrayList tmpBuffer = new ArrayList();

                do
                {
                    int bytesRead = info.socket.Receive(info.buffer);
                    receivedLen += bytesRead;

                    byte[] msg = new byte[bytesRead];
                    Array.Copy(info.buffer, 0, msg, 0, bytesRead);
                    tmpBuffer.AddRange(msg);

                    if (bytesRead == 0)
                    {
                        int ErrorCode = 102;
                        string ErrorMsg = "Conexion terminada por EndPoint.";

                        return SendResponse(info, ErrorCode, ErrorMsg);
                    }

                    byte[] LenData = new byte[sizeof(int)];

                    Array.Copy(info.buffer, 0, LenData, 0, sizeof(int));

                    if (expectedLen == 0 && bytesRead >= 4)
                    {
                        expectedLen = GetLenght(LenData);
                    }

                    if (expectedLen.Equals(0))
                    {
                        int ErrorCode = 102;
                        string ErrorMsg = "La informacion enviada por el EndPoint es invalida. -Len.";

                        return SendResponse(info, ErrorCode, ErrorMsg);
                    }

                    if (expectedLen > 102400)
                    {
                        int ErrorCode = 102;
                        string ErrorMsg = "La informacion enviada por el EndPoint es invalida. +Len.";

                        return SendResponse(info, ErrorCode, ErrorMsg);
                    }

                    if (receivedLen >= expectedLen)
                    {
                        try
                        {
                            int ErrorCode = 0;
                            string ErrorMsg = "OK";

                            byte[] PayLoadData = (byte[])tmpBuffer.GetRange(sizeof(int), expectedLen - sizeof(int)).ToArray(typeof(byte));

                            string StrPayLoadData = Encoding.UTF8.GetString(PayLoadData);

                            Response = Library.Base64Decode(StrPayLoadData);

                            TCPResponse TResponse = JsonConvert.DeserializeObject<TCPResponse>(Response);


                            return SendResponse(info, TResponse.ErrorCode, TResponse.ErrorMsg, TResponse.Data);
                        }
                        catch (Exception e)
                        {
                            int ErrorCode = 102;
                            string ErrorMsg = "La informacion enviada por el EndPoint es invalida. Ex-Encoding.";

                            return SendResponse(info, ErrorCode, ErrorMsg);
                        }
                    }

                    if (loop > 25)
                    {
                        int ErrorCode = 102;
                        string ErrorMsg = "La informacion enviada por el EndPoint es invalida. +Loop.";

                        return SendResponse(info, ErrorCode, ErrorMsg);
                    }

                    loop++;

                }
                while (receivedLen < expectedLen);

            }
            catch (Exception e)
            {
                int ErrorCode = 102;
                string ErrorMsg = "La informacion enviada por el EndPoint es invalida. Ex. " + e.Message;

                return SendResponse(info, ErrorCode, ErrorMsg);
            }

            finally
            {

                // JCuadra. Cerramos el socket
                disconnect(info);


            }

            return string.Empty;
        }

        private TSocket connect()
        {
            
            string l_Host = RemoteHost;
            int l_Port = RemotePort;
            int l_Timeout = RemoteTimeout;


            IPAddress IPHost = Library.resolveHostName(l_Host);                     // Resuelve la direccion IP
            IPEndPoint clientEndpoint = new IPEndPoint(IPHost, l_Port);     // Crea un EndPoint
            TSocket info = new TSocket();                                   // Crea Socket

            info.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            info.socket.SendTimeout = l_Timeout;
            info.socket.ReceiveTimeout = l_Timeout;

            try
            {
                info.socket.Connect(clientEndpoint);
                info.Conected = info.socket.Connected;
            }
            catch (Exception e)
            {
                LogClass.LogError("", "Connect", "No es posible establecer conexion con el servidor " + e.Message);
                return null;
            }

            return info;
        }
        private void disconnect(TSocket Conn)
        {
            // Termino la recepcion o falla de la respuesta
            // Cerramos el socket
            if (Conn != null)
            {
                Conn.socket.Disconnect(false);
                Conn.socket.Shutdown(SocketShutdown.Send);

                if (Conn.socket.Connected)
                {
                    Conn.socket.Close();
                }
            }
        }

        private string SendResponse(TSocket Conn, int ErrorCode, string ErrorMsg, string Data = null, string DataType = null)
        {
            // JCuadra. No cerramos el socket aqui. Se cierra en el finally del metodo: send
            //disconnect(Conn);

            TCPResponse Respuesta = new TCPResponse() { ErrorCode = ErrorCode, ErrorMsg = ErrorMsg, Data = Data };

            return JsonConvert.SerializeObject(Respuesta, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return Data;
        }
        private int GetLenght(byte[] LenData)
        {
            const int lensize = sizeof(int);
            int bytesRead = LenData.Length;

            try
            {
                if (bytesRead != lensize)
                    throw new Exception(string.Format("Expected {0} bytes but read {1}", lensize, bytesRead));
                return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(LenData, 0));
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
