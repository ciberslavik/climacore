using Clima.Basics.Services.Communication;
using Clima.Core.DataModel;
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
            return CreateResponse();
        }

        [ServiceMethod]
        public ProductionStateResponse StartPreparing(PreparingConfigRequest request)
        {
            _scheduler.StartPreparing(request.Config);
            return CreateResponse();
        }

        [ServiceMethod]
        public ProductionStateResponse StopProduction(DefaultRequest request)
        {
            _scheduler.StopProduction();
            return CreateResponse();
        }

        [ServiceMethod]
        public ProductionStateResponse StartProduction(DefaultRequest request)
        {
            _scheduler.StartProduction();
            return CreateResponse();
        }
        private ProductionStateResponse CreateResponse()
        {
            
            return new ProductionStateResponse()
            {
                State = new ProductionState()
                {
                    State = (int) _scheduler.SchedulerState,
                    StartDate = _scheduler.StartDate,
                    CurrentDay = _scheduler.CurrentDay,
                    CurrentHeads = _scheduler.CurrentHeads
                }
            };
        }
    }
}