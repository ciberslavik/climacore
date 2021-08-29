using Clima.Basics.Services.Communication;

namespace Clima.Core.Network.Messages
{
    public class GetProfileRequest<TResp> : IReturn<TResp>
    {
        public GetProfileRequest()
        {
        }

        public int ProfileType { get; set; }
        public string ProfileKey { get; set; }
    }
}