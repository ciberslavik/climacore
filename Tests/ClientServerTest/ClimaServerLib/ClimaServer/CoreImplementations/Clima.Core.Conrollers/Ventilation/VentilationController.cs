using System.Collections.Generic;
using Clima.Core.Controllers.Ventilation;

namespace Clima.Core.Conrollers.Ventilation.Ventilation
{
    public class VentilationController:IVentilationController
    {
        private List<Fan> _fans;
        
        public VentilationController()
        {
        }


        public IEnumerable<Fan> Fans { get; }
        public void AddFan(Fan fan)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFan(Fan fan)
        {
            throw new System.NotImplementedException();
        }

        public void SetPerformance(double performance)
        {
            throw new System.NotImplementedException();
        }
    }
}