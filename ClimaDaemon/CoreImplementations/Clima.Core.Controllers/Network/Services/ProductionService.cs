using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;

namespace Clima.Core.Controllers.Network.Services
{
    public class ProductionService : INetworkService
    {
        public ProductionService()
        {
        }

        [ServiceMethod]
        public DefaultResponse StartProduction()
        {
            return new DefaultResponse();
        }

        [ServiceMethod]
        public DefaultResponse StopProduction()
        {
            return new DefaultResponse();
        }
    }
}