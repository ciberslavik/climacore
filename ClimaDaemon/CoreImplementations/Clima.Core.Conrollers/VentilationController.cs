using System.Collections.Generic;
using System.Linq;
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
        }


        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public bool IsRunning => _isRunning;
        public long TotalPerformance { get; private set; }
        public IList<IFan> Fans { get; }

        public void AddFan(IFan fan)
        {
            if(_fans.ContainsValue(fan))
                return;

            _fans.Add(fan.State.FanId, fan);
        }

        public void RemoveFan(IFan fan)
        {
            if (_fans.ContainsValue(fan))
                _fans.Remove(fan.State.FanId);
        }

        public void SetPerformance(long performance)
        {
            if ((performance > 0) && (performance != _currentPerformance))
            {
                foreach (var tableItem in _fanTable)
                {
                    if(_fans[tableItem.FanId].GetType().IsAssignableTo(typeof(IAnalogFan)))
                        continue;
                    
                    if (performance > tableItem.StartPerformance)
                    {
                        tableItem.IsRunning = true;
                        _fans[tableItem.FanId].Start();
                    }
                    else
                    {
                        tableItem.IsRunning = false;
                        _fans[tableItem.FanId].Stop();
                    }
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