using System;
using Clima.Basics.Services;
using Clima.Core.DataModel;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Scheduler;

namespace Clima.Core.Scheduler
{
    public interface IClimaScheduler : IService
    {
        void SetTemperatureProfile(string profileKey);
        void SetVentilationProfile(string profileKey);
        void SetValveProfile(string profileKey);
        void SetMineProfile(string profileKey);

        void ReloadProfiles();
        //Livestock

        void LivestockPlanting(int heads, DateTime opDate);
        void LivestockRefraction(int heads, DateTime opDate);
        void LivestockDeath(int heads, DateTime opDate);
        void LivestockKill(int heads, DateTime opDate);
        void LivestockReset();
        LivestockState GetLivestockState();
        
        //Production

        void StartPreparing(PreparingConfig config);
        void StartProduction(ProductionConfig config);
        void StopProduction();
        SchedulerState SchedulerState { get; }

        int CurrentDay { get; }
        int CurrentHeads { get; }
        DateTime StartDate { get; }
        SchedulerInfo SchedulerInfo { get; }
    }
}