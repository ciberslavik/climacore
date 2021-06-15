using System;
using DataContract;
using Newtonsoft.Json;

namespace NewtonsoftJsonSerializer
{
    public class NewtonsoftSerializer:IDataSerializer
    {
        private readonly JsonSerializerSettings Settings;

        public NewtonsoftSerializer()
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
        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value, Settings);
        }

        public T Deserialize<T>(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(data, Settings);
            }
            catch (JsonSerializationException e)
            {
                throw new SerializerDataNotSupportException(e);
            }  
        }
    }

    public class SerializerDataNotSupportException : Exception
    {
        public SerializerDataNotSupportException(Exception ex = null):base(ex.Message)
        {
            
        }
    }
}