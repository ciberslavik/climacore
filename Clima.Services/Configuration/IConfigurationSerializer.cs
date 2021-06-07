namespace Clima.Services.Configuration
{
    public interface IConfigurationSerializer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string data)where T: new();
        string DataExtension { get; }
    }
}