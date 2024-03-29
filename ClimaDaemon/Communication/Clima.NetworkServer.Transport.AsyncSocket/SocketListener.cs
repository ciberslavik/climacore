using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Clima.Basics.Services;
using SocketAsyncServer;

namespace Clima.NetworkServer.Transport.AsyncSocket
{
    /// <summary>
    /// Based on example from http://msdn2.microsoft.com/en-us/library/system.net.sockets.socketasynceventargs.aspx
    /// Implements the connection logic for the socket server.  
    /// After accepting a connection, all data read from the client is sent back. 
    /// The read and echo back to the client pattern is continued until the client disconnects.
    /// </summary>
    public class SocketListener:IServer
    {
        /// <summary>
        /// The socket used to listen for incoming connection requests.
        /// </summary>
        private Socket _listenSocket;

        private SocketServerConfig _config;
        /// <summary>
        /// Mutex to synchronize server execution.
        /// </summary>
        private static Mutex _mutex = new Mutex();

        /// <summary>
        /// Buffer size to use for each socket I/O operation.
        /// </summary>
        private Int32 _bufferSize;

        /// <summary>
        /// The total number of clients connected to the server.
        /// </summary>
        private Int32 _numConnectedSockets;

        /// <summary>
        /// the maximum number of connections the sample is designed to handle simultaneously.
        /// </summary>
        private Int32 _numConnections;

        /// <summary>
        /// Pool of reusable SocketAsyncEventArgs objects for write, read and accept socket operations.
        /// </summary>
        private SocketAsyncEventArgsPool _readWritePool;

        /// <summary>
        /// Controls the total number of clients connected to the server.
        /// </summary>
        private Semaphore _semaphoreAcceptedClients;

        /// <summary>
        /// Create an uninitialized server instance.  
        /// To start the server listening for connection requests
        /// call the Init method followed by Start method.
        /// </summary>
        /// <param name="numConnections">Maximum number of connections to be handled simultaneously.</param>
        /// <param name="bufferSize">Buffer size to use for each socket I/O operation.</param>
        public SocketListener()
        {
            
        }
        public ISystemLogger Log { get; set; }
        /// <summary>
        /// Close the socket associated with the client.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed send/receive operation.</param>
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            Token token = e.UserToken as Token;
            this.CloseClientSocket(token, e);
        }

        private void CloseClientSocket(Token token, SocketAsyncEventArgs e)
        {
            token.Dispose();

            // Decrement the counter keeping track of the total number of clients connected to the server.
            this._semaphoreAcceptedClients.Release();
            Interlocked.Decrement(ref this._numConnectedSockets);
            Console.WriteLine("A client has been disconnected from the server. There are {0} clients connected to the server", this._numConnectedSockets);

            // Free the SocketAsyncEventArg so they can be reused by another client.
            this._readWritePool.Push(e);
        }

        /// <summary>
        /// Callback method associated with Socket.AcceptAsync 
        /// operations and is invoked when an accept operation is complete.
        /// </summary>
        /// <param name="sender">Object who raised the event.</param>
        /// <param name="e">SocketAsyncEventArg associated with the completed accept operation.</param>
        private void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            this.ProcessAccept(e);
        }

        /// <summary>
        /// Callback called whenever a receive or send operation is completed on a socket.
        /// </summary>
        /// <param name="sender">Object who raised the event.</param>
        /// <param name="e">SocketAsyncEventArg associated with the completed send/receive operation.</param>
        private void OnIOCompleted(object sender, SocketAsyncEventArgs e)
        {
            // Determine which type of operation just completed and call the associated handler.
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    this.ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    this.ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        /// <summary>
        /// Process the accept for the socket listener.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed accept operation.</param>
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Socket s = e.AcceptSocket;
            if (s.Connected)
            {
                try
                {
                    SocketAsyncEventArgs readEventArgs = this._readWritePool.Pop();
                    if (readEventArgs != null)
                    {
                        // Get the socket for the accepted client connection and put it into the 
                        // ReadEventArg object user token.
                        var token = new Token(s, this._bufferSize);
                        token.MessageReceived += TokenOnMessageReceived;
                        token.Disconnected += TokenOnDisconnected;
                        readEventArgs.UserToken = token;

                        Interlocked.Increment(ref this._numConnectedSockets);
                        Console.WriteLine("Client connection accepted. There are {0} clients connected to the server",
                            this._numConnectedSockets);

                        if (!s.ReceiveAsync(readEventArgs))
                        {
                            this.ProcessReceive(readEventArgs);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no more available sockets to allocate.");
                    }
                }
                catch (SocketException ex)
                {
                    Token token = e.UserToken as Token;
                    Console.WriteLine("Error when processing data received from {0}:\r\n{1}", token.Connection.RemoteEndPoint, ex.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                // Accept the next connection request.
                this.StartAccept(e);
            }
        }

        private void TokenOnDisconnected(object sender, MessageEventArgs e)
        {
            ClientDisconnected?.Invoke(sender, e);
        }

        private void TokenOnMessageReceived(object sender, MessageEventArgs e)
        {
            MessageReceived?.Invoke(sender, e); 
        }

        private void ProcessError(SocketAsyncEventArgs e)
        {
            Token token = e.UserToken as Token;
            IPEndPoint localEp = token.Connection.LocalEndPoint as IPEndPoint;

            this.CloseClientSocket(token, e);

            Console.WriteLine("Socket error {0} on endpoint {1} during {2}.", (Int32)e.SocketError, localEp, e.LastOperation);
        }

        /// <summary>
        /// This method is invoked when an asynchronous receive operation completes. 
        /// If the remote host closed the connection, then the socket is closed.  
        /// If data was received then the data is echoed back to the client.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed receive operation.</param>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            // Check if the remote host closed the connection.
            if (e.BytesTransferred > 0)
            {
                if (e.SocketError == SocketError.Success)
                {
                    if (e.UserToken is Token token)
                    {
                        token.SetData(e);

                        Socket s = token.Connection;
                        if (s.Available == 0)
                        {
                            // Set return buffer.
                            token.ProcessData(e);
                            if (!s.SendAsync(e))
                            {
                                // Set the buffer to send back to the client.
                                this.ProcessSend(e);
                            }
                        }
                        else if (!s.ReceiveAsync(e))
                        {
                            // Read the next block of data sent by client.
                            this.ProcessReceive(e);
                        }
                    }
                }
                else
                {
                    this.ProcessError(e);
                }
            }
            else
            {
                this.CloseClientSocket(e);
            }
        }

        /// <summary>
        /// This method is invoked when an asynchronous send operation completes.  
        /// The method issues another receive on the socket to read any additional 
        /// data sent from the client.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed send operation.</param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // Done echoing data back to the client.
                Token token = e.UserToken as Token;

                if (!token.Connection.ReceiveAsync(e))
                {
                    // Read the next block of data send from the client.
                    this.ProcessReceive(e);
                }
            }
            else
            {
                this.ProcessError(e);
            }
        }

        /// <summary>
        /// Starts the server listening for incoming connection requests.
        /// </summary>
        /// <param name="port">Port where the server will listen for connection requests.</param>
        public void Start()
        {
            Log.Debug("Starting socket server");
            // Get host related information.
            //IPAddress[] addressList = Dns.GetHostEntry(Environment.MachineName).AddressList;

            // Get endpoint for the listener.
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, _config.Port);
            Log.Debug($"Listening on:{localEndPoint.Address.ToString()}");
            // Create the socket which listens for incoming connections.
            this._listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this._listenSocket.ReceiveBufferSize = this._bufferSize;
            this._listenSocket.SendBufferSize = this._bufferSize;

            if (localEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
            {
                // Set dual-mode (IPv4 & IPv6) for the socket listener.
                // 27 is equivalent to IPV6_V6ONLY socket option in the winsock snippet below,
                // based on http://blogs.msdn.com/wndp/archive/2006/10/24/creating-ip-agnostic-applications-part-2-dual-mode-sockets.aspx
                this._listenSocket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
                this._listenSocket.Bind(new IPEndPoint(IPAddress.IPv6Any, localEndPoint.Port));
            }
            else
            {
                // Associate the socket with the local endpoint.
                this._listenSocket.Bind(localEndPoint);
            }

            // Start the server.
            this._listenSocket.Listen(this._numConnections);

            // Post accepts on the listening socket.
            this.StartAccept(null);

            // Blocks the current thread to receive incoming messages.
            _mutex.WaitOne();
        }

        /// <summary>
        /// Begins an operation to accept a connection request from the client.
        /// </summary>
        /// <param name="acceptEventArg">The context object to use when issuing 
        /// the accept operation on the server's listening socket.</param>
        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
            }
            else
            {
                // Socket must be cleared since the context object is being reused.
                acceptEventArg.AcceptSocket = null;
            }

            this._semaphoreAcceptedClients.WaitOne();
            if (!this._listenSocket.AcceptAsync(acceptEventArg))
            {
                this.ProcessAccept(acceptEventArg);
            }
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        public void Stop()
        {
            this._listenSocket.Close();
            _mutex.ReleaseMutex();
        }

        public void Cycle()
        {
            
        }

        public void Init(object config)
        {
            if (config is SocketServerConfig cfg)
            {
                _config = cfg;
            }
            else
            {
                throw new ArgumentException("SocketListener Init");
            }
            Log.Debug("Socket server initialize...");
            _numConnectedSockets = 0;
            _numConnections = _config.MaxConnections;
            _bufferSize = _config.BufferSize;

            this._readWritePool = new SocketAsyncEventArgsPool(_numConnections);
            this._semaphoreAcceptedClients = new Semaphore(_numConnections, _numConnections);

            // Preallocate pool of SocketAsyncEventArgs objects.
            for (Int32 i = 0; i < this._numConnections; i++)
            {
                SocketAsyncEventArgs readWriteEventArg = new SocketAsyncEventArgs();
                readWriteEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(OnIOCompleted);
                readWriteEventArg.SetBuffer(new Byte[1024], 0, 1024);

                // Add SocketAsyncEventArg to the pool.
                this._readWritePool.Push(readWriteEventArg);
            }
            Log.Debug("Socket server initialized");
        }

        public Type ConfigType => typeof(SocketServerConfig);
        
        public ServiceState ServiceState { get; }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(string connectionId, string data)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<MessageEventArgs> ClientConnected;
        public event EventHandler<MessageEventArgs> ClientDisconnected;
        public IConnection TryGetConnection(string connectionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IConnection> Connections { get; }

        private void OnMessageReceived(MessageEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        private void OnClientConnected(MessageEventArgs e)
        {
            ClientConnected?.Invoke(this, e);
        }
    }
}
