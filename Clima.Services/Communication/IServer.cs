using System;
using System.Diagnostics;

namespace Clima.Services.Communication
{
    public interface IServer
    {
        event SessionCreatedEventHandler SessionCreated;
        event DataReceivedEventHandler DataReceived;
        bool StartServer();
        void WaitStopServer();
        void SendBroadcast();

        void Send(Guid sessionId, string data);
    }
}