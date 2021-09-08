using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;
using Clima.Core.Scheduler.Network.Messages;

namespace Clima.Core.Scheduler.Network.Services
{
    public class ProductionService:INetworkService
    {
        private readonly IClimaScheduler _scheduler;

        public ProductionService(IClimaScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        [ServiceMethod]
        public ProductionStateResponse GetProductionState(DefaultRequest request)
        {
            return new ProductionStateResponse();
        }

        [ServiceMethod]
        public ProductionStateResponse StartPreparing(PreparingConfigRequest request)
        {
            return new();
        }
        
    }
}