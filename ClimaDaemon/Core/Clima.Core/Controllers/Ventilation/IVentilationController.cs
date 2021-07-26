using System.Collections.Generic;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IVentilationController
    {
        void Start();
        void Stop();
        IList<IFan> Fans { get; }
        void AddFan(IFan fan);
        void RemoveFan(IFan fan);
        void SetPerformance(double performance);
    }
}