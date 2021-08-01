using System;
using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;
using Clima.Communication.Impl;
using Clima.Communication.Messages;
using Clima.Communication.Services;
using Clima.NetworkServer.Exceptions;
using IServiceProvider = Clima.Basics.Services.IServiceProvider;

namespace Clima.Communication
{
    public class BasicNetworkInstaller:INetworkInstaller
    {
        public ISystemLogger Logger { get; set; }
        public void RegisterServices(IServiceProvider provider)
        {
            Logger.Debug("BasicNetworkInstaller");
            provider.Register<IServerInfoService, ServerInfoService>();
        }

        public void RegisterMessages(IMessageTypeProvider messageProvider)
        {
            messageProvider.Register(
                nameof(ServerInfoService), 
                nameof(ServerInfoService.GetServerVersion),
                typeof(ServerInfoVersionRequest),
                typeof(ServerInfoVersionResponse));
        }

        public void RegisterHandlers(IServiceExecutor serviceExecutor, IServiceProvider provider)
        {
            serviceExecutor.RegisterHandler(
                nameof(ServerInfoService),
                nameof(ServerInfoService.GetServerVersion),
                p =>
                {
                    if (p is ServerInfoVersionRequest request)
                    {
                        return provider.Resolve<IServerInfoService>().GetServerVersion(request);
                    }

                    throw new InvalidRequestException($"Request is not a {nameof(ServerInfoVersionRequest)}");
                });
        }
    }
}