using Clima.Basics.Services.Communication;
using Clima.Basics.Services.Communication.Messages;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class CreateProfileRequest: IReturn<CreateResultRespose>
    {
        public int GraphType { get; set; }
        public ProfileInfo Info { get; } = new ProfileInfo();
    }
}