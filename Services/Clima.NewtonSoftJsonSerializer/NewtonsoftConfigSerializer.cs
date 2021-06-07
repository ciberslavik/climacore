using Clima.Services.Configuration;
using Clima.Services.Exceptions;
using Newtonsoft.Json;

namespace Clima.NewtonSoftJsonSerializer
{
    public class NewtonsoftConfigSerializer:IConfigurationSerializer
    {
        private readonly JsonSerializerSettings Settings;
        public NewtonsoftConfigSerializer()
        {
            Settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                //ContractResolver = new ProjectContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
                //Converters =
                //{
                //    new KeyValuePairConverter(),
                //    new ArgbColorJsonConverter(),
                //    new FontStyleConverter(),
                //    new ShapeStateConverter()
                //}
            };
        }


        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }

        public T Deserialize<T>(string data) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(data, Settings);
            }
            catch (JsonSerializationException e)
            {
                throw new SerializerDataNotSupportException();
            }
        }

        public string DataExtension => ".jconf";
    }
}