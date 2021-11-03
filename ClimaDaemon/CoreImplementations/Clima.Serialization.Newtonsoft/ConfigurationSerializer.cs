using System;
using Clima.Basics.Configuration;
using Newtonsoft.Json;

namespace Clima.Serialization.Newtonsoft
{
    public class ConfigurationSerializer : IConfigurationSerializer
    {
        private JsonSerializerSettings Settings;

        public ConfigurationSerializer()
        {
            Settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.None,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                //ContractResolver = new ProjectContractResolver(),
                NullValueHandling = NullValueHandling.Include
                //Converters =
                //{
                //    new KeyValuePairConverter(),
                //    new ArgbColorJsonConverter(),
                //    new FontStyleConverter(),
                //    new ShapeStateConverter()
                //}
            };
        }


        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, Settings);
        }

        public T Deserialize<T>(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(data, Settings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string DataExtension => ".jconf";
    }
}