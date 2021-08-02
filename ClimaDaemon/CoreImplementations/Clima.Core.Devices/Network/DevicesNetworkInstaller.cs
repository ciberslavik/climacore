using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.Basics.Services.Communication.Messages;
using Clima.Core.Devices.Network.Messages;
using Clima.Core.Devices.Network.Services;
using Clima.NetworkServer.Exceptions;

namespace Clima.Core.Devices.Network
{
    public class DevicesNetworkInstaller:INetworkInstaller
    {
        public void RegisterServices(IServiceProvider provider)
        {
            provider.Register<ISensorsService, SensorsService>();
        }

        public void RegisterMessages(IMessageTypeProvider messageProvider)
        {
            messageProvider.Register(
                nameof(SensorsService),
                nameof(SensorsService.ReadSensors),
                typeof(SensorsServiceReadRequest),
                typeof(SensorsServiceReadResponse));
        }

        public void RegisterHandlers(IServiceExecutor serviceExecutor, IServiceProvider provider)
        {
            serviceExecutor.RegisterHandler(
                nameof(SensorsService),
                nameof(SensorsService.ReadSensors), p =>
                {
                    if (p is SensorsServiceReadRequest request)
                    {
                        var service = provider.Resolve<ISensorsService>();
                        return service.ReadSensors(request);
                    }
                    throw new InvalidRequestException($"Request is not a {nameof(SensorsService)}");
                });
        }
    }
}