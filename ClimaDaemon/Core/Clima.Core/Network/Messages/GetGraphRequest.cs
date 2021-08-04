using Clima.Basics.Services.Communication;

namespace Clima.Core.Network.Messages
{
    public class GetGraphRequest<TResp>:IReturn<TResp>
    {
        public GetGraphRequest()
        {
        }

        public string GraphKey { get; set; }
    }
}