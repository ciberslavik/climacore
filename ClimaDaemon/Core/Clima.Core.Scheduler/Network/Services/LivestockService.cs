using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;
using Clima.Core.Scheduler.Network.Messages;
using Clima.Core.Scheduler;

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
        public LivestockStateResponse PlantHeads(LivestockOperationRequest request)
        {
            _scheduler.LivestockPlanting(request.HeadsCount, request.OperationDate);
            return new LivestockStateResponse()
            {
                State = _scheduler.GetLivestockState()
            };
        }
        [ServiceMethod]
        public LivestockStateResponse KillHeads(LivestockOperationRequest request)
        {
            _scheduler.LivestockKill(request.HeadsCount, request.OperationDate);
            return new LivestockStateResponse()
            {
                State = _scheduler.GetLivestockState()
            };
        }
        [ServiceMethod]
        public LivestockStateResponse DeathHeads(LivestockOperationRequest request)
        {
            _scheduler.LivestockDeath(request.HeadsCount, request.OperationDate);
            return new LivestockStateResponse()
            {
                State = _scheduler.GetLivestockState()
            };
        }
        [ServiceMethod]
        public LivestockStateResponse RefractHeads(LivestockOperationRequest request)
        {
            _scheduler.LivestockRefraction(request.HeadsCount, request.OperationDate);
            return new LivestockStateResponse()
            {
                State = _scheduler.GetLivestockState()
            };
        }
        [ServiceMethod]
        public LivestockStateResponse GetLivestockState(DefaultRequest request)
        {
            return new LivestockStateResponse()
            {
                State = _scheduler.GetLivestockState()
            };
        }
        
    }
}