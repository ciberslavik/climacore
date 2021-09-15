﻿using System;
using System.Collections.Generic;
using System.Linq;
using Clima.Basics;
using Clima.Basics.Configuration;
using Clima.Basics.Services;
using Clima.Core.Conrollers.Ventilation.DataModel;
using Clima.Core.Controllers.Configuration;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Conrollers.Ventilation
{
    public class VentilationController : IVentilationController
    {
        private readonly IDeviceProvider _devProvider;
        private Dictionary<string, FanControllerTableItem> _fanTable;
        private VentilationControllerConfig _config;
        private IServoDrive _valveServo = null;
        private bool _valveManual = false;
        private IServoDrive _mineServo = null;
        private bool _mineManual = false;
        
        
        private IFrequencyConverter _analogFan;
        
        private int _currentPerformance;
        private int _totalPerformance;
        public VentilationController(IDeviceProvider devProvider)
        {
            _devProvider = devProvider;
            _fanTable = new Dictionary<string, FanControllerTableItem>();

            ServiceState = ServiceState.NotInitialized;
        }
        public void Start()
        {
            if(ServiceState == ServiceState.Running)
                return;
            
            if (ServiceState == ServiceState.Initialized)
            {
                ServiceState = ServiceState.Running;
            }
        }

        public void Stop()
        {
            if (ServiceState == ServiceState.Running)
            {
                ServiceState = ServiceState.Stopped;
            }
        }

        public void Init(object config)
        {
            if (config is VentilationControllerConfig cfg)
            {
                _config = cfg;
                
                CreateFans();
            }
        }

        public Type ConfigType => typeof(VentilationControllerConfig);
        public ServiceState ServiceState { get; private set; }
        
        public Dictionary<string, FanState> FanStates {
            get
            {
                var states = new Dictionary<string, FanState>();
                foreach (var fanItem in _fanTable.Values)
                {
                    var state = new FanState();
                    state.Info = fanItem.Info;
                    if (fanItem.IsManual)
                        state.Mode = FanModeEnum.Manual;
                    else if (fanItem.IsHermetise)
                        state.Mode = FanModeEnum.Hermetise;
                    else
                        state.Mode = FanModeEnum.Auto;

                    if (fanItem.IsRunning)
                        state.State = FanStateEnum.Running;
                    else
                        state.State = FanStateEnum.Stopped;
                    states.Add(fanItem.Info.Key, state);
                }
                return states;
            } 
        } 
            

        public string CreateOrUpdateFan(FanInfo fanInfo)
        {
            try
            {
                if (!string.IsNullOrEmpty(fanInfo.Key)) //Key not empty
                {
                    if (_config.FanInfos.ContainsKey(fanInfo.Key)) //Update existing
                    {
                        _config.FanInfos[fanInfo.Key] = fanInfo;
                    }
                    else //Create new for info key
                    {
                        _config.FanInfos.Add(fanInfo.Key, fanInfo);
                    }
                }
                else //Create new key and record
                {
                    fanInfo.Key = _config.GetNewFanInfoKey();
                    _config.FanInfos.Add(fanInfo.Key, fanInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ClimaContext.Current.SaveConfiguration();
                CreateFans();
            }

            return fanInfo.Key;
        }

        public void RemoveFan(string fanKey)
        {
            if (_config.FanInfos.ContainsKey(fanKey))
            {
                _config.FanInfos.Remove(fanKey);
                ClimaContext.Current.SaveConfiguration();
            }
            CreateFans();
        }

        public void SetPerformance(int performance)
        {
            
            
            _currentPerformance = performance;
            int perfCounter = 0;
            foreach (var fanTableValue in _fanTable.Values)
            {
                if(fanTableValue.IsHermetise || fanTableValue.IsManual || fanTableValue.IsAnalog)
                    continue;
                
                if (fanTableValue.StartPerformance <= _currentPerformance)
                {
                    perfCounter += fanTableValue.Info.Performance * fanTableValue.Info.FanCount;
                    
                    fanTableValue.Relay.On();
                    fanTableValue.IsRunning = true;
                }
                else
                {
                    fanTableValue.Relay.Off();
                    fanTableValue.IsRunning = false;
                }
                
            }

            int powerToAnalog = _currentPerformance - perfCounter;
            Console.WriteLine($"Power for analog:{powerToAnalog}");
        }

        

        public int TotalPerformance => _totalPerformance;
        public int CurrentPerformance => _currentPerformance;


        private void CreateFans()
        {
            _fanTable.Clear();
            
            List<FanInfo> infos = _config.FanInfos.Values.ToList();
            infos.Sort((p, o) => p.Performance - o.Performance);

            int perfCounter = 0;
            int prevPerf = 0;
            foreach (var info in infos)
            {
                if ((!info.Hermetise)||(!info.IsAnalog))
                {
                    prevPerf = perfCounter;
                    perfCounter += info.Performance * info.FanCount;
                }
                var fanTableItem = new FanControllerTableItem();
                fanTableItem.Priority = info.Priority;
                fanTableItem.IsHermetise = info.Hermetise;
                fanTableItem.IsManual = info.IsManual;
                fanTableItem.IsAnalog = info.IsAnalog;
                if (info.IsAnalog)
                {
                    fanTableItem.AnalogFan = _devProvider.GetFrequencyConverter("FC:0");
                    fanTableItem.CurrentPerformance = info.Performance * info.FanCount;
                }
                else
                {
                    fanTableItem.Relay = _devProvider.GetRelay(info.RelayName);
                    fanTableItem.CurrentPerformance = perfCounter;
                    fanTableItem.StartPerformance = prevPerf + info.StartValue;
                }
                
                fanTableItem.IsRunning = false;
                fanTableItem.Info = info;
                
                _fanTable.Add(info.Key, fanTableItem);
            }

            _totalPerformance = perfCounter;
        }

        public void UpdateFanState(FanState fanState)
        {
            var key = fanState.Info.Key;
            if(_fanTable.ContainsKey(key))
            {
                if(fanState.Info.IsManual)
                {
                    if (fanState.State == FanStateEnum.Running)
		            {
                        _fanTable[key].Relay.On();
                    }
                    else if(fanState.State == FanStateEnum.Stopped)
                    {
                        _fanTable[key].Relay.Off();
                    }
                }
                _fanTable[key].IsManual = fanState.Info.IsManual;
            }
        }

        

        public float ValveCurrentPos
        {
            get
            {
                _valveServo ??= _devProvider.GetServo("SERVO:0");
                return _valveServo.CurrentPosition;
            }
        }

        public float ValveSetPoint
        {
            get
            {
                _valveServo ??= _devProvider.GetServo("SERVO:0");
                return _valveServo.SetPoint;
            }
        }

        public bool ValveIsManual
        {
            get => _valveManual;
            set
            {
                _valveManual = value;
            }
        }

        public float MineCurrentPos
        {
            get
            {
                _mineServo ??= _devProvider.GetServo("SERVO:1");
                return _mineServo.CurrentPosition;
            }
        }

        public float MineSetPoint
        {
            get
            {
                _mineServo ??= _devProvider.GetServo("SERVO:1");
                return _mineServo.SetPoint;
            }
        }

        public bool MineIsManual
        {
            get => _mineManual;
            set
            {
                _mineManual = value;
            }
        }

        public void SetMinePosition(float position)
        {
            _mineServo ??= _devProvider.GetServo("SERVO:1");
            if (position >= 0 && position <= 100)
                _mineServo.SetPosition(position);
        }
        
        public void SetValvePosition(float position)
        {
            _valveServo ??= _devProvider.GetServo("SERVO:0");
            if (position >= 0 && position <= 100)
                _valveServo.SetPosition(position);
        }
    }
}