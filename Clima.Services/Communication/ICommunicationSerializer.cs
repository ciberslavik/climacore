namespace Clima.Services.Communication
{
    public interface ICommunicationSerializer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string data)where T: new();
    }
}