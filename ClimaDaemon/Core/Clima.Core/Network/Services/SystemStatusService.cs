using Clima.Basics.Services.Communication;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;
using Clima.Core.Network.Messages;
using Clima.Core.Scheduler;

namespace Clima.Core.Network.Services
{
    public class SystemStatusService:INetworkService
    {
        private readonly IClimaScheduler _scheduler;
        private readonly IVentilationController _ventilationController;
        private readonly ISensors _sensors;

        public SystemStatusService(IClimaScheduler scheduler, IVentilationController ventilationController)
        {
            _scheduler = scheduler;
            _ventilationController = ventilationController;
            _sensors = ClimaContext.Current.Sensors;
        }
        
        [ServiceMethod]
        public ClimatStateResponse GetClimatState(DefaultRequest request)
        {
            var s = ClimaContext.Current.Sensors;;
            var response = new ClimatStateResponse()
            {
                FrontTemperature = s.FrontTemperature,
                RearTemperature = s.RearTemperature,
                OutdoorTemperature = s.OutdoorTemperature,
                Humidity = s.Humidity,
                Pressure = s.Pressure,
                ValvePosition = s.Valve1,
                MinePosition = s.Valve2,
                TempSetPoint = _scheduler.SchedulerProcessInfo.TemperatureSetPoint,
                VentilationSetPoint = _scheduler.SchedulerProcessInfo.VentilationSetPoint,
                AnalogFanPower = _ventilationController.AnalogPower
            };
            

            return response;
        }

        [ServiceMethod]
        public TemperatureStateResponse GetTemperatureState(DefaultRequest request)
        {
            var response = new TemperatureStateResponse()
            {
                FrontTemperature = _sensors.FrontTemperature,
                RearTemperature = _sensors.RearTemperature,
                OutdoorTemperature = _sensors.OutdoorTemperature,
                AverageTemperature = 0
            };

            return response;
        }
    }
}