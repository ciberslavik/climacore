using System;
using Clima.Basics.Services;
using Clima.Core.DataModel;
using Clima.Core.DataModel.GraphModel;
using Clima.Core.Scheduler;

namespace Clima.Core.Scheduler
{
    public interface IClimaScheduler : IService
    {
        void SetTemperatureGraph(GraphBase<ValueByDayPoint> graph);
        void SetVentilationGraph(GraphBase<MinMaxByDayPoint> graph);
        void SetValveGraph(GraphBase<ValueByValuePoint> graph);
        

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
        ProductionState ProductionState { get; }
    }
}