using System.Collections.Generic;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IVentilationController
    {
        IEnumerable<Fan> Fans { get; }
        void AddFan(Fan fan);
        void RemoveFan(Fan fan);
        void SetPerformance(double performance);
    }
}