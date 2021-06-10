using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Clima.TcpServer
{
 public class Server
 {
     public delegate void ConnectionHandlerDelegate(NetworkStream connectedAutoDisposedNetStream);
     
     private TcpListener _listener;
     private Thread _listenThread;
     private Dictionary<string, ClientWorker> _clientWorkers;
     protected List<Task> TcpClientTasks = new List<Task>();
     protected int MaxConcurrentListeners = 3;
     protected int AwaiterTimeoutInMS = 500;
     
     public Server(ConnectionHandlerDelegate connectionHandler)
     {
         this.OnHandleConnection = connectionHandler ?? throw new ArgumentNullException();
     }
     protected readonly ConnectionHandlerDelegate OnHandleConnection; //the connection handler logic will be performed by the consumer of this class
     private volatile bool _ExitSignal;
     public virtual bool ExitSignal
     {
         get => this._ExitSignal;
         set => this._ExitSignal = value;
     }
     public void StartServerListening()
     {
         
     }
     protected virtual void ConnectionLooper()
     {
         while (this.TcpClientTasks.Count < this.MaxConcurrentListeners) //Maximum number of concurrent listeners
         {
             var AwaiterTask = Task.Run(async () =>
             {
                 //this.OnMessage.Invoke("Listening... on Thread " + Thread.CurrentThread.ManagedThreadId.ToString());
                 this.ProcessMessagesFromClient(await this._listener.AcceptTcpClientAsync());
             });
             this.TcpClientTasks.Add(AwaiterTask);
         }
         int RemoveAtIndex = Task.WaitAny(this.TcpClientTasks.ToArray(), this.AwaiterTimeoutInMS); //Synchronously Waits up to 500ms for any Task completion
         if (RemoveAtIndex > 0) //Remove the completed task from the list
             this.TcpClientTasks.RemoveAt(RemoveAtIndex);
     }
     protected virtual void ProcessMessagesFromClient(TcpClient Connection)
     {
         using (Connection) //Auto dispose of the cilent connection
         {
             //this.OnMessage.Invoke("Connection established on Thread " + Thread.CurrentThread.ManagedThreadId.ToString());
             if (!Connection.Connected) //Abort if not connected
                 return;

             using (var netstream = Connection.GetStream()) //Auto dispose of the netstream connection
             {
                 this.OnHandleConnection.Invoke(netstream);
             }
         }
         //this.OnMessage.Invoke("client disconnected from Thread " + Thread.CurrentThread.ManagedThreadId.ToString());
     }
 }
}