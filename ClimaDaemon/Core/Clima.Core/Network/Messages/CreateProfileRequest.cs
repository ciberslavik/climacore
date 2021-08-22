using Clima.Basics.Services.Communication;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class CreateProfileRequest:IReturn<CreateResultRespose>
    {
        public GraphType GraphType { get; set; }
        public ProfileInfo Info { get; } = new ProfileInfo();
    }
}