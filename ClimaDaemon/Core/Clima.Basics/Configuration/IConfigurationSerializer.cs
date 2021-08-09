namespace Clima.Basics.Configuration
{
    public interface IConfigurationSerializer
    {
        string Serialize(object value);
        T Deserialize<T>(string data);

        string DataExtension { get; }
    }
}