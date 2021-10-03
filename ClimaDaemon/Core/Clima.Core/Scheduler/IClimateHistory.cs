using System;
using System.Collections.Generic;
using Clima.Core.DataModel.History;

namespace Clima.Core.Scheduler
{
    public interface IClimateHistory
    {
        void AddClimatePoint(ClimatStateHystoryItem point);
        List<ClimatStateHystoryItem> GetHistoryPeriod(DateTime start, DateTime end);
    }
}