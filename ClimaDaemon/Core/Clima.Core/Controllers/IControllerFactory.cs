using Clima.Core.Controllers.Light;

namespace Clima.Core.Controllers
{
    public interface IControllerFactory
    {
        ILightController CreateLightController();
        
    }
}