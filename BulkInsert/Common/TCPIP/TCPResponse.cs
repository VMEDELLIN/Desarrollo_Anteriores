using System;

namespace Common.TCPIP
{
    public class TCPResponse
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string Data { get; set; }
        public string DataType { get; set; }
    }
}
