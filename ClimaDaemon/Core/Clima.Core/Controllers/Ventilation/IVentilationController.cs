using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IVentilationController:IService
    {
        Dictionary<string, FanState> FanStates { get; }
        string CreateOrUpdateFan(FanInfo fanInfo);
        void UpdateFanState(FanState fanState);
        void RemoveFan(string fanKey);
        void SetPerformance(int performance);
        void SetValvePosition(float position);
        float ValveCurrentPos { get; }
        float ValveSetPoint { get; }
        bool ValveIsManual { get; set; }
        float MineCurrentPos { get; }
        float MineSetPoint { get; }
        bool MineIsManual { get; set; }
        void SetMinePosition(float position);
        int TotalPerformance { get; }
        int CurrentPerformance { get; }
        
        float AnalogPower { get; }
    }
}