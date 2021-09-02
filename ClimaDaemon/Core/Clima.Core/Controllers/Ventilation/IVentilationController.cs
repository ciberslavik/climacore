using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IVentilationController:IService
    {
        Dictionary<string, FanState> FanStates { get; }
        string CreateOrUpdate(FanInfo fanInfo);
        void RemoveFan(string fanKey);
        void SetPerformance(int performance);
        int TotalPerformance { get; }
        int CurrentPerformance { get; }
        
        
    }
}