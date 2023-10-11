using System;
using System.Collections;
using System.Net.Sockets;

namespace Common.TCPIP.Server
{
    public class TCPConnection : IDisposable
    {
        // Track whether Dispose has been called.
        private bool disposed = false;

        public string Id { get; set; }
        public Socket ClientSocket { get; set; }
        public byte[] Buffer { get; set; }
        public string ClientHost { get; set; }
        public DateTime RequestTime { get; set; }
        public ArrayList DataBuffer { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }

        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    ClientSocket.Dispose();
                    Buffer = null;
                    ClientHost = null;
                    if (DataBuffer != null) { DataBuffer.Clear(); }
                    DataBuffer = null;
                    RequestData = null;
                    ResponseData = null;
                }

                // Note disposing has been done.
                disposed = true;

            }
        }

        ~TCPConnection()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
    }
}
