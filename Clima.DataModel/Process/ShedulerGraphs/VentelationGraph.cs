using System;
using System.Collections.Generic;

namespace Clima.DataModel.Process.ShedulerGraphs
{
    public class VentelationGraph: GraphPressetBase
    {
        private List<VentelationPoint> _points;
        private PerformanceUnitEnum _performanceUnit;
        public VentelationGraph()
        {
            PerformanceUnit = PerformanceUnitEnum.TotalMPerHour;
            
        }

        public IList<VentelationPoint> Points => _points;

        public void AddPoint(VentelationPoint point)
        {
            if (point == null)
            {
                throw new ArgumentNullException($"argument:{nameof(point)} is null");
            }

            if (!_points.Contains(point))
            {
                _points.Add(point);
                var eventArgs = new PressetModifiedEventArgs();
                OnPressetModified(eventArgs);
            }
        }
        public PerformanceUnitEnum PerformanceUnit
        {
            get => _performanceUnit;
            set => _performanceUnit = value;
        }

        public enum PerformanceUnitEnum
        {
            MeterPerHead,
            Percent,
            TotalMPerHour
        }
    }
}