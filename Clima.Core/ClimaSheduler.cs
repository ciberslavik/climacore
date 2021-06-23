using System.Timers;
using Clima.DataModel.Process;
using Clima.Services.Devices;

namespace Clima.Core
{
    public class ClimaSheduler
    {
        

        #region Private Variables

        private Timer _controlCycleTimer;
        private Timer _measureCycleTimer;
        private readonly IDeviceFactory _deviceFactory;

        #endregion Private Variables
        public ClimaSheduler(IDeviceFactory deviceFactory)
        {
            _deviceFactory = deviceFactory;

            _controlCycleTimer = new Timer();
            _controlCycleTimer.Elapsed += ControlCycleProcess;
            
            _measureCycleTimer = new Timer();
            _measureCycleTimer.Elapsed += MeasureCycleProcess;
        }

        private void MeasureCycleProcess(object sender, ElapsedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ControlCycleProcess(object sender, ElapsedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        void StartSheduler()
        {
            
        }

        void StopSheduler()
        {
            
        }
        
        int ControlCycleTime { get; set; }
        int MeasureCycleTime { get; set; }
        
        ShedulerContext Context { get; set; }
    }
}