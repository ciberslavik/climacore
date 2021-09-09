using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;
using Clima.Core.Scheduler.Network.Messages;
using Clima.Core.Scheduler;

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
            return new ProductionStateResponse()
            {
                State = (int) _scheduler.ProductionState
            };
        }

        [ServiceMethod]
        public ProductionStateResponse StartPreparing(PreparingConfigRequest request)
        {
            _scheduler.StartPreparing(request.Config);
            return new ProductionStateResponse()
            {
                State = (int) _scheduler.ProductionState
            };
        }

        [ServiceMethod]
        public ProductionStateResponse StopProduction(DefaultRequest request)
        {
            _scheduler.StopProduction();
            return new ProductionStateResponse()
            {
                State = (int) _scheduler.ProductionState
            };
        }
    }
}