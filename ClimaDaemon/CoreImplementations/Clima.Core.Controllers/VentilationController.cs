using System;
using System.Collections.Generic;
using System.Linq;
using Clima.Basics.Services;
using Clima.Core.Conrollers.Ventilation.DataModel;
using Clima.Core.Controllers.Configuration;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Controllers
{
    public class VentilationController : IVentilationController
    {
        private readonly IDeviceProvider _devProvider;
        private readonly Dictionary<string, FanControllerTableItem> _fanTable;
        private VentilationControllerConfig _config;
        private IServoDrive _valveServo = null;
        private bool _valveManual = false;
        private IServoDrive _mineServo = null;
        private bool _mineManual = false;
        
        
        private float _currentPerformance;
        private int _totalPerformance;
        private float _analogPower;
        private float _analogManualPower;
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
                if (_fanTable.ContainsKey("FAN:0"))
                {
                    _fanTable["FAN:0"].AnalogFan.Start();
                    
                }
                ServiceState = ServiceState.Running;
            }
        }

        
        public float AnalogPower =>_fanTable["FAN:0"].Info.AnalogPower; 
        
        
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
                ServiceState = ServiceState.Initialized;
            }
        }

        public Type ConfigType => typeof(VentilationControllerConfig);
        public ServiceState ServiceState { get; private set; }
        
        public Dictionary<string, FanInfo> FanInfos {
            get
            {
                var states = new Dictionary<string, FanInfo>();
                foreach (var fanItem in _fanTable.Values)
                {
                    states.Add(fanItem.Info.Key, fanItem.Info);
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

        public void SetPerformance(float performance)
        {
            
            _currentPerformance = performance;
            int perfCounter = 0;
            FanControllerTableItem analogItem = null;
            foreach (var fanTableValue in _fanTable.Values)
            {
                if(fanTableValue.Info.Hermetised || fanTableValue.Info.Mode == FanModeEnum.Manual)
                    continue;
                
                if(fanTableValue.Info.IsAnalog)
                {
                    analogItem = fanTableValue;
                    continue;
                }

                if (fanTableValue.Info.Mode == FanModeEnum.Auto)
                {
                    if (fanTableValue.StartPerformance <= _currentPerformance)
                    {
                        perfCounter += fanTableValue.Info.Performance * fanTableValue.Info.FanCount;
                        fanTableValue.Info.State = FanStateEnum.Running;
                        fanTableValue.Relay.On();
                    }
                    else
                    {
                        fanTableValue.Info.State = FanStateEnum.Stopped;
                        fanTableValue.Relay.Off();
                    }
                }
            }
            
            if(analogItem is not null)
            {
                analogItem.Info.State = FanStateEnum.Running;
                float powerToAnalog = _currentPerformance - perfCounter;
                float powerPercent = (powerToAnalog / analogItem.CurrentPerformance) * 100.0f;
                if(powerPercent < 15)
                  powerPercent = 15;
                if(powerPercent >100)
                  powerPercent = 100;
                
                _analogPower = powerPercent;
                if (analogItem.Info.Mode == FanModeEnum.Auto)
                {
                    analogItem.AnalogFan.SetPower(powerPercent);
                    analogItem.Info.AnalogPower = powerPercent;
                }
                else if(analogItem.Info.Mode == FanModeEnum.Manual)
                {
                    analogItem.AnalogFan.SetPower(_analogManualPower);
                    analogItem.Info.AnalogPower = _analogManualPower;
                }

                Console.WriteLine($"Analog performance:{powerToAnalog} percent:{powerPercent}");
            }
        }


        public void SetAnalogPower(float setPoint)
        {
            throw new NotImplementedException();
        }

        public int TotalPerformance => _totalPerformance;
        public float CurrentPerformance => _currentPerformance;


        private void CreateFans()
        {
            _fanTable.Clear();
            
            List<FanInfo> infos = _config.FanInfos.Values.ToList();
            infos.Sort((p, o) => p.Priority - o.Priority);

            int perfCounter = 0;
            int prevPerf = 0;
            foreach (var info in infos)
            {
                if ((!info.Hermetised)||(!info.IsAnalog))
                {
                    prevPerf = perfCounter;
                    perfCounter += info.Performance * info.FanCount;
                }
                var fanTableItem = new FanControllerTableItem();
                fanTableItem.Info = info;
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
                
                fanTableItem.Info = info;
                
                _fanTable.Add(info.Key, fanTableItem);
            }

            _totalPerformance = perfCounter;
        }

        public void SetFanMode(string key, FanModeEnum mode)
        {
            if (_fanTable.ContainsKey(key))
            {
                if (_fanTable[key].Info.Mode != mode)
                {
                    _fanTable[key].Info.Mode = mode;
                }
            }
        }
        public void SetFanState(string key, FanStateEnum state, float analogPower )
        {
            if(_fanTable.ContainsKey(key))
            {
                if (_fanTable[key].Info.IsAnalog)
                {
                    if (_fanTable[key].Info.Mode == FanModeEnum.Manual)
                    {
                        _fanTable[key].AnalogFan.SetPower(analogPower);
                    }
                }
                else
                {
                    if (_fanTable[key].Info.State != state)
                    {
                        

                        if (_fanTable[key].Info.Mode == FanModeEnum.Manual)
                        {
                            if (state == FanStateEnum.Running)
                            {
                                _fanTable[key].Relay.On();
                            }
                            else if (state == FanStateEnum.Stopped)
                            {
                                _fanTable[key].Relay.Off();
                            }
                            _fanTable[key].Info.State = state;
                        }
                    }
                }

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