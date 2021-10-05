using System;
using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.Controllers.Configuration;
using Clima.Core.Controllers.Heater;
using Clima.Core.DataModel;
using Clima.Core.Devices;
using Clima.Core.IO;

namespace Clima.Core.Controllers
{
    public class HeaterController:IHeaterController
    {
        private readonly IIOService _ioService;
        private readonly IDeviceProvider _deviceProvider;

        private HeaterControllerConfig _config;
        private Dictionary<string, HeaterState> _heaters;
        public HeaterController(IIOService ioService, IDeviceProvider deviceProvider)
        {
            _ioService = ioService;
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
                _ioService.Pins.DiscreteOutputs[heater.Info.PinName].SetState(false);
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
            //UpdateHeater();

            return _config.Infos[info.Key];
        }

        private void UpdateHeater(HeaterState state)
        {
            var key = state.Info.Key;
            if (_config.Infos.ContainsKey(key))
            {
                _config.Infos[key].Hysteresis = state.Info.Hysteresis;
                _config.Infos[key].IsManual = state.Info.IsManual;
                _config.Infos[key].ControlZone = state.Info.ControlZone;
                _config.Infos[key].ManualSetPoint = state.Info.ManualSetPoint;

                _heaters[key].Info = _config.Infos[key];
                _heaters[key].IsRunning = state.IsRunning;
            }
        }

        public void SetHeaterState(HeaterState newState)
        {
            UpdateHeater(newState);

            var key = newState.Info.Key;
            if (_heaters[key].Info.IsManual)
            {
                if (newState.IsRunning)
                    _ioService.Pins.DiscreteOutputs[_heaters[key].Info.PinName].SetState(true);
                else
                    _ioService.Pins.DiscreteOutputs[_heaters[key].Info.PinName].SetState(false);
            }
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

        private void ProcessHeater(float setpoint, string key)
        {
            HeaterInfo info = _heaters[key].Info;
            
            if (!info.IsManual)
            {
                //Get current temperature in selected zone
                float currTemp = 0;
                if (info.ControlZone == 0)
                    currTemp = _deviceProvider.GetSensors().FrontTemperature;
                else if (info.ControlZone == 1)
                    currTemp = _deviceProvider.GetSensors().RearTemperature;
                
                //Calculate start and stop temperatures
                var heatOn = setpoint - info.Hysteresis;
                var heatOff = setpoint - (info.Hysteresis * 0.5);
                Log.Debug($"curr:{currTemp} setpoint:{setpoint} on temp:{heatOn} off temp:{heatOff}");
                if (currTemp < heatOn)
                {
                    _ioService.Pins.DiscreteOutputs[info.PinName].SetState(true);
                    _heaters[key].IsRunning = true;
                }

                if (currTemp > heatOff)
                {
                    _ioService.Pins.DiscreteOutputs[info.PinName].SetState(false);
                    _heaters[key].IsRunning = false;
                }
            }
        }
        public void Process(float setpoint)
        {
            foreach (var key in _heaters.Keys)
            {
                ProcessHeater(setpoint, key);
            }
        }
    }
}