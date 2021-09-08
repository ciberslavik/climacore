using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;
using Clima.Core.Scheduler.Network.Messages;

namespace Clima.Core.Scheduler.Network.Services
{
    public class LivestockService:INetworkService
    {
        private readonly IClimaScheduler _scheduler;

        public LivestockService(IClimaScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        [ServiceMethod]
        public DefaultResponse PlantHeads(LivestockOperationRequest request)
        {
            _scheduler.LivestockPlanting(request.HeadsCount, request.OperationDate);
            return new DefaultResponse();
        }
        [ServiceMethod]
        public DefaultResponse KillHeads(LivestockOperationRequest request)
        {
            _scheduler.LivestockKill(request.HeadsCount, request.OperationDate);
            return new DefaultResponse();
        }
        [ServiceMethod]
        public DefaultResponse DeathHeads(LivestockOperationRequest request)
        {
            _scheduler.LivestockDeath(request.HeadsCount, request.OperationDate);
            return new DefaultResponse();
        }
        [ServiceMethod]
        public DefaultResponse RefractHeads(LivestockOperationRequest request)
        {
            _scheduler.LivestockRefraction(request.HeadsCount, request.OperationDate);
            return new DefaultResponse();
        }
        [ServiceMethod]
        public LivestockStateResponse GetState(DefaultRequest request)
        {
            return new()
            {
                State = _scheduler.GetLivestockState()
            };
        }
        
    }
}