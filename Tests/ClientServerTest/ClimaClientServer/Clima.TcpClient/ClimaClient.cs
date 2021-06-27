using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Buffer = DataContract.Buffer;

namespace Clima.TcpClient
{
    public class ClimaClient
    {
        #region Private fields

        private readonly ClientOption _option;
        private Thread _receiveThread;
        #endregion
        #region Events
        
        public delegate void ConnectionEventHandler();

        public delegate void DataReceivedEventHandler(DataReceivedEventArgs ea);
        
        public event ConnectionEventHandler Connecting;
        public event ConnectionEventHandler Connected;
        public event ConnectionEventHandler Disconnecting;
        public event ConnectionEventHandler Disconnected;

        public event DataReceivedEventHandler DataReceived;
        #region Event Invocators

        protected virtual void OnConnected()
        {
            Connected?.Invoke();
        }

        protected virtual void OnConnecting()
        {
            Connecting?.Invoke();
        }

        protected virtual void OnDisconnecting()
        {
            Disconnecting?.Invoke();
        }
        protected virtual void OnDisconnected()
        {
            Disconnected?.Invoke();
        }

        protected virtual void OnError(SocketError error)
        {
            
        }

        protected virtual void OnSent(long send, long pending) {}

        protected virtual void OnReceived(byte[] buffer, int i, long received)
        {
            string data = Encoding.UTF8.GetString(buffer, 0, (int)received);
            
            DataReceived?.Invoke(new DataReceivedEventArgs(data));
        }
        protected virtual void OnEmpty() {}
        #endregion  Event invocators
        #endregion  Events

        #region Constructors

        public ClimaClient(ClientOption option)
        {
            EndPoint = new IPEndPoint(IPAddress.Parse(option.Host), option.Port);
            _option = option;

        }
        public ClimaClient(IPAddress address,int port):this(new IPEndPoint(address,port)){}
        public ClimaClient(string address, int port):this(new IPEndPoint(IPAddress.Parse(address),port )){}
        public ClimaClient(IPEndPoint endpoint)
        {
            EndPoint = endpoint;
            _option = new ClientOption();
        }
        #endregion Constructors

        #region Public properties
        
        public Guid Id { get; private set; }
        public IPEndPoint EndPoint { get; private set; }
        public Socket Socket { get; private set; }
        public long BytesPending { get; private set; }
        public long BytesSending { get; private set; }
        public long BytesSent { get; private set; }
        public long BytesReceived { get; private set; }
        public bool IsSocketDisposed { get; private set; }
        public bool IsConnecting { get; private set; }
        public bool IsConnected { get; private set; }
        #endregion Public properties
        
        #region Connect/Disconnect client

        private SocketAsyncEventArgs _connectEventArg;

        protected virtual Socket CreateSocket()
        {
            return new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Connect the client (synchronous)
        /// </summary>
        /// <remarks>
        /// Please note that synchronous connect will not receive data automatically!
        /// You should use Receive() or ReceiveAsync() method manually after successful connection.
        /// </remarks>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public virtual bool Connect()
        {
            if (IsConnected || IsConnecting)
                return false;

            // Setup buffers
            _receiveBuffer = new Buffer();
            _sendBufferMain = new Buffer();
            _sendBufferFlush = new Buffer();

            // Setup event args
            _connectEventArg = new SocketAsyncEventArgs();
            _connectEventArg.RemoteEndPoint = EndPoint;
            _connectEventArg.Completed += OnAsyncCompleted;
            _receiveEventArg = new SocketAsyncEventArgs();
            _receiveEventArg.Completed += OnAsyncCompleted;
            _sendEventArg = new SocketAsyncEventArgs();
            _sendEventArg.Completed += OnAsyncCompleted;

            // Create a new client socket
            Socket = CreateSocket();

            // Update the client socket disposed flag
            IsSocketDisposed = false;
            
            // Call the client connecting handler
            OnConnecting();

            try
            {
                // Connect to the server
                Socket.Connect(EndPoint);
            }
            catch (SocketException ex)
            {
                // Call the client error handler
                SendError(ex.SocketErrorCode);

                // Reset event args
                _connectEventArg.Completed -= OnAsyncCompleted;
                _receiveEventArg.Completed -= OnAsyncCompleted;
                _sendEventArg.Completed -= OnAsyncCompleted;

                // Call the client disconnecting handler
                OnDisconnecting();

                // Close the client socket
                Socket.Close();

                // Dispose the client socket
                Socket.Dispose();

                // Dispose event arguments
                _connectEventArg.Dispose();
                _receiveEventArg.Dispose();
                _sendEventArg.Dispose();

                // Call the client disconnected handler
                OnDisconnected();

                return false;
            }

            // Prepare receive & send buffers
            _receiveBuffer.Reserve(_option.ReceiveBufferSize);
            _sendBufferMain.Reserve(_option.SendBufferSize);
            _sendBufferFlush.Reserve(_option.SendBufferSize);

            // Reset statistic
            BytesPending = 0;
            BytesSending = 0;
            BytesSent = 0;
            BytesReceived = 0;

            // Update the connected flag
            IsConnected = true;
            Guid sessionId = Guid.NewGuid();
            int bufferSize = Socket.Available;
            byte[] bufferId = new byte[bufferSize];
            
            Receive(bufferId, 0, bufferSize);

            sessionId = new Guid(bufferId);
            
            Console.WriteLine($"Session id:{sessionId.ToString()}");
            
            // Call the client connected handler
            OnConnected();

            // Call the empty send buffer handler
            if (_sendBufferMain.IsEmpty)
                OnEmpty();

            return true;
        }
        public virtual bool Disconnect()
        {
            if (!IsConnected && !IsConnecting)
                return false;

            // Cancel connecting operation
            if (IsConnecting)
                Socket.CancelConnectAsync(_connectEventArg);

            // Reset event args
            _connectEventArg.Completed -= OnAsyncCompleted;
            _receiveEventArg.Completed -= OnAsyncCompleted;
            _sendEventArg.Completed -= OnAsyncCompleted;

            // Call the client disconnecting handler
            OnDisconnecting();

            try
            {
                try
                {
                    // Shutdown the socket associated with the client
                    Socket.Shutdown(SocketShutdown.Both);
                }
                catch (SocketException) {}

                // Close the client socket
                Socket.Close();

                // Dispose the client socket
                Socket.Dispose();

                // Dispose event arguments
                _connectEventArg.Dispose();
                _receiveEventArg.Dispose();
                _sendEventArg.Dispose();

                // Update the client socket disposed flag
                IsSocketDisposed = true;
            }
            catch (ObjectDisposedException) {}

            // Update the connected flag
            IsConnected = false;

            // Update sending/receiving flags
            _receiving = false;
            _sending = false;

            // Clear send/receive buffers
            ClearBuffers();

            // Call the client disconnected handler
            OnDisconnected();

            return true;
        }

        private void ClearBuffers()
        {
            lock (_sendLock)
            {
                // Clear send buffers
                _sendBufferMain.Clear();
                _sendBufferFlush.Clear();
                _sendBufferFlushOffset= 0;

                // Update statistic
                BytesPending = 0;
                BytesSending = 0;
            }
        }

        public virtual bool DisconnectAsync() { return Disconnect(); }
        
        private void SendError(SocketError error)
        {
            // Skip disconnect errors
            if ((error == SocketError.ConnectionAborted) ||
                (error == SocketError.ConnectionRefused) ||
                (error == SocketError.ConnectionReset) ||
                (error == SocketError.OperationAborted) ||
                (error == SocketError.Shutdown))
                return;

            OnError(error);
        }

        private void ProcessConnect(SocketAsyncEventArgs e)
        {
            IsConnecting = false;

            if (e.SocketError == SocketError.Success)
            {
                
                

                // Prepare receive & send buffers
                _receiveBuffer.Reserve(_option.ReceiveBufferSize);
                _sendBufferMain.Reserve(_option.SendBufferSize);
                _sendBufferFlush.Reserve(_option.SendBufferSize);

                // Reset statistic
                BytesPending = 0;
                BytesSending = 0;
                BytesSent = 0;
                BytesReceived = 0;

                // Update the connected flag
                IsConnected = true;

                // Try to receive something from the server
                TryReceive();

                // Check the socket disposed state: in some rare cases it might be disconnected while receiving!
                if (IsSocketDisposed)
                    return;

                // Call the client connected handler
                OnConnected();

                // Call the empty send buffer handler
                if (_sendBufferMain.IsEmpty)
                    OnEmpty();
            }
            else
            {
                // Call the client disconnected handler
                SendError(e.SocketError);
                OnDisconnected();
            }
        }

        private void OnAsyncCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (IsSocketDisposed)
                return;

            // Determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e);
                    break;
                case SocketAsyncOperation.Receive:
                    if (ProcessReceive(e))
                        TryReceive();
                    break;
                case SocketAsyncOperation.Send:
                    if (ProcessSend(e))
                        TrySend();
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        #endregion Connect/Disconnect client

        #region Send / Receive section

        private readonly object _sendLock = new object();
        private bool _sending;
        private Buffer _sendBufferMain;
        private Buffer _sendBufferFlush;
        private SocketAsyncEventArgs _sendEventArg;
        private long _sendBufferFlushOffset;
        
        private bool _receiving;
        private Buffer _receiveBuffer;
        private SocketAsyncEventArgs _receiveEventArg;
        private bool _isSocketDisposed;

        public virtual long Send(byte[] buffer) { return Send(buffer, 0, buffer.Length); }
        public virtual long Send(byte[] buffer, long offset, long size)
        {
            if (!IsConnected)
                return 0;

            if (size == 0)
                return 0;

            // Sent data to the server
            long sent = Socket.Send(buffer, (int)offset, (int)size, SocketFlags.None, out SocketError ec);
            if (sent > 0)
            {
                // Update statistic
                BytesSent += sent;

                // Call the buffer sent handler
                OnSent(sent, BytesPending + BytesSending);
            }

            // Check for socket error
            if (ec != SocketError.Success)
            {
                SendError(ec);
                Disconnect();
            }

            return sent;
        }
        public virtual long Send(string text) { return Send(Encoding.UTF8.GetBytes(text)); }
        
        public virtual bool SendAsync(byte[] buffer) { return SendAsync(buffer, 0, buffer.Length); }
        public virtual bool SendAsync(byte[] buffer, long offset, long size)
        {
            if (!IsConnected)
                return false;

            if (size == 0)
                return true;

            lock (_sendLock)
            {
                // Check the send buffer limit
                if (((_sendBufferMain.Size + size) > _option.SendBufferLimit) && (_option.SendBufferLimit > 0))
                {
                    SendError(SocketError.NoBufferSpaceAvailable);
                    return false;
                }

                // Fill the main send buffer
                _sendBufferMain.Append(buffer, offset, size);

                // Update statistic
                BytesPending = _sendBufferMain.Size;

                // Avoid multiple send handlers
                if (_sending)
                    return true;
                else
                    _sending = true;

                // Try to send the main buffer
                TrySend();
            }

            return true;
        }
        public virtual bool SendAsync(string text) { return SendAsync(Encoding.UTF8.GetBytes(text)); }
        
        public virtual long Receive(byte[] buffer) { return Receive(buffer, 0, buffer.Length); }
        public virtual long Receive(byte[] buffer, long offset, long size)
        {
            if (!IsConnected)
                return 0;

            if (size == 0)
                return 0;

            // Receive data from the server
            long received = Socket.Receive(buffer, (int)offset, (int)size, SocketFlags.None, out SocketError ec);
            if (received > 0)
            {
                // Update statistic
                BytesReceived += received;

                // Call the buffer received handler
                OnReceived(buffer, 0, received);
            }

            // Check for socket error
            if (ec != SocketError.Success)
            {
                SendError(ec);
                Disconnect();
            }

            return received;
        }

        

        public virtual string Receive(long size)
        {
            var buffer = new byte[size];
            var length = Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, (int)length);
        }
        public virtual void ReceiveAsync()
        {
            // Try to receive data from the server
            TryReceive();
        }
        private void TryReceive()
        {
            if (_receiving)
                return;

            if (!IsConnected)
                return;

            bool process = true;

            while (process)
            {
                process = false;

                try
                {
                    // Async receive with the receive handler
                    _receiving = true;
                    _receiveEventArg.SetBuffer(_receiveBuffer.Data, 0, (int)_receiveBuffer.Capacity);
                    if (!Socket.ReceiveAsync(_receiveEventArg))
                        process = ProcessReceive(_receiveEventArg);
                }
                catch (ObjectDisposedException) {}
            }
        }
        private void TrySend()
        {
            if (!IsConnected)
                return;

            bool empty = false;
            bool process = true;

            while (process)
            {
                process = false;

                lock (_sendLock)
                {
                    // Is previous socket send in progress?
                    if (_sendBufferFlush.IsEmpty)
                    {
                        // Swap flush and main buffers
                        _sendBufferFlush = Interlocked.Exchange(ref _sendBufferMain, _sendBufferFlush);
                        _sendBufferFlushOffset = 0;

                        // Update statistic
                        BytesPending = 0;
                        BytesSending += _sendBufferFlush.Size;

                        // Check if the flush buffer is empty
                        if (_sendBufferFlush.IsEmpty)
                        {
                            // Need to call empty send buffer handler
                            empty = true;

                            // End sending process
                            _sending = false;
                        }
                    }
                    else
                        return;
                }

                // Call the empty send buffer handler
                if (empty)
                {
                    OnEmpty();
                    return;
                }

                try
                {
                    // Async write with the write handler
                    _sendEventArg.SetBuffer(_sendBufferFlush.Data, (int)_sendBufferFlushOffset, (int)(_sendBufferFlush.Size - _sendBufferFlushOffset));
                    if (!Socket.SendAsync(_sendEventArg))
                        process = ProcessSend(_sendEventArg);
                }
                catch (ObjectDisposedException) {}
            }
        }
        private bool ProcessReceive(SocketAsyncEventArgs e)
        {
            if (!IsConnected)
                return false;

            long size = e.BytesTransferred;

            // Received some data from the server
            if (size > 0)
            {
                // Update statistic
                BytesReceived += size;

                // Call the buffer received handler
                OnReceived(_receiveBuffer.Data, 0, size);

                // If the receive buffer is full increase its size
                if (_receiveBuffer.Capacity == size)
                {
                    // Check the receive buffer limit
                    if (((2 * size) > _option.ReceiveBufferLimit) && (_option.ReceiveBufferLimit > 0))
                    {
                        SendError(SocketError.NoBufferSpaceAvailable);
                        DisconnectAsync();
                        return false;
                    }

                    _receiveBuffer.Reserve(2 * size);
                }
            }

            _receiving = false;

            // Try to receive again if the client is valid
            if (e.SocketError == SocketError.Success)
            {
                // If zero is returned from a read operation, the remote end has closed the connection
                if (size > 0)
                    return true;
                else
                    DisconnectAsync();
            }
            else
            {
                SendError(e.SocketError);
                DisconnectAsync();
            }

            return false;
        }
        private bool ProcessSend(SocketAsyncEventArgs e)
        {
            if (!IsConnected)
                return false;

            long size = e.BytesTransferred;

            // Send some data to the server
            if (size > 0)
            {
                // Update statistic
                BytesSending -= size;
                BytesSent += size;

                // Increase the flush buffer offset
                _sendBufferFlushOffset += size;

                // Successfully send the whole flush buffer
                if (_sendBufferFlushOffset == _sendBufferFlush.Size)
                {
                    // Clear the flush buffer
                    _sendBufferFlush.Clear();
                    _sendBufferFlushOffset = 0;
                }

                // Call the buffer sent handler
                OnSent(size, BytesPending + BytesSending);
            }

            // Try to send again if the client is valid
            if (e.SocketError == SocketError.Success)
                return true;
            else
            {
                SendError(e.SocketError);
                DisconnectAsync();
                return false;
            }
        }
        #endregion Send / Receive

        #region Private Methods

        private void RunReceiveLoop()
        {
            
        }

        #endregion
    }
}