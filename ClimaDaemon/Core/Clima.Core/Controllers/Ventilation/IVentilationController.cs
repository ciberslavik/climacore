using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.Devices;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IVentilationController:IService
    {
        bool IsRunning { get; }
        IList<IFan> Fans { get; }
        void AddFan(IFan fan);
        void RemoveFan(IFan fan);
        void SetPerformance(float performance);
        
        long TotalPerformance { get; }
    }
}