namespace Clima.Core.Scheduler
{
    public partial class ClimaScheduler
    {
        private ProductionState _currentState;
        public void StartPreparing()
        {
            if (_currentState != ProductionState.Preparing)
            {
                _currentState = ProductionState.Preparing;
            }
        }

        public void StartProduction()
        {
            if (_currentState != ProductionState.Production)
            {
                _currentState = ProductionState.Production;
            }
        }

        public void StopProduction()
        {
            if (_currentState != ProductionState.Stopped)
            {
                _currentState = ProductionState.Stopped;
            }
        }

        public ProductionState ProductionState => _currentState;
    }
}