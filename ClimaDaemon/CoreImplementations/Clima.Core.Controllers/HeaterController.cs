using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private Dictionary<string, HeaterState> _heaterStates;
        private float _currentSetPoint;
        public HeaterController(IIOService ioService, IDeviceProvider deviceProvider)
        {
            _ioService = ioService;
            _deviceProvider = deviceProvider;

            _heaterStates = new Dictionary<string, HeaterState>();
            _currentSetPoint = 0;
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
            foreach (var heaterConfig in _config.Infos.Values)
            {
                _ioService.Pins.DiscreteOutputs[heaterConfig.PinName].SetState(false);
            }
        }
        public void Init(object config)
        {
            Log.Debug("Heater controller init");
            
            if(config is HeaterControllerConfig cfg)
            {
                _config = cfg;
                foreach (var heatInfo in _config.Infos.Values)
                {
                    _heaterStates.Add(heatInfo.Key,new HeaterState()
                    {
                        CorrectedSetPoint = 0,
                        SetPoint = 0,
                        IsRunning = false
                    });
                }
                ServiceState = ServiceState.Initialized;
            }
        }

        public Type ConfigType => typeof(HeaterControllerConfig);
        public ServiceState ServiceState { get; private set; }

        public List<HeaterParams> UpdateHeaterParams(List<HeaterParams> heaterParams)
        {
            foreach (var heaterNew in heaterParams)
            {
                if (_config.Infos.ContainsKey(heaterNew.Key))
                {
                    _config.Infos[heaterNew.Key].DeltaOn = heaterNew.DeltaOn;
                    _config.Infos[heaterNew.Key].DeltaOff = heaterNew.DeltaOff;
                    _config.Infos[heaterNew.Key].Correction = heaterNew.Correction;
                    _config.Infos[heaterNew.Key].ControlZone = heaterNew.ControlZone;
                }
            }
            ClimaContext.Current.SaveConfiguration();
            return _config.Infos.Values.ToList();
        }
        
        public void UpdateHeaterState(string key, HeaterState newState)
        {
            if (_config.Infos[key].IsManual)
            {
                if (newState.IsRunning)
                    _ioService.Pins.DiscreteOutputs[_config.Infos[key].PinName].SetState(true);
                else
                    _ioService.Pins.DiscreteOutputs[_config.Infos[key].PinName].SetState(false);

                _heaterStates[key] = newState;
            }
        }

        public HeaterState GetHeaterState(string key)
        {
            if (_heaterStates.ContainsKey(key))
            {
                return _heaterStates[key];
            }
            throw new ArgumentException($"Heater state for key:{key} not found");
        }

        public Dictionary<string, HeaterState> States => _heaterStates;
        public Dictionary<string, HeaterParams> Params => _config.Infos;
        public float SetPoint => _currentSetPoint;
        private void ProcessHeater(float setpoint, string key)
        {
            HeaterParams @params = _config.Infos[key];
            
            if (!@params.IsManual)
            {
                //Get current temperature in selected zone
                float currTemp = 0;
                if (@params.ControlZone == 0)
                    currTemp = _deviceProvider.GetSensors().FrontTemperature;
                else if (@params.ControlZone == 1)
                    currTemp = _deviceProvider.GetSensors().RearTemperature;
                
                //Add correction
                var corrected = setpoint + @params.Correction;
                //Calculate start and stop temperatures
                var heatOn = setpoint + @params.DeltaOn;
                var heatOff = setpoint + @params.DeltaOff;
                Log.Debug($"curr:{currTemp} setpoint:{setpoint} corrected:{corrected} on temp:{heatOn} off temp:{heatOff}");
                if (currTemp < heatOn)
                {
                    _ioService.Pins.DiscreteOutputs[@params.PinName].SetState(true);
                    _heaterStates[key].IsRunning = true;
                }

                if (currTemp > heatOff)
                {
                    _ioService.Pins.DiscreteOutputs[@params.PinName].SetState(false);
                    _heaterStates[key].IsRunning = false;
                }
            }
        }
        public void Process(float setpoint)
        {
            _currentSetPoint = setpoint;
            foreach (var key in _heaterStates.Keys)
            {
                ProcessHeater(setpoint, key);
            }
        }
    }
}