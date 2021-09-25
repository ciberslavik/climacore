﻿using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.DataModel;
using Clima.Core.Devices;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IVentilationController:IService
    {
        Dictionary<string, FanInfo> FanInfos { get; }
        string CreateOrUpdateFan(FanInfo fanInfo);
        void RemoveFan(string fanKey);
        void SetFanState(string key, FanStateEnum state, float analogPower = 0f);
        void SetFanMode(string key, FanModeEnum mode);
        
        void SetPerformance(float performance);
        void SetValvePosition(float position);
        float ValveCurrentPos { get; }
        float ValveSetPoint { get; }
        bool ValveIsManual { get; set; }
        float MineCurrentPos { get; }
        float MineSetPoint { get; }
        bool MineIsManual { get; set; }
        void SetMinePosition(float position);
        bool AnalogIsManual { get; set; }
        void SetAnalogPower(float setPoint);
        int TotalPerformance { get; }
        float CurrentPerformance { get; }
        float AnalogPower { get; }
    }
}