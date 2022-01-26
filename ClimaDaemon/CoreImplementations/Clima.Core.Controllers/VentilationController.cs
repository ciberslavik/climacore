using System;
using System.Collections.Generic;
using System.Linq;
using Clima.Basics.Services;
using Clima.Core.Alarm;
using Clima.Core.Conrollers.Ventilation.DataModel;
using Clima.Core.Controllers.Configuration;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Controllers
{
    public class VentilationController : IVentilationController, IAlarmSource
    {
        private readonly IDeviceProvider _devProvider;
        private readonly Dictionary<string, FanControllerTableItem> _fanTable;
        private VentilationControllerConfig _config;
        private IServoDrive _valveServo = null;
        private IServoDrive _mineServo = null;


        private float _currentPerformance;
        private int _totalPerformance;
        private int _discreteDelta;

        private LogFileWriter _log;
        private bool _isAlarm;
        private IEnumerable<AlarmInfo> _provideAlarms;
        public ISystemLogger Log { get; set; }
        
        public VentilationController(IDeviceProvider devProvider)
        {
            _devProvider = devProvider;
            _fanTable = new Dictionary<string, FanControllerTableItem>();

            ServiceState = ServiceState.NotInitialized;
            _discreteDelta = 500;
            _log = new LogFileWriter("Ventilation.log");
            _isAlarm = false;
            
            _provideAlarms = new List<AlarmInfo>();
            
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

        
        public float AnalogPower => _fanTable["FAN:0"].Info.AnalogPower; 
        
        
        public void Stop()
        {
            if (ServiceState == ServiceState.Running)
            {
                ServiceState = ServiceState.Stopped;
                _fanTable["FAN:0"].AnalogFan.Stop();
            }
        }

        public void Init(object config)
        {
            if (config is VentilationControllerConfig cfg)
            {
                _config = cfg;
                foreach (var fan in _config.FanInfos.Values)
                {
                    fan.IsAlarm = false;
                }
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

        public void UpdateFanInfos(Dictionary<string, FanInfo> infos)
        {
            if (_config.FanInfos.Count == infos.Count)
            {
                _config.FanInfos = infos;
                ClimaContext.Current.SaveConfiguration();
                CreateFans();
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

        public void ProcessController(float performance)
        {
            Log.Debug("Process ventilation");
            _currentPerformance = performance;
            int perfCounter = 0;
            FanControllerTableItem analogItem = null;
            foreach (var fanTableValue in _fanTable.Values)
            {
                if(fanTableValue.Info.IsAnalog)
                {
                    analogItem = fanTableValue;
                    continue;
                }

                if (fanTableValue.Info.Mode == FanModeEnum.Auto)
                {
                    if (!fanTableValue.Info.IsAlarm)
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
                else if (fanTableValue.Info.Mode == FanModeEnum.Disabled)
                {
                    fanTableValue.Info.State = FanStateEnum.Stopped;
                    fanTableValue.Relay.Off();
                }
                else if (fanTableValue.Info.Mode == FanModeEnum.Manual)
                {
                    if (fanTableValue.Info.State == FanStateEnum.Running)
                    {
                        fanTableValue.Relay.On();
                    }
                    else if (fanTableValue.Info.State == FanStateEnum.Stopped ||
                             fanTableValue.Info.IsAlarm)
                    {
                        fanTableValue.Relay.Off();
                    }
                }
            }
            
            if(analogItem is not null)
            {
                analogItem.Info.State = FanStateEnum.Running;
                float powerToAnalog = _currentPerformance - perfCounter;
                
                //перевод в проценты
                float powerPercent = (powerToAnalog / analogItem.CurrentPerformance) * 100.0f;
                
                //Ограничения
                if(powerPercent < analogItem.Info.StopValue)
                  powerPercent = analogItem.Info.StopValue;
                if(powerPercent > analogItem.Info.StartValue)
                  powerPercent = analogItem.Info.StartValue;
                
                
                if (analogItem.Info.Mode == FanModeEnum.Auto)
                {
                    //analogItem.AnalogFan.Start();
                    analogItem.AnalogFan.SetPower(powerPercent);
                    analogItem.Info.AnalogPower = powerPercent;
                    _log.Debug($"Analog AUT performance:{powerToAnalog} percent:{powerPercent}");
                }
                else if(analogItem.Info.Mode == FanModeEnum.Manual)
                {
                    //analogItem.AnalogFan.Start();
                    analogItem.AnalogFan.SetPower(analogItem.Info.AnalogPower);
                    
                    _log.Debug($"Analog MAN percent:{analogItem.Info.AnalogPower}");
                }
            }
        }


        public void SetAnalogPower(float setPoint)
        {
            throw new NotImplementedException();
        }

        public int TotalPerformance => _totalPerformance;
        public float CurrentPerformance => _currentPerformance;


        private void CreateFans2()
        {
            _fanTable.Clear();
            
            List<FanInfo> infos = _config.FanInfos.Values.ToList();
            infos.Sort((p, o) => p.Priority - o.Priority);

            int perfCounter = 0;
            int prevPerf = 0;

            string creationLog="";
            foreach (var info in infos)
            {
                if ((!info.IsAnalog)||(info.Mode != FanModeEnum.Disabled))
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
                    fanTableItem.StartPerformance = info.StartValue;
                    fanTableItem.StopPerformance = info.StopValue;
                }
                else
                {
                    fanTableItem.Relay = _devProvider.GetRelay(info.RelayName);
                    fanTableItem.CurrentPerformance = perfCounter;
                    fanTableItem.StartPerformance = prevPerf + info.StartValue;
                    fanTableItem.StopPerformance = prevPerf + info.StopValue;
                    
                    if (info.Mode == FanModeEnum.Auto)
                    {
                        fanTableItem.Relay.Off();
                        info.State = FanStateEnum.Stopped;
                    }
                    
                }
                
                fanTableItem.Info = info;
                _log.WriteLine(fanTableItem.Info.FanName + " start:" + fanTableItem.StartPerformance + " stop:" +
                               fanTableItem.StopPerformance+"\n");
                
                _fanTable.Add(info.Key, fanTableItem);
            }
            Log.Debug(creationLog);
            _totalPerformance = perfCounter;
        }

        private void CreateFans()
        {
            foreach (var i in _fanTable.Values)
            {
                if (i.Relay is IAlarmNotifier notifier)
                {
                    notifier.Notify -= AlarmNotifierOnNotify;
                }
            }
            _fanTable.Clear();
            
            List<FanInfo> infos = _config.FanInfos.Values.ToList();
            infos.Sort((p, o) => p.Priority - o.Priority);

            var perfCounter = 0;
            var totalPerfCounter = 0;
            
            _log.Debug("==================Update fan table ====================");
            foreach (var info in infos)
            {
                //Create new table item and set info
                var tableItem = new FanControllerTableItem();
                tableItem.Info = info;

                //Calculate fan performance
                var fanPerformance = info.Performance * info.FanCount;

                //Calculate total performance
                totalPerfCounter += fanPerformance;


                if (info.IsAnalog)
                {
                    //Initialize analog fan
                    tableItem.AnalogFan = _devProvider.GetFrequencyConverter("FC:0");
                    tableItem.AnalogFan.Start();
                    tableItem.CurrentPerformance = info.Performance * info.FanCount;
                    tableItem.StartPerformance = info.StartValue;
                    tableItem.StopPerformance = info.StopValue;
                }
                else
                {
                    //Initialize discrete fan

                    tableItem.Relay = _devProvider.GetRelay(info.RelayName);
                    if (tableItem.Relay is IAlarmNotifier relayAlarm)
                    {
                        relayAlarm.Notify += AlarmNotifierOnNotify;
                    }

                    if (info.Mode == FanModeEnum.Auto || info.Mode == FanModeEnum.Manual)
                    {
                        tableItem.StartPerformance = perfCounter + info.StartValue;
                        tableItem.StopPerformance = perfCounter + info.StopValue;

                        perfCounter += fanPerformance;
                        tableItem.CurrentPerformance = perfCounter;
                    }
                }

                _fanTable.Add(info.Key, tableItem);

                _log.WriteLine(tableItem.Info.FanName + " perf:" + tableItem.CurrentPerformance + " start:" +
                               tableItem.StartPerformance + " stop:" +
                               tableItem.StopPerformance);
            }

            _totalPerformance = totalPerfCounter;
        }

        private void AlarmNotifierOnNotify(object sender, AlarmEventArgs ea)
        {
            var relay = (IRelay) sender;
            var fan = _fanTable.Values.Where(f => f.Relay != null).FirstOrDefault(f => f.Relay.Name == relay.Name);
            
            if(fan is null)
                return;
            
            fan.PreAlarmState = fan.Info.State;
            fan.Info.IsAlarm = true;
            _log.Error(ea.Message);
        }

        public void SetFanMode(string key, FanModeEnum mode)
        {
            if (_fanTable.ContainsKey(key))
            {
                if (_fanTable[key].Info.Mode != mode)
                {
                    _fanTable[key].Info.Mode = mode;
                    _config.FanInfos[key].Mode = mode;
                    _log.Debug($"Mode changed fan: [{key}] mode: {mode}");
                    ClimaContext.Current.SaveConfiguration();
                    CreateFans();
                }
            }
        }

        public void SetFanState(string key, FanStateEnum state, float analogPower)
        {
            if (_fanTable.ContainsKey(key))
            {
                if (_fanTable[key].Info.IsAlarm)
                    return;

                if (_fanTable[key].Info.IsAnalog)
                {
                    if (_fanTable[key].Info.Mode == FanModeEnum.Manual)
                    {
                        _fanTable[key].AnalogFan.SetPower(analogPower);
                        _fanTable[key].Info.AnalogPower = analogPower;
                    }
                    else
                    {
                        _fanTable[key].Info.State = FanStateEnum.Running;
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
                ClimaContext.Current.SaveConfiguration();
            }
        }


        public float ValveCurrentPos
        {
            get
            {
                _valveServo ??= _devProvider.GetServo(_config.ValveServoName);
                return _valveServo.CurrentPosition;
            }
        }

        public float ValveSetPoint
        {
            get
            {
                _valveServo ??= _devProvider.GetServo(_config.ValveServoName);
                return _valveServo.SetPoint;
            }
        }

        public bool ValveIsManual { get; set; } = false;

        public float MineCurrentPos
        {
            get
            {
                _mineServo ??= _devProvider.GetServo(_config.MineServoName);
                return _mineServo.CurrentPosition;
            }
        }

        public float MineSetPoint
        {
            get
            {
                _mineServo ??= _devProvider.GetServo(_config.MineServoName);
                return _mineServo.SetPoint;
            }
        }

        public bool MineIsManual { get; set; } = false;

        public int DiscreteDelta
        {
            get => _discreteDelta;
            set => _discreteDelta = value;
        }

        public void SetMinePosition(float position)
        {
            _mineServo ??= _devProvider.GetServo(_config.MineServoName);
            if (position >= 0 && position <= 100)
                _mineServo.SetPosition(position);
        }
        
        public void SetValvePosition(float position)
        {
            _valveServo ??= _devProvider.GetServo(_config.ValveServoName);
            if (position >= 0 && position <= 100)
                _valveServo.SetPosition(position);
        }

        public event EventHandler<AlarmEventArgs> Alarm;

        public bool IsAlarm => _isAlarm;

        public IEnumerable<AlarmInfo> ProvideAlarms => _provideAlarms;

        public void Reset()
        {
            var discrFans = _fanTable.Values.Where(f => f.Relay != null);
            foreach (var fanItem in discrFans)
            {
                if (fanItem.Relay is IAlarmNotifier relayAlarm)
                {
                    if (fanItem.Info.IsAlarm)
                    {
                        fanItem.Info.IsAlarm = false;
                        relayAlarm.Reset();
                        fanItem.Info.State = fanItem.PreAlarmState;
                    }
                }
            }
        }

        protected virtual void OnAlarm(AlarmEventArgs ea)
        {
            Alarm?.Invoke(this, ea);
        }
    }
}