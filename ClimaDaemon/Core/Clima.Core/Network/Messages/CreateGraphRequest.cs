using Clima.Basics.Services.Communication;
using Clima.Core.DataModel.GraphModel;

namespace Clima.Core.Network.Messages
{
    public class CreateGraphRequest:IReturn<CreateResultRespose>
    {
        public string GraphType { get; set; }
        public GraphInfo Info { get; } = new GraphInfo();
    }
}