using System.Threading.Tasks;

namespace Clima.AgavaModBusIO.Transport
{
    public interface IAgavaMaster
    {
        AgavaReply WriteRequest(AgavaRequest request);

        Task<AgavaReply> WriteRequestAsync(AgavaRequest request);
        event ReplyReceivedEventHandler ReplyReceived;

        int ReadTimeout { get; set; }
        int WriteTimeout { get; set; }
    }
}