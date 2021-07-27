using System.Collections.Generic;
using Clima.Core.Devices;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IVentilationController
    {
        void Start();
        void Stop();
        bool IsRunning { get; }
        IList<IFan> Fans { get; }
        void AddFan(IFan fan);
        void RemoveFan(IFan fan);
        void SetPerformance(double performance);
    }
}