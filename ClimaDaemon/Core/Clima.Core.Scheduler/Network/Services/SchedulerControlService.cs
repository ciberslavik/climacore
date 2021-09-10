using Clima.Basics.Services.Communication;
using Clima.Core.Network.Messages;
using Clima.Core.Scheduler.Network.Messages;

namespace Clima.Core.Scheduler.Network.Services
{
    public class SchedulerControlService:INetworkService
    {
        private readonly IClimaScheduler _scheduler;

        public SchedulerControlService(IClimaScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        [ServiceMethod]
        public DefaultResponse SetTemperatureProfile(SetGraphRequest request)
        {
            _scheduler.SetTemperatureGraph(request.GraphKey);
            return new DefaultResponse();
        }
        [ServiceMethod]
        public DefaultResponse SetVentilationProfile(SetGraphRequest request)
        {
            _scheduler.SetVentilationGraph(request.GraphKey);
            return new DefaultResponse();
        }
        [ServiceMethod]
        public DefaultResponse SetValveProfile(SetGraphRequest request)
        {
            _scheduler.SetValveGraph(request.GraphKey);
            return new DefaultResponse();
        }
    }
}