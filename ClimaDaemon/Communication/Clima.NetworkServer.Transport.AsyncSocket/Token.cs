using System;
using System.Globalization;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using Clima.NetworkServer.Sessions;

namespace Clima.NetworkServer.Transport.AsyncSocket
{
    delegate void ProcessData(SocketAsyncEventArgs args);
    
    
    /// <summary>
    /// Token for use with SocketAsyncEventArgs.
    /// </summary>
    internal sealed class Token : IConnection, IDisposable
    {
        private Socket _connection;

        private StringBuilder _sb;

        private Int32 currentIndex;
        private readonly Guid _sessionId;
        private string _endMessageToken;
        private Session _session;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="connection">Socket to accept incoming data.</param>
        /// <param name="bufferSize">Buffer size for accepted data.</param>
        internal Token(Socket connection, Int32 bufferSize)
        {
            this._connection = connection;
            this._sb = new StringBuilder(bufferSize);
            _sessionId = Guid.NewGuid();
            _endMessageToken = "<EOF>";
            _session = new Session(_sessionId.ToString(), default(IIdentity));
        }

        internal event EventHandler<MessageEventArgs> MessageReceived;
        internal event EventHandler<MessageEventArgs> Disconnected; 
        /// <summary>
        /// Accept socket.
        /// </summary>
        internal Socket Connection
        {
            get { return this._connection; }
        }

        public Guid SessionId => _sessionId;

        public string EndMessageToken
        {
            get => _endMessageToken;
            set => _endMessageToken = value;
        }

        /// <summary>
        /// Process data received from the client.
        /// </summary>
        /// <param name="args">SocketAsyncEventArgs used in the operation.</param>
        internal void ProcessData(SocketAsyncEventArgs args)
        {
            // Get the message received from the client.
            var received = this._sb.ToString();
            if(String.IsNullOrEmpty(received))
                return;
            
            // Check end message token and clean message
            if (received.Contains(_endMessageToken))
            {
                received = received.Substring(0, received.IndexOf(_endMessageToken, StringComparison.Ordinal)); 
            }
            
            Console.WriteLine("Received: \"{0}\". The server has read {1} bytes.", received, received.Length);
            
            if (received == "GetSessionID")
            {
                //If received GetSessionID request, send current session guid 
                Console.WriteLine($"Send client session id:{_sessionId}");
                Byte[] sendBuffer = Encoding.UTF8.GetBytes($"GetSessionID:{_sessionId}{_endMessageToken}");
                args.SetBuffer(sendBuffer, 0, sendBuffer.Length);
            }
            else
            {
                //If receive message, invoke MessageReceived event
                var recvArgs = new MessageEventArgs();
                recvArgs.ConnectionId = _sessionId.ToString();
                recvArgs.Data = received;
                OnMessageReceived(recvArgs);

                if (!String.IsNullOrEmpty(recvArgs.Result))
                {
                    Byte[] sendBuffer = Encoding.UTF8.GetBytes(recvArgs.Result + _endMessageToken);
                    args.SetBuffer(sendBuffer, 0, sendBuffer.Length);
                }
            }
            // Clear StringBuffer, so it can receive more data from a keep-alive connection client.
            _sb.Length = 0;
            this.currentIndex = 0;
        }

        /// <summary>
        /// Set data received from the client.
        /// </summary>
        /// <param name="args">SocketAsyncEventArgs used in the operation.</param>
        internal void SetData(SocketAsyncEventArgs args)
        {
            Int32 count = args.BytesTransferred;

            if ((this.currentIndex + count) > this._sb.Capacity)
            {
                throw new ArgumentOutOfRangeException("count",
                    String.Format(CultureInfo.CurrentCulture, "Adding {0} bytes on buffer which has {1} bytes, the listener buffer will overflow.", count, this.currentIndex));
            }

            _sb.Append(Encoding.UTF8.GetString(args.Buffer, args.Offset, count));
            this.currentIndex += count;
        }

        #region IDisposable Members

        /// <summary>
        /// Release instance.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this._connection.Shutdown(SocketShutdown.Send);
            }
            catch (Exception)
            {
                // Throw if client has closed, so it is not necessary to catch.
            }
            finally
            {
                this._connection.Close();
            }
        }

        #endregion

        public string ConnectionId => _sessionId.ToString();

        public Session Session
        {
            get => _session;
            set => _session = value;
        }

        private void OnMessageReceived(MessageEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        private void OnDisconnected(MessageEventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }
    }
}
