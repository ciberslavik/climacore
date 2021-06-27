using Clima.Services.Communication;
using Clima.Services.Exceptions;
using Newtonsoft.Json;

namespace Clima.NewtonSoftJsonSerializer
{
    public class NewtonsoftCommunicationSerializer:ICommunicationSerializer
    {
        private JsonSerializerSettings Settings;
        public NewtonsoftCommunicationSerializer()
        {
            Settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.None,
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
    }
}