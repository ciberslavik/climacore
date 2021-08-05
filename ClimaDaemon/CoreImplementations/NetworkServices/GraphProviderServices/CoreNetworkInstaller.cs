using Clima.Basics.Services;
using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Exceptions;
using Clima.Basics.Services.Communication.Messages;
using Clima.Core.Network.Messages;
using Clima.Core.Network.Services;

namespace GraphProviderServices
{
    public class CoreNetworkInstaller:INetworkInstaller
    {
        public CoreNetworkInstaller()
        {
        }


        public void RegisterServices(IServiceProvider provider)
        {
            provider.Register<IGraphProviderService, GraphProviderService>();
        }

        public void RegisterMessages(IMessageTypeProvider messageProvider)
        {
            messageProvider.Register(
                nameof(GraphProviderService),
                nameof(GraphProviderService.GetTemperatureGraphInfos),
                typeof(GraphInfosRequest),
                typeof(GraphInfosResponse));
            
            messageProvider.Register(
                nameof(GraphProviderService),
                nameof(GraphProviderService.GetTemperatureGraph),
                typeof(GetGraphRequest<TemperatureGraphResponse>),
                typeof(TemperatureGraphResponse));
        }

        public void RegisterHandlers(IServiceExecutor serviceExecutor, IServiceProvider provider)
        {
            serviceExecutor.RegisterHandler(
                nameof(GraphProviderService),
                nameof(GraphProviderService.GetTemperatureGraphInfos),
                p =>
                {
                    if (p is GraphInfosRequest request)
                    {
                        return provider.Resolve<IGraphProviderService>().GetTemperatureGraphInfos(request);
                    }
                    throw new InvalidRequestException($"error request{nameof(GraphProviderService)}");
                });
            
            serviceExecutor.RegisterHandler(
                nameof(GraphProviderService),
                nameof(GraphProviderService.GetTemperatureGraph),
                p =>
                {
                    if (p is GetGraphRequest<TemperatureGraphResponse> request)
                    {
                        return provider.Resolve<IGraphProviderService>().GetTemperatureGraph(request);
                    }
                    throw new InvalidRequestException($"error request{nameof(GraphProviderService)}");
                });
        }
    }
}