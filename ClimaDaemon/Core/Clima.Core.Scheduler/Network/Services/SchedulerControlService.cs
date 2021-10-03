using Clima.Basics.Services.Communication;
using Clima.Core.DataModel;
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
        public SchedulerProcessInfo GetProcessInfo(DefaultRequest request)
        {
            return _scheduler.SchedulerProcessInfo;
        }

        [ServiceMethod]
        public SchedulerProfilesInfo GetProfilesInfo(DefaultRequest request)
        {
            return _scheduler.SchedulerProfilesInfo;
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

        [ServiceMethod]
        public SchedulerDebugResponse GetSchedulerDebugInfo(DefaultRequest request)
        {
            return new SchedulerDebugResponse();
        }

        [ServiceMethod]
        public VentilationParamsResponse UpdateVentParameters(VentilationParamsRequest request)
        {
            _scheduler.VentilationParameters = request.Parameters;
            return new VentilationParamsResponse()
            {
                Parameters = _scheduler.VentilationParameters
            };
        }

        [ServiceMethod]
        public VentilationParamsResponse GetVentilationParams(DefaultRequest request)
        {
            return new VentilationParamsResponse() {Parameters = _scheduler.VentilationParameters};
        }
    }
}