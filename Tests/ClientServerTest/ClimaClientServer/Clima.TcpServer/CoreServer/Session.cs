using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DataContract;
using Buffer = DataContract.Buffer;

namespace Clima.TcpServer.CoreServer
{
    public class Session
    {
        #region Events
        public delegate void SessionStateEventHundler(Session session);
        public delegate void DataReceivedEventHandler(DataReceivedEventArgs ea);

        
        
        public event DataReceivedEventHandler DataReceived;
        protected virtual void OnDataReceived(DataReceivedEventArgs ea)
        {
            DataReceived?.Invoke(ea);
        }
        public event SessionStateEventHundler Connecting;
        protected virtual void OnConnecting()
        {
            Connecting?.Invoke(this);
        }
        public event SessionStateEventHundler Connected;
        protected virtual void OnConnected()
        {
            Connected?.Invoke(this);
        }
        public event SessionStateEventHundler Disconnecting;
        protected virtual void OnDisconnecting()
        {
            Console.WriteLine($"Session Id:{Id.ToString()} Disconnecting");
            Disconnecting?.Invoke(this);
        }
        public event SessionStateEventHundler Disconnected;
        protected virtual void OnDisconnected()
        {
            Console.WriteLine($"Session Id:{Id.ToString()} Disconnected");
            Disconnected?.Invoke(this);
        }
        
        #endregion Events
        
        #region Private Fields

        private readonly Server _server;
        #endregion Private Fields
        #region Public Fields
        public Guid Id { get; }
        public Server Server { get; private set; }
        public bool IsSocketDisposed { get; private set; }
        public bool IsConnected { get; private set; }
        public Socket Socket { get; private set; }
        
        public long BytesReceived { get; private set; }
        public long BytesSent { get; private set; }
        #endregion
        
        
        public Session(Server server)
        {
            _server = server;
            Id = Guid.NewGuid();
            
            Console.WriteLine($"Session created ID:{Id.ToString()}");
        }
        internal void Connect(Socket socket)
        {
            Socket = socket;
            
            
            IsSocketDisposed = false;
            
            _receiveBuffer = new Buffer();
            _sendBufferFlush = new Buffer();
            _sendBufferMain = new Buffer();
            
            _receiveEventArg = new SocketAsyncEventArgs();
            _receiveEventArg.Completed += OnAsyncCompleted;
            _sendEventArg = new SocketAsyncEventArgs();
            _sendEventArg.Completed += OnAsyncCompleted;

            _sendBufferFlush.Reserve(1024);
            _sendBufferMain.Reserve(1024);
            _receiveBuffer.Reserve(1024);
            
            BytesSent = 0;
            BytesReceived = 0;
            OnConnecting();

            IsConnected = true;
            
            TryReceive();
            if(IsSocketDisposed)
                return;
            EstabilishSession(Id);
            OnConnected();
        }

        private void EstabilishSession(Guid id)
        {
            SendString(id.ToString());
            
        }

        public bool Disconnect()
        {
            if (!IsConnected)
                return false;

            _receiveEventArg.Completed -= OnAsyncCompleted;
            _sendEventArg.Completed -= OnAsyncCompleted;
            OnDisconnecting();

            try
            {
                try
                {
                    Socket.Shutdown((SocketShutdown.Both));
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                Socket.Close();
                Socket.Dispose();
                
                _receiveEventArg.Dispose();
                _sendEventArg.Dispose();

                IsSocketDisposed = true;
            }
            catch (ObjectDisposedException){}

            IsConnected = false;
            _receiving = false;
            _sending = false;
            
            OnConnected();

            _server.UnregisterSession(Id);
            return true;
        }
        
        #region Send-Read data
        
        private readonly object _sendLock = new object();
        private bool _sending;
        private SocketAsyncEventArgs _sendEventArg;
        private Buffer _sendBufferFlush;
        private Buffer _sendBufferMain;
        private long _sendBufferFlushOffset;
        
        private bool _receiving;
        private Buffer _receiveBuffer;
        private SocketAsyncEventArgs _receiveEventArg;
        public virtual long Receive(byte[] buffer) { return Receive(buffer, 0, buffer.Length); }
        public virtual long Receive(byte[] buffer, long offset, long size)
        {
            if (!IsConnected)
                return 0;

            if (size == 0)
                return 0;

            // Receive data from the client
            long received = Socket.Receive(buffer, (int)offset, (int)size, SocketFlags.None, out SocketError ec);
            if (received > 0)
            {
                // Update statistic
                BytesReceived += received;
                //Interlocked.Add(ref Server._bytesReceived, received);

                // Call the buffer received handler
                Console.WriteLine("Data received:");
                //OnReceived(buffer, 0, received);
            }

            // Check for socket error
            if (ec != SocketError.Success)
            {
                Console.WriteLine("Receive error:");
                SendError(ec);
                Disconnect();
            }

            return received;
        }
        private void TryReceive()
        {
            if(_receiving)
                return;
            
            if(!IsConnected)
                return;
            bool process = true;
            while (process)
            {
                process = false;
                try
                {
                    _receiving = true;
                    _receiveEventArg.SetBuffer(_receiveBuffer.Data, 0, (int)_receiveBuffer.Capacity);
                    if (!Socket.ReceiveAsync(_receiveEventArg))
                        process = ProcessReceive(_receiveEventArg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public virtual long Send(byte[] buffer) { return Send(buffer, 0, buffer.Length); }
        public virtual long Send(byte[] buffer, long offset, long size)
        {
            if (!IsConnected)
                return 0;

            if (size == 0)
                return 0;

            //Add <EOF> Symbol to send data
            
            
            // Sent data to the client
            long sent = Socket.Send(buffer, (int)offset, (int)size, SocketFlags.None, out SocketError ec);
            if (sent > 0)
            {
                // Update statistic
                BytesSent += sent;
                //Interlocked.Add(ref Server._bytesSent, size);

                // Call the buffer sent handler
                //OnSent(sent, BytesPending + BytesSending);
                Console.WriteLine("Data sended");
            }

            // Check for socket error
            if (ec != SocketError.Success)
            {
                Console.WriteLine("Data send error");
                SendError(ec);
                Disconnect();
            }

            return sent;
        }
/// <summary>
/// Sending string data with end <EOF> and encoding UTF8
/// </summary>
/// <param name="text">data to send</param>
/// <returns>sended bytes</returns>
        public virtual long SendString(string text)
        {
            return Send(Encoding.UTF8.GetBytes(text + "<EOF>"));
        }
        //Asynchronous Send Data
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
                if (((_sendBufferMain.Size + size) > _server.Confoguration.SendBufferLimit) && (_server.Confoguration.SendBufferLimit > 0))
                {
                    SendError(SocketError.NoBufferSpaceAvailable);
                    return false;
                }

                // Fill the main send buffer
                _sendBufferMain.Append(buffer, offset, size);

                // Update statistic
                //BytesPending = _sendBufferMain.Size;

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
                        //BytesPending = 0;
                        BytesSent += _sendBufferFlush.Size;

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
                    Console.WriteLine($"OnEmpty GUID:{Id}");
                    //OnEmpty();
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
        
        private void OnAsyncCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (IsSocketDisposed)
                return;

            // Determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
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

        private bool ProcessReceive(SocketAsyncEventArgs e)
        {
            if (!IsConnected)
                return false;

            long size = e.BytesTransferred;

            // Received some data from the client
            if (size > 0)
            {
                // Update statistic
                BytesReceived += size;
                
                string str = Encoding.UTF8.GetString(_receiveBuffer.Data);
                if (str.Contains("<EOF>"))
                {
                    str = str.Substring(0, str.IndexOf("<EOF>", StringComparison.Ordinal));
                }
                DataReceivedEventArgs arg = new DataReceivedEventArgs(this, str);
                OnDataReceived(arg);
            }

            return true;
        }
        private bool ProcessSend(SocketAsyncEventArgs e)
        {
            if (!IsConnected)
                return false;

            long size = e.BytesTransferred;

            // Send some data to the client
            if (size > 0)
            {
                // Update statistic
                BytesSent -= size;
                BytesSent += size;
                //Interlocked.Add(ref Server._bytesSent, size);

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
                //OnSent(size,  BytesSent);
            }

            // Try to send again if the session is valid
            if (e.SocketError == SocketError.Success)
                return true;
            else
            {
                SendError(e.SocketError);
                Disconnect();
                return false;
            }
        }
        #endregion
        #region Private Methods
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

        protected virtual void OnError(SocketError error)
        {
            throw new NotImplementedException();
        }

        #endregion Private Methods
    }
}