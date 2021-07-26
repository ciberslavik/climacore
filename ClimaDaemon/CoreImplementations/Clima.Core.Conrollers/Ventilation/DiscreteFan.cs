using Clima.Core.Controllers.Ventilation;

namespace Clima.Core.Conrollers.Ventilation.Ventilation
{
    public class DiscreteFan:IDiscreteFan
    {
        public DiscreteFan()
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

        public int FanId { get; set; }
        public string FanName { get; set; }
        public bool Disabled { get; set; }
        public bool Hermetise { get; set; }
        public int Performance { get; set; }
        public int FansCount { get; set; }
        public int Priority { get; set; }
        public double StartValue { get; set; }
        public double StopValue { get; set; }
    }
}