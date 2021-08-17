using Clima.Core.Network.Messages;

namespace Clima.Core.Network.Services
{
    public interface ILivestockService
    {
        LivestockStateResponse GetState(DefaultRequest request);
    }
}