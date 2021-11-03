using System;
using System.Linq;
using Clima.Core.DataModel;
using Clima.Core.Scheduler.Configuration;

namespace Clima.Core.Scheduler
{
    public partial class ClimaScheduler
    {
        public void LivestockPlanting(int heads, DateTime opDate)
        {
            var op = new LivestockOperation();
            op.HedCount = heads;
            op.OpertionDate = opDate;
            op.OpertionType = LivestockOpType.Planted;
            
            _config.LivestockOperations.Add(op);
            GetCurrentHeads();
            ClimaContext.Current.SaveConfiguration();
        }

        public void LivestockReset()
        {
            
        }

        private int GetCurrentHeads()
        {
            
            _config.LivestockOperations.Sort();
            int result = 0;
            
            var ops = _config.LivestockOperations
                .Where((p) => p.OpertionDate >= _config.ProductionConfig.StartDate);
            
            foreach (var operation in ops)
            {
                if (operation.OpertionType == LivestockOpType.Planted)
                    result += operation.HedCount;
                else
                    result -= operation.HedCount;
            }

            _state.CurrentHeads = result;
            return result;
        }
        public LivestockState GetLivestockState()
        {
            _config.LivestockOperations.Sort();
            var result = new LivestockState();
            
            var ops = _config.LivestockOperations
                .Where((p) => p.OpertionDate >= _config.ProductionConfig.StartDate);
            
            foreach (var operation in ops)
            {
                switch (operation.OpertionType)
                {
                    case LivestockOpType.Planted:
                        result.TotalPlantedHeads += operation.HedCount;
                        result.CurrentHeads += operation.HedCount;
                        break;
                    case LivestockOpType.Killed:
                        result.TotalKilledHeads += operation.HedCount;
                        result.CurrentHeads -= operation.HedCount;
                        break;
                    case LivestockOpType.Death:
                        result.TotalDeadHeads += operation.HedCount;
                        result.CurrentHeads -= operation.HedCount;
                        break;
                    case LivestockOpType.Refracted:
                        result.TotalRefracted += operation.HedCount;
                        result.CurrentHeads -= operation.HedCount;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }

            result.CurrentDay = 1;
            return result;
        }

        public void LivestockRefraction(int heads, DateTime opDate)
        {
            var op = new LivestockOperation();
            op.HedCount = heads;
            op.OpertionDate = opDate;
            op.OpertionType = LivestockOpType.Refracted;
            
            _config.LivestockOperations.Add(op);
            ClimaContext.Current.SaveConfiguration();
        }

        public void LivestockDeath(int heads, DateTime opDate)
        {
            var op = new LivestockOperation();
            op.HedCount = heads;
            op.OpertionDate = opDate;
            op.OpertionType = LivestockOpType.Death;
            
            _config.LivestockOperations.Add(op);
            ClimaContext.Current.SaveConfiguration();
        }

        public void LivestockKill(int heads, DateTime opDate)
        {
            var op = new LivestockOperation();
            op.HedCount = heads;
            op.OpertionDate = opDate;
            op.OpertionType = LivestockOpType.Killed;
            
            _config.LivestockOperations.Add(op);
            ClimaContext.Current.SaveConfiguration();
        }
    }
}