using System;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.Basics.Services.Communication.Messages;
using Clima.Communication.Impl;
using Clima.Communication.Messages;
using Clima.Communication.Services;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;

namespace Clima.Communication
{
    public class BasicNetworkInstaller : INetworkInstaller
    {
        public ISystemLogger Logger { get; set; }

        public void InstallServices(INetworkServiceRegistrator registrator)
        {
            registrator.RegisterNetworkService<ServerInfoService>();
        }
    }
}