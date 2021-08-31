using System;
using System.Collections.Generic;
using System.Linq;
using Clima.Basics;
using Clima.Basics.Services;
using Clima.Core.Conrollers.Ventilation.DataModel;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;

namespace Clima.Core.Conrollers.Ventilation
{
    public class VentilationController : IVentilationController
    {
        private Dictionary<int,IFan> _fans;
        private bool _isRunning;
        private FanControllerTable _fanTable;
        private long _currentPerformance;
        
        public VentilationController()
        {
            _fans = new Dictionary<int,IFan>();
            _fanTable = new FanControllerTable();
            _currentPerformance = 0;
            _isRunning = false;
        }


        public void Start()
        {
            if(_isRunning)
                return;

            _isRunning = true;
            RebuildControllerTable();
        }

        public void Stop()
        {
            if (_isRunning)
                _isRunning = false;
        }

        public void Init(object config)
        {
            throw new NotImplementedException();
        }

        public Type ConfigType { get; }
        public ServiceState ServiceState { get; }

        public bool IsRunning => _isRunning;
        public long TotalPerformance { get; private set; }
        public IList<IFan> Fans { get; }

        public void AddFan(IFan fan)
        {
            if(_fans.ContainsValue(fan))
                return;

            fan.State.PropertyChanged+= StateOnPropertyChanged;
            _fans.Add(fan.State.FanId, fan);
        }

        private void StateOnPropertyChanged(object sender, PropertyChangedEventArgs ea)
        {
            if (ea.PropertyName.Equals(nameof(FanState.Disabled)) ||
                ea.PropertyName.Equals(nameof(FanState.Hermetise)) ||
                ea.PropertyName.Equals(nameof(FanState.Performance)) ||
                ea.PropertyName.Equals(nameof(FanState.Priority)) ||
                ea.PropertyName.Equals(nameof(FanState.FansCount)) ||
                ea.PropertyName.Equals(nameof(FanState.StartValue)) ||
                ea.PropertyName.Equals(nameof(FanState.StopValue)))
            {
                RebuildControllerTable();
            }
        }

        public void RemoveFan(IFan fan)
        {
            if (_fans.ContainsValue(fan))
            {
                _fans.Remove(fan.State.FanId);
                RebuildControllerTable();
            }
        }

        public void SetPerformance(float performance)
        {
            if ((performance > 0) && (performance != _currentPerformance))
            {
                IAnalogFan analogFan = default;
                float discrPerformance = 0;
                float analogPerformance = 0;
                foreach (var tableItem in _fanTable)
                {
                    if(_fans[tableItem.FanId].GetType().IsAssignableTo(typeof(IAnalogFan)))
                    {
                        analogFan = _fans[tableItem.FanId] as IAnalogFan;
                        if (analogFan is not null)
                            analogPerformance = analogFan.State.Performance * analogFan.State.FansCount;
                        
                        continue;
                    }
                    
                    if (performance > tableItem.StartPerformance)
                    {
                        discrPerformance = tableItem.CurrentPerformance;
                        
                        tableItem.IsRunning = true;
                        _fans[tableItem.FanId].Start();
                        
                    }
                    else if(performance < tableItem.StopPerformance)
                    {
                        tableItem.IsRunning = false;
                        _fans[tableItem.FanId].Stop();
                    }
                }

                float remainPower = performance - discrPerformance;
                if (analogFan != null)
                {
                    float analogPower = (remainPower / (analogFan.State.Performance * analogFan.State.FansCount)) * 100;
                
                    Console.WriteLine($"Discrete fans performance:{discrPerformance} remain power:{remainPower} analog power:{analogPower}%");
                }
            }
        }

        internal void RebuildControllerTable()
        {
            _fanTable.Clear();
            var fans = _fans.Values.ToList();
            fans.Sort();

            long performanceCounter = 0;
            foreach (var fan in fans)
            {
                if ((!fan.State.Hermetise) || (!fan.State.Disabled))
                {
                    
                    int fanPerf = fan.State.Performance * fan.State.FansCount;
                    float startPerf = fanPerf * fan.State.StartValue;
                    float stopPerf = fanPerf * fan.State.StopValue;
                    
                    var tableItem = new FanControllerTableItem();
                    tableItem.FanId = fan.State.FanId;
                    tableItem.Priority = fan.State.Priority;
                    tableItem.StartPerformance = performanceCounter + startPerf;
                    tableItem.StopPerformance = performanceCounter + stopPerf;

                    performanceCounter += fanPerf;

                    tableItem.CurrentPerformance = performanceCounter;

                    _fanTable.Add(tableItem);
                }
            }

            TotalPerformance = performanceCounter;
        }
    }
}