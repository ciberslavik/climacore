using Clima.Basics.Services.Communication;

namespace Clima.Core.Network.Messages
{
    public class GetProfileRequest<TResp> : IReturn<TResp>
    {
        public GetProfileRequest()
        {
            ProfileKey = "";
            ProfileType = 0;
        }

        public int ProfileType { get; set; }
        public string ProfileKey { get; set; }
    }
}