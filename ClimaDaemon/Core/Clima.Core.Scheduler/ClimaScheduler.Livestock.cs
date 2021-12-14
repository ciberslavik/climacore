using System;
using System.Collections.Generic;
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
            op.HeadCount = heads;
            op.OperationDate = opDate;
            op.OperationType = LivestockOpType.Planted;
            
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
                .Where((p) => p.OperationDate >= _config.ProductionConfig.StartDate);
            
            foreach (var operation in ops)
            {
                if (operation.OperationType == LivestockOpType.Planted)
                    result += operation.HeadCount;
                else
                    result -= operation.HeadCount;
            }

            _context.CurrentHeads = result;
            return result;
        }
        public LivestockState GetLivestockState()
        {
            _config.LivestockOperations.Sort();
            var result = new LivestockState();

            var ops = _config.LivestockOperations
                .Where((p) =>
                    (p.OperationDate >= _config.ProductionConfig.StartDate) && (p.OperationDate <= DateTime.Now));
            
            foreach (var operation in ops)
            {
                switch (operation.OperationType)
                {
                    case LivestockOpType.Planted:
                        result.TotalPlantedHeads += operation.HeadCount;
                        result.CurrentHeads += operation.HeadCount;
                        break;
                    case LivestockOpType.Killed:
                        result.TotalKilledHeads += operation.HeadCount;
                        result.CurrentHeads -= operation.HeadCount;
                        break;
                    case LivestockOpType.Death:
                        result.TotalDeadHeads += operation.HeadCount;
                        result.CurrentHeads -= operation.HeadCount;
                        break;
                    case LivestockOpType.Refracted:
                        result.TotalRefracted += operation.HeadCount;
                        result.CurrentHeads -= operation.HeadCount;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }

            result.CurrentDay = 1;
            return result;
        }

        public IEnumerable<LivestockOperation> GetOperations(DateTime start, DateTime end)
        {
            return _config.LivestockOperations.Select(op => op)
                .Where(op => (op.OperationDate >= start) && (op.OperationDate <= end));
        }

        public IEnumerable<LivestockOperation> GetOperationsPerCurrentParty()
        {
            if (_config.LastSchedulerState == SchedulerState.Production)
            {
                return _config.LivestockOperations.Select(op => op)
                    .Where(op =>
                        (op.OperationDate >= _config.ProductionConfig.StartDate) && (op.OperationDate <= DateTime.Now));
            }

            return null;
        }

        public void LivestockRefraction(int heads, DateTime opDate)
        {
            var op = new LivestockOperation();
            op.HeadCount = heads;
            op.OperationDate = opDate;
            op.OperationType = LivestockOpType.Refracted;
            
            _config.LivestockOperations.Add(op);
            ClimaContext.Current.SaveConfiguration();
        }

        public void LivestockDeath(int heads, DateTime opDate)
        {
            var op = new LivestockOperation();
            op.HeadCount = heads;
            op.OperationDate = opDate;
            op.OperationType = LivestockOpType.Death;
            
            _config.LivestockOperations.Add(op);
            ClimaContext.Current.SaveConfiguration();
        }

        public void LivestockKill(int heads, DateTime opDate)
        {
            var op = new LivestockOperation();
            op.HeadCount = heads;
            op.OperationDate = opDate;
            op.OperationType = LivestockOpType.Killed;
            
            _config.LivestockOperations.Add(op);
            ClimaContext.Current.SaveConfiguration();
        }
        
        
    }
}