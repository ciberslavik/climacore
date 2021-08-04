using System.Collections.Generic;
using Clima.Core.Controllers.Ventilation;
using Clima.Core.Devices;

namespace Clima.Core.Conrollers.Ventilation
{
    public class VentilationController:IVentilationController
    {
        private Dictionary<string, IFan> _fans;
        private bool _isRunning;
        public VentilationController()
        {
            _fans = new Dictionary<string, IFan>();
        }


        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public bool IsRunning => _isRunning;
        
        public IList<IFan> Fans { get; }
        public void AddFan(IFan fan)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFan(IFan fan)
        {
            throw new System.NotImplementedException();
        }

        public void SetPerformance(double performance)
        {
            throw new System.NotImplementedException();
        }
    }
}