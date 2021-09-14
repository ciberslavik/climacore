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
            _scheduler.SetTemperatureProfile(request.ProfileKey);
            return new DefaultResponse();
        }

        [ServiceMethod]
        public SchedulerInfoResponse GetSchedulerInfo(DefaultRequest request)
        {
            return new SchedulerInfoResponse()
            {
                Info = _scheduler.SchedulerInfo
            };
        }
        [ServiceMethod]
        public DefaultResponse SetVentilationProfile(SetProfileRequest request)
        {
            _scheduler.SetVentilationProfile(request.ProfileKey);
            return new DefaultResponse();
        }
        [ServiceMethod]
        public DefaultResponse SetValveProfile(SetProfileRequest request)
        {
            _scheduler.SetValveProfile(request.ProfileKey);
            return new DefaultResponse();
        }
        [ServiceMethod]
        public DefaultResponse SetMineProfile(SetProfileRequest request)
        {
            _scheduler.SetMineProfile(request.ProfileKey);
            return new DefaultResponse();
        }
    }
}