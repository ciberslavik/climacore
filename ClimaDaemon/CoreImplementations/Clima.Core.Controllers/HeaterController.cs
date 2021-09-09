using System;
using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.Controllers.Configuration;
using Clima.Core.Controllers.Heater;
using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Controllers
{
    public class HeaterController:IHeaterController
    {
        private readonly IDeviceProvider _deviceProvider;
        private HeaterControllerConfig _config;
        private Dictionary<string, HeaterState> _heaters;
        public HeaterController(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
            _heaters = new Dictionary<string, HeaterState>();
        }
        public ISystemLogger Log { get; set; }
        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        public void StopHeater()
        {
            foreach (var heater in _heaters.Values)
            {
                _deviceProvider.GetHeater(heater.Info.Key).Off();
            }
        }
        public void Init(object config)
        {
            Log.Debug("Heater controller init");
            
            if(config is HeaterControllerConfig cfg)
            {
                _config = cfg;

                foreach (var heaterInfo in _config.Infos.Values)
                {
                    var heater = new HeaterState()
                    {
                        Info = heaterInfo,
                        IsRunning = false
                    };
                    _heaters.Add(heaterInfo.Key, heater);
                }
            }
        }

        public Type ConfigType => typeof(HeaterControllerConfig);
        public ServiceState ServiceState { get; }

        public HeaterInfo UpdateHeaterInfo(HeaterInfo info)
        {
            if (_config.Infos.ContainsKey(info.Key))
            {
                _config.Infos[info.Key] = info;
                _heaters[info.Key].Info = info;
            }

            return _config.Infos[info.Key];
        }
        public void SetHeaterState(HeaterState newState)
        {
            var key = newState.Info.Key;
            if (_heaters.ContainsKey(key))
            {
                _heaters[key] = newState;
                if (_heaters[key].Info.IsManual)
                {
                    if (newState.IsRunning)
                        _deviceProvider.GetHeater(_heaters[key].Info.PinName).On();
                    else
                        _deviceProvider.GetHeater(_heaters[key].Info.PinName).Off();
                }
            }
            throw new ArgumentException($"Heater state for key:{newState.Info.Key} not found");
        }
        
        public HeaterState GetHeaterState(string key)
        {
            if (_heaters.ContainsKey(key))
            {
                return _heaters[key];
            }
            throw new ArgumentException($"Heater state for key:{key} not found");
        }

        public Dictionary<string, HeaterState> States => _heaters;

        public void Process(float setpoint)
        {
            Log.Debug($"Heater process value:{setpoint}");

            var currFront = _deviceProvider.GetSensors().FrontTemperature;
            var currRear = _deviceProvider.GetSensors().RearTemperature;

            var heat1On = setpoint - _heaters["HEAT:0"].Info.Hysteresis;
            var heat1Off = setpoint -(_heaters["HEAT:0"].Info.Hysteresis * 0.5f);
            var heat2On = setpoint - _heaters["HEAT:1"].Info.Hysteresis;
            var heat2Off = setpoint - (_heaters["HEAT:1"].Info.Hysteresis * 0.5f);
            
            if (!_heaters["HEAT:0"].Info.IsManual)
            {
                if (currFront <= heat1On)
                {
                    _deviceProvider.GetHeater("HEAT:0").On();
                }
                else if (currFront >= heat1Off)
                {
                    _deviceProvider.GetHeater("HEAT:0").Off();
                }
            }

            if (!_heaters["HEAT:1"].Info.IsManual)
            {
                if (currRear <= heat2On)
                {
                    _deviceProvider.GetHeater("HEAT:1").On();
                }
                else if (currRear >= heat2Off)
                {
                    _deviceProvider.GetHeater("HEAT:1").Off();
                }
            }
        }
    }
}