using Clima.Core.Devices;

namespace Clima.Core.Controllers.Ventilation
{
    public interface IFan
    {
        void Start();
        void Stop();
        int FanId { get; set; }
        string FanName { get; set; }
        bool Disabled { get; set; }
        bool Hermetise { get; set; }
        int Performance { get; set; }
        int FansCount { get; set; }
        int Priority { get; set; }
        
        double StartValue { get; set; }
        double StopValue { get; set; }
    }
}