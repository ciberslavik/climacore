using System;
using Clima.Basics.Services;
using Clima.Core.DataModel;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Scheduler;

namespace Clima.Core.Scheduler
{
    public interface IClimaScheduler : IService
    {
        void SetTemperatureGraph(string graph);
        void SetVentilationGraph(string graph);
        void SetValveGraph(string graph);
        
        string TemperatureGraph { get; }
        string VentilationGraph { get; }
        string ValveGraph { get; }
        
    
        //Livestock

        void LivestockPlanting(int heads, DateTime opDate);
        void LivestockRefraction(int heads, DateTime opDate);
        void LivestockDeath(int heads, DateTime opDate);
        void LivestockKill(int heads, DateTime opDate);
        void LivestockReset();
        LivestockState GetLivestockState();
        
        //Production

        void StartPreparing(PreparingConfig config);
        void StartProduction();
        void StopProduction();
        SchedulerState SchedulerState { get; }
        DateTime StartDate { get; }
        
        int CurrentDay { get; }
        int CurrentHeads { get; }
    }
}