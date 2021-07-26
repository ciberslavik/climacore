using System.Collections.Generic;
using Clima.Core.Controllers.Ventilation;

namespace Clima.Core.Conrollers.Ventilation.Ventilation
{
    public class VentilationController:IVentilationController
    {
        private List<IFan> _fans;
        
        public VentilationController()
        {
        }


        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

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