using System;
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
        
        public Dictionary<string, FanState> FanStates { get; } 
            = new Dictionary<string, FanState>();

        public string CreateOrUpdateFan(FanInfo fanInfo)
        {
            try
            {
                if (!string.IsNullOrEmpty(fanInfo.Key)) //Key not empty
                {
                    if (_config.FanInfos.ContainsKey(fanInfo.Key)) //Update existing
                    {
                        _config.FanInfos[fanInfo.Key] = fanInfo;
                        FanStates[fanInfo.Key].Info = fanInfo;
                    }
                    else //Create new for info key
                    {
                        _config.FanInfos.Add(fanInfo.Key, fanInfo);
                        FanStates.Add(fanInfo.Key, new FanState()
                        {
                            Info = fanInfo,
                            State = FanStateEnum.Stopped
                        });
                    }
                }
                else //Create new key and record
                {
                    fanInfo.Key = _config.GetNewFanInfoKey();
                    _config.FanInfos.Add(fanInfo.Key, fanInfo);
                    FanStates.Add(fanInfo.Key, new FanState()
                    {
                        Info = fanInfo,
                        State = FanStateEnum.Stopped
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ClimaContext.Current.SaveConfiguration();
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
        }

        public void SetPerformance(int performance)
        {
            if(_currentPerformance == performance)
                return;
            _currentPerformance = performance;
            foreach (var fanTableValue in _fanTable.Values)
            {
                if(fanTableValue.IsHermetise || fanTableValue.IsManual)
                    continue;
                
                if (fanTableValue.StartPerformance <= _currentPerformance)
                {
                    fanTableValue.Relay.On();
                }
                else
                {
                    fanTableValue.Relay.Off();
                }
            }
        }

        

        public int TotalPerformance => _totalPerformance;
        public int CurrentPerformance => _currentPerformance;


        private void CreateFans()
        {
            List<FanInfo> infos = _config.FanInfos.Values.ToList();
            infos.Sort((p,o) => p.Performance - o.Performance);
            int perfCounter = 0;
            int prevPerf = 0;
            foreach(var info in infos)
            {
                if (!info.Hermetise)
                {
                    prevPerf = perfCounter;
                    perfCounter += info.Performance * info.FanCount;
                }

                FanStates.Add(info.Key, new FanState(){
                    Info = info,
                    State = FanStateEnum.Stopped
                });
                var fanTableItem = new FanControllerTableItem();
                fanTableItem.Priority = info.Priority;
                fanTableItem.IsHermetise = info.Hermetise;
                fanTableItem.IsManual = info.IsManual;
                fanTableItem.IsAnalog = info.IsAnalog;
                
                fanTableItem.CurrentPerformance = perfCounter;
                fanTableItem.StartPerformance = prevPerf + info.StartValue;
                fanTableItem.IsRunning = false;
                
                fanTableItem.Relay = _devProvider.GetRelay(info.RelayName);
                _fanTable.Add(info.Key, fanTableItem);
            }

            _totalPerformance = perfCounter;
        }

        public void UpdateFanState(FanState fanState)
        {
            var key = fanState.Info.Key;
            if(FanStates.ContainsKey(key))
            {
                if(fanState.Info.IsManual)
                {
                    if (fanState.State == FanStateEnum.Running)
		            {
			            FanStates[key].State = FanStateEnum.Running;
                        _fanTable[key].Relay.On();
                    }
                    else if(fanState.State == FanStateEnum.Stopped)
                    {
			            FanStates[key].State = FanStateEnum.Stopped;
                        _fanTable[key].Relay.Off();
                    }
                }
		        FanStates[key].Info.IsManual = fanState.Info.IsManual;
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