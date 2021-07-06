using Clima.Basics.Configuration;

namespace Clima.Serialization.Newtonsoft
{
    public class ConfigurationSerializer:IConfigurationSerializer
    {
        public ConfigurationSerializer()
        {
        }


        public string Serialize(object value)
        {
            throw new System.NotImplementedException();
        }

        public T Deserialize<T>(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}