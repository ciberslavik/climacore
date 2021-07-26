namespace Clima.AgavaModBusIO.Transport
{
    
    public interface IAgavaMaster
    {
        void WriteRequest(AgavaRequest request);
        event ReplyReceivedEventHandler ReplyReceived;
        
        int ReadTimeout { get; set; }
        int WriteTimeout { get; set; }
    }
}