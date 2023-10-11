using System.Net.Sockets;


namespace Common.TCPIP.Client
{
    class TSocket
    {
        public byte[] buffer = new byte[2048];
        public Socket socket = null;
        public bool Conected = false;
    }
}
