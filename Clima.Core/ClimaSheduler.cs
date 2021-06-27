using System.Timers;
using Clima.DataModel.Process;
using Clima.Services.Devices;

namespace Clima.Core
{
    public class ClimaSheduler : IClimaSheduler
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

        public void StartSheduler()
        {
            
        }

        public void StopSheduler()
        {
            
        }

        public int ControlCycleTime { get; set; }
        public int MeasureCycleTime { get; set; }

        public ShedulerContext Context { get; set; }
    }
}