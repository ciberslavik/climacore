using System;
using System.Data.SqlTypes;
using System.IO;
using System.Net.Sockets;
using System.Text;
using DataContract;

namespace Clima.TcpServer
{
    public class ClientWorker
    {
        private Socket _socket;
        private readonly IDataSerializer _serializer;

        public delegate NetworkReply MessageReceivedHandler(NetworkRequest request);

        public event MessageReceivedHandler MessageReceived;
        public ClientWorker(Socket soket, IDataSerializer serializer)
        {
            _socket = soket;
            _serializer = serializer;
        }

        public Socket ClientConnection => _socket;
        

        public void Process()
        {
            StateObject state = new StateObject();
            state.workSocket = _socket;
            _socket.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,  
                ReadCallback, state);
        }

        public void Send(string data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);  
  
            // Begin sending the data to the remote device.  
            _socket.BeginSend(byteData, 0, byteData.Length, 0,  
                SendCallback, _socket);
        }
        private void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;  
  
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject) ar.AsyncState;  
            Socket handler = state.workSocket;  
  
            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);  
  
            if (bytesRead > 0) {  
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(  
                    state.buffer, 0, bytesRead));  
  
                // Check for end-of-file tag. If it is not there, read
                // more data.  
                content = state.sb.ToString();  
                if (content.IndexOf("<EOF>") > -1) {  
                    // All the data has been read from the
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",  
                        content.Length, content );
                    content = content.Substring(0, content.IndexOf("<EOF>"));
                    //try Deserialize data
                    NetworkRequest request;
                    try
                    {
                        request = _serializer.Deserialize<NetworkRequest>(content);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    if (request != null)
                    {
                        var reply = OnMessageReceived(request);
                        string replyStr = _serializer.Serialize(reply);
                        byte[] messsage = Encoding.UTF8.GetBytes(replyStr + "<EOF>");
                        Send(replyStr + "<EOF>");
                    }
                } else {  
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,  
                        new AsyncCallback(ReadCallback), state);  
                }  
            }  
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket) ar.AsyncState;  
  
                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);  
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);  
  
  //              handler.Shutdown(SocketShutdown.Both);  
  //              handler.Close();  
  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());  
            }  
        }

        protected virtual NetworkReply OnMessageReceived(NetworkRequest request)
        {
            return MessageReceived?.Invoke(request);
        }
    }
}