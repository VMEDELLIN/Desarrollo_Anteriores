using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Common.TCPIP.Server
{
    public delegate void OnMessageReceivedEventHandler(TCPConnection Connection);
    public class TCPServer
    {
        public event OnMessageReceivedEventHandler MessagedReceived;

        //  IPEndPoint local. Establece la(s) direccion(es) IP Local(es) y el puerto de escucha.
        private IPEndPoint m_serverEndPoint;
        //  Socket que escucha por peticiones de los clientes.
        private Socket m_serverSocket;
        //  ListeningPort
        private int m_serverPort = 5001;
        //  Maximum connections
        private int m_maxConnections = 15;
        //  Simultaneous Connections
        private int m_simultaneousConnections = 15;
        //  BufferSize
        private int m_sufferSize = 1024;
        //  Connection LIst
        private List<TCPConnection> ConnectionList = new List<TCPConnection>();
        //  Numero actual de conexiones
        private int m_currentConnections = 0;

        public int ListenningPort
        {
            set { m_serverPort = value; }
            get { return m_serverPort; }
        }
        public int MaximumConnections
        {
            set { m_maxConnections = value; }
            get { return m_maxConnections; }
        }
        public int SimultaneousConnections
        {
            set { m_simultaneousConnections = value; }
            get { return m_simultaneousConnections; }
        }
        public int BufferSize
        {
            set { m_sufferSize = value; }
            get { return m_sufferSize; }
        }
        public int Connections
        {
            get { return m_currentConnections; }
        }

        public bool Start()
        {
            return StartServerSocket();
        }
        public bool Stop()
        {
            StopServerSocket();

            while (ConnectionList.Count > 0)
            {
                Thread.Sleep(5000);
            }

            LogClass.LogInfo("System", "TCPServer:Stop", "The Server has stopped");

            GC.Collect();

            return true;
        }
        private void sendToClient(TCPConnection Conn)
        {
            LogClass.LogInfo(Conn.Id, "TCPServer:sendToClient", "Begin Send Information");

            Socket handler = Conn.ClientSocket;

            // JCuadra. 

            //if (!handler.Connected)
            //{
            //    CloseConnection(Conn);

            //    return;
            //}
            
            string Base64Data = Library.Base64Encode(Conn.ResponseData);

            var databuf = Encoding.UTF8.GetBytes(Base64Data);

            var lenbuf = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(databuf.Length + sizeof(int)));

            byte[] DataArray = new byte[databuf.Length + sizeof(int)];
            Array.Copy(lenbuf, 0, DataArray, 0, lenbuf.Length);
            Array.Copy(databuf, 0, DataArray, lenbuf.Length, databuf.Length);

            try
            {
                IAsyncResult sendResult = handler.BeginSend(DataArray, 0, DataArray.Length, 0, new AsyncCallback(SendCallback), Conn);
            }
            catch (SocketException se)
            {
                LogClass.LogError(Conn.Id, "TCPServer:sendToClient", "Socket Exception: " + se.Message);
                //CloseConnection(Conn);                
            }
            catch (Exception e)
            {
                LogClass.LogError(Conn.Id, "TCPServer:sendToClient", "Exception: " + e.Message);
                //CloseConnection(Conn);
            }
            finally
            {
                // JCuadra. 
                //CloseConnection(Conn);
            }
            //Finaliza Envio de Respuesta  
        }

        private bool StartServerSocket()
        {
            LogClass.LogInfo("System", "TCPServer:StartServerSocket", "Begin Server");

            try
            {
                m_serverEndPoint = new IPEndPoint(IPAddress.Any, m_serverPort);

                // Create the socket, bind it, and start listening
                m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_serverSocket.Blocking = false;

                m_serverSocket.Bind(m_serverEndPoint);
                m_serverSocket.Listen(m_maxConnections);
                //m_serverSocket.Listen((int)SocketOptionName.MaxConnections);

                for (int i = 0; i < m_simultaneousConnections; i++)
                    m_serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), m_serverSocket);

                LogClass.LogInfo("System", "TCPServer:StartServerSocket", "Listening for Connections");
            }
            catch (Exception e)
            {
                LogClass.LogError("System", "TCPServer:StartServerSocket", "Exception: " + e.Message);
                LogClass.LogError("System", "TCPServer:StartServerSocket", "StackTrace: " + e.StackTrace);

                return false;
            }

            LogClass.LogInfo("System", "TCPServer:StartServerSocket", "Listening to Port: " + m_serverPort);
            LogClass.LogInfo("System", "TCPServer:StartServerSocket", "Local EndPoint set to: " + m_serverEndPoint.ToString());

            //Configuracion del Buffer
            LogClass.LogInfo("System", "TCPServer:StartServerSocket", "Buffer Size set to: " + BufferSize);

            //Configuracion de conexiones simultaneas
            LogClass.LogInfo("System", "TCPServer:StartServerSocket", "Simultaneous Connections set to: " + SimultaneousConnections);

            //Configuracion numero maximo de conexiones permitidas antes de negar el servicio
            LogClass.LogInfo("System", "TCPServer:StartServerSocket", "Maximun Accepted Clients set to: " + MaximumConnections);

            Console.WriteLine(m_serverEndPoint);
            return true;
        }
        private void StopServerSocket()
        {
            LogClass.LogInfo("System", "TCPServer:StopServerSocket", "Stop Server");
            try
            {
                if (m_serverSocket != null)
                {
                    m_serverSocket.Close();
                }

                //if (m_serverSocket != null && m_serverSocket.Connected)
                //{
                //    m_serverSocket.Disconnect(false);
                //    m_serverSocket.Close();
                //    m_serverSocket.Shutdown(SocketShutdown.Send);
                //}
            }
            catch (Exception e)
            {
                LogClass.LogError("System", "TCPServer:StopServerSocket", "Exception: " + e.Message);
                LogClass.LogError("System", "TCPServer:StopServerSocket", "StackTrace: " + e.StackTrace);
            }
        }
        private void AcceptCallback(IAsyncResult result)
        {
            LogClass.LogInfo("System", "TCPServer:AcceptCallbank", "Connection Accepted", 2);

            TCPConnection Connection = new TCPConnection();

            try
            {
                // Finish Accept
                Socket s = (Socket)result.AsyncState;
                try
                {
                    Connection.ClientSocket = s.EndAccept(result);
                }
                catch (SystemException e)
                {
                    return;
                }
                Connection.Id = DateTime.Now.ToString("yyMMddHHmmssfff");
                Connection.ClientSocket.Blocking = false;
                Connection.Buffer = new byte[m_sufferSize];
                Connection.DataBuffer = new ArrayList();
                Connection.ClientHost = Connection.ClientSocket.RemoteEndPoint.ToString();
                Connection.RequestTime = DateTime.Now;

                lock (ConnectionList)
                {
                    ConnectionList.Add(Connection);
                    Console.WriteLine($"Se ha conectado: { Connection.ClientHost }");
                    m_currentConnections++;
                }

                LogClass.LogInfo(Connection.Id, "TCPServer:AcceptCallbank", "Client " + Connection.ClientHost + " Total: " + m_currentConnections, 1);

                // Start Receive
                Connection.ClientSocket.BeginReceive(Connection.Buffer, 0, Connection.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), Connection);

                // Start new Accept
                m_serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), result.AsyncState);
            }
            catch (Exception exc)
            {
                LogClass.LogError("System", "TCPServer:AcceptCallbank", "Exception: " + exc.Message);
                LogClass.LogError("System", "TCPServer:AcceptCallbank", "StackTrace: " + exc.StackTrace);
                CloseConnection(Connection);
            }
        }
        private void ReceiveCallback(IAsyncResult result)
        {
            TCPConnection Connection = (TCPConnection)result.AsyncState;

            try
            {
                int bytesRead = Connection.ClientSocket.EndReceive(result);

                LogClass.LogInfo(Connection.Id, "TCPServer:ReceiveCallback", "Received: " + bytesRead + " Bytes", 2);

                if (bytesRead > 0)
                {
                    byte[] data = new byte[bytesRead];
                    Array.Copy(Connection.Buffer, 0, data, 0, bytesRead);
                    Connection.DataBuffer.AddRange(data);

                    int BufferLenght = Connection.DataBuffer.Count;

                    if (BufferLenght >= 4)
                    {

                        byte[] LenData = (byte[])Connection.DataBuffer.GetRange(0, sizeof(int)).ToArray(typeof(byte));

                        int DataLength = GetLenght(LenData);

                        if (DataLength.Equals(0))
                        {
                            LogClass.LogError(Connection.Id, "TCPServer:ReceiveCallback", "Message Lenght Invalid-");

                            int ErrorCode = 101;
                            string ErrorMsg = "Mensaje Invalido. -Longitud";

                            SendResponse(Connection, ErrorCode, ErrorMsg);
                            //CloseConnection(Connection);
                            return;
                        }

                        if (DataLength > 102400)
                        {
                            LogClass.LogError(Connection.Id, "TCPServer:ReceiveCallback", "Message Lenght Invalid+");

                            int ErrorCode = 101;
                            string ErrorMsg = "Mensaje Invalido. +Longitud";

                            SendResponse(Connection, ErrorCode, ErrorMsg);
                            //CloseConnection(Connection);
                            return;
                        }

                        if (BufferLenght >= DataLength)
                        {
                            //Message Found
                            try
                            {
                                byte[] PayLoadData = (byte[])Connection.DataBuffer.GetRange(sizeof(int), DataLength - sizeof(int)).ToArray(typeof(byte));

                                string StrPayLoadData = Encoding.UTF8.GetString(PayLoadData);

                                Connection.RequestData = Library.Base64Decode(StrPayLoadData);

                                MessagedReceived(Connection);
                            }
                            catch (Exception e)
                            {
                                LogClass.LogError(Connection.Id, "TCPServer:ReceiveCallback", "Message Exception: " + e.Message);

                                int ErrorCode = 101;
                                string ErrorMsg = "Mensaje Invalido. -Ex Encoding";

                                SendResponse(Connection, ErrorCode, ErrorMsg);
                                //CloseConnection(Connection);
                                return;
                            }
                        }
                        else
                        {
                            Connection.ClientSocket.BeginReceive(Connection.Buffer, 0, Connection.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), Connection);
                        }
                    }
                    else
                    {
                        Connection.ClientSocket.BeginReceive(Connection.Buffer, 0, Connection.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), Connection);
                    }

                }
                else
                {
                    CloseConnection(Connection);
                }
            }
            catch (SocketException se)
            {
                LogClass.LogError(Connection.Id, "TCPServer:ReceiveCallback", "ReceiveCallback Socket Exception: " + se.Message);

                int ErrorCode = 101;
                string ErrorMsg = "Mensaje Invalido. -SoEx";

                SendResponse(Connection, ErrorCode, ErrorMsg);
                //CloseConnection(Connection);
            }
            catch (Exception e)
            {
                LogClass.LogError(Connection.Id, "TCPServer:ReceiveCallback", "ReceiveCallback Exception: " + e.Message);

                int ErrorCode = 101;
                string ErrorMsg = "Ex";

                SendResponse(Connection, ErrorCode, ErrorMsg);
                //CloseConnection(Connection);
            }
        }
        private void CloseConnection(TCPConnection Conn)
        {

            // close the socket associated with the client
            try
            {
                //JCuadra. Se reordena la logica del cierre y destrucción de la conección del socket
                if (Conn.ClientSocket != null && Conn.ClientSocket.Connected)
                {
                    Conn.ClientSocket.Disconnect(false);
                    Conn.ClientSocket.Shutdown(SocketShutdown.Both);
                    Conn.ClientSocket.Close();
                    Conn.Dispose();

                    lock (ConnectionList)
                    {
                        if (ConnectionList.Remove(Conn))
                        {
                            m_currentConnections--;
                        }
                    }

                    GC.Collect();

                    LogClass.LogInfo(Conn.Id, "TCPServer:CloseConnection", "Client Disconnected", 1);
                }
            }
            // throws if client process has already closed
            catch (SocketException se)
            {
                LogClass.LogError(Conn.Id, "TCPServer:CloseConnection", "Socket Exception: " + se.Message);
            }

            catch (Exception e)
            {
                LogClass.LogError(Conn.Id, "TCPServer:CloseConnection", "Exception: " + e.Message);
                LogClass.LogError(Conn.Id, "TCPServer:CloseConnection", "StackTrace: " + e.StackTrace);
            }

        }
        public void SendResponse(TCPConnection Conn, int ErrorCode, string ErrorMsg)
        {
            LogClass.LogInfo(Conn.Id, "TCPServer:SendResponse", "Response", 2);

            TCPResponse Respuesta = new TCPResponse() { ErrorCode = ErrorCode, ErrorMsg = ErrorMsg, Data = Conn.ResponseData };

            Conn.ResponseData = JsonConvert.SerializeObject(Respuesta, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            LogClass.LogInfo(Conn.Id, "TCPServer:SendResponse", "Data: " + Conn.ResponseData, 4);

            sendToClient(Conn);
        }
        private void SendCallback(IAsyncResult sendResult)
        {
            // Retrieve the socket from the state obj
            TCPConnection Conn = (TCPConnection)sendResult.AsyncState;
            Socket handler = Conn.ClientSocket;

            try
            {
                // Complete sending the data to the remote device.
                if (handler.Connected)
                {
                    int bytesSent = handler.EndSend(sendResult);

                    LogClass.LogInfo(Conn.Id, "TCPServer:SendCallback", "Sent " + bytesSent + " bytes to client.", 3);
                }
            }
            catch (Exception e)
            {
                LogClass.LogError(Conn.Id, "TCPServer:SendCallback", "Exception: " + e.Message);
            }

            CloseConnection(Conn);
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
