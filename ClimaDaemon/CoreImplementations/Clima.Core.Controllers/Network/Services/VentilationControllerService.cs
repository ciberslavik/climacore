using Clima.Basics.Services.Communication;
using Clima.Core.Controllers.Network.Messages;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Services
{
    public class VentilationControllerService : INetworkService
    {
        public VentilationControllerService()
        {
        }

        [ServiceMethod]
        public FanInfosResponse GetFanInfos(DefaultRequest request)
        {
            return new FanInfosResponse();
        }

        [ServiceMethod]
        public CreateFanResponse CreateFan(FanInfoRequest request)
        {
            return new CreateFanResponse();
        }

        [ServiceMethod]
        public DefaultResponse UpdateFan(FanInfoRequest request)
        {
            return new DefaultResponse();
        }
    }
}