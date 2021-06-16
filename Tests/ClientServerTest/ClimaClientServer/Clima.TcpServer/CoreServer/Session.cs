using System;
using System.Net.Sockets;
using System.Text;

namespace Clima.TcpServer.CoreServer
{
    public class Session
    {
        #region Events
        public delegate void SessionStateEventHundler();

        public delegate void DataReceivedEventHandler(DataReceivedEventArgs ea);

        public event DataReceivedEventHandler DataReceived;
        protected virtual void OnDataReceived(DataReceivedEventArgs ea)
        {
            DataReceived?.Invoke(ea);
        }
        public event SessionStateEventHundler Connecting;
        protected virtual void OnConnecting()
        {
            Connecting?.Invoke();
        }
        public event SessionStateEventHundler Connected;
        protected virtual void OnConnected()
        {
            Connected?.Invoke();
        }
        public event SessionStateEventHundler Disconnecting;
        protected virtual void OnDisconnecting()
        {
            Disconnecting?.Invoke();
        }
        public event SessionStateEventHundler Disconnected;
        protected virtual void OnDisconnected()
        {
            Disconnected?.Invoke();
        }
        
        #endregion Events
        
        #region Private Fields
        private readonly TcpClient _client;
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
            this.Server = server;
            Id = Guid.NewGuid();
        }

        internal void Process()
        {
            
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
            
            OnConnected();
            
        }
        #region Send-Read data

        private bool _sending;
        private SocketAsyncEventArgs _sendEventArg;
        private Buffer _sendBufferFlush;
        private Buffer _sendBufferMain;
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
                //SendError(ec);
                //Disconnect();
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
        public virtual long Send(byte[] buffer, long offset, long size)
        {
            if (!IsConnected)
                return 0;

            if (size == 0)
                return 0;

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
                //SendError(ec);
                //Disconnect();
            }

            return sent;
        }
        
        private void TrySend()
        {
            if(!IsConnected)
                return;
            bool empty = false;
            bool process = true;
            while (process)
            {
                process = false;
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
                    //if (ProcessSend(e))
                     //   TrySend();
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
                DataReceivedEventArgs arg = new DataReceivedEventArgs(1, str);
                OnDataReceived(arg);
            }

            return true;
        }
        #endregion
    }
}