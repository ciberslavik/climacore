using System;
using System.Collections.Generic;
using Clima.Basics.Services;
using Clima.Core.DataModel;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Scheduler;

namespace Clima.Core.Scheduler
{
    public interface IClimaScheduler : IService
    {
        //Profiles config
        void SetTemperatureProfile(string profileKey);
        void SetVentilationProfile(string profileKey);
        void SetValveProfile(string profileKey);
        void SetMineProfile(string profileKey);
        
        //Livestock operations

        void LivestockPlanting(int heads, DateTime opDate);
        void LivestockRefraction(int heads, DateTime opDate);
        void LivestockDeath(int heads, DateTime opDate);
        void LivestockKill(int heads, DateTime opDate);
        void LivestockReset();
        LivestockState GetLivestockState();
        IEnumerable<LivestockOperation> GetOperations(DateTime start, DateTime end);
        IEnumerable<LivestockOperation> GetOperationsPerCurrentParty();

        //Production operations

        void StartPreparing(PreparingConfig config);
        void StartProduction(ProductionConfig config);
        void StopProduction();
        
        //Process control
        VentilationParams VentilationParameters { get; set; }
        //Scheduler info
        SchedulerState SchedulerState { get; }

        int CurrentDay { get; }
        int CurrentHeads { get; }
        DateTime StartDate { get; }
        SchedulerProcessInfo SchedulerProcessInfo { get; }
        SchedulerProfilesInfo SchedulerProfilesInfo { get; }
        
    }
}