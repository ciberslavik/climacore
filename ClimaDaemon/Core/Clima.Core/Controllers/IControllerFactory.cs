using Clima.Core.Controllers.Light;
using Clima.Core.Controllers.Ventilation;

namespace Clima.Core.Controllers
{
    public interface IControllerFactory
    {
        ILightController GetLightController();
        IVentilationController GetVentilationController();
    }
}