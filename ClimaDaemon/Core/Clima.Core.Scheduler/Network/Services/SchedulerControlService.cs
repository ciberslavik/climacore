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
        public DefaultResponse SetTemperatureProfile(SetProfileRequest request)
        {
            _scheduler.SetTemperatureProfile(request.GraphKey);
            return new DefaultResponse();
        }

        [ServiceMethod]
        public SchedulerInfoResponse GetSchedulerInfo(DefaultRequest request)
        {
            return new SchedulerInfoResponse();
        }
        [ServiceMethod]
        public DefaultResponse SetVentilationProfile(SetProfileRequest request)
        {
            _scheduler.SetVentilationProfile(request.GraphKey);
            return new DefaultResponse();
        }
        [ServiceMethod]
        public DefaultResponse SetValveProfile(SetProfileRequest request)
        {
            _scheduler.SetValveProfile(request.GraphKey);
            return new DefaultResponse();
        }
    }
}